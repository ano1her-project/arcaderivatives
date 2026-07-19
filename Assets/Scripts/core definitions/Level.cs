public class Level
{
    public EnemyWaveData[] startWaves, mainWaves;

    public Level(EnemyWaveData[] p_startWaves, EnemyWaveData[] p_mainWaves)
    {
        startWaves = p_startWaves;
        mainWaves = p_mainWaves;
    }
}