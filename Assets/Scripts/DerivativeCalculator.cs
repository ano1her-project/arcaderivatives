using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivativeCalculator : MonoBehaviour
{
    public static DerivativeCalculator instance;
    public int derivativeCount;
    public int pastSliceCount;
    public int controlledDerivativeIndex;
    List<float[]> derivativeHistory = new(); // a fixed-size rolling buffer
                                             // each array contains all derivatives for a single frame
    void Start()
    {
        DerivativeCalculator.instance = this;
        //
        for (int i = 0; i < pastSliceCount; i++)
            derivativeHistory.Add(new float[derivativeCount]);
        //
        SetControlledDerivative(2, true);
    }

    void FixedUpdate()
    {
        // pastSlices = {3 frames ago; 2 frames ago; 1 frame ago}

        float[] currentSlice = new float[derivativeCount];
        // current value of the controlled derivative
        currentSlice[controlledDerivativeIndex] = SliderSpawner.instance.handles[controlledDerivativeIndex].value; 
        // differentiate
        for (int i = controlledDerivativeIndex + 1; i < derivativeCount; i++)
            currentSlice[i] = (currentSlice[i - 1] - derivativeHistory[0][i - 1]) / (Time.deltaTime * pastSliceCount);
        // integrate
        for (int i = controlledDerivativeIndex - 1; i >= 0; i--)
            currentSlice[i] = derivativeHistory[^1][i] + currentSlice[i + 1] * Time.deltaTime;
        // advance history
        derivativeHistory.Add(currentSlice); // add the new slice
        derivativeHistory.RemoveAt(0); // and remove the oldest one.

        // pastSlices = {2 frames ago; 1 frame ago; this frame}

        // update non-controlled sliders:
        for (int i = 0; i < derivativeCount; i++)
        {
            if (i == controlledDerivativeIndex)
                continue;
            SliderSpawner.instance.handles[i].SetValue(currentSlice[i], true);
        }
    }

    public void SetControlledDerivative(int index, bool onlyDraggableOneToo)
    {
        controlledDerivativeIndex = index;
        if (!onlyDraggableOneToo)
            return;
        for (int i = 0; i < derivativeCount; i++)
            SliderSpawner.instance.handles[i].draggable = (i == index);
    }
}
