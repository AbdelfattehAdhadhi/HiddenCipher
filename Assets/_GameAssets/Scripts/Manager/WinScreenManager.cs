using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class WinScreenManager : Singleton<WinScreenManager>
{
    [SerializeField] private CanvasGroup winScreenCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private TextMeshProUGUI levelCompleteText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI TopComboText;

    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        winScreenCanvasGroup.alpha = 0;
        winScreenCanvasGroup.interactable = false;
        winScreenCanvasGroup.blocksRaycasts = false;

        nextLevelButton.onClick.AddListener(OnNextLevel);
        reloadButton.onClick.AddListener(OnReloadLevel);
        menuButton.onClick.AddListener(OnMenu);
    }

    public void ShowWinScreen()
    {
        winScreenCanvasGroup.DOFade(1, fadeDuration)
            .OnStart(() =>
            {
                winScreenCanvasGroup.interactable = true;
                winScreenCanvasGroup.blocksRaycasts = true;
            });
    }

    public void HideWinScreen()
    {
        winScreenCanvasGroup.DOFade(0, fadeDuration)
            .OnComplete(() =>
            {
                winScreenCanvasGroup.interactable = false;
                winScreenCanvasGroup.blocksRaycasts = false;
            });
    }

    public void SetupWinScreen(float time, int moves, int combo, int topCombo)
    {
        timeText.text = FormatTime(time);
        movesText.text = moves.ToString();
        comboText.text = "X" + combo;
        TopComboText.text = "X" + topCombo;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        string timeFormatted = string.Format("{0:0}:{1:00}", minutes, seconds);
        return timeFormatted;
    }

    private void OnNextLevel()
    {
        GameProgressManager.Instance.LoadNextLevel();
    }

    private void OnReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}