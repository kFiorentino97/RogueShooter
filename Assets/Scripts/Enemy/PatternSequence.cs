using UnityEngine;

namespace Enemy
{
    /// <summary>
    ///     <c>PatternSequence</c> is the sequence of multiple patterns for the enemy to follow. Needed even if with one
    ///     path.
    /// </summary>
    public class PatternSequence : MonoBehaviour
    {
        
        /// <summary>
        ///     What causes the pattern to change.
        /// </summary>
        public enum ChangeType
        {
            Time,
            Damage,
            None
        }
        
        public float[] times;
        public int[] damages;
        public ChangeType changeType;
        private bool _change = true;    // Marks whether the pattern is changing.
        private int _currPattern;       // Index of the current pattern.
        private EnemyHealth _health;
        private float _nextChangeTime, _nextChangeDamage;

        private EnemyPattern[] _patterns;
        private bool _started;

        /// <summary>
        ///     Sets variables based on changeType and gets components.
        /// </summary>
        private void Start()
        {
            _patterns = gameObject.GetComponents<EnemyPattern>();
            _health = gameObject.GetComponent<EnemyHealth>();
            switch (changeType)
            {
                case ChangeType.Time:
                    _nextChangeTime = times[_currPattern];
                    break;
                case ChangeType.Damage:
                    _nextChangeDamage = _health.health - damages[_currPattern];
                    break;
                case ChangeType.None:
                    break;
            }
        }

        /// <summary>
        ///     Executes the shoot method and checks for pattern changes. Pattern changes based on order of scripts in
        ///     the inspector.
        /// </summary>
        private void Update()
        {
            PatternChangeCheck();
            Shoot();
        }

        private void PatternChangeCheck()
        {
            switch (changeType)
            {
                case ChangeType.Time:
                    TimeChangeCheck();
                    break;
                case ChangeType.Damage:
                    DamageChangeCheck();
                    break;
                case ChangeType.None:
                    break;
            }
        }

        /// <summary>
        ///     Runs the Begin() function when it starts, runs the End() function when it changes and goes to the next
        ///     pattern.
        /// </summary>
        private void Shoot()
        {
            if (!_started)
            {
                _patterns[_currPattern].Begin();
                _change = false;
                _started = true;
            }
            else if (_change)
            {
                _patterns[_currPattern - 1].End();
                _patterns[_currPattern].Begin();
                _change = false;
            }
        }

        /// <summary>
        ///     Checks if the conditions are met to change patterns based on damage.
        /// </summary>
        private void DamageChangeCheck()
        {
            if (_health.health <= _nextChangeDamage && _currPattern < _patterns.Length - 1 && !_change)
            {
                _currPattern += 1;
                _nextChangeDamage = _currPattern < damages.Length - 1
                    ? _nextChangeDamage - damages[_currPattern + 1]
                    : -1;
                _change = true;
            }
        }

        /// <summary>
        ///     Checks if the conditions are met to change patterns based on time.
        /// </summary>
        private void TimeChangeCheck()
        {
            if (Time.time >= _nextChangeTime && _currPattern < _patterns.Length - 1 && !_change)
            {
                _currPattern += 1;
                _nextChangeTime = _currPattern < times.Length - 1
                    ? _nextChangeTime + times[_currPattern + 1]
                    : _nextChangeTime;
                _change = true;
            }
        }
    }
}