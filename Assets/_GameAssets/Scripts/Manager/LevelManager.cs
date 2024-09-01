using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelButton[] levelButtons;

    private int unlockedLevels;

    private void Start()
    {
        unlockedLevels = SaveSystem.Load("UnlockedLevels", 1);
        Debug.Log(unlockedLevels);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            if (levelIndex > unlockedLevels)
            {
                levelButtons[i].button.interactable = false;
            }
            else
            {
                levelButtons[i].button.interactable = true;
            }
        }
    }
}