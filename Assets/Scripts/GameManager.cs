using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitBetweenLevels;
    public int currentLevelIndex = -1;

    void Start()
    {
        instance = this;
    }

    bool onEnemyDeathUpdate = false;
    bool onPlayerDeathUpdate = false;
    float scheduledNextLevelStart = 0f;

    void Update()
    {
        if (onEnemyDeathUpdate)
            UpdateAfterEnemyDeath();
        if (Time.time < scheduledNextLevelStart)
            return;
        scheduledNextLevelStart = float.PositiveInfinity;
        StartNextLevel();
    }

    void StartNextLevel()
    {
        currentLevelIndex++;
        DerivativeCalculator.instance.SetControlledDerivative(Catalog.instance.levels[currentLevelIndex].controlledDerivativeIndex, true);
        EnemySpawner.instance.SpawnLevel(currentLevelIndex);
    }

    public void OnShipDeath(bool wasEnemy)
    {
        if (wasEnemy)
            onEnemyDeathUpdate = true;
        else
            onPlayerDeathUpdate = true;
    }

    public void UpdateAfterEnemyDeath() // missing gameobject references only seem to update during an Update()
    {
        onEnemyDeathUpdate = false;
        if (!EnemySpawner.instance.AllWavesSpawned())
            return;
        var spawnedEnemies = EnemySpawner.instance.spawnedEnemies;
        int aliveEnemyCount = 0;
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy)
                aliveEnemyCount++;
        }
        if (aliveEnemyCount == 0)
            scheduledNextLevelStart = Time.time + waitBetweenLevels; // all waves have been spawned AND all enemies are dead, start the next level        
    }
}
