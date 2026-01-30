using UnityEngine;

public class LinkWithSlider : MonoBehaviour
{
    Vector3 origin;
    public int derivativeIndex;

    void Start()
    {
        origin = transform.position;
    }

    bool firstUpdate = true;

    void Update()
    {
        if (!firstUpdate)
            return;       
        FirstUpdate();
        firstUpdate = false;
    }

    void FirstUpdate()
    {
        SliderSpawner.instance.handles[derivativeIndex].links.Add(this);
    }

    public void UpdatePosition(float position)
    {
        transform.position = origin + Vector3.right * position;
    }
}
