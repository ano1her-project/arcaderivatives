using UnityEngine;

public class PassiveYBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    => Destroy(other.gameObject);
}
