using Player;
using UnityEngine;


namespace Enemy
{
    /// <summary>
    ///     Abstract class for new enemy patterns.
    /// </summary>
    public abstract class EnemyPattern : MonoBehaviour
    {
        public GameObject enemyBulletPrefab;
        public float rateOfFire, acceleration, velocity, delay;
        public GameObject target;

        protected Vector2 FinalVelocity;
        protected EnemyBulletFire FireScript;
        protected bool Invoking;
        protected Lives Life;

        /// <summary>
        ///     Gets necessary elements for all enemy patterns.
        /// </summary>
        public void GetElements()
        {
            FireScript = enemyBulletPrefab.GetComponent<EnemyBulletFire>();
            target = GameObject.FindGameObjectWithTag("Player");
            Life = target.GetComponent<Lives>();
        }

        /// <summary>
        ///     Fires a singular bullet at a velocity.
        /// </summary>
        /// <param name="vel">Vector3D representing velocity.</param>
        /// <param name="startOffset">Offset from GameObject's position.</param>
        public void SingleFire(Vector3 vel, Vector3 startOffset)
        {
            GameObject bulletInstance;
            var position = transform.position;
            FinalVelocity = vel;
            var startPos = new Vector3(position.x, position.y, position.z);
            bulletInstance = Instantiate(enemyBulletPrefab, startPos + startOffset, transform.rotation);
            FireScript = bulletInstance.GetComponent<EnemyBulletFire>();
            FireScript.startingVelocity.x = FinalVelocity.x;
            FireScript.startingVelocity.y = FinalVelocity.y;
        }

        /// <summary>
        ///     Fires a singular bullet at a velocity with keyframes for changing velocity.
        /// </summary>
        /// <param name="vel">Vector3D representing velocity.</param>
        /// <param name="startOffset">Offset from GameObject's position</param>
        /// <param name="timeline">Times that the bullet will change speed.</param>
        /// <param name="speeds">Velocities that the bullet will change to.</param>
        public void SingleFire(Vector3 vel, Vector3 startOffset, float[] timeline, float[] speeds)
        {
            GameObject bulletInstance;
            var position = transform.position;
            FinalVelocity = vel;
            var startPos = new Vector3(position.x, position.y, position.z);
            bulletInstance = Instantiate(enemyBulletPrefab, startPos + startOffset, transform.rotation);
            FireScript = bulletInstance.GetComponent<EnemyBulletFire>();
            FireScript.timeline = timeline;
            FireScript.speeds = speeds;
            FireScript.accelerates = true;
            FireScript.startingVelocity.x = FinalVelocity.x;
            FireScript.startingVelocity.y = FinalVelocity.y;
        }

        /// <summary>
        ///     Fires a singular bullet at a velocity with keyframes for changing velocity.
        /// </summary>
        /// <param name="vel">Vector3D representing velocity.</param>
        /// <param name="timeline">Times that the bullet will change speed.</param>
        /// <param name="speeds">Velocities that the bullet will change to.</param>
        public void SingleFire(Vector3 vel, float[] timeline, float[] speeds)
        {
            GameObject bulletInstance;
            var position = transform.position;
            FinalVelocity = vel;
            var startPos = new Vector3(position.x, position.y, position.z);
            bulletInstance = Instantiate(enemyBulletPrefab, startPos, transform.rotation);
            FireScript = bulletInstance.GetComponent<EnemyBulletFire>();
            FireScript.timeline = timeline;
            FireScript.speeds = speeds;
            FireScript.accelerates = true;
            FireScript.startingVelocity.x = FinalVelocity.x;
            FireScript.startingVelocity.y = FinalVelocity.y;
        }

        /// <summary>
        ///     Fires a singular bullet at a velocity.
        /// </summary>
        /// <param name="vel">Vector3D representing velocity.</param>
        public void SingleFire(Vector3 vel)
        {
            GameObject bulletInstance;
            var position = transform.position;
            FinalVelocity = vel;
            var startPos = new Vector3(position.x, position.y, position.z);
            bulletInstance = Instantiate(enemyBulletPrefab, startPos, transform.rotation);
            FireScript = bulletInstance.GetComponent<EnemyBulletFire>();
            FireScript.startingVelocity.x = FinalVelocity.x;
            FireScript.startingVelocity.y = FinalVelocity.y;
        }
        
        /// <summary>
        ///     Begins the enemy pattern to be repeated.
        /// </summary>
        public virtual void Begin()
        {
            InvokeRepeating("Fire", delay, rateOfFire);
            Invoking = true;
        }
        
        /// <summary>
        ///     Ends the enemy pattern.
        /// </summary>
        public virtual void End()
        {
            if (Invoking)
            {
                CancelInvoke("Fire");
                Invoking = false;
            }
        }
        
        /// <summary>
        ///     Abstract method to be called by Begin() and End() or somewhere else within child object.
        /// </summary>
        public abstract void Fire();
    }
}