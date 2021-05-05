using static Namespaces.Helpers;

namespace Enemy.BulletPatterns
{
    
    /// <summary>
    ///     Bullets are spawned in a way that they expand out as a circle.
    /// </summary>
    public class CircleBlast : EnemyPattern
    {
        public int bulletsPerShot;          // Amount of bullets spawned in a circle.
        public bool accelerates;
        public float[] timeline, speeds;    // Acceleration keyframes.
        private int _bulletSpread;
        private int _currentAngle;

        private void Start()
        {
            GetElements();
            _bulletSpread = 360 / bulletsPerShot;
        }

        public override void Fire()
        {
            if (!Life.IsDead && !Life.IsDown)
                for (var i = 0; i < bulletsPerShot; i++)
                {
                    FinalVelocity = AngleToVec(_currentAngle, velocity);
                    _currentAngle += _bulletSpread;
                    if (accelerates)
                        SingleFire(FinalVelocity, timeline, speeds);
                    else
                        SingleFire(FinalVelocity);
                }
        }
    }
}