using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public GunData data;
    public float bulletDirection;
    float scheduledFire;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time < scheduledFire)
            return;
        // shoot
        data.bullet.Spawn(transform.position + Vector3.up * 0.5f * bulletDirection, bulletDirection);
        // schedule next bullet
        scheduledFire = Time.time + data.interval;
    }
}
