using System;
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
        if (!other.TryGetComponent<HealthHandler>(out _))  // ..then it's a bullet.
            Destroy(other.gameObject);
        TakeDamage();
    }

    public void TakeDamage()
    {
        lives--;
        if (lives <= 0)
        {
            GameManager.instance.OnShipDeath(gameObject.CompareTag("Enemy"));
            Destroy(gameObject);            
        }
    }
}