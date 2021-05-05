using UnityEngine;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>Pause</c> deals with pausing the game, and opening the pause menu when needed.
    /// </summary>
    public class Pause : MonoBehaviour
    {
        public bool isPaused;

        public GameObject pauseUI;

        private void Update()
        {
            if (Input.GetButtonDown("pause") && !isPaused)
                PauseGame();
            else if (Input.GetButtonDown("pause") && isPaused) Resume();
        }

        public void PauseGame()
        {
            pauseUI.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            pauseUI.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}