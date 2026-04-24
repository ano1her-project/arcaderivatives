public class Level
{/*
    public int baseIntensity;
    public intmaxIntensity;*/

    public int[] waveIntensities;
    public int controlledDerivativeIndex;

    public Level(int[] p_waveIntensities, int p_controlledDerivativeIndex)
    {
        waveIntensities = p_waveIntensities;
        controlledDerivativeIndex = p_controlledDerivativeIndex;
    }
}