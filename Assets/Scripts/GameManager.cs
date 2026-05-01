using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int levelIndex = -1;
    Level[] levels = new Level[] { 
        new(new int[] { 0, 0 }, new int[] { 0, 1 }, 0)
    };

    public Level GetCurrentLevel()
        => levels[levelIndex];

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
        firstUpdate = false;
    }

    void FirstUpdate()
    {
        NextLevel();
    }

    void NextLevel()
    {
        levelIndex++;
        DerivativeCalculator.instance.SetControlledDerivative(GetCurrentLevel().controlledDerivativeIndex, true);
        EnemySpawner.instance.SpawnLevel();
    }
}
