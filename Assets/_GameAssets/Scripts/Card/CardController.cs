using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    [SerializeField] private Sprite faceSprite;
    [SerializeField] private Sprite backSprite;

    private void Start()
    {
        button.onClick.AddListener(FlipCard);
    }

    public void Init(int id, Sprite sprite)
    {
        this.id = id;
        image.sprite = backSprite;
        faceSprite = sprite;
    }

    public void FlipCard()
    {
        Debug.Log($"Flip Card: {id}");
    }
}
