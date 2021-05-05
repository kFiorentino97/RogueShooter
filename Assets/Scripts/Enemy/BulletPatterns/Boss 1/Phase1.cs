using UnityEngine;
using static Namespaces.Helpers;


namespace Enemy.BulletPatterns.Boss_1
{
    
    /// <summary>
    ///     <c>Phase1</c> is the first pattern of the first boss. Bullets go in a circle around enemy based on radius,
    ///     expand into a spirals and then contracts and then explodes.
    /// </summary>
    public class Phase1 : EnemyPattern
    {
        public float radius;
        public float repeatDelay;           // The amount of time before the pattern repeats.
        public int bulletCount;             // Number of bullets in a circle.
        public int direction = 1;           // Current direction of pattern.
        public float[] timeline, speeds;    // Keyframe data for the acceleration of the pattern.
        public float rotations;             // Number of spirals in the line.
        private Vector3 _currentPosition;   // Current spawn position of the bullet.
        private float _currentAngle;
        private int _currSpawn;             // Which bullet is being spawned.
        private float _currRotation;        // Current angle in the rotation.

        private float _angleInterval;       // Angle between bullet spawns.
        private float _rotateInterval;      // The amount added of rotation each spawn.
        private float _timer;

        /// <summary>
        ///     Gets elements and does related math for intervals.
        /// </summary>
        private void Start()
        {
            GetElements();
            _angleInterval = 360f / bulletCount;
            _rotateInterval = 360f * rotations / bulletCount;
            _currentPosition = new Vector3(transform.position.x, transform.position.y + radius, transform.position.z);
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
        }

        public override void Begin()
        {
            InvokeRepeating("Fire", delay, rateOfFire);
            Invoking = true;
        }

        public override void Fire()
        {
            if (_timer <= 0)
            {
                IndividualFire();
                _currSpawn += 1;
            }
        }
        
        /// <summary>
        ///     For each individual unit spawned.
        /// </summary>
        public void IndividualFire()
        {
            if (!Life.IsDead && !Life.IsDown)
            {
                if (_currSpawn >= bulletCount)
                {
                    direction = direction * -1;
                    _currSpawn = 0;
                    _timer = repeatDelay;
                }

                _currentPosition = AngleToVec(_currentAngle, radius);
                _currentAngle += _angleInterval;
                SingleFire(AngleToVec(_currRotation, velocity), _currentPosition, timeline, speeds);
                _currRotation += _rotateInterval;
            }
        }
    }
}