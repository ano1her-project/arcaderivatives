using System.Collections.Generic;
using UnityEngine;

public static class Spacing
{
    public static float[] BetweenWalls(int count, float walls, float objWidth)
    // accounts for the width of the individual ships
    {
        float[] xs = new float[count];
        float x = -walls - objWidth / 2f;
        float xIncrement = (2 * walls + objWidth) / (count + 1); // just trust me bro
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }
}
