public class MoveCounter : GameMetric
{
    public override void UpdateMetric()
    {
        value++;
        UpdateUI();
    }

    public override void UpdateUI()
    {
        UIManager.Instance.UpdateMovesUI(value);
    }
}