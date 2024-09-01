using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenuCanvasGroup;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;
    [SerializeField] private Image soundButtonImage;

    private bool isPaused = false;
    private bool isSoundOn;

    private Tween pauseMenuTween;

    private void Start()
    {
        isSoundOn = SaveSystem.Load("SoundState", 1) == 1;
        UpdateSoundState();

        menuButton.onClick.AddListener(OnMenuClicked);
        replayButton.onClick.AddListener(OnReplayClicked);
        soundButton.onClick.AddListener(OnSoundClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);

        HidePauseMenu();
    }

    private void OnDisable()
    {
        pauseMenuTween.Kill();
    }

    private void Update()
    {
        // Toggle pause menu on/off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseButtonClicked();
        }
    }

    public void OnPauseButtonClicked()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        // Show pause menu with animation
        pauseMenuTween = pauseMenuCanvasGroup.DOFade(1, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        });
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Hide pause menu with animation
        pauseMenuTween = pauseMenuCanvasGroup.DOFade(0, 0.2f).SetUpdate(true).OnComplete(() =>
        {
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        });
    }

    private void OnMenuClicked()
    {
        ResumeGame();
        SceneManager.LoadScene("Menu");
    }

    private void OnReplayClicked()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSoundClicked()
    {
        // Toggle sound on/off
        isSoundOn = !isSoundOn;
        UpdateSoundState();

        // Save the sound state
        SaveSystem.Save("SoundState", isSoundOn ? 1 : 0);
    }

    private void UpdateSoundState()
    {
        AudioListener.volume = isSoundOn ? 1 : 0;

        
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
        }
    }

    private void OnExitClicked()
    {
        ResumeGame();
    }

    private void HidePauseMenu()
    {
        pauseMenuCanvasGroup.alpha = 0;
        pauseMenuCanvasGroup.interactable = false;
        pauseMenuCanvasGroup.blocksRaycasts = false;
    }
}
