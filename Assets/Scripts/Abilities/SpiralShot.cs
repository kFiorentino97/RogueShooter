using Abilities.Helpers;
using Player;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    ///     <c>SpiralShot</c> is a player ability where the player shoots bullets in a double spiral pattern around
    ///     their ship.
    /// </summary>
    public class SpiralShot : Ability
    {
        public GameObject bulletPrefab;
        public CreateSparks createSparks;

        public float velocity, rateOfFire, rotationSpeed;
        private float _currentAngle; // The current angle that is being fired at.
        private Vector2 _finalVelocity;

        private BulletFire _fireScript;

        private bool _invoking;

        /// <summary>
        ///     Gets prefabs and scripts.
        /// </summary>
        private void Start()
        {
            createSparks = GetComponent<CreateSparks>();
            _fireScript = bulletPrefab.GetComponent<BulletFire>();
        }

        public override void Begin()
        {
            InvokeRepeating("Fire", 0f, rateOfFire);
            createSparks.Begin();
            _invoking = true;
        }

        public override void End()
        {
            if (_invoking)
            {
                CancelInvoke("Fire");
                createSparks.End();
                _invoking = false;
            }
        }

        /// <summary>
        ///     Fire will alternate between negative and positive angle to give the ability a double spiral pattern.
        ///     Each time it is fired it instantiates a bullet that will be fired at the angle and velocity set by the
        ///     variables.
        /// </summary>
        private void Fire()
        {
            GameObject bulletInstance;
            var startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            bulletInstance = Instantiate(bulletPrefab, startPos, transform.rotation);
            bulletInstance.transform.eulerAngles = new Vector3(0, 0, _currentAngle);
            _finalVelocity = new Vector2(0, velocity);
            _currentAngle += rotationSpeed * Time.deltaTime;
            if (_currentAngle >= 360) _currentAngle = _currentAngle % 360;
            _fireScript.velocity.x = _finalVelocity.x;
            _fireScript.velocity.y = _finalVelocity.y;
            _currentAngle = -_currentAngle;
            rotationSpeed = -rotationSpeed;
        }
    }
}