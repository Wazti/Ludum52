using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;

            return vector;
        }

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}