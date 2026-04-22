using UnityEngine;

public class EnemyData
{
    public static GameObject prefab;

    public Sprite sprite;
    public GunData? gun;
    public float velocity;

    public EnemyData(Sprite p_sprite, GunData? p_gun, float p_velocity)
    {
        sprite = p_sprite;
        gun = p_gun;
        velocity = p_velocity;
    }

    public GameObject Spawn(Vector3 position)
    {
        var inst = GameObject.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<SpriteRenderer>().sprite = sprite;
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * velocity;
        if (gun is null)
            Object.Destroy(inst.GetComponent<ShipGun>());
        else
            inst.GetComponent<ShipGun>().data = gun;
        return inst;
    }
}