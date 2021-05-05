using UnityEngine;

namespace Misc._Mechanics
{
    /// <summary>
    ///     Individual sound effects or music tracks
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        public string id;
        public AudioClip audioClip;
        [Range(0f, 1f)] public float volume;
        [HideInInspector] public AudioSource audioSource;
        public bool loop;

        /// <summary>
        ///     Sets the volume to <p>vol</p>.
        /// </summary>
        /// <param name="vol">Volume to be set to.</param>
        public void SetVolume(float vol)
        {
            volume = vol;
            audioSource.volume = volume;
        }

        /// <summary>
        ///     Toggles mute on the sound.
        /// </summary>
        public void ToggleMute()
        {
            if (audioSource.volume != 0)
                audioSource.volume = 0;
            else
                audioSource.volume = volume;
        }
        
        /// <summary>
        ///     Mutes sound based on <p>mute</p>.
        /// </summary>
        /// <param name="mute">True: mute, False: unmute.</param>
        public void ToggleMute(bool mute)
        {
            if (mute)
                audioSource.volume = 0;
            else
                audioSource.volume = volume;
        }
    }
}