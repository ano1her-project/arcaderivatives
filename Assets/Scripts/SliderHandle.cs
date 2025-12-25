using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHandle : MonoBehaviour
{
    public float max;
    public bool draggable;
    public bool clamp;
    bool dragging;
    public float value;

    void Start()
    {
        
    }

    void Update()
    {
        if (!Input.GetMouseButton(0)) // if the mouse is released *anywhere*
            dragging = false;
        if (!dragging)
            return;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetValue(mouseWorldPos.x, false);
    }

    void OnMouseDown() // the mouse is pressed while over this collider
    {
        if (!draggable)
            return;
        dragging = true;
    }

    public void SetValue(float p_value, bool external)
    {
        if (clamp)
            p_value = Mathf.Clamp(p_value, -max, max);
        value = p_value;
        transform.position = transform.parent.position + Vector3.right * value; // assuming that position and value are 1:1
        if (external && draggable)
            Debug.Log("A draggable slider handle is being overriden!");
    }
}
