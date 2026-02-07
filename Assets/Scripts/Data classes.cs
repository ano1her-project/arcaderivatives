using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public static GameObject prefab;

    public float velocity;

    public BulletData(float p_velocity)
    {
        velocity = p_velocity;
    }

    public void Spawn(Vector3 position, float direction)
    {
        var inst = Object.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * velocity * direction;
    }
}

public class GunData
{
    public BulletData bullet;
    public float interval;

    public GunData(BulletData p_bullet, float p_interval)
    {
        bullet = p_bullet;
        interval = p_interval;
    }
}

public class EnemyData
{
    public static GameObject prefab;

    public GunData? gun;
    public float velocity;

    public EnemyData(GunData? p_gun)
    {
        gun = p_gun;
    }

    public void Spawn(Vector3 position)
    {
        var inst = GameObject.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * velocity;
        if (gun is null)
            Object.Destroy(inst.GetComponent<ShipGun>());
        else
            inst.GetComponent<ShipGun>().data = gun;
    }
}

public class EnemyPlacement
{
    public EnemyData enemy;
    public float xPos;

    public EnemyPlacement(EnemyData p_enemy, float p_xPos)
    {
        enemy = p_enemy;
        xPos = p_xPos;
    }

    public void Spawn(float yPos)
    {
        enemy.Spawn(Vector3.up * yPos + Vector3.right * xPos);
    }
}

public class EnemyWaveData
{
    public EnemyPlacement[] enemyPlacements;

    public static EnemyWaveData FromOneEnemyRepeatedSpaced(EnemyData enemy, int count, float bounds)
    {
        var xs = SpacingGenerator.GetSpacedXs(count, bounds, 1f);
        EnemyWaveData wave = new();
        for (int i = 0; i < count; i++)
            wave.enemyPlacements[i] = new(enemy, xs[i]);
        return wave;
    }

    public void Spawn(float yPos)
    {
        foreach (var placement in enemyPlacements)
            placement.Spawn(yPos);
    }
}

public class SpacingGenerator
{
    /*public static float[] GetSpacedXs(int count, float bounds)
    // probably just use the one below, idk when you'd want to use this one
    {
        float[] xs = new float[count];
        float x = -bounds;
        float xIncrement = 2 * bounds / (count + 1);
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }*/

    public static float[] GetSpacedXs(int count, float walls, float objWidth)
    // accounts for the width of the individual ships
    // that is, for the difference between "ship every x meters" and "x meters between ships"
    {
        float[] xs = new float[count];
        float x = -walls - objWidth / 2f;
        float xIncrement = 2 * walls / (count + 1) - count / (count + 1) * objWidth + objWidth;
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }
}