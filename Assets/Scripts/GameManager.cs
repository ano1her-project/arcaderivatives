using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevelIndex = -1;

    void Start()
    {
        instance = this;
    }

    bool firstUpdate = true;
    bool onEnemyDeathUpdate = false;
    bool onPlayerDeathUpdate = false;

    void Update()
    {
        if (firstUpdate)
            FirstUpdate();
        if (onEnemyDeathUpdate)
            UpdateAfterEnemyDeath();
    }

    void FirstUpdate()
    {
        firstUpdate = false;
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
            StartNextLevel(); // all waves have been spawned AND all enemies are dead, start the next level        
    }
}
