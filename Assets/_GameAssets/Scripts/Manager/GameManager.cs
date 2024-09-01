using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameManager : Singleton<GameManager>
{
    private Timer levelTimer;
    private MoveCounter moveCounter;
    private ComboCounter comboCounter;
    private MatchCounter matchCounter;

    private void Start()
    {
        levelTimer = new Timer();
        moveCounter = new MoveCounter();
        comboCounter = new ComboCounter();
        matchCounter = new MatchCounter();

        //StartLevel();
    }

    private void OnEnable()
    {
        if (CardMatchManager.Instance != null)
        {
            CardMatchManager.Instance.OnMoveMade += HandleMoveMade;
            CardMatchManager.Instance.OnMatchMade += HandleMatchMade;
            CardMatchManager.Instance.OnComboMade += HandleComboMade;
            CardMatchManager.Instance.OnMatchFound += HandleWinCondition;
        }
    }

    private void OnDisable()
    {
        if (CardMatchManager.Instance != null)
        {
            CardMatchManager.Instance.OnMoveMade -= HandleMoveMade;
            CardMatchManager.Instance.OnMatchMade -= HandleMatchMade;
            CardMatchManager.Instance.OnComboMade -= HandleComboMade;
            CardMatchManager.Instance.OnMatchFound -= HandleWinCondition;
        }
    }

    private void Update()
    {
        levelTimer.UpdateMetric();
    }

    public void StartLevel()
    {
        levelTimer.StartTimer();
    }

    public void StopTimer()
    {
        levelTimer.StopTimer();
    }

    public void EndLevel()
    {
        levelTimer.StopTimer();

        WinScreenManager.Instance.SetupWinScreen(
            levelTimer.GetElapsedTime(),
            moveCounter.GetValue(),
            comboCounter.GetValue(),
            comboCounter.GetTopCombo()
            );
        WinScreenManager.Instance.ShowWinScreen();
        if (GameProgressManager.Instance != null)
            GameProgressManager.Instance.CompleteLevel();
    }

    public void HandleMoveMade()
    {
        moveCounter.UpdateMetric();
    }

    public void HandleMatchMade()
    {
        matchCounter.UpdateMetric();
    }

    public void HandleComboMade(int comboCount)
    {
        comboCounter.UpdateMetric();
        comboCounter.UpdateTopCombo(comboCount);
    }

    public void HandleWinCondition()
    {
        if (matchCounter.GetValue() >= GridManager.Instance.GetCardsCount() / 2)
        {
            EndLevel();
            AudioManager.Instance?.PlayWinSound();
        }
    }
}
