using Misc._Mechanics;
using UnityEngine;

namespace Player
{
    /// <summary>
    ///     <c>Movement</c> deals with player movement and movement related input.
    /// </summary>
    public class Movement : MonoBehaviour
    {
        public float velocity = 1f;
        public float slow;                      // Speed modifier for when "slow" button is pressed.
        public float rotation;                  // The amount the ship tilts.
        public float rotationSmooth = 5.0f;     // How long the ship takes to fully tilt.
        public GameObject guiManager;
        public Vector2 rightBound, leftBound;   // Screen limits.
        
        private Lives _life;
        private float _modifier;                // Speed modifier.
        private Pause _pause;
        public Vector3 _posTarget;             // Target location.
        private float _rotationMod;
        private Quaternion _target;             // Target rotation.

        /// <summary>
        ///     Gets components and sets defaults.
        /// </summary>
        private void Start()
        {
            _life = gameObject.GetComponent<Lives>();
            _posTarget = transform.position;
            Application.targetFrameRate = -1;
            _pause = guiManager.GetComponent<Pause>();
        }

        /// <summary>
        ///     Changes position and rotates player ship.
        /// </summary>
        private void Update()
        {
            if (_pause.isPaused || _life.IsDown)
            {
                _posTarget = _life.respawnPosition;
                return;
            }

            _rotationMod = 0f;
            if (Input.GetButton("slow"))
                _modifier = slow;
            else
                _modifier = 1f;
            if (Input.GetButton("up")) 
                _posTarget += new Vector3(0, velocity * _modifier * Time.deltaTime, 0);
            if (Input.GetButton("down")) 
                _posTarget += new Vector3(0, -velocity * _modifier * Time.deltaTime, 0);
            if (Input.GetButton("right"))
            {
                _posTarget += new Vector3(velocity * _modifier * Time.deltaTime, 0, 0);
                _rotationMod = rotation;
            }
            else if (Input.GetButton("left"))
            {
                _posTarget += new Vector3(-velocity * _modifier * Time.deltaTime, 0, 0);
                _rotationMod = -rotation;
            }
            else
            {
                _rotationMod = 0f;
            }

            _target = Quaternion.Euler(0, _rotationMod, 0);
            _posTarget.x = Mathf.Clamp(_posTarget.x, rightBound.x, leftBound.x);
            _posTarget.y = Mathf.Clamp(_posTarget.y, rightBound.y, leftBound.y);
            transform.position = _posTarget;
            transform.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime * rotationSmooth);
        }
    }
}