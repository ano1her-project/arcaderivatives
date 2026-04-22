using System.Collections.Generic;
using UnityEngine;

public static class Spacing
{
    /*public static float[] GetSpacedXs(int count, float bounds)
    // probably just use the one below, idk when you'd want to use this one
    {
        float[] xs = new float[count];
        float x = -bounds;
        float xIncrement = 2 * bounds / (count + 1);
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }*/

    public static float[] GetSpacedXs(int count, float walls, float objWidth)
    // accounts for the width of the individual ships
    // that is, for the difference between "ship every x meters" and "x meters between ships"
    {
        float[] xs = new float[count];
        float x = -walls - objWidth / 2f;
        float xIncrement = 2 * walls / (count + 1) + objWidth / (count + 1); // just trust me bro
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }
}
