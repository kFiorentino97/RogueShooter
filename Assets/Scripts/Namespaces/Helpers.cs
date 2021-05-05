using System;
using UnityEngine;
using Random = System.Random;

namespace Namespaces
{
    /// <summary>
    ///     <c>Helpers</c> has functions for helping compute math related issues.
    /// </summary>
    internal class Helpers
    {
        
        /// <summary>
        ///     Returns a double between two values.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="random"><c>Random</c> object.</param>
        /// <returns>Random number between min and max.</returns>
        public static double RandomDouble(double min, double max, Random random)
        {
            return random.NextDouble() * (max - min) + min;
        }
        
        /// <summary>
        ///     Returns a double between two values.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Random number between min and max.</returns>
        public static double RandomDouble(double min, double max)
        {
            var random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
        
        
        
        /// <summary>
        ///     Converts an angle to a vector using an angle and the hypotenuse.
        /// </summary>
        /// <param name="angle">Angle of the vector.</param>
        /// <param name="velocity">Hypotenuse of the vector.</param>
        /// <returns>Vector of angle and velocity.</returns>
        public static Vector3 AngleToVec(float angle, float velocity)
        {
            Vector3 calculated;
            calculated.x = velocity * Mathf.Sin(angle * Mathf.Deg2Rad);
            calculated.y = velocity * Mathf.Cos(angle * Mathf.Deg2Rad);
            calculated.z = 0;
            return calculated;
        }

        /// <summary>
        ///     Returns the angle of a vector.
        /// </summary>
        /// <param name="vec">Vector to find the angle of.</param>
        /// <returns>Angle of vector.</returns>
        public static float VectorAngle(Vector2 vec)
        {
            return Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
        }

        /// <summary>
        ///     returns the Vector's hypotenuse.
        /// </summary>
        /// <param name="vec">Vector to get the hypotenuse of.</param>
        /// <returns>Hypotenuse of vector.</returns>
        public static float VectorVelocity(Vector2 vec)
        {
            return Mathf.Sqrt(Mathf.Pow(vec.x, 2) + Mathf.Pow(vec.y, 2));
        }

        /// <summary>
        ///     Creates a vector aimed towards a location.
        /// </summary>
        /// <param name="start">Starting position of vector.</param>
        /// <param name="target">Target end position of vector.</param>
        /// <returns>Resulting vector connecting two points.</returns>
        public static Vector2 DrawLineTo(Vector2 start, Vector2 target)
        {
            Vector2 calculated;
            calculated.x = target.x - start.x;
            calculated.y = start.y - target.y;
            return calculated;
        }

        /// <summary>
        ///     Creates a vector aimed towards a location with a specified velocity.
        /// </summary>
        /// <param name="start">Starting position of vector.</param>
        /// <param name="target">Target end position of vector.</param>
        /// <param name="velocity">Target hypotenuse of vector.</param>
        /// <returns>Resulting vector connecting two points with specified velocity..</returns>
        public static Vector2 DrawLineTo(Vector2 start, Vector2 target, float velocity)
        {
            var calculated = DrawLineTo(start, target);
            // Pythagorean theorem :)
            var initVelocity = (float) Math.Sqrt(Math.Pow(calculated.x, 2) + Math.Pow(calculated.y, 2));
            var velocityRatio = velocity / initVelocity;
            calculated.x = calculated.x * velocityRatio;
            calculated.y = calculated.y * velocityRatio;
            return calculated;
        }
        
        /// <summary>
        ///     Gets angle that aims towards a location from position.
        /// </summary>
        /// <param name="start">The starting position the angle aims from.</param>
        /// <param name="target">Target end position to aim towards.</param>
        /// <returns>Resulting angle pointing from start to target.</returns>
        public static float AngleTo(Vector3 start, Vector3 target)
        {
            return VectorAngle(DrawLineTo(start, target));
        }


    }
}