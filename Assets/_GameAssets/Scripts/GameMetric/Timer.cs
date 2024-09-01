using UnityEngine;

public class Timer : GameMetric
{
    private bool isRunning;
    private float elapsedTime;

    public Timer()
    {
        elapsedTime = 0f;
        isRunning = false;
    }

    public override void UpdateMetric()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateUI();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateUI();
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public override void UpdateUI()
    {
        UIManager.Instance?.UpdateTimeUI(elapsedTime);
    }
}