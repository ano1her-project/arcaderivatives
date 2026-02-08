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

    public GameObject Spawn(Vector3 position)
    {
        var inst = GameObject.Instantiate(prefab, position, Quaternion.identity);
        inst.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * velocity;
        if (gun is null)
            Object.Destroy(inst.GetComponent<ShipGun>());
        else
            inst.GetComponent<ShipGun>().data = gun;
        return inst;
    }
}

public class EnemyWaveData
{
    public EnemyData[] enemies;
    public float[] xs;

    public EnemyWaveData(EnemyData[] p_enemies, float[] p_xs)
    {
        enemies = p_enemies;
        xs = p_xs;
    }

    public static EnemyWaveData FromEnemyAmongFiller(EnemyData enemy, EnemyData filler, float[] xs)
    {
        EnemyData[] enemies = new EnemyData[xs.Length];
        enemies[Random.Range(0, xs.Length)] = enemy; // the one non-filler enemy
        for (int i = 0; i < xs.Length; i++)
        {
            if (enemies[i] is null)
                enemies[i] = filler;
        }
        return new(enemies, xs);
    }

    public static EnemyWaveData FromEnemiesAmongFiller(EnemyData[] nonFillerEnemies, EnemyData filler, float[] xs)
    {
        EnemyData[] enemies = new EnemyData[xs.Length];
        List<int> vacantIndexes = new();
        for (int i = 0; i < xs.Length; i++) // fill up vacantIndexes with numbers from 0 to n
            vacantIndexes.Add(i);
        // place non filler enemies
        for (int e = 0; e < nonFillerEnemies.Length; e++)
        {
            int i = Random.Range(0, vacantIndexes.Count);
            enemies[i] = nonFillerEnemies[e];
            vacantIndexes.RemoveAt(i);
        }
        // fill all other spots with filler
        for (int i = 0; i < xs.Length; i++) // using whatever's still left in vacantIndexes here is probably possible but feels more expensive, idk
        {
            if (enemies[i] is null)
                enemies[i] = filler;
        }
        return new(enemies, xs);
    }

    public GameObject[] Spawn(float yPos)
    {        
        if (enemies.Length != xs.Length)
        {
            Debug.LogError("Enemy amount doesn't match amount of provided x positions! Spawn() aborted. Fuck you.");
            return null;
        }
        var gameObjects = new GameObject[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
            gameObjects[i] = enemies[i].Spawn(Vector3.up * yPos + Vector3.right * xs[i]);
        return gameObjects;
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
        float xIncrement = 2 * walls / (count + 1) + objWidth / (count + 1); // just trust me bro
        for (int i = 0; i < count; i++)
        {
            x += xIncrement;
            xs[i] = x;
        }
        return xs;
    }
}

public static class Extensions
{
    public static T[] Repeat<T>(this T obj, int count)
    {
        T[] result = new T[count];
        System.Array.Fill(result, obj);
        return result;
    }
}