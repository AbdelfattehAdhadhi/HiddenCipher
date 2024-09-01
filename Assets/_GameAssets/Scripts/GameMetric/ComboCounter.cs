using System;
using UnityEngine;

public class ComboCounter : GameMetric
{
    private int TopCombo;
    private int CurrentTopCombo;

    public override void UpdateMetric()
    {
        value++;
        UpdateUI();
    }

    public void UpdateTopCombo(int comboCount)
    {
        CurrentTopCombo = (int)Math.Pow(2, comboCount);
        if(CurrentTopCombo > TopCombo)
        {
            TopCombo = CurrentTopCombo;
        }
        Debug.Log($"TopCombo: {CurrentTopCombo}");
    }

    public override void UpdateUI()
    {
        UIManager.Instance.UpdateComboUI(value);
    }

    public int GetTopCombo()
    {
        return TopCombo;
    }
}