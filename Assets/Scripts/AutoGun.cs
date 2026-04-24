using UnityEngine;

public class AutoGun : MonoBehaviour
{
    public GunData data;
    float scheduledFire;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time < scheduledFire)
            return;
        // shoot
        float direction = transform.rotation.eulerAngles.z;
        data.bullet.Spawn(transform.position, direction, gameObject.tag);
        // schedule next bullet
        scheduledFire = Time.time + data.interval;
    }
}
