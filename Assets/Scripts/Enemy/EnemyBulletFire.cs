using UnityEngine;
using static Namespaces.Helpers;

namespace Enemy
{
    
    /// <summary>
    ///     The bullet's method of finding it's velocity and changing it's position.
    /// </summary>
    public class EnemyBulletFire : MonoBehaviour
    {
        public float lifespan = 10f;
        public Vector3 startingVelocity, velocity, targetVelocity;
        public bool accelerates;
        public float[] timeline, speeds;
        public bool intangible;             // If intangible, bullet will not be destroyed on hit, and player will not
                                            // take damage.
        
        private Vector3 _accPs, _prevVel; // acceleration per second
        private int _currKf; // current keyframe
        private bool _finished;
        private Vector3[] _spdVecs; // Speed vectors
        private Vector3 _bulletPos;
        private float _timer;
        
        /// <summary>
        ///     Sets up velocity and variables related to acceleration if it accelerates.
        /// </summary>
        private void Start()
        {
            velocity = startingVelocity;
            if (!accelerates)
                return;
            _spdVecs = new Vector3[speeds.Length];
            CalculateVelocity();
            var angle = VectorAngle(startingVelocity);
            _prevVel = startingVelocity;
            for (var i = 0; i < _spdVecs.Length; i++) _spdVecs[i] = AngleToVec(angle, speeds[i]);
            _timer = timeline[_currKf];
        }

        /// <summary>
        ///     Move the bullet to the next position.
        /// </summary>
        private void Update()
        {
            _timer -= Time.deltaTime;
            lifespan -= Time.deltaTime;
            velocity = accelerates && !_finished ? _accPs : velocity;
            if (accelerates)
                CalculateVelocity();

            if (!_finished && _timer <= 0 && accelerates)
                UpdateAnimationFrame();

            if (_finished)
                velocity = _spdVecs[_currKf - 1];
            transform.Translate(Vector2.up * (velocity.y * Time.deltaTime));
            transform.Translate(Vector2.right * (velocity.x * Time.deltaTime));
            if (lifespan <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        ///     Proceeds to the next frame of the animation.
        /// </summary>
        private void UpdateAnimationFrame()
        {
            velocity = _spdVecs[_currKf];
            _timer = timeline[_currKf];
            _prevVel = _spdVecs[_currKf];
            _currKf += 1;
        }
        
        /// <summary>
        ///     Destroys game object when it becomes invisible.
        /// </summary>
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        /// <summary>
        ///     Checks for collision with player
        /// </summary>
        /// <param name="col">the object collided with the bullet (not always player.)</param>
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && !intangible) 
                Destroy(gameObject);
        }

        /// <summary>
        ///     Called when bullet is destroyed.
        /// </summary>
        public void Destroy()
        {
            Destroy(gameObject);
        }

        /// <summary>
        ///     Calculates velocity during acceleration.
        /// </summary>
        private void CalculateVelocity()
        {
            if (_currKf >= timeline.Length)
                _finished = true;
            else
                _accPs = _prevVel + (1f - _timer / timeline[_currKf]) * _spdVecs[_currKf];
        }
    }
}