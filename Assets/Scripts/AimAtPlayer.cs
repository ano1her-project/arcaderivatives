using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    Transform player;

    void Start()
    => player = GameObject.Find("Player").transform;

    void Update()
    {
        var displacement = player.position - transform.position;
        var angle = Mathf.Atan2(displacement.y, displacement.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}
