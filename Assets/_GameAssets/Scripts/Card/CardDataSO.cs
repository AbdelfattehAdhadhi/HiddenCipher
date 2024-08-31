using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardDataSO : ScriptableObject
{
    public Card[] cards;
    public int rows;
    public int cols;

    public Card[] GetCards()
    {
        int totalCards = rows * cols;
        totalCards = totalCards % 2 == 0 ? totalCards : totalCards - 1;

        int halfCards = totalCards / 2;

        Card[] result = new Card[totalCards];

        for (int i = 0; i < halfCards; i++)
        {
            result[i] = cards[i];
            result[i + halfCards] = cards[i];
        }

        result.Shuffle();

        return result;
    }
}

[Serializable]
public class Card
{
    public Sprite cardSprite;
    public int id;
}