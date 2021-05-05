using UnityEngine;
using static Namespaces.Helpers;

namespace Enemy.BulletPatterns.Boss_1
{
    /// <summary>
    ///     There's a line of bullet spawns from startLocation to endLocation, these swing slightly back and forth.
    /// </summary>
    public class Phase4 : EnemyPattern
    {
        public Vector3 startLocation, endLocation;
        public int bulletCount;                     // Count of bullets on one side
        public float leftAngle, rightAngle;         // Angle of the shots at the left and right side of the line.
        public float swingAngle;                    // How many degrees the angle will change each shot
        public int swingCount;                      // Amount of shots before changing direction.

        private int _currentSwingInterval;          // Current times shot since last direction change.
        private int _direction = 1;
        private int _actualCount;                   // Count of bullets on both sides.
        private Vector3[] _spawnLocations;          // Holds the location of each bullet spawn
        private Vector3 _splitVector;               // Vector representing the distance and direction between spawns

        /// <summary>
        ///     Gets elements and calculates variables
        /// </summary>
        private void Start()
        {
            GetElements();
            _currentSwingInterval = 0;
            _actualCount = bulletCount * 2;
            _spawnLocations = new Vector3[_actualCount];
            _splitVector = (endLocation - startLocation) / _actualCount;
            for (int i = 0; i < _actualCount; i++)
            {
                _spawnLocations[i] = startLocation + (_splitVector * i);
            }
        }
        
        /// <summary>
        ///     Fires shots and changes directions when needed.
        /// </summary>
        public override void Fire()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                SingleFire(AngleToVec(leftAngle + swingAngle * _currentSwingInterval, velocity), _spawnLocations[i]);
            }

            for (int i = bulletCount; i < _actualCount; i++)
            {
                SingleFire(AngleToVec(rightAngle + swingAngle * _currentSwingInterval, velocity), _spawnLocations[i]);
            }

            _currentSwingInterval += _direction;
            if ((_currentSwingInterval > swingCount && _direction == 1) || (_currentSwingInterval < 0 && _direction == -1)) 
                _direction = _direction * -1;
        }
    }
}
