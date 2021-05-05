using UnityEngine;

namespace Enemy.BulletPatterns
{
    
    /// <summary>
    ///     Draws a line of bullets that are aimed at the palyer.
    /// </summary>
    public class EpAimLine : EnemyPattern
    {
        public Vector3 lineStart, lineEnd;  // The start and end of the drawn line.
        public int bulletCount;             // The amount of bullets in a line.
        public bool bounceBack;             // Whether or not it bounces back.
        public int direction = 1;           // Current direction.
        public bool doubleShot;             // Whether or not there are 2 lines.
        private Vector3 _currentDistance;   // Current spawn position of bullet.
        private Vector3 _distanceInterval;  // Current distance between bullet spawns.

        private EpAim _epAim;
        private Vector3 _lineReference;     // Either the lineStart or lineEnd depending on the direction.
        private int _currSpawn;             // Current bullet to spawned.

        /// <summary>
        ///     Gets elements and does math for related variables.
        /// </summary>
        private void Start()
        {
            GetElements();
            _epAim = gameObject.GetComponent<EpAim>();
            _distanceInterval = (lineEnd - lineStart) / bulletCount;
            _currentDistance = lineEnd * direction;
            _epAim.velocity = velocity;
            _lineReference = lineEnd;
        }

        public override void Begin()
        {
            InvokeRepeating("Fire", delay, rateOfFire);
            Invoking = true;
            _currSpawn += 1;
        }
        
        /// <summary>
        ///     Repeats individual fire until it reaches the end of the line, then changes direction.
        /// </summary>
        public override void Fire()
        {
            IndividualFire();
            if (doubleShot)
            {
                direction = direction * -1;
                _lineReference = direction == 1 ? lineEnd : lineStart;
                IndividualFire();
                direction = direction * -1;
                _lineReference = direction == 1 ? lineEnd : lineStart;
            }

            _currSpawn += 1;
        }

        public void IndividualFire()
        {
            if (!Life.IsDead && !Life.IsDown)
            {
                _epAim.startOffset = _currentDistance;
                if (_currSpawn >= bulletCount)
                {
                    if (bounceBack)
                    {
                        direction = direction * -1;
                        _lineReference = direction == 1 ? lineEnd : lineStart;
                    }
                    else
                    {
                        _currentDistance = _lineReference;
                    }

                    _currSpawn = 0;
                }

                _currentDistance = _lineReference + _distanceInterval * (_currSpawn * -direction);

                _epAim.Fire();
            }
        }
    }
}