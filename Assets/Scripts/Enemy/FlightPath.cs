using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    /// <summary>
    ///     <c>FlightPath</c> is the path that the enemy will follow and move with.
    /// </summary>
    public class FlightPath : MonoBehaviour
    {
        
        #region enums

        public enum MovementType
        {
            Lerp,
            Linear
        }

        public enum PathEndType
        {
            Time,
            Damage
        }


        #endregion enums

        #region public variables

        public MovementType type = MovementType.Linear;
        [FormerlySerializedAs("path")] public MovementPath[] paths;
        public float speed, reqDistance;                // reqDistance: the amount of distance needed to move to next
                                                        // part of the path
        public int direction;
        public float[] times;                           // Timeline of changes in pattern based on time.
        public int[] damages;                           // Timeline of changes in pattern based on damage.
        public PathEndType endType;                     // How pattern changes.
        public int CurrPath { get; private set; }

        #endregion public variables

        #region private

        private IEnumerator<Transform> _currPoint;
        private bool _safe = true;                          // Flag for whether pattern is safe to use or not.
        private float _nextChangeTime, _nextChangeDamage;   // Amount needed to change pattern.
        private EnemyHealth _health;
        private Vector3 _currPosition, _targetPosition;

        #endregion
        
        /// <summary>
        ///     Finds if path is valid and gets components.
        /// </summary>
        private void Start()
        {
            _health = gameObject.GetComponent<EnemyHealth>();
            FindEndType();
            
            // Finds if path is valid or not.
            if (paths[0].pathSequence.Length < 2 || paths[0].pathSequence == null)
            {
                Debug.LogError("Path is invalid", gameObject);
                _safe = false;
                return;
            }
            
            CurrPath = 0;
            _currPoint = paths[0].GetNextPathPoint();
            _currPoint.MoveNext();
            if (_currPoint.Current == null)
            {
                Debug.LogError("Path point invalid", gameObject);
                _safe = false;
                return;
            }

            transform.position = _currPoint.Current.position;

            _currPoint = paths[0].GetNextPathPoint();
            _currPoint.MoveNext();
        }

        /// <summary>
        ///     Moves enemy and checks if the path has changed.
        /// </summary>
        private void Update()
        {
            PathChangeCheck();
            Move();
        }

        /// <summary>
        ///     Moves the enemy along the path based on MovementType
        /// </summary>
        private void Move()
        {
            if (!_safe || _currPoint == null || _currPoint.Current == null) return;

            if (type == MovementType.Linear)
            {
                _currPosition = transform.position;
                _targetPosition = _currPoint.Current.position;
                transform.position = Vector3.MoveTowards(transform.position, _currPoint.Current.position,
                    Time.deltaTime * speed);
            }
            else if (type == MovementType.Lerp)
            {
                transform.position =
                    Vector3.Lerp(transform.position, _currPoint.Current.position, Time.deltaTime * speed);
            }

            var distance = (transform.position - _currPoint.Current.position).sqrMagnitude;
            if (distance < reqDistance * reqDistance) _currPoint.MoveNext();
        }

        /// <summary>
        ///     Checks whether path should be changed or not.
        /// </summary>
        private void PathChangeCheck()
        {
            switch (endType)
            {
                case PathEndType.Time:
                    if (Time.time >= _nextChangeTime && CurrPath < paths.Length - 1)
                    {
                        CurrPath += 1;
                        _currPoint = paths[CurrPath].GetNextPathPoint();
                        _currPoint.MoveNext();
                        _nextChangeTime = CurrPath < times.Length - 1
                            ? _nextChangeTime + times[CurrPath + 1]
                            : _nextChangeTime;
                    }

                    break;
                case PathEndType.Damage:
                    if (_health.health <= _nextChangeDamage && CurrPath < paths.Length - 1)
                    {
                        Debug.Log("Changing");
                        CurrPath += 1;
                        _currPoint = paths[CurrPath].GetNextPathPoint();
                        _currPoint.MoveNext();
                        _nextChangeDamage = CurrPath < damages.Length - 1
                            ? _nextChangeDamage - damages[CurrPath + 1]
                            : _nextChangeDamage;
                    }

                    break;
            }
        }
        
        private void FindEndType()
        {
            switch (endType)
            {
                case PathEndType.Time:
                    _nextChangeTime = times[CurrPath];
                    break;
                case PathEndType.Damage:
                    _nextChangeDamage = _health.health - damages[CurrPath];
                    break;
            }
        }
    }
}