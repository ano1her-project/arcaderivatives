public readonly struct GunData
{
    public readonly BulletData bullet;
    public readonly float interval;

    public GunData(BulletData p_bullet, float p_interval)
    {
        bullet = p_bullet;
        interval = p_interval;
    }
}