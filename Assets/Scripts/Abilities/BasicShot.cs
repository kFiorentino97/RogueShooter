using Abilities.Helpers;
using Player;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    ///     <c>BasicShot</c> is the basic shooting ability for the player, holding down the fire key will shoot bullets at the
    ///     set velocity.
    /// </summary>
    public class BasicShot : Ability
    {
        public GameObject bulletPrefab;
        public CreateSparks createSparks;

        public Vector2 velocity;
        public float rateOfFire;

        private BulletFire _fireScript;

        private bool _invoking;

        /// <summary>
        ///     Gets prefabs and scripts
        /// </summary>
        private void Start()
        {
            createSparks = GetComponent<CreateSparks>();
            _fireScript = bulletPrefab.GetComponent<BulletFire>();
        }

        public override void Begin()
        {
            InvokeRepeating("fire", 0f, rateOfFire);
            createSparks.Begin();
            _invoking = true;
        }

        public override void End()
        {
            if (_invoking)
            {
                CancelInvoke("fire");
                createSparks.End();
                _invoking = false;
            }
        }

        /// <summary>
        ///     The method invoked repeatedly from begin. Creates and fires bullets.
        /// </summary>
        private void fire()
        {
            GameObject bulletInstance;
            var startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            bulletInstance = Instantiate(bulletPrefab, startPos, transform.rotation);
            _fireScript.velocity.x = velocity.x;
            _fireScript.velocity.y = velocity.y;
        }
    }
}