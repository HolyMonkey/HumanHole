using UnityEngine;


public static class DataExtensions
{
    public static string ToJson(this object obj) =>
        JsonUtility.ToJson(obj);

    public static T FromJson<T>(this string json) =>
        JsonUtility.FromJson<T>(json);
}