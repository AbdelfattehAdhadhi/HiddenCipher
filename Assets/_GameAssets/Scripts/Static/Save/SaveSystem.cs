using UnityEngine;

[System.Serializable]
public static class SaveSystem
{
    public static void Save<T>(string Tag, T Json)
    {
        PlayerPrefs.SetString(Tag, Utils.Serialize(Json));
        Debug.Log($"{Tag} Saved as a json");
    }

    public static void Save(string Tag, int value)
    {
        PlayerPrefs.SetInt(Tag, value);
        Debug.Log($"{Tag} Saved as an integer");
    }

    public static T Load<T>(string Tag, T Default)
    {
        if (PlayerPrefs.HasKey(Tag))
        {
            Debug.Log($"Load {Tag} from PlayerPrefs");
            return Utils.Deserialize<T>(PlayerPrefs.GetString(Tag));
        }
        else
        {
            Debug.Log($"No save found for {Tag}, creating new one!");
            Save(Tag, Default);
            return Default;
        }
    }

    public static int Load(string Tag, int Default)
    {
        if (PlayerPrefs.HasKey(Tag))
        {
            Debug.Log($"Load {Tag} from PlayerPrefs");
            return PlayerPrefs.GetInt(Tag);
        }
        else
        {
            Debug.Log($"No save found for {Tag}, creating new one!");
            Save(Tag, Default);
            return Default;
        }
    }
}