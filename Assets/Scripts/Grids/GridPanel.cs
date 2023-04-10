using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public int CellCount => CellRowCount * CellColumnCount;


        public void SerializeGrid(int gridB)
        {
            //00.0068988 seconds
            var str = Convert.ToString(gridB, 2);
            var c = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                bool isLight = (str[i] & 1) > 0;
                _gridBList[c] = isLight;
                c++;
            }

            //00.007372 seconds
            // Debug.Log("Mask: ");
            /*for (int i = 0; i < CellCount; i++)
            {
                var mask = 1 << i;
                // Debug.Log(Convert.ToString(mask, 2));
                // Debug.Log(Convert.ToString(gridB, 2));
                bool isLight = (gridB & mask)>0;
                // Debug.Log(isLight);
                _gridBList[i] = isLight;
            }*/

            var j = string.Join(",", _gridBList);
            Debug.Log(j);
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