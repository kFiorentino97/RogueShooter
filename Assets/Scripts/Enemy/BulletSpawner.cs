using UnityEngine;

namespace Enemy
{
    
    /// <summary>
    ///     <c>BulletSpawner</c> is an bullet object that can spawn bullets itself based on an <c>EnemyPattern</c>
    /// </summary>
    public class BulletSpawner : MonoBehaviour
    {
        public EnemyPattern spawnedPattern;
        public float waitTime, repeatTime;      // Time before bullets are spawned.
        public bool repeats;                    // whether or not bullets are spawned repeatedly 
        private bool _hasShot;
        private float _timer;

        private void Start()
        {
            _timer = waitTime;
            if (_timer <= 0.1f) _timer = 0.1f;
            spawnedPattern = gameObject.GetComponent<EnemyPattern>();
        }

        private void Update()
        {
            if (_hasShot && !repeats)
                return;
            _timer -= Time.deltaTime;
            if (_timer <= 0 && spawnedPattern.target != null)
            {
                spawnedPattern.Fire();
                _timer = repeatTime;
                if (_timer <= 0.1f) _timer = 0.1f;
                _hasShot = true;
            }
        }
    }
}