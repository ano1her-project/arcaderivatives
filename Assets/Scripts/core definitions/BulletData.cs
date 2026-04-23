using UnityEngine;

public class BulletData
{
    public static GameObject prefab;

    public float velocity;

    public BulletData(float p_velocity)
    {
        velocity = p_velocity;
    }

    public void Spawn(Vector3 position, float direction, string tag)
    {
        var inst = Object.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * (velocity * direction);
        inst.tag = tag;
    }
}