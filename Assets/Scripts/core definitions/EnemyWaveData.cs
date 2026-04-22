using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveData
{
    public EnemyData[] enemies;
    public float[] xs;
    //
    public int intensity;

    public EnemyWaveData(EnemyData[] p_enemies, float[] p_xs, int p_intensity)
    {
        enemies = p_enemies;
        xs = p_xs;
        intensity = p_intensity;
    }

    public static EnemyWaveData FromEnemyAmongFiller(EnemyData enemy, EnemyData filler, float[] xs, int intensity)
    {
        EnemyData[] enemies = new EnemyData[xs.Length];
        enemies[Random.Range(0, xs.Length)] = enemy; // the one non-filler enemy
        for (int i = 0; i < xs.Length; i++)
        {
            if (enemies[i] is null)
                enemies[i] = filler;
        }
        return new(enemies, xs, intensity);
    }

    public static EnemyWaveData FromEnemiesAmongFiller(EnemyData[] nonFillerEnemies, EnemyData filler, float[] xs, int intensity)
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
        return new(enemies, xs, intensity);
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