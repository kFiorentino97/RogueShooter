using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>NewGameAction</c> controls the action that occurs when the new game menu option is selected.
    /// </summary>
    public class NewGameAction : MonoBehaviour
    {
        public float transitionTime;    // The amount of seconds it takes to transition to new scene.
        public Color endColor;          // The color at the end of the transition.
        private bool _beginTransition;  // Flag when the transition is supposed to begin.
        private Color _currentColor;
        private Image _fade;            // texture which overlays screen to give fade to black effect.
        private float _timer;

        /// <summary>
        ///     Gets components, deactivates _fade and sets variables.
        /// </summary>
        private void Start()
        {
            _fade = GameObject.Find("Fade").GetComponent<Image>();
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0);
            _currentColor = _fade.color;
            _fade.gameObject.SetActive(false);
        }

        /// <summary>
        ///     Handles fade to black transition effect.
        /// </summary>
        private void Update()
        {
            if (!_beginTransition)
                return;
        
            _currentColor += endColor * (Time.deltaTime / transitionTime);
            _currentColor = _currentColor.a > endColor.a ? endColor : _currentColor;
            _timer += Time.deltaTime;
            if (_timer >= transitionTime)
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

            _fade.color = _currentColor;
        }

        /// <summary>
        ///     Begins the transition when mouse is released from button.
        /// </summary>
        public void OnMouseUp()
        {
            if (!_beginTransition)
            {
                _timer = 0;
                _fade.gameObject.SetActive(true);
            }

            _beginTransition = true;
        }
    }
}