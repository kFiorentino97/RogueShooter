using System;
using UnityEngine;

namespace Abilities.Helpers
{
    /// <summary>
    ///     Increases bombs size and resets it when it gets to maxScale.
    /// </summary>
    public class BombHelper : MonoBehaviour
    {
        public Vector3 scalingRate;
        public Vector3 maxScale;
        private void Update()
        {
            if (transform.localScale.x >= maxScale.x)
            {
                gameObject.SetActive(false);
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = new Vector3(300, 300, 300);
            }
            transform.localScale = transform.localScale + scalingRate * Time.timeScale;
        }
        
    }
}