using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public float yPos;
    public float xBounds;
    public float intervalBetweenStartWaves, intervalBetweenMainWaves;

    void Start()
    {
        instance = this;        
    }

    Level level; // current level, cached for performance
    bool isInStartPhase;
    int currentWaveIndex;
    float scheduledWaveSpawn = float.MaxValue;
    float currentYPos;
    public List<GameObject> spawnedEnemies;

    public void SpawnLevel(int levelIndex)
    {
        level = Catalog.instance.levels[levelIndex];
        isInStartPhase = true;
        currentWaveIndex = 0;
        scheduledWaveSpawn = Time.time + intervalBetweenStartWaves;
        currentYPos = yPos - 2f * (level.startWaves.Length - 1);
    } 

    void Update()
    {
        if (Time.time < scheduledWaveSpawn)
            return;
        if (AllWavesSpawned())
            return;
        var wave = isInStartPhase ? level.startWaves[currentWaveIndex] : level.mainWaves[currentWaveIndex];        
        spawnedEnemies.AddRange(wave.Spawn(currentYPos));        
        currentWaveIndex++;
        if (isInStartPhase && currentWaveIndex >= level.startWaves.Length)
        {
            isInStartPhase = false;
            currentWaveIndex = 0;
        }
        if (isInStartPhase)
            currentYPos += 2f;
        scheduledWaveSpawn += isInStartPhase ? 
            intervalBetweenStartWaves : 
            intervalBetweenMainWaves;
    }

    public bool AllWavesSpawned()
        => !isInStartPhase && currentWaveIndex >= level.mainWaves.Length;
}
