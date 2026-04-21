using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int level;

    void Start()
    {
        GameManager.instance = this; // oh boy! i sure hope using singletons doesnt come back to bite me in the ass!
    }

    void Update()
    {
        
    }
}
