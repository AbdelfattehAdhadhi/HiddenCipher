using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI matchText;
    public void UpdateTimeUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        string timeFormatted = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = timeFormatted;
    }

    public void UpdateMovesUI(int moves)
    {
        moveText.text = $"{moves}";
    }

    public void UpdateComboUI(int combo)
    {
        comboText.text = $"{combo}";
    }

    public void UpdateMatchUI(int match)
    {
        matchText.text = $"{match}";
    }
}
