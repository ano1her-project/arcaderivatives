using UnityEngine;

public class SetDataClassStaticValues : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

    void Start()
    {
        BulletData.prefab = bulletPrefab;
        EnemyData.prefab = enemyPrefab;
    }
}
