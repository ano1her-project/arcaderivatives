using UnityEngine;
using System.Linq;
using System;

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
    float firstStartWaveYPos;

    public void SpawnLevel(int levelIndex)
    {
        level = Catalog.instance.levels[levelIndex];
        isInStartPhase = true;
        currentWaveIndex = 0;
        scheduledWaveSpawn = Time.time + intervalBetweenStartWaves;
        firstStartWaveYPos = yPos - 2f * (level.startWaves.Length - 1);
    } 

    void Update()
    {
        if (Time.time < scheduledWaveSpawn)
            return;
        if (!isInStartPhase && currentWaveIndex >= level.mainWaves.Length)
            return;
        var wave = isInStartPhase ? level.startWaves[currentWaveIndex] : level.mainWaves[currentWaveIndex];
        float currentYPos = isInStartPhase ?
            firstStartWaveYPos + 2f * currentWaveIndex :
            yPos;
        wave.Spawn(currentYPos);
        currentWaveIndex++;
        if (isInStartPhase && currentWaveIndex >= level.startWaves.Length)
        {
            isInStartPhase = false;
            currentWaveIndex = 0;
        }
        scheduledWaveSpawn += isInStartPhase ? 
            intervalBetweenStartWaves : 
            intervalBetweenMainWaves;
    }
}
