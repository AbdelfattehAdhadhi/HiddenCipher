public abstract class GameMetric
{
    protected int value;

    public abstract void UpdateMetric();


    public virtual void ResetMetric()
    {
        value = 0;
    }

    public int GetValue()
    {
        return value;
    }

    public virtual void UpdateUI()
    {

    }
}