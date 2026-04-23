using UnityEngine;
using System.Linq;

public static class Extensions
{
    public static T[] Repeat<T>(this T obj, int count)
        => Enumerable.Repeat(obj, count).ToArray();

    public static T[] And<T>(this T[] a, T[] b)
        => a.Concat(b).ToArray();

    public static T[] And<T>(this T a, T[] b)
        => And(new T[] { a }, b);

    public static T[] And<T>(this T[] a, T b)
        => And(a, new T[] { b });

    public static T ChooseRandom<T>(this T[] array)
        => array[Random.Range(0, array.Length)];
}
