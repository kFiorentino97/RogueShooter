using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemy
{

    /// <summary>
    ///     <c>EnemyHealth</c> holds enemy health and does functions related to health (Changing health bar size,
    ///     destroying gameObject on death, and playing death animation).
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        public float health;
        public int explosionSparks;         // Amount of explosion sparks on death.
        public GameObject explosionPrefab;
        public Vector2 explosionOffset;
        public GameObject healthBar;
        public bool fromEvent;              // Mark whether enemy is from an event or not.
        private Vector3 _barScale, _fullBarScale;

        private List<Damage> _damageSources;    // List of damage sources for when enemy is taking damage from multiple
                                                // dps sources.
        private float _maxHealth;
        private bool _takingDps;
        public bool ToBeDestroyed { get; protected set; }   // Flag for when the object is to be destroyed.

        private void Start()
        {
            _damageSources = new List<Damage>();
            _maxHealth = health;
            _barScale = healthBar.transform.localScale;
            _fullBarScale = _barScale;
        }

        private void Update()
        {
            if (_takingDps)
                foreach (var source in _damageSources)
                    TakeDamage(source.dmg * Time.deltaTime);
        }

        /// <summary>
        ///     When a player hitbox enters collider, it applies damage.
        /// </summary>
        /// <param name="Col">Collider that enters object.</param>
        private void OnTriggerEnter2D(Collider2D Col)
        {
            if (Col.gameObject.CompareTag("PlayerHitbox"))
            {
                var damageScript = Col.gameObject.GetComponent<Damage>();
                if (!damageScript.dps)
                {
                    TakeDamage(damageScript.dmg);
                }
                else
                {
                    _damageSources.Add(damageScript);
                    _takingDps = true;
                }
            }
        }

        /// <summary>
        ///     When a damage source exits the enemies collider, said damage source is removed from _damageSources.
        /// </summary>
        /// <param name="Col">Collider that exits object.</param>
        private void OnTriggerExit2D(Collider2D Col)
        {
            if (Col.gameObject.CompareTag("PlayerHitbox"))
            {
                for (var i = 0; i < _damageSources.Count; i++)
                    if (Col.GetComponent<Damage>() == _damageSources[i])
                    {
                        _damageSources.RemoveAt(i);
                        i--;
                    }

                if (_damageSources.Count == 0) _takingDps = false;
            }
        }
        
        public void Explode()
        {
            for (var i = 0; i < explosionSparks; i++) Invoke("Spark", 0f);
            Destroy(gameObject);
        }

        /// <summary>
        ///     Creates one spark at objects location.
        /// </summary>
        private void Spark()
        {
            var position = transform.position;
            var startPos = new Vector3(position.x + explosionOffset.x, position.y + explosionOffset.y,
                position.z);
            Instantiate(explosionPrefab, startPos, transform.rotation);
        }

        /// <summary>
        ///     Does math for damage taken and scales health bar.
        /// </summary>
        /// <param name="damage">The amount of damage taken.</param>
        private void TakeDamage(float damage)
        {
            health -= damage;
            _barScale.x = _fullBarScale.x * (health / _maxHealth);
            healthBar.transform.localScale = _barScale;
            if (health <= 0)
            {
                if (!fromEvent)
                    Invoke("Explode", 0f);
                else
                    ToBeDestroyed = true;
            }
        }
    }
}