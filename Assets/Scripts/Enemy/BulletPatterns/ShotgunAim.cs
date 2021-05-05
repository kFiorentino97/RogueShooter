using UnityEngine;
using static Namespaces.Helpers;

namespace Enemy.BulletPatterns
{
    /// <summary>
    ///     Shoots at the player in a Shotgun-like pattern a set number of times before pausing for a time and repeating
    ///     again.
    /// </summary>
    public class ShotgunAim : EnemyPattern
    {
        public float spread, repeatDelay;
        public int density, repeats;        // density: number of bullets in a spread, repeats: shots until pattern
                                            // pauses

        private float _distanceBetweenShots, _currShotAngle, _cooldownTimer, _repeatCount;
        private Vector3 _targetPos;

        private void Start()
        {
            GetElements();
        }

        private void Update()
        {
            _cooldownTimer = _cooldownTimer > 0 ? _cooldownTimer - Time.deltaTime : 0;
        }

        public override void Fire()
        {
            if (Life.IsDead || Life.IsDown)
                return;
            if (_repeatCount <= repeats && _cooldownTimer <= 0)
            {
                _distanceBetweenShots = spread / (density - 1);
                _targetPos = _repeatCount == 0 ? target.transform.position : _targetPos;
                _currShotAngle = AngleTo(transform.position, _targetPos) - spread / 2;
                for (var i = 0; i < density; i++)
                {
                    SingleFire(AngleToVec(_currShotAngle, velocity));
                    _currShotAngle += _distanceBetweenShots;
                }

                _repeatCount += 1;
            }
            else if (_repeatCount > repeats)
            {
                _cooldownTimer = repeatDelay;
                _repeatCount = 0;
            }
        }
    }
}