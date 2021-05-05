using System.Net.Sockets;
using UnityEngine;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>BackgroundScroll</c> handles the animation of the background image's scroll by moving two images down.
    ///     When one of the images become invisible, it's moved to the top of the companion image.
    /// </summary>
    public class BackgroundScroll : MonoBehaviour
    {
        public float scrollSpeed = 1f;
        public bool isTop;                  // Flag for whether the image is the top at the start one or not. Top one is
                                            // moved above the other one at the start.
        public GameObject companionBackground;
        
        private BoxCollider2D _col;
        private Vector3 _companionPos;
        private Transform _companionTransform;
        private Vector3 _nextPosition;
        
        /// <summary>
        ///     Gets components and sets position of top image.
        /// </summary>
        private void Awake()
        {
            _col = gameObject.GetComponent<Collider2D>() as BoxCollider2D;
            _companionTransform = companionBackground.GetComponent<Transform>();
            _companionPos = _companionTransform.position;
            if (_col == null) return;
            if (isTop)
                transform.position = new Vector3(_companionPos.x, _companionPos.y + _col.size.y * 
                    gameObject.transform.localScale.y - 0.01f, _companionPos.z);
        }

        private void Update()
        {
            transform.Translate(-Vector2.up * (scrollSpeed * Time.deltaTime));
        }

        /// <summary>
        ///     When the object becomes invisible, it moves to the top of the companionBackground.
        /// </summary>
        private void OnBecameInvisible()
        {
            _companionPos = _companionTransform.position;
            transform.position = new Vector3(_companionPos.x,
                _companionPos.y + _col.size.y * gameObject.transform.localScale.y - 0.01f, _companionPos.z);
        }
    }
}