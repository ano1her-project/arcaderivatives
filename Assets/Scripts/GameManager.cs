using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevelIndex = -1;
    readonly int[] controlledDerivativeIndexes = new int[] {0};

    void Start()
    {
        instance = this;
    }

    bool firstUpdate = true;

    void Update()
    {
        if (!firstUpdate)
            return;
        FirstUpdate();        
    }

    void FirstUpdate()
    {
        NextLevel();
        firstUpdate = false;
    }

    void NextLevel()
    {
        currentLevelIndex++;
        DerivativeCalculator.instance.SetControlledDerivative(controlledDerivativeIndexes[currentLevelIndex], true);
        EnemySpawner.instance.SpawnLevel(currentLevelIndex);
    }
}
