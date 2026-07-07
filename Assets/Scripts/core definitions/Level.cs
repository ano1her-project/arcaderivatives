public class Level
{
    public int[] startWaveIntensities, continuousWaveIntensities;
    public int controlledDerivativeIndex;

    public Level(int[] p_startWaveIntensities, int[] p_continuousWaveIntensities, int p_controlledDerivativeIndex)
    {
        startWaveIntensities = p_startWaveIntensities;
        continuousWaveIntensities = p_continuousWaveIntensities;
        controlledDerivativeIndex = p_controlledDerivativeIndex;
    }
}