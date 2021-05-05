using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    ///     Displays the movement path within the Unity editor.
    /// </summary>
    public class MovementPath : MonoBehaviour
    {
        public enum PathTypes
        {
            Loop,
            Linear,     // Enemy goes back and forth repeatedly.
            End
        }

        public PathTypes pathType;

        public int movementDirection = 1;
        public int nextPoint;
        public Transform[] pathSequence;


        public bool Ended { get; private set; }


        public void Reset()
        {
            nextPoint = 0;
        }

        private void Start()
        {
            Ended = false;
        }

        /// <summary>
        ///     Draws lines in unity editor to represent paths.
        /// </summary>
        public void OnDrawGizmos()
        {
            if (pathSequence == null || pathSequence.Length < 2) return;

            for (var i = 1; i < pathSequence.Length; i++)
                Gizmos.DrawLine(pathSequence[i - 1].position, pathSequence[i].position);

            if (pathType == PathTypes.Loop)
                Gizmos.DrawLine(pathSequence[0].position, pathSequence[pathSequence.Length - 1].position);
        }
        
        /// <summary>
        ///     Gets the next point in the path sequence.
        /// </summary>
        /// <returns>Next point in the path sequence.</returns>
        public IEnumerator<Transform> GetNextPathPoint()
        {
            if (pathSequence == null || pathSequence.Length < 1)
                yield break;

            while (true)
            {
                yield return pathSequence[nextPoint];
                if (pathSequence.Length == 1)
                    continue;

                if (pathType == PathTypes.Linear)
                {
                    if (nextPoint <= 0)
                        movementDirection = 1;
                    if (nextPoint >= pathSequence.Length - 1)
                        movementDirection = -1;
                }

                if (pathType != PathTypes.End || nextPoint < pathSequence.Length - 1)
                    nextPoint = nextPoint + movementDirection;
                if (pathType == PathTypes.End && nextPoint >= pathSequence.Length - 1 && !Ended) Ended = true;

                if (pathType == PathTypes.Loop)
                {
                    if (nextPoint >= pathSequence.Length)
                        nextPoint = 0;
                    if (nextPoint < 0)
                        nextPoint = pathSequence.Length - 1;
                }
            }
        }
    }
}