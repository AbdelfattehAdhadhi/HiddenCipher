using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCounter : GameMetric
{
    public override void UpdateMetric()
    {
        value++;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        UIManager.Instance.UpdateMatchUI(value);
    }
}
