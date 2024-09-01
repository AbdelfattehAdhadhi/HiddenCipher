using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int levelIndex = 1;
    public Button button;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float animationDuration = 0.2f;

    private Vector3 originalScale;
    private Tween tween;

    private void Start()
    {
        levelText.text = $"Level {levelIndex}";
        originalScale = transform.localScale;
        button.onClick.AddListener(OnLoadLevel);
    }

    private void OnLoadLevel()
    {
        GameProgressManager.Instance.LevelIndex = levelIndex;
        string levelName = "Level" + levelIndex;
        SceneManager.LoadScene(levelName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;
        tween = transform.DOScale(originalScale * hoverScale, animationDuration).SetEase(Ease.OutBounce);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;
        tween = transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBounce);
    }

    private void OnDisable()
    {
        tween?.Kill();
    }
}