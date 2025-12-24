using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSpawner : MonoBehaviour
{
    public static SliderSpawner instance;
    public GameObject sliderPrefab;
    public int sliderCount;
    public Vector3 firstPosition;
    public Vector3 offset;
    public SliderHandle[] handles;

    void Start()
    {
        SliderSpawner.instance = this;
        //
        handles = new SliderHandle[sliderCount];
        for (int i = 0; i < sliderCount; i++)
            handles[i] = Object.Instantiate(sliderPrefab, firstPosition + offset * i, Quaternion.identity).GetComponentInChildren<SliderHandle>();
    }

    void Update()
    {
        
    }
}
