using System;
using UnityEngine;

public readonly struct EnemyData
{
    public static GameObject prefab;

    public readonly Sprite sprite;
    public readonly GunData? gun;
    public readonly float velocity;
    public readonly bool aimsAtPlayer;
    public readonly int linkedDerivative; // -1 if none

    public EnemyData(Sprite p_sprite, GunData? p_gun, float p_velocity, bool p_aimsAtPlayer, int p_linkedDerivative)
    {
        sprite = p_sprite;
        gun = p_gun;
        velocity = p_velocity;
        aimsAtPlayer = p_aimsAtPlayer;
        linkedDerivative = p_linkedDerivative;
    }

    public EnemyData(Sprite sprite, GunData? gun, float velocity) : this(sprite, gun, velocity, false, -1) { }
    public EnemyData(Sprite sprite, GunData? gun, float velocity, bool aimsAtPlayer) : this(sprite, gun, velocity, aimsAtPlayer, -1) { }
    public EnemyData(Sprite sprite, GunData? gun, float velocity, int linkedDerivative) : this(sprite, gun, velocity, false, linkedDerivative) { }

    public GameObject Spawn(Vector3 position)
    {
        var inst = GameObject.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, -90f));
        inst.GetComponent<SpriteRenderer>().sprite = sprite;
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * velocity;
        // individual components
        if (gun is not null)
            inst.AddComponent<AutoGun>().data = (GunData)gun;
        if (aimsAtPlayer)
            inst.AddComponent<AimAtPlayer>();
        if (linkedDerivative != -1)
            inst.AddComponent<LinkWithSlider>().derivativeIndex = linkedDerivative;
        //
        return inst;
    }
}