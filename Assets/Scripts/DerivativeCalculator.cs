// TODO: smooth out differentiation, whether thats gonna be by lerping or blurring the slider handles
// for now, im choosing to focus on literally everything else, as differentiation is just kinda visual
// it sucks, it's annoying, but oh well.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivativeCalculator : MonoBehaviour
{
    public static DerivativeCalculator instance;
    public int derivativeCount;
    public int controlledDerivativeIndex;
    //public float lerp;
    public float minInterval; // time between 
    public float[] currentValues;
    float[] prevValues;
    public bool[] clamps;
    public float[] maxs;
    float scheduledTick;

    void Start()
    {
        DerivativeCalculator.instance = this;
        //
        prevValues = new float[derivativeCount];
    }

    void Update()
    {
        if (Time.time < scheduledTick)
            return;
        SetControlledDerivative(controlledDerivativeIndex, true);
        currentValues = new float[derivativeCount];
        currentValues[controlledDerivativeIndex] = SliderSpawner.instance.handles[controlledDerivativeIndex].value; /*currentValues[controlledDerivativeIndex] = Time.time;*/
        // at this point, scheduledTick has just been passed
        // Time.time >= scheduledTick
        // last tick = scheduledTick - minInterval
        // measuredInterval = actual time since last tick =
        // = Time.time - last tick =
        // = Time.time - (scheduledTick - minInterval) =
        // = Time.time - scheduledTick + minInterval
        float measuredInterval = Time.time - scheduledTick + minInterval; //Debug.Log(measuredInterval);
        // the main shit
        Integrate(measuredInterval);
        Differentiate(measuredInterval);
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
        // schedule next tick
        scheduledTick = Time.time + minInterval;
    }

    void Differentiate(float deltaTime)
    {
        for (int i = controlledDerivativeIndex + 1; i < derivativeCount; i++)
            currentValues[i] = (currentValues[i - 1] - prevValues[i - 1]) / deltaTime;
    }

    void Integrate(float deltaTime)
    {
        for (int i = controlledDerivativeIndex - 1; i >= 0; i--)
            currentValues[i] = SmartClamp(prevValues[i] + currentValues[i + 1] * deltaTime, i);
    }

    public void SetControlledDerivative(int index, bool onlyDraggableOneToo)
    {
        controlledDerivativeIndex = index;
        if (!onlyDraggableOneToo)
            return;
        for (int i = 0; i < derivativeCount; i++)
            SliderSpawner.instance.handles[i].draggable = (i == index);
    }

    float SmartClamp(float value, int derivativeIndex)
    {
        if (!clamps[derivativeIndex])
            return value;
        return Mathf.Clamp(value, -maxs[derivativeIndex], maxs[derivativeIndex]);
    }
}
