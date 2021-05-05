using System;
using Player;
using UnityEngine;

namespace Abilities.Helpers
{
    /// <summary>
    ///     Script on the sword object to handle swinging, collision tracking and fade animation.
    /// </summary>
    public class SwordScript : MonoBehaviour
    {
        public float beginAngle, endAngle;  // beginning and ending angle of the sword's swing.
        public float speed;
        public float startUp, repeatDelay;  // Delay before sword swings back..
        public bool finished;               // Flags whether the animation has finished.
        public float fadeTime;              // Time it takes for the sword to fade away.
        public Color endColor;              // Color used to lower sword alpha.

        private float _currentAngle;
        private int _direction = 1;
        private float _directionChangeTimer = 0;    // Time the sword stops after swinging one way.
        private bool _fading;                       // Flags when sword finished swing animation and is playing fade
                                                    // animation
        private float _fadeTimer;
        private Color _currentColor;
        private SpriteRenderer _sordGraphic;
        
        private Damage _dmg;                        // Used to disable damage on sword when fading.
        private float _initialDamage;

        /// <summary>
        ///     Initializes variables and grabs components.
        /// </summary>
        private void Start()
        {
            _currentAngle = beginAngle;
            transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            _sordGraphic = GetComponent<SpriteRenderer>();
            _dmg = GetComponent<Damage>();
            _initialDamage = _dmg.dmg;
            _currentColor = _sordGraphic.color;
        }
        
        /// <summary>
        ///     Handles timer, swing animation, and calling Fade() animation.
        /// </summary>
        public void Update()
        {
            if (_fading)
            {
                Fade();
                return;
            }
            if(_directionChangeTimer < 0)
                _currentAngle += (speed * _direction) * Time.deltaTime;
            
            _directionChangeTimer -= Time.deltaTime;
            if (_direction == 1 && _currentAngle > endAngle)
            {
                _directionChangeTimer = repeatDelay;
                _direction *= -1;
            }
            transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            if (_direction == -1 && _currentAngle < beginAngle)
            {
                Debug.Log(_currentColor);
                finished = true;
                _fading = true;
            }
        }

        public void OnTriggerEnter2D(Collider2D Col)
        {
            if(_fading)
                return;
            if (Col.gameObject.CompareTag("EnemyBullet"))
            {
                Destroy(Col.gameObject);
            }
        }

        /// <summary>
        ///     Resets variables.
        /// </summary>
        public void Finish()
        {
            _direction = 1;
            _currentAngle = beginAngle;
            transform.rotation = Quaternion.Euler(0, 0, _currentAngle);
            finished = false;
            _fading = false;
            _sordGraphic.color = new Color(1, 1, 1, 1);
            _currentColor = _sordGraphic.color;
            _dmg.dmg = _initialDamage;
            _fadeTimer = 0;
            gameObject.SetActive(false);
        }
        
        /// <summary>
        ///     Lowers sword's alpha.
        /// </summary>
        private void Fade()
        {
            _dmg.dmg = 0;
            _currentColor += (endColor - _currentColor) * (Time.deltaTime / fadeTime);
            _currentColor = _currentColor.a < endColor.a ? endColor : _currentColor;
            _fadeTimer += Time.deltaTime;
            if (_fadeTimer >= fadeTime)
            {
                Finish();
            }

            _sordGraphic.color = _currentColor;
        }
    }
}