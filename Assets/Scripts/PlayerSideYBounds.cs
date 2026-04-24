using UnityEngine;

public class PlayerSideYBounds : MonoBehaviour
{
    public HealthHandler playerHealth;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HealthHandler>(out _))
            playerHealth.TakeDamage();
        Destroy(other.gameObject);
    }
}
