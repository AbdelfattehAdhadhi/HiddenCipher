using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : Singleton<GridManager>
{
    public RectTransform gridRectTransform;
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform matchingBoard;

    public GameObject cardPrefab;
    public GameObject emptyPrefab;

    [SerializeField] private int rows;
    [SerializeField] private int cols;

    private void Start()
    {
        CalculateGridCellSizeAndSpacing();
        SetupGrid(rows, cols);
    }

    private void SetupGrid(int rowCount, int colCount)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int totalCards = rowCount * colCount;
        bool isEven = totalCards % 2 == 0;
        int emptySlotIndex = (totalCards - 1) / 2;

        for (int i = 0; i < totalCards; i++)
        {
            if (!isEven && i == emptySlotIndex)
            {
                Instantiate(emptyPrefab, transform); // Empty slot
            }
            else
            {
                Instantiate(cardPrefab, transform);
            }
        }
    }

    private void CalculateGridCellSizeAndSpacing()
    {
        float gridWidth = gridRectTransform.rect.width;
        float gridHeight = gridRectTransform.rect.height;
        
        float spacingRatio = 0.1f;
       
        float totalAvailableWidth = gridWidth * (1 - spacingRatio);
        float totalAvailableHeight = gridHeight * (1 - spacingRatio);

        
        float cellSize = Mathf.Min(totalAvailableWidth / cols, totalAvailableHeight / rows);

        
        float spacingX = (gridWidth - (cellSize * cols)) / cols;
        float spacingY = (gridHeight - (cellSize * rows)) / rows;

        
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.spacing = new Vector2(spacingX, spacingY);
        gridLayoutGroup.constraintCount = cols;

        matchingBoard.sizeDelta = new Vector2(cellSize, cellSize);
    }
}
