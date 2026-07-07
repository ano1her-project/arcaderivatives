using UnityEngine;
using System.Linq;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public float yPos;
    public float xBounds;
    public float intervalBetweenStartWaves, intervalBetweenContinuousWaves;

    public Sprite bulletSprite;
    public Sprite unarmedSprite, cannonSprite, turretSprite;

    // enemy pool: // waves built from enemies are hardcoded and not random, so there'd be no use for an array of enemies and i simply name them as separate variables.
    EnemyData unarmed, cannon, turret;
    // wave pool:  // levels built from waves, however, are built by picking waves from a set randomly, so there needs to be a pool array.
    EnemyWaveData[] wavePool;
    // levels are built in the GameManager

    void Start()
    {
        instance = this;
        // enemy catalogue:
        unarmed = new(unarmedSprite, null, 1f);
        cannon = new(cannonSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 1f);
        turret = new(turretSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 1f, true);
        // wave catalogue:
        wavePool = new EnemyWaveData[] {
            new(unarmed.Repeat(2), Spacing.FromSetIncrement(2, 2f), 0),
            new(unarmed.Repeat(2), Spacing.FromSetIncrement(2, 4f), 0),

            new(unarmed.Repeat(3), Spacing.FromSetIncrement(3, 2f), 1),
            new(unarmed.Repeat(3), Spacing.FromSetIncrement(3, 4f), 1),

            new(cannon.And(unarmed.Repeat(2)), Spacing.FromSetIncrement(3, 2f), -1),
            new(cannon.And(unarmed.Repeat(2)), Spacing.FromSetIncrement(3, 4f), -1),
            new(unarmed.And(cannon).And(unarmed), Spacing.FromSetIncrement(3, 2f), -1),
            new(unarmed.And(cannon).And(unarmed), Spacing.FromSetIncrement(3, 4f), -1),
            new(unarmed.Repeat(2).And(cannon), Spacing.FromSetIncrement(3, 2f), -1),
            new(unarmed.Repeat(2).And(cannon), Spacing.FromSetIncrement(3, 4f), -1),

            new(unarmed.Repeat(4), Spacing.FromSetIncrement(4, 2f), 2),
            new(unarmed.Repeat(4), Spacing.FromSetIncrement(4, 4f), 2),

            new(cannon.And(unarmed.Repeat(3)), Spacing.FromSetIncrement(4, 2f), -2),
            new(cannon.And(unarmed.Repeat(3)), Spacing.FromSetIncrement(4, 4f), -2),
            new(unarmed.And(cannon).And(unarmed.Repeat(2)), Spacing.FromSetIncrement(4, 2f), -2),
            new(unarmed.And(cannon).And(unarmed.Repeat(2)), Spacing.FromSetIncrement(4, 4f), -2),
            new(unarmed.Repeat(2).And(cannon).And(unarmed), Spacing.FromSetIncrement(4, 2f), -2),
            new(unarmed.Repeat(2).And(cannon).And(unarmed), Spacing.FromSetIncrement(4, 4f), -2),
            new(unarmed.Repeat(3).And(cannon), Spacing.FromSetIncrement(4, 2f), -2),
            new(unarmed.Repeat(3).And(cannon), Spacing.FromSetIncrement(4, 4f), -2),
        };
        // dev info
        float exampleSpeed = 1f;
        Debug.Log($"a ship with speed {exampleSpeed}m/s will take {(yPos - 1) / exampleSpeed}s to reach the player and {yPos / exampleSpeed}s to reach the player-side bounds.");
    }

    Level level; // current level, cached for performance
    EnemyWaveData? previousWave; // the same wave mustn't appear twice in a row
    bool isInStartPhase;
    int currentWave; // index
    float scheduledWaveSpawn = float.MaxValue;
    float firstStartWaveYPos;

    public void SpawnLevel()
    {
        level = GameManager.instance.GetCurrentLevel();
        isInStartPhase = true;
        currentWave = 0;
        scheduledWaveSpawn = Time.time + intervalBetweenStartWaves;
        previousWave = null;
        firstStartWaveYPos = yPos - 2f * (level.startWaveIntensities.Length - 1);
    } 

    void Update()
    {
        if (Time.time < scheduledWaveSpawn)
            return;
        if (!isInStartPhase && currentWave >= level.continuousWaveIntensities.Length)
            return;
        int currentWaveIntensity = isInStartPhase ? 
            level.startWaveIntensities[currentWave] : 
            level.continuousWaveIntensities[currentWave]; 
        var wave = wavePool
            .Where(wave => wave.intensity == currentWaveIntensity
            && (previousWave is null || wave != previousWave))
            .ToArray().ChooseRandom();
        float currentYPos = isInStartPhase ?
            firstStartWaveYPos + 2f * currentWave :
            yPos;
        wave.Spawn(currentYPos);  
        currentWave++;
        if (isInStartPhase && currentWave >= level.startWaveIntensities.Length)
        {
            isInStartPhase = false;
            currentWave = 0;
        }
        previousWave = wave;
        scheduledWaveSpawn += isInStartPhase ? 
            intervalBetweenStartWaves : 
            intervalBetweenContinuousWaves;
    }
}
