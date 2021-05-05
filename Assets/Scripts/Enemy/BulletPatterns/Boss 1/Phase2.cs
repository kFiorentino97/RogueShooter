using UnityEngine;
using static Namespaces.Helpers;
using Random = System.Random;

namespace Enemy.BulletPatterns.Boss_1
{
    /// <summary>
    ///     <c>Phase2</c> is similar to <c>CircleBlast</c> in that it's being shot repeatedly in a circle. The main
    ///     difference is that this one swings back and forth and includes random bullet shots.
    /// </summary>
    public class Phase2 : EnemyPattern
    {
        public float[] timeline, speeds;    // Acceleration keyframes.
        public int bulletsPerShot;          // Amount of bullets spawned in a circle.
        public int shotsBeforeBounce;       // Shots before the swing changes direction
        public float swingInterval;
        
        public float randomShotCd;
        public float randomShotVelocityMax, randomShotVelocityMin;
        public bool hasRandomShots;
        
        // Variables related to the random bullet shots.
        private Random _random = new Random();
        private float _randomShotAngle;
        private float _randomShotTimer;
        private float _randomShotVelocity;
        
        private float _angleInterval;
        private float _currentAngle;
        private float _currentSwing;
        private int _direction = 1;
        private int _shotsLeft;         // Shots left before swing changes direction.
        
        public override void Fire()
        {
            if (Life.IsDown) return;
            if (--_shotsLeft <= 0)
            {
                _direction *= -1;
                _shotsLeft = shotsBeforeBounce;
            }
            _currentSwing += swingInterval * _direction;
            for (var i = 0; i < bulletsPerShot; i++)
            {
                FinalVelocity = AngleToVec(_currentAngle + _currentSwing, velocity);
                _currentAngle += (_angleInterval);
                SingleFire(FinalVelocity, timeline, speeds);
            }
            _currentAngle = 0;
        }

        /// <summary>
        ///     Gets components and sets variables.
        /// </summary>
        private void Start()
        {
            GetElements();
            _angleInterval = 360f / bulletsPerShot;
            _shotsLeft = shotsBeforeBounce;
            _randomShotTimer = delay;
        }

        /// <summary>
        ///     Deals with random shots.
        /// </summary>
        private void Update()
        {
            if(!hasRandomShots) return;
            _randomShotTimer -= Time.deltaTime;
            if (Life.IsDown) return;

            if (_randomShotTimer <= 0)
            {
                _randomShotAngle = (float) _random.NextDouble() * 360f;
                _randomShotVelocity = (float) _random.NextDouble() * (randomShotVelocityMax - randomShotVelocityMin) -
                                      randomShotVelocityMin;
                SingleFire(AngleToVec(_randomShotAngle, _randomShotVelocity));
                _randomShotTimer = randomShotCd;
            }
        }
    }
}