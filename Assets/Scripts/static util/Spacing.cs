using System.Collections.Generic;
using UnityEngine;

public static class Spacing
{
    public static float[] BetweenWalls(int count, float walls, float objWidth)
    // the distance between the wall and the first ship is the same as the distance between ships
    // accounts for the width of the individual ships
    {
        float[] xs = new float[count];
        float x = -walls - objWidth / 2f;
        float increment = (2 * walls + objWidth) / (count + 1); // just trust me bro
        for (int i = 0; i < count; i++)
        {
            x += increment;
            xs[i] = x;
        }
        return xs;
    }

    public static float[] FromSetIncrement(int count, float increment)
    // centered
    // the distance between ships is set
    // the distance between the wall and the first ship is not even considered
    {
        float[] xs = new float[count];
        float x = -((count - 1) * increment) / 2f;
        for (int i = 0; i < count; i++)
        {
            xs[i] = x;
            x += increment;
        }
        return xs;
    }

    public static float[] FromSetGaps(int count, float objWidth, float gapWidth)
        => FromSetIncrement(count, objWidth + gapWidth);
}