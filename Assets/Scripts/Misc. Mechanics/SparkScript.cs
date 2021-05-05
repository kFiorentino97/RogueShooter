using UnityEngine;
using static Namespaces.Helpers;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>SparkScript</c> deals with the movement of spark particles and effects. sparks have a random speed and
    ///     angle.
    /// </summary>
    public class SparkScript : MonoBehaviour
    {
        public float minSpeed;
        public float maxSpeed;
        public float lifespan;
        public float maxSpread;
        public float minSpread;
        public bool spreadSet;      // Flag for whether or not the spread has been set.
        
        private bool _velocitySet;  // Flag for whether or not the velocity has been set.
        private Vector2 _velocity;

        /// <summary>
        ///     Sets the velocity of the spark and when that's done, move it.
        /// </summary>
        private void Update()
        {
            if (!spreadSet)
            {
                return;
            }

            if (spreadSet && !_velocitySet)
            {
                _velocitySet = true;
                _velocity = AngleToVec(Random.Range(minSpread, maxSpread), Random.Range(minSpeed, maxSpeed));
            }

            transform.Translate(Vector2.up * (_velocity.y * Time.deltaTime));
            transform.Translate(Vector2.right * (_velocity.x * Time.deltaTime));
            lifespan -= Time.deltaTime;
            if (lifespan <= 0.0f) Destroy(gameObject);
        }
    
    }
}