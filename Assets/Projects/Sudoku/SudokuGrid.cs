using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SudokuGrid : MonoBehaviour
{
    [System.Serializable]
    public class GridCell
    {
        public int value;
        public TextMeshProUGUI textUI;

        public void UpdateUI()
        {
            textUI.text = value > 0 ? value.ToString() : "";
        }
    }

    public GameObject cellPrefab;      // Assign this in the Inspector
    public Transform gridParent;       // Assign the parent (with GridLayoutGroup)

    public GridCell[] grid = new GridCell[81];
    public int[] testgrid ;

    public int unsolvedIndex = 0;

    public SudokuLoader loader; // <-- Reference to SudokuLoader script

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        //import Grid text 
        int[] boardValues = loader.LoadGridFromText("board"); // board.txt in Resources;
        
        if (boardValues == null)
        {
            return;
        }
        else
        {
            Debug.Log("Grid Sucess");
        }

        for (int i = 0; i < 81; i++)
        {
            GameObject cellGO = Instantiate(cellPrefab, gridParent);
            TextMeshProUGUI textUI = cellGO.GetComponentInChildren<TextMeshProUGUI>();

            GridCell cell = new GridCell
            {
                value = boardValues[i],
                textUI = textUI
            };

            cell.UpdateUI(); // Call method after construction

            grid[i] = cell;

        }

        RandomizeGrid();
    }

    public void UpdateSlotInt(int gridIndex, int num)
    {
        if (gridIndex < 0 || gridIndex >= grid.Length) return;
        grid[gridIndex].value = num;
        grid[gridIndex].UpdateUI();
    }

    public void UpdateSlotUI(int gridIndex)
    {
        if (gridIndex < 0 || gridIndex >= grid.Length) return;
        grid[gridIndex].UpdateUI();
    }

    public void RandomizeGrid()
    {
        testgrid = new int[81];
        
        for (int i = 0; i < 81; i++)
        {
            testgrid[i] = grid[i].value;
        }

        grid[unsolvedIndex].value = 0;

        grid[unsolvedIndex].UpdateUI();

    }
}
