using DG.Tweening;
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

    [SerializeField] private bool facedUp = false;
    [SerializeField] private bool canFlipCard = true;

    private const float FlipDuration = 0.3f;
    [SerializeField] private Transform _transform;

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

        if (!canFlipCard) return;

        canFlipCard = false;

        Sequence flipSequence = DOTween.Sequence();

        flipSequence.Append(_transform.DORotate(new Vector3(0f, 90f, 0f), FlipDuration))
                    .AppendCallback(() =>
                    {
                        image.sprite = facedUp ? backSprite : faceSprite;
                    })
                    .Append(_transform.DORotate(Vector3.zero, FlipDuration / 1.2f));

        flipSequence.OnComplete(() =>
        {
            facedUp = !facedUp;
            canFlipCard = true;
        });

        flipSequence.Play();
    }
}
