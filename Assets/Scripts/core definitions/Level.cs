public class Level
{
    public EnemyWaveData[] startWaves, mainWaves;
    public int controlledDerivativeIndex;

    public Level(EnemyWaveData[] p_startWaves, EnemyWaveData[] p_mainWaves, int p_controlledDerivativeIndex)
    {
        startWaves = p_startWaves;
        mainWaves = p_mainWaves;
        controlledDerivativeIndex = p_controlledDerivativeIndex;
    }
}