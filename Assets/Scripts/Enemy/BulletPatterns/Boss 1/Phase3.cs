using System;
using UnityEngine;
using static Namespaces.Helpers;

namespace Enemy.BulletPatterns.Boss_1
{
    /// <summary>
    ///     Bullet spawns go around the enemy in a circle, aimed at player. There are 4 other shots that are offset by
    ///     angle offset.
    /// </summary>
    public class Phase3 : EnemyPattern
    {
// 
        public float radius;
        public float repeatDelay;           // The amount of time before the pattern repeats.
        public int bulletCount;             // Number of bullets in a circle. This is per grouping.
        public float[] timeline, speeds;    // Keyframe data for the acceleration of the pattern.
        public float angleOffset;           // Angle between ongle offsets.
        public int repeats;
        
        private Vector3 _currentPosition;   // Current spawn position of the bullet.
        private float _currentAngle;
        private int _currSpawn;             // Which bullet is being spawned.
        private float _currRotation;        // Current angle in the rotation.

        private float _angleInterval;       // Angle between bullet spawns.
        private float _rotateInterval;      // The amount added of rotation each spawn.
        private float _timer;               // Timer for timing how long it's been since last repeat ended.
        private Vector3 _targetPos;
        private float _targetAngle;

        // _position, _distance, and _angle are all temporary variables holding the location of the next shots target.
        private Vector3 _position;
        private float _distance, _angle;
        private Vector3[] pos = new Vector3[5]; // Holds the location of all places that the spawned bullets needs to
                                                // target.
        
        /// <summary>
        ///     Fires when repeat cooldown is over. 
        /// </summary>
        public override void Fire()
        {
            if (_timer <= 0)
            {
                IndividualFire();
                _currSpawn += 1;
            }
        }
        
        /// <summary>
        ///     Fires at all target locations within pos
        /// </summary>
        private void IndividualFire()
        {
            _position = transform.position;
            if (Life.IsDead || Life.IsDown)
                return;
            if (_currSpawn >= bulletCount)
            {
                _currSpawn = 0;
                _timer = repeatDelay;
                _targetPos = target.transform.position;
                UpdateTargets();
                return;
            }
            
            _currentAngle += _angleInterval;
            for (int i = 0; i < 5; i++)
            {
                _currentPosition = AngleToVec(_currentAngle, radius);
                SingleFire(DrawLineTo(transform.position + _currentPosition, pos[i], velocity),  _currentPosition);
            }
        }
        
        /// <summary>
        ///     Sets the variables to the proper positions.
        /// </summary>
        public override void Begin()
        {
            _targetPos = target.transform.position;
            _position = transform.position;
            UpdateTargets();
            InvokeRepeating("Fire", delay, rateOfFire);
            Invoking = true;
        }

        /// <summary>
        ///     Sets proper variables.
        /// </summary>
        private void Start()
        {
            GetElements();
            _angleInterval = 360f / (float) ((float) bulletCount / (float) repeats);
            var position = transform.position;
            _currentPosition = new Vector3(position.x, position.y + radius, position.z);
        }
        private void Update()
        {
            _timer -= Time.deltaTime;
        }
        
        /// <summary>
        ///     calculates the locations of all target locations.
        /// </summary>
        private void UpdateTargets()
        {
            for (int i = 0; i < 5; i++)
            {
                _angle = AngleTo(_position + _currentPosition, _targetPos) + angleOffset * (i - 2);
                _distance = VectorVelocity(DrawLineTo(_position + _currentPosition, _targetPos));
                pos[i].x = AngleToVec(_angle, _distance).x + (_position + _currentPosition).x;
                pos[i].y = -(AngleToVec(_angle, _distance).y - (_position + _currentPosition).y);
            }
        }
    }
}
