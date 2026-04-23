using UnityEngine;

public class AutoGun : MonoBehaviour
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
        data.bullet.Spawn(transform.position + Vector3.up * (0.5f * bulletDirection), bulletDirection, gameObject.tag);
        // schedule next bullet
        scheduledFire = Time.time + data.interval;
    }
}
