using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxLevel; // how many levels are defined
    public float yPos;
    public float xBounds;
    public float intervalBetweenWaves;

    public Sprite unarmedSprite;

    // enemy catalogue:
    EnemyData unarmed;

    // wave catalogue:
    EnemyWaveData[][] waveCatalogue;

    void Start()
    {
        // enemy catalogue:
        unarmed = new(unarmedSprite, null, 0.4f);
        // wave catalogue:
        waveCatalogue = new EnemyWaveData[maxLevel][];
    }

    void SpawnLevel(int levelIndex)
    {

    }
}
