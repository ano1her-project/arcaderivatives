using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{
    public GunData data;
    InputAction shootAction;
    float lastFireTime;

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    void Update()
    {
        if (shootAction.IsPressed() && Time.time >= lastFireTime + data.interval)
        {
            data.bullet.Spawn(transform.position + Vector3.up * 0.5f, 1f, gameObject.tag);
            lastFireTime = Time.time;
        }
    }
}
