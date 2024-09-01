using UnityEngine;

public static class Utils
{
    public static string Serialize<T>(this T toSerialize)
    {
        string json = JsonUtility.ToJson(toSerialize);
        return json;
    }
    public static T Deserialize<T>(this string toDeserialize)
    {
        T ret;
        ret = JsonUtility.FromJson<T>(toDeserialize);
        return ret;
    }
}