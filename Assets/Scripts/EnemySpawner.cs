using UnityEngine;
using System.Linq;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public float yPos;
    public float xBounds;
    public float intervalBetweenWaves;

    public Sprite bulletSprite;
    public Sprite unarmedSprite, cannonSprite, turretSprite;

    // enemy pool: // waves built from enemies are hardcoded and not random, so there'd be no use for an array and i simply name them as separate variables.
    EnemyData unarmed, cannon, turret;
    // wave pool:  // levels built from waves, however, are built by picking waves from a set randomly, so there needs to be a pool array.
    EnemyWaveData[] wavePool;
    // levels are built in the GameManager

    void Start()
    {
        instance = this;
        // dev info
        Debug.Log($"a ship with speed {1f}m/s will take {(yPos - 1) / 1f}s to reach the player and {yPos / 1f}s to reach the player-side bounds. it will travel {1f * intervalBetweenWaves}m before the next wave spawns.");
        // enemy catalogue:
        unarmed = new(unarmedSprite, null, 1f);
        cannon = new(cannonSprite, new(new(bulletSprite, 0.25f, 5f), 1f), 1f);
        turret = new(turretSprite, new(new(bulletSprite, 0.25f, 6f), 1f), 1f, true);
        // wave catalogue:
        wavePool = new EnemyWaveData[] {
            new(unarmed.Repeat(4), Spacing.FromSetIncrement(4, 2f), 0),
            new(unarmed.Repeat(5), Spacing.FromSetIncrement(5, 2f), 0),
        };
    }

    EnemyWaveData? lastWave; // last as in previous
    int currentWave;
    float scheduledWaveSpawn = float.MaxValue;

    public void SpawnLevel()
    {
        currentWave = 0;
        scheduledWaveSpawn = Time.time + intervalBetweenWaves;
        lastWave = null;
    }

    void Update()
    {
        if (Time.time < scheduledWaveSpawn)
            return;
        var level = GameManager.instance.GetCurrentLevel();
        if (currentWave >= level.waveIntensities.Length)
            return;
        var wave = wavePool
            .Where(wave => wave.intensity == level.waveIntensities[currentWave]
            && (lastWave is null || wave != lastWave))
            .ToArray().ChooseRandom();
        wave.Spawn(yPos);        
        currentWave++;
        lastWave = wave;
        scheduledWaveSpawn += intervalBetweenWaves;
    }
}
