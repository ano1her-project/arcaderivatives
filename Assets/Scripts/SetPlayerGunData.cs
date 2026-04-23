using UnityEngine;

// GunData, obviously, doesn't show up in the editor.
// for enemies, it's set by code.
// for the player, we must set it here.
public class SetPlayerGunData : MonoBehaviour
{
    public float bulletVelocity;
    public float gunfireInterval;

    void Start()
    {
        GetComponent<PlayerGun>().data = new(new(bulletVelocity), gunfireInterval);
    }
}
