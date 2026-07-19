using UnityEngine;
using System.Linq;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public float yPos;
    public float xBounds;
    public float intervalBetweenStartWaves, intervalBetweenMainWaves;

    public Sprite bulletSprite;
    public Sprite unarmedSprite, cannonSprite, turretSprite;

    Level[] levels;

    void Start()
    {
        instance = this;
        // enemy catalogue:
        EnemyData unarmed = new(unarmedSprite, null, 2f);
        EnemyData cannon = new(cannonSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 2f);
        EnemyData turret = new(turretSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 2f, true);
        // wave catalogue:
        EnemyWaveData u = new(new EnemyData[] {unarmed}, new float[] { 0f });
        EnemyWaveData u_u = new EnemyWaveData(unarmed.Repeat(2)).SpacingFromSetIncrement(2f);
        // levels:
        levels = new Level[] {
            new(new EnemyWaveData[] {
                u,
                u,
                u_u}, 
                new EnemyWaveData[] {
                u.OffsetX(-5f),
                u.OffsetX(5f),
                u_u.OffsetX(-5f),
                u_u.OffsetX(5f),
                u_u.OffsetX(-5f),
                u_u.OffsetX(5f),
                u_u,
                u_u,
                u_u}),
            new(new EnemyWaveData[] {
                },
                new EnemyWaveData[] {

                })
        };
        // dev info
        float exampleSpeed = unarmed.velocity;
        Debug.Log($"a ship with speed {exampleSpeed}m/s will take {(yPos - 1) / exampleSpeed}s to reach the player and {yPos / exampleSpeed}s to reach the player-side bounds.");
    }

    Level level; // current level, cached for performance/legibility
    bool isInStartPhase;
    int currentWaveIndex;
    float scheduledWaveSpawn = float.MaxValue;
    float firstStartWaveYPos;

    public void SpawnLevel(int levelIndex)
    {
        level = levels[levelIndex];
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
