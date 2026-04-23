using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public int lives;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
            return;
        if (!other.TryGetComponent<HealthHandler>(out _))
            Destroy(other.gameObject);
        lives--;
        if (lives <= 0)
            Destroy(gameObject);
    }
}