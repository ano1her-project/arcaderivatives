using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveData
{
    public EnemyData[] enemies;
    public float[] xs;

    public EnemyWaveData(EnemyData[] p_enemies)
    {
        enemies = p_enemies;
    }

    public EnemyWaveData(EnemyData[] p_enemies, float[] p_xs)
    {
        enemies = p_enemies;
        xs = p_xs;
    }

    public EnemyWaveData SpacingFromSetIncrement(float increment)
    {
        float[] xs = new float[enemies.Length];
        float x = -((xs.Length - 1) * increment) / 2f;
        for (int i = 0; i < xs.Length; i++)
        {
            xs[i] = x;
            x += increment;
        }
        return new(enemies, xs);
    }

    public EnemyWaveData OffsetX(float offset)
        => new(enemies, xs.Select(x => x + offset).ToArray());

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