using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : Singleton<GridManager>
{
    public RectTransform gridRectTransform;
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform matchingBoard;

    [SerializeField] private CardDataSO cardDataSO;

    public CardController cardPrefab;
    public GameObject emptyPrefab;

    [SerializeField]
    private List<CardController> cardControllers = new();
    private int rows;
    private int cols;

    private void Start()
    {
        rows = cardDataSO.rows;
        cols = cardDataSO.cols;

        CalculateGridCellSizeAndSpacing();
        SetupGrid(rows, cols);
        //OnFlipCard();
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
                CardController card = Instantiate(cardPrefab, transform);
                cardControllers.Add(card);
            }
        }

        InitializeCards();
    }

    private void InitializeCards()
    {
        var cards = cardDataSO.GetCards();
        for (int i = 0; i < cardControllers.Count; i++)
        {
            cardControllers[i].Init(cards[i].id, cards[i].cardSprite);
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

    public int GetCardsCount()
    {
        //return (rows * cols) & ~1;
        return cardControllers.Count;
    }

    [ContextMenu("FlipAllCards")]
    public void OnFlipCard()
    {
        FlipAllCards(cardControllers, true);
    }

    public void FlipAllCards(List<CardController> allCards, bool faceUp)
    {
        StartCoroutine(FlipAllCardsCoroutine(allCards, faceUp));
    }

    private IEnumerator FlipAllCardsCoroutine(List<CardController> allCards, bool faceUp)
    {
        foreach (var card in allCards)
        {
            card.FlipCardInFirstTime(faceUp);
        }

        yield return new WaitForSeconds(0.3f); // wait animation time

        float waitTimePerCard = 0.09f;
        float waitTime = waitTimePerCard * GetCardsCount();
        yield return new WaitForSeconds(waitTime);

        foreach (var card in allCards)
        {
            card.FlipBack();
        }
    }
}
