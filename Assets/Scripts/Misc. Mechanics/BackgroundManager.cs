using UnityEngine;
using System;
using UnityEditor;

namespace Misc._Mechanics
{
    /// <summary>
    ///     Manages the backgrounds during level transitions.
    /// </summary>
    public class BackgroundManager : MonoBehaviour
    {
        public Background[] backgrounds;
        public string bg;

        private SpriteRenderer _sr, _srC;
        private int _bg = -1;
        /// <summary>
        ///     Gets components and sets initial background.
        /// </summary>
        private void Awake()
        {
            _bg = Array.FindIndex(backgrounds, background => background.id == bg);
            if (_bg == -1) return;
            _sr = gameObject.transform.Find("Background").gameObject.GetComponent<SpriteRenderer>();
            _srC = gameObject.transform.Find("BackgroundCompanion").gameObject.GetComponent<SpriteRenderer>();
            _sr.sprite = backgrounds[_bg].background;
            _srC.sprite = backgrounds[_bg].background;
        }

        /// <summary>
        ///     Sets background to one matching <p>id</p>
        /// </summary>
        /// <param name="id">id of the background.</param>
        public void SetBackground(string id)
        {
            var b = Array.FindIndex(backgrounds, background => background.id == id);
            if (b == -1 || _bg == b || backgrounds[b] == null) return;
            _sr.sprite = backgrounds[b].background;
            _srC.sprite = backgrounds[b].background;
            _bg = b;
        }
        
        
    }
}