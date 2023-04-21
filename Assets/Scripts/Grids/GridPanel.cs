using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Managers;
using UI.Puzzle;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Grids
{
    public class GridPanel : MonoBehaviour
    {
        private const int CellRowCount = 5;
        private const int CellColumnCount = 5;

        [SerializeField] private List<Cell> cellList;
        [SerializeField] private Button restartButton;

        
        // [SerializeField] private int testGridNumber;

        private bool[] _gridBList = new bool[CellColumnCount * CellRowCount];
        private WaitForSeconds _patternYield;
        private PuzzleUI _puzzleUI;

        public List<Cell> CellList
        {
            get => cellList;
            set => cellList = value;
        }

        public bool IsInit { get; set; }
        public int[] GridMask { get; private set; }

        public int CellCount => CellRowCount * CellColumnCount;

        private void OnEnable()
        {
            foreach (var cell in CellList)
            {
                cell.AddListener(CellClick);
            }
            
            IsInit = false;

            // int v = 0;
            // Debug.Log(Convert.ToString(v,2));
            // Debug.Log(Convert.ToString(v-1,2));
            CreateMask();
            LevelManager.LevelStart += InitLevel;
            LevelManager.LevelComplete += ResetPattern;
            restartButton.onClick.AddListener(RestartPattern);
            // int g = 0b1000111011;
        }

        private void OnDisable()
        {
            LevelManager.LevelComplete -= ResetPattern;
            LevelManager.LevelStart -= InitLevel;
            restartButton.onClick.RemoveListener(RestartPattern);

        }

        private void Start()
        {
            Debug.Log("StartLevel");
            _puzzleUI = DependencyInjector.Instance.Resolve<PuzzleUI>();
            LevelManager.StartLevel();
        }

        private void InitLevel()
        {
          
            if (PlayerPrefKeys.StartingGridPattern == 0)
            {
                _puzzleUI.GroupSceneTransition.CanvasGroup.interactable = false;
                Debug.Log("Create a new Grid");
                CreateNewGrid(IsInit);
            }
            else
            {
                Debug.Log("Load Last Saved Grid: " + PlayerPrefKeys.CurrentGridPattern);
                SerializeGrid(PlayerPrefKeys.CurrentGridPattern);
            }
            IsInit = true;
        }
       
        private void ResetPattern()
        {
            PlayerPrefKeys.StartingGridPattern = 0;
        }
        private void RestartPattern()
        {
            SerializeGrid(PlayerPrefKeys.StartingGridPattern);
            PlayerPrefKeys.CurrentGridPattern = PlayerPrefKeys.StartingGridPattern;
        }
        private void CreateNewGrid(bool isAnimated)
        {
            StartCoroutine(CreateAnimatedGridPattern(isAnimated));
        }

        private IEnumerator CreateAnimatedGridPattern(bool isAnimated)
        {
            var newGrid = 0;
            var maxC = Extensions.Map(PlayerPrefKeys.CurrentPuzzleLevel, 0, 100, 20, 200);
            var range = Random.Range(10, maxC);
            Debug.Log("Range: " + range);
            _patternYield = new WaitForSeconds(1.5f / range);

            for (int i = 0; i < range; i++)
            {
                newGrid ^= GridMask[Random.Range(0, CellCount)];
                if (isAnimated)
                {
                    SetGrid(newGrid);
                    yield return _patternYield;
                }
            }

            SetGrid(newGrid);
            
            yield return null;
            
            PlayerPrefKeys.CurrentGridPattern = newGrid;
            PlayerPrefKeys.StartingGridPattern = newGrid;
            _puzzleUI.GroupSceneTransition.CanvasGroup.interactable = true;

        }


        private void CreateMask()
        {
            GridMask = new int[CellCount];

            Debug.Log("Init Mask: " + GridMask.Length);
            for (int i = 0; i < GridMask.Length; i++)
            {
                int gridCell = 0;
                gridCell += 1 << i;
                if ((i + 1) % 5 != 0)
                {
                    gridCell += 1 << i + 1;
                }

                if (i % 5 != 0)
                {
                    gridCell += 1 << i - 1;
                }

                if (i + 5 <= 24)
                {
                    gridCell += 1 << i + 5;
                }

                if (i - 5 >= 0)
                {
                    gridCell += 1 << i - 5;
                }

                GridMask[i] = gridCell;
            }
        }

        private void CellClick(int lightIndex)
        {
            // Debug.Log($"CurrentGrid {Convert.ToString(PlayerPrefKeys.CurrentGridPattern, 2)}");
            // Debug.Log($"GridMask {lightIndex} : {Convert.ToString(GridMask[lightIndex], 2)}");
            var newGrid = PlayerPrefKeys.CurrentGridPattern ^ GridMask[lightIndex];
            // Debug.Log($"newGrid {Convert.ToString(newGrid, 2)}");
            SetGrid(newGrid);
            PlayerPrefKeys.CurrentGridPattern = newGrid;

            CheckWinCondition(newGrid);
        }

        private void SetGrid(int newGrid)
        {
            SerializeGrid(newGrid);
        }

        private void CheckWinCondition(int value)
        {
            //In my first task, I thought job chain was looking for win condition,
            //but the only remaining buttons also gives me win...
            // if ((value & (value - 1)) == 0)
            if(value==0)
            {
                Debug.Log("WIN: "+PlayerPrefKeys.CurrentGridPattern);
                Debug.Log("value : "+ value);
                Debug.Log("value - 1 : "+ (value - 1));
                LevelManager.InitLevelComplete();
            }
        }

        // public int CurrentGrid => GetCurrentGrid();

        private int GetCurrentGrid()
        {
            int grid = 0;
            for (int i = 0; i < CellList.Count; i++)
            {
                grid <<= Convert.ToInt32(CellList[i].IsOn);
                Debug.Log(grid);
            }

            return grid;
            // return CellList.Sum(t => 1 << Convert.ToInt32(t.IsOn));
        }

        public void SerializeGrid(int gridB)
        {
            //00.0068988 seconds (but data??)
            /*
             var str = Convert.ToString(gridB, 2);
            var c = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                bool isLight = (str[i] & 1) > 0;
                _gridBList[c] = isLight;
                c++;
            }
            */

            //00.007372 seconds
            for (int i = 0; i < CellCount; i++)
            {
                var mask = 1 << i;
                // Debug.Log(Convert.ToString(mask, 2));
                // Debug.Log(Convert.ToString(gridB, 2));
                bool isLight = (gridB & mask) > 0;
                // Debug.Log(isLight);
                _gridBList[i] = isLight;
                CellList[i].Switch(isLight);
            }


            // var j = string.Join("", _gridBList);
            // PlayerPrefKeys.CurrentGridPattern= Convert.ToInt32(j);
        }
    }
}