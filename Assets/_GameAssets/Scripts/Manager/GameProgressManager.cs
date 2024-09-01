using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : Singleton<GameProgressManager>
{
    [SerializeField] private int levelIndex = 1;

    public int LevelIndex
    {
        set { levelIndex = value; }
        get { return levelIndex; }
    }
    public void CompleteLevel()
    {
        int unlockedLevels = SaveSystem.Load("UnlockedLevels", 1);

        if (levelIndex >= unlockedLevels)
        {
            unlockedLevels = levelIndex + 1;
            SaveSystem.Save("UnlockedLevels", unlockedLevels);
        }
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 1;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}