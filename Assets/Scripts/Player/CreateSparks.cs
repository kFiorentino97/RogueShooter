using Misc._Mechanics;
using UnityEngine;

namespace Player
{
    /// <summary>
    ///     <c>CreateSparks</c> creates sparks at a set rate.
    /// </summary>
    public class CreateSparks : MonoBehaviour
    {
        public GameObject sparkPrefab;
        public Vector2 sparkOffset1, sparkOffset2;
        public float sparkRate, minSpread, maxSpread;
        public bool beginSparking;  // If this is true, once the spark script is created it will start out sparking.

        private void Start()
        {
            if (beginSparking)
                Begin();
        }

        private void OnEnable()
        {
            if (beginSparking)
                Begin();
        }

        private void OnDisable()
        {
            if (beginSparking)
                End();
        }

        private void OnDestroy()
        {
            if (beginSparking)
                End();
        }

        /// <summary>
        ///     Begins the sparking process until End() is called.
        /// </summary>
        public void Begin()
        {
            InvokeRepeating("Sparks", 0f, sparkRate);
        }
        
        /// <summary>
        ///     Ends the sparking process.
        /// </summary>
        public void End()
        {
            CancelInvoke("Sparks");
        }

        /// <summary>
        ///     Creates two sparks at the specified location.
        /// </summary>
        public void Sparks()
        {
            SparkScript sparkScript;
            GameObject sparkInstance;

            var startPos = new Vector3(transform.position.x + sparkOffset1.x, transform.position.y + sparkOffset1.y,
                transform.position.z);
            sparkInstance = Instantiate(sparkPrefab, startPos, transform.rotation);
            sparkScript = sparkInstance.GetComponent<SparkScript>();
            sparkScript.minSpread = minSpread;
            sparkScript.maxSpread = maxSpread;
            sparkScript.spreadSet = true;
            GameObject sparkInstance2;
            var startPos2 = new Vector3(transform.position.x + sparkOffset2.x, transform.position.y + sparkOffset2.y,
                transform.position.z);
            sparkInstance2 = Instantiate(sparkPrefab, startPos2, transform.rotation);
            sparkScript = sparkInstance2.GetComponent<SparkScript>();
            sparkScript.minSpread = minSpread;
            sparkScript.maxSpread = maxSpread;
            sparkScript.spreadSet = true;
        }
    }
}