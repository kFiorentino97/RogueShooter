using Misc._Mechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    /// <summary>
    ///     <c>BulletFire</c> handles the movement of player bullets, and spark animations when bullets are destroyed.
    /// </summary>
    public class BulletFire : MonoBehaviour
    {
        public Vector2 velocity;
        public bool destroy;                        // Flag for if bullet should be destroyed after time limit.
        public float lifespan = 3.0f;               // Time limit for the player bullet.
        public GameObject sparkPrefab;
        public Vector2 sparkOffset1, sparkOffset2;  // Offset for two spark locations.

        private SparkScript _sparkScript;
        private CreateSparks _createScript;

        private void Start()
        {
            _createScript = gameObject.GetComponent<CreateSparks>();
        }

        /// <summary>
        ///     Moves bullets at a specific velocity. Destroys them after time limit is up.
        /// </summary>
        private void Update()
        {
            transform.Translate(Vector2.up * (velocity.y * Time.deltaTime));
            transform.Translate(Vector2.right * (velocity.x * Time.deltaTime));
            lifespan -= Time.deltaTime;
            if (lifespan <= 0.0f && destroy) Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        /// <summary>
        ///     Checks for collision with enemies.
        /// </summary>
        /// <param name="col">Collider that collided with bullet.</param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                _createScript.Sparks();
                Destroy(gameObject);
            }
        }
    }
}