using UnityEngine;
using System.Linq;

public static class Extensions
{
    public static T[] Repeat<T>(this T obj, int count)
        => Enumerable.Repeat(obj, count).ToArray();

    public static T ChooseRandom<T>(this T[] array)
        => array[Random.Range(0, array.Length)];
}
