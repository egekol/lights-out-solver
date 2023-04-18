using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Managers;
using Unity.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Grids
{
    public class GridPanel : MonoBehaviour
    {
        private const int CellRowCount = 5;
        private const int CellColumnCount = 5;

        [SerializeField] private List<Cell> cellList;
        [SerializeField] private int testGridNumber;

        private bool[] _gridBList = new bool[CellColumnCount * CellRowCount];

        public List<Cell> CellList
        {
            get => cellList;
            set => cellList = value;
        }

        public int[] GridMask { get; private set; }

        public int CellCount => CellRowCount * CellColumnCount;

        private void OnEnable()
        {
            foreach (var cell in CellList)
            {
                cell.AddListener(CellClick);
            }

            CreateMask();
            SerializeGrid(PlayerPrefKeys.CurrentGridPattern);
        }

        private void CreateMask()
        {
            GridMask = new int[CellCount];

            Debug.Log("Init");
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

                if (i - 5 > 0)
                {
                    gridCell += 1 << i - 5;
                }

                GridMask[i] = gridCell;
                Debug.Log(Convert.ToString(gridCell, 2));
            }
        }


        private void CellClick(int lightIndex)
        {
            Debug.Log($"CurrentGrid {Convert.ToString(PlayerPrefKeys.CurrentGridPattern, 2)}");
            Debug.Log($"GridMask {lightIndex} : {Convert.ToString(GridMask[lightIndex], 2)}");
            var newGrid = PlayerPrefKeys.CurrentGridPattern ^ GridMask[lightIndex];
            Debug.Log($"newGrid {Convert.ToString(newGrid, 2)}");
            SerializeGrid(newGrid);
            PlayerPrefKeys.CurrentGridPattern = newGrid;
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
            // var str = Convert.ToString(gridB, 2);
            // var c = 0;
            // for (int i = str.Length - 1; i >= 0; i--)
            // {
            //     bool isLight = (str[i] & 1) > 0;
            //     _gridBList[c] = isLight;
            //     c++;
            // }

            //00.007372 seconds
            // Debug.Log("Mask: ");
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

        //
        // [ContextMenu("TestNumber")]
        // public void Test()
        // {
        //     Stopwatch st = new Stopwatch();
        //     st.Start();
        //     SerializeGrid(testGridNumber);
        //     if (CellList.Count >= _gridBList.Length)
        //     {
        //         for (var i = 0; i < CellList.Count; i++)
        //         {
        //             var cell = CellList[i];
        //             cell.SetLight(_gridBList[i]);
        //         }
        //     }
        //
        //     st.Stop();
        //     Debug.Log(st.Elapsed);
        // }
    }
}