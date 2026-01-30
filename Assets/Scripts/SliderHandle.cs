using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHandle : MonoBehaviour
{
    public float visualMax;
    public float valueToPosRatio;
    public bool draggable;
    public bool visualClamp;
    bool dragging;
    public float value;
    public List<LinkWithSlider> links = new();

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
        SetValue(mouseWorldPos.x * valueToPosRatio, false);
    }

    void OnMouseDown() // the mouse is pressed while over this collider
    {
        if (!draggable)
            return;
        dragging = true;
    }

    public void SetValue(float p_value, bool external)
    {
        if (visualClamp)
            p_value = Mathf.Clamp(p_value, -visualMax, visualMax);
        value = p_value;
        transform.position = transform.parent.position + Vector3.right * value / valueToPosRatio;
        foreach (var link in links)
            link.UpdatePosition(value / valueToPosRatio);
        if (external && draggable)
            Debug.Log("A draggable slider handle is being overriden!");
    }
}
