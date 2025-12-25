using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivativeCalculator : MonoBehaviour
{
    public static DerivativeCalculator instance;
    public int derivativeCount;
    public int controlledDerivativeIndex;
    //public float lerp;
    float[] prevValues;

    void Start()
    {
        DerivativeCalculator.instance = this;
        //
        prevValues = new float[derivativeCount];
    }

    void FixedUpdate()
    {
        SetControlledDerivative(2, true);

        float[] currentValues = new float[derivativeCount];
        // current value of the controlled derivative
        currentValues[controlledDerivativeIndex] = SliderSpawner.instance.handles[controlledDerivativeIndex].value;
        // differentiate
        for (int i = controlledDerivativeIndex + 1; i < derivativeCount; i++)
            currentValues[i] = (currentValues[i - 1] - prevValues[i - 1]) / Time.deltaTime;
        // integrate
        for (int i = controlledDerivativeIndex - 1; i >= 0; i--)
            currentValues[i] = prevValues[i] + currentValues[i + 1] * Time.deltaTime;
        // advance history
        for (int i = 0; i < derivativeCount; i++)
            prevValues[i] = currentValues[i];

        // update non-controlled sliders:
        for (int i = 0; i < derivativeCount; i++)
        {
            if (i == controlledDerivativeIndex)
                continue;
            SliderSpawner.instance.handles[i].SetValue(currentValues[i], true);
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
