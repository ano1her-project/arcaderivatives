using System.Linq;

public static class Extensions
{
    public static T[] Repeat<T>(this T obj, int count)
        => Enumerable.Repeat(obj, count).ToArray(); 
}
