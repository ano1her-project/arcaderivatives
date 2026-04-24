using UnityEngine;

public class BulletData
{
    public static GameObject prefab;

    public Sprite sprite;
    public float hitboxRadius;
    public float velocity;

    public BulletData(Sprite p_sprite, float p_hitboxRadius, float p_velocity)
    {
        sprite = p_sprite;
        hitboxRadius = p_hitboxRadius;
        velocity = p_velocity;
    }

    public void Spawn(Vector3 position, float direction, string tag)
    {
        var inst = Object.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<SpriteRenderer>().sprite = sprite;
        inst.GetComponent<CircleCollider2D>().radius = hitboxRadius;
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * (velocity * direction);
        inst.tag = tag;
    }
}