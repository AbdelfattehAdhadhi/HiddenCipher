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
}

[Serializable]
public class Card
{
    public Sprite cardSprite;
    public int id;
}