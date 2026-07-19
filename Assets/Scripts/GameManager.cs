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
        DerivativeCalculator.instance.SetControlledDerivative(Catalog.instance.levels[currentLevelIndex].controlledDerivativeIndex, true);
        EnemySpawner.instance.SpawnLevel(currentLevelIndex);
    }
}
