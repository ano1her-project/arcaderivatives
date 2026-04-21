using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public float yPos;
    public float xBounds;
    public float intervalBetweenWaves;

    public Sprite unarmedSprite;

    // enemy catalogue:
    EnemyData unarmed;

    // wave catalogue:
    EnemyWaveData[] waveCatalogue;

    void Start()
    {
        // enemy catalogue:
        unarmed = new(unarmedSprite, null, 0.4f);
        // wave catalogue:

    }

    void SpawnLevel(int levelIndex)
    {

    }
}
