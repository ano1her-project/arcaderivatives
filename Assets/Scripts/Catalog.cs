using UnityEngine;

public class Catalog : MonoBehaviour
{
    public static Catalog instance;

    public Sprite bulletSprite;
    public Sprite unarmedSprite, cannonSprite, turretSprite;

    public Level[] levels;

    void Start()
    {
        instance = this;
        //
        // enemy catalogue:
        EnemyData unarmed = new(unarmedSprite, null, 2f);
        EnemyData cannon = new(cannonSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 2f);
        EnemyData turret = new(turretSprite, new(new(bulletSprite, 0.25f, 8f), 2f), 2f, true);
        // wave catalogue:
        EnemyWaveData u = new(new EnemyData[] { unarmed }, new float[] { 0f });
        EnemyWaveData u_u = new EnemyWaveData(unarmed.Repeat(2)).SpacingFromSetIncrement(2f);
        EnemyWaveData u___u = new EnemyWaveData(unarmed.Repeat(2)).SpacingFromSetIncrement(4f);
        EnemyWaveData u_____u = new EnemyWaveData(unarmed.Repeat(2)).SpacingFromSetIncrement(6f);
        EnemyWaveData u_______u = new EnemyWaveData(unarmed.Repeat(2)).SpacingFromSetIncrement(8f);
        // levels:
        levels = new Level[] {
            new(new EnemyWaveData[] {
                    u,
                    u,
                    u_u},
                new EnemyWaveData[] {
                    u.OffsetX(-5f),
                    u.OffsetX(5f),
                    u_u.OffsetX(-5f),
                    u_u.OffsetX(5f),
                    u_u.OffsetX(-5f),
                    u_u.OffsetX(5f),
                    u_u,
                    u_u,
                    u_u}, 0),
            new(new EnemyWaveData[] {
                    u_______u,
                    u_______u,
                    u_______u},
                new EnemyWaveData[] {
                    u_____u,
                    u___u,
                    u_u,
                    u_u,
                    u_u
                
                }, 0)
        };
    }
}
