using UnityEngine;

public class BulletData
{
    public static GameObject prefab;

    public Sprite sprite;
    public float hitboxRadius;
    public float speed;

    public BulletData(Sprite p_sprite, float p_hitboxRadius, float p_speed)
    {
        sprite = p_sprite;
        hitboxRadius = p_hitboxRadius;
        speed = p_speed;
    }

    public void Spawn(Vector3 position, float angle, string tag)
    {
        var inst = Object.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<SpriteRenderer>().sprite = sprite;
        inst.GetComponent<CircleCollider2D>().radius = hitboxRadius;
        float radians = angle * Mathf.Deg2Rad;
        inst.GetComponent<Rigidbody2D>().linearVelocity = (Vector2.up * Mathf.Sin(radians) + Vector2.right * Mathf.Cos(radians)) * speed;
        inst.tag = tag;
    }
}