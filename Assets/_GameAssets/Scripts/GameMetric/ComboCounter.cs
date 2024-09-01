using System;
using UnityEngine;

public class ComboCounter : GameMetric
{
    private int TopCombo;

    public override void UpdateMetric()
    {
        value++;
        UpdateUI();
    }

    public void UpdateTopCombo(int comboCount)
    {
        TopCombo = (int)Math.Pow(2, comboCount);
        Debug.Log($"TopCombo: {TopCombo}");
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