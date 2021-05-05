using Misc._Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace EventSystem
{
    /// <summary>
    ///     <c>LevelTransition</c> is a class that allows for a fade to black and fade from black animation.
    /// </summary>
    public class LevelTransition : MonoBehaviour
    {
        public float transitionTime;
        public bool Finished { private set; get; } // Whether the transition has reached a finished state or not
        public float holdTime;                      // How long the game waits before fading out
        public string nextBackground;
        
        private float _timer;
        private Image _fade;
        private bool _fadingIn;
        private bool _fading, _faded;
        private Color _endColor, _startColor, _currentColor;
        private BackgroundManager _bm; // _bmC is the companion background of _bm

        /// <summary>
        ///     Begins fading in
        /// </summary>
        public void FadeIn()
        {
            _fading = true;
            _fadingIn = true;
            Finished = false;
            _timer = 0;
            _faded = false;
        }

        /// <summary>
        ///     Begins fading out
        /// </summary>
        public void FadeOut()
        {
            _fading = true;
            _fadingIn = false;
            Finished = false;
            _faded = true;
            _timer = 0;
            _bm.SetBackground(nextBackground);
            Debug.Log(nextBackground);
        }
        
        private void Update()
        {
            if (Finished)
            {
                _timer += Time.deltaTime;       // Begins countdown for hold
                if (_timer >= Time.deltaTime && !_faded)
                {
                    FadeOut();
                }
            }
            if (!_fading || Finished)
                return;
            
            _currentColor = _fadingIn ? _currentColor + _endColor * (Time.deltaTime / transitionTime) :
                _currentColor - _endColor * (Time.deltaTime / transitionTime); // calculation for changing alpha
            _currentColor = _currentColor.a >= _endColor.a && _fadingIn ? _endColor : _currentColor;
            _currentColor = _currentColor.a <= _startColor.a && !_fadingIn ? _startColor : _currentColor;
            _fade.color = _currentColor;
            if (_currentColor.a <= _startColor.a && !_fadingIn || _currentColor.a >= _endColor.a && _fadingIn)
            {
                _fading = false;
                Finished = true;
            }
        }

        /// <summary>
        ///     Gets components and sets colors
        /// </summary>
        private void Start()
        {
            _endColor = new Color(0f, 0f, 0f, 1f);
            _fade = gameObject.GetComponent<Image>();
            _fade.color = new Color(0f, 0f, 0f, 0f);
            _currentColor = _fade.color;
            _startColor = _fade.color;
            Finished = true;
            _bm = GameObject.FindGameObjectWithTag("bgManager").GetComponent<BackgroundManager>();
            nextBackground = _bm.bg;
        }
    }
}
