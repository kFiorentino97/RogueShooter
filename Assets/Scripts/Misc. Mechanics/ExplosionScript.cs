using UnityEngine;
using static Namespaces.Helpers;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>ExplosionScript</c> deals with the movement of explosion sparks. each spark has a random speed, lifespan
    ///     and spread. The spark slows down over time.
    /// </summary>
    public class ExplosionScript : MonoBehaviour
    {
        public float minSpeed;
        public float maxSpeed;
        public float minLifespan, maxLifespan;
        public float maxSpread;
        public float minSpread;
        public float minDecelleration, maxDecelleration;
        private float _lifespan;
        private Vector2 _velocity, _decelleration, _initDirection;

        /// <summary>
        ///     Calculates and randomly chooses velocity and angle.
        /// </summary>
        private void Awake()
        {
            var angle = Random.Range(minSpread, maxSpread);
            var speed = Random.Range(minSpeed, maxSpeed);
            var decel = Random.Range(minDecelleration, maxDecelleration);
            _lifespan = Random.Range(minLifespan, maxLifespan);
            _velocity = AngleToVec(angle, speed);
            _decelleration = AngleToVec(angle, decel);
            _initDirection.x = _velocity.x > 0f ? 1f : -1f;
            _initDirection.y = _velocity.y > 0f ? 1f : -1f;
        }

        /// <summary>
        ///     Moves spark and decreases speed, once slow enough, spark is destroyed.
        /// </summary>
        private void Update()
        {
            transform.Translate(Vector2.up * (_velocity.y * Time.deltaTime));
            transform.Translate(Vector2.right * (_velocity.x * Time.deltaTime));
            _lifespan -= Time.deltaTime;
            _velocity.x -= _decelleration.x * Time.deltaTime;
            _velocity.y -= _decelleration.y * Time.deltaTime;
            if (_lifespan <= 0.0f || _initDirection.x == 1f && _velocity.x < 0 || _initDirection.x == -1f && _velocity.x > 0)
                Destroy(gameObject);
        }

    }
}