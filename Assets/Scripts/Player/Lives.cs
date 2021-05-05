using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    ///     <c>Lives</c> holds the amount of lives the player has left. Destroys player when they run out.
    /// </summary>
    public class Lives : MonoBehaviour
    {
        public int lives;
        public int explosionSparks;             // Amount of sparks created when player dies.
        public Vector2 respawnPosition;
        public GameObject explosionPrefab2;
        public float deathCooldown;             // Time before player respawns after death.
        public bool invincible;

        private float _deathTimer;              // Countdown before player respawns.
        private Text _lifeCounter;
        private SpriteRenderer _spriteRenderer;
        private readonly List<SpriteRenderer> _srHb = new List<SpriteRenderer>(); // Holds child object sprites..
        public bool IsDead { get; private set; }
        public bool IsDown { get; private set; }

        /// <summary>
        ///     Gets components and children.
        /// </summary>
        private void Start()
        {
            foreach (Transform child in transform) _srHb.Add(child.gameObject.GetComponent<SpriteRenderer>());
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _lifeCounter = GameObject.FindGameObjectWithTag("lifeCounter").GetComponent<Text>();
            _lifeCounter.text = lives.ToString();
        }

        /// <summary>
        ///     If player is dead, start the death counter and disable the sprites.
        /// </summary>
        private void Update()
        {
            if (!IsDown || IsDead)
                return;
            _deathTimer += Time.deltaTime;
            if (_deathTimer >= deathCooldown)
            {
                _deathTimer = 0;
                IsDown = false;
                _spriteRenderer.enabled = true;
                foreach (var child in _srHb) child.enabled = true;
            }
            
        }

        /// <summary>
        ///     If collided with enemy bullet, explode.
        /// </summary>
        /// <param name="col">Object that the player collided with.</param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (invincible)
                return;
            if (!(col.gameObject.CompareTag("EnemyBullet") || col.gameObject.CompareTag("Enemy")))
                return;
            if (col.GetComponent<EnemyBulletFire>().intangible)
                return;
        
            var bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (var item in bullets) Destroy(item);

            Debug.Log(respawnPosition);
            IsDown = true;
            Invoke("Explode", 0f);
            lives -= 1;
            _lifeCounter.text = lives.ToString();
            foreach (var child in _srHb) child.enabled = false;

            _spriteRenderer.enabled = false;
            if (lives < 0) IsDead = true;
        }

        private void Explode()
        {
            for (var i = 0; i < explosionSparks; i++) Invoke("Spark2", 0f);
        }

        private void Spark2()
        {
            GameObject explosionInstance;
            var startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            explosionInstance = Instantiate(explosionPrefab2, startPos, transform.rotation);
        }
    }
}