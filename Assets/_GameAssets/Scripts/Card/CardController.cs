using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int id;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    [SerializeField] private Sprite faceSprite;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private bool facedUp = false;
    [SerializeField] private bool canFlipCard = true;

    private const float FlipDuration = 0.3f;
    private const float FlipBackDuration = FlipDuration / 1.2f;
    [SerializeField] private Transform _transform;
    public int Id => id;

    private bool animationComplete = true;

    private void Start()
    {
        button.onClick.AddListener(FlipCard);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }

    public void Init(int id, Sprite sprite)
    {
        this.id = id;
        image.sprite = backSprite;
        faceSprite = sprite;
    }

    public void FlipCard()
    {
        if (!canFlipCard) return;

        canFlipCard = false;
        SetButtonInteractable(false); // Disable button click

        float targetRotation = facedUp ? 0f : 90f;
        Sprite newSprite = facedUp ? backSprite : faceSprite;

        CreateFlipSequence(targetRotation, newSprite, () =>
        {
            facedUp = !facedUp;
            canFlipCard = true;
            animationComplete = true;
            if (facedUp)
            {
                OnCardSelected();
            }
        });

        AudioManager.Instance?.PlayFlipSound();
    }

    public void FlipBack()
    {
        if (!facedUp) return;

        canFlipCard = false;
        SetButtonInteractable(false);

        CreateFlipSequence(90f, backSprite, () =>
        {
            facedUp = false;
            canFlipCard = true;
            animationComplete = true;
            SetButtonInteractable(true);
        });
    }

    private void CreateFlipSequence(float targetRotation, Sprite newSprite, TweenCallback onCompleteCallback)
    {
        DOTween.Sequence()
            .Append(_transform.DORotate(new Vector3(0f, targetRotation, 0f), FlipDuration))
            .AppendCallback(() => image.sprite = newSprite)
            .Append(_transform.DORotate(Vector3.zero, FlipBackDuration))
            .OnStart(() => animationComplete = false)
            .OnComplete(onCompleteCallback)
            .Play();
    }

    public void OnMatchFound()
    {
        SetButtonInteractable(false);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(_transform.DOMove(GridManager.Instance.matchingBoard.position, 0.7f))
            .Join(_transform.DORotate(new Vector3(0f, 0f, -360f), 0.7f, RotateMode.FastBeyond360))
            .OnStart(() =>
            {
                _transform.SetParent(GridManager.Instance.matchingBoard.parent);
            })
            .OnComplete(() =>
            {
                _transform.DOScale(0, 0.1f)
                .OnComplete(() =>
                {
                    _transform.gameObject.SetActive(false);
                });
            });
    }
    public bool IsAnimationComplete()
    {
        return animationComplete;
    }

    private void SetButtonInteractable(bool value)
    {
        button.interactable = value;
    }

    public void OnCardSelected()
    {
        CardMatchManager.Instance.CardSelected(this);
    }

    public void FlipCardInFirstTime(bool faceUp)
    {
        SetButtonInteractable(false);
        canFlipCard = false;
        float targetRotation = facedUp ? 0f : 90f;
        Sprite newSprite = facedUp ? backSprite : faceSprite;

        CreateFlipSequence(targetRotation, newSprite, () =>
        {
            facedUp = faceUp;
            canFlipCard = true;
            animationComplete = true;
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //_transform.DORotate(new Vector3(0f, 0f, -3f), 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //_transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.OutQuad);
    }
}
