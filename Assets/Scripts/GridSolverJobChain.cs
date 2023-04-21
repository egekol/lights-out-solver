// 21042023

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Grids;
using Managers;
using UI.Puzzle;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public class GridSolverJobChain : MonoBehaviour
{
    // private Dictionary<int, Dictionary<string, int>> _stateLookup = new Dictionary<int, Dictionary<string, int>>();
    //todo native dictionary array doesnt work for trees, working  coroutine version is below
    private NativeParallelHashMap<int, NativeParallelHashMap<int, int>> _stateLookup;

    // private Queue<int> _gridQueue = new Queue<int>();
    private NativeQueue<int> _gridQueue = new NativeQueue<int>();
    private GridPanel _gridPanel;
    private NativeArray<int> _maskArray;

    public Action<NativeArray<int>> OnComplete;

    private IEnumerator Start()
    {
        yield return null;
        _gridPanel = DependencyInjector.Instance.Resolve<PuzzleUI>().GridPanel;
        _maskArray = new NativeArray<int>(_gridPanel.GridMask.Length, Allocator.Persistent);
        for (int i = 0; i < _maskArray.Length; i++)
        {
            _maskArray[i] = _gridPanel.GridMask[i];
        }
    }

    [ContextMenu("TEST")]
    public void Test()
    {
        // StartCoroutine(Main());
        Solve();
    }

    public void Solve()
    {
        int start_state = 0;
        OnComplete += GetArray;
        _stateLookup = new NativeParallelHashMap<int, NativeParallelHashMap<int, int>>(2048, Allocator.Persistent);
        _stateLookup.Add(start_state,
            new NativeParallelHashMap<int, int> { { 0, -1 }, { 1, -1 } });
        _gridQueue.Enqueue(start_state);

        BFS(default);
    }

    struct Node
    {
        public NativeArray<int> Value;
    }

    private void GetArray(NativeArray<int> obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] == i)
            {
                Debug.Log("FOUND IT!!! - " + Convert.ToString(_stateLookup[1][i], 2));

                var a = _stateLookup[1][i];
                Debug.Log("A: " + Convert.ToString(a, 2));
                List<int> l = new List<int>();
                while (a != -1)
                {
                    l.Add(a);

                    if (_stateLookup.TryGetValue(a, out var _))
                    {
                        Debug.Log("button A: " + _stateLookup[a][1]);
                        a = _stateLookup[a][0];
                        Debug.Log("new A: " + Convert.ToString(a, 2));
                    }
                }
            }
        }
    }

    private void BFS(JobHandle dependency)
    {
        var maskJob = new MaskJob
        {
            StateLookup = _stateLookup,
            MaskArray = _maskArray,
            currentState = _gridQueue.Dequeue(),
            GridQueue = _gridQueue
        };
        var maskJobHandle = maskJob.Schedule(_maskArray.Length, 32, dependency);
        var filterJob = new FilterJob
        {
            StateLookup = _stateLookup,
            currentGrid = PlayerPrefKeys.CurrentGridPattern,
            resultArray = new NativeArray<int>(_stateLookup.Count(), Allocator.TempJob)
        };
        var filterJobHandle = filterJob.Schedule(_stateLookup.Count(), 2048, maskJobHandle);

        var onCompleteJob = new OnCompleteJob
        {
            resultArray = filterJob.resultArray,
            onComplete = OnComplete
        };
        var onCompleteJobHandle = onCompleteJob.Schedule(filterJobHandle);
        if (_gridQueue.Count > 0)
        {
            BFS(onCompleteJobHandle);
        }
    }

    struct MaskJob : IJobParallelFor
    {
        public NativeParallelHashMap<int, NativeParallelHashMap<int, int>> StateLookup;
        [ReadOnly] public NativeArray<int> MaskArray;
        [ReadOnly] public int currentState;
        public NativeQueue<int> GridQueue;


        public void Execute(int index)
        {
            var newState = currentState ^ MaskArray[index];

            if (StateLookup.ContainsKey(newState)) return;
            Debug.Log("currentState: " + Convert.ToString(currentState, 2));
            Debug.Log("newState: " + Convert.ToString(newState, 2));
            StateLookup.Add(newState,
                new NativeParallelHashMap<int, int> { { 0, currentState }, { 1, index } });
            GridQueue.Enqueue(newState);
        }
    }

    struct FilterJob : IJobParallelFor
    {
        public NativeParallelHashMap<int, NativeParallelHashMap<int, int>> StateLookup;
        [ReadOnly] public int currentGrid;
        public NativeArray<int> resultArray;

        public void Execute(int index)
        {
            resultArray[index] = StateLookup[index][0] & currentGrid;
        }
    }

    private struct OnCompleteJob : IJob
    {
        [DeallocateOnJobCompletion] public NativeArray<int> resultArray;

        public Action<NativeArray<int>> onComplete;

        public void Execute()
        {
            if (resultArray.Contains(1))
            {
                onComplete(resultArray);
            }
        }
    }
}


//This bfs works properly asynchronous but too slow
/*public IEnumerator Main()
{
    int start_state = 0; // Assuming start_state is initialized with an appropriate value
    int[] masks = _gridPanel.GridMask;
    int currentGrid = PlayerPrefKeys.CurrentGridPattern;
    // int currentGrid = 0b100011;
    // var a = new NativeHashMap<string, object>();
    _stateLookup.Add(start_state,
        new NativeParallelHashMap<int, int> { { 0, -1 }, { 1, -1 } });
    _gridQueue.Enqueue(start_state);

    Debug.Log("Generating Lookup");
    yield return new WaitWhile(() =>
    {
        int currentState = _gridQueue.Dequeue();

        Debug.Log("currentState: " + Convert.ToString(currentState, 2));
        Debug.Log("Grid: " + Convert.ToString(currentGrid, 2));
        for (int i = 0; i < masks.Length; i++)
        {
            int newState = currentState ^ masks[i];

            Debug.Log("newState: " + Convert.ToString(newState, 2));

            if (!_stateLookup.ContainsKey(newState))
            {
                // _stateLookup.get
                if (newState == currentGrid)
                {
                    Debug.Log("FOUND IT!!! - " + Convert.ToString(newState, 2));
                    // Debug.Log($"Final states length: {_stateLookup.GetKeyArray().Length}");
                    Debug.Log("button AAA: " + i);

                    var a = currentState;
                    Debug.Log("A: " + Convert.ToString(a, 2));
                    List<int> l = new List<int>();
                    while (a != -1)
                    {
                        l.Add(a);

                        if (_stateLookup.TryGetValue(a, out var _))
                        {
                            Debug.Log("button A: " + _stateLookup[a][1]);
                            a = _stateLookup[a][0];
                            Debug.Log("new A: " + Convert.ToString(a, 2));
                        }
                    }

                    Debug.Log("STR: ");
                    l.ForEach(s => Debug.Log(Convert.ToString(s, 2)));
                    Debug.Break();
                    continue;
                }

                _stateLookup.Add(newState,
                    new NativeParallelHashMap<int, int> { { 0, currentState }, { 1, i } });
                _gridQueue.Enqueue(newState);
            }
        }

        return _gridQueue.Count > 0;
    });
    yield break;

}*/