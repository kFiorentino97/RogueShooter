using UnityEngine;
using System;
using UnityEditor;

namespace Misc._Mechanics
{
    /// <summary>
    ///     <c>AudioManager</c> exists to help aid with playing sounds, especially background music transitions.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public string bgTrack;
        public bool startMute;

        private int _bgTrack = -1; // index of the background track
        
        // Gets components, sets and plays initial song.
        private void Awake()
        {
            foreach (var s in sounds)
            {
                s.audioSource = gameObject.AddComponent<AudioSource>();
                s.audioSource.clip = s.audioClip;
                s.audioSource.volume = s.volume;
                s.audioSource.loop = s.loop;
            }

            _bgTrack = Array.FindIndex(sounds, sound => sound.id == bgTrack);
            if (_bgTrack != -1)
                PlaySound(sounds[_bgTrack].id);
            else return;
            if (startMute)
                sounds[_bgTrack].audioSource.volume = 0f;
        }

        /// <summary>
        ///     For playing an individual sound.
        /// </summary>
        /// <param name="id">The id of the sound.</param>
        public void PlaySound(string id)
        {
            var s = Array.FindIndex(sounds, sound => sound.id == id);
            if (sounds[s] == null) return;
            sounds[s].audioSource.Play();
        }
        
        /// <summary>
        ///     For setting the background track of the game.
        /// </summary>
        /// <param name="id">The id of the track.</param>
        public void SetBgTrack(string id)
        {
            if(_bgTrack != -1)
                if (id == sounds[_bgTrack].id) return;
            var s = Array.FindIndex(sounds, sound => sound.id == id);
            if(sounds[s] == null || _bgTrack == -1) return;
            sounds[_bgTrack].audioSource.Stop();
            sounds[s].audioSource.Play();
            bgTrack = id;
            _bgTrack = s;
            Debug.Log("Switched to " + id);
        }

        /// <summary>
        ///     Toggles mute on the background track.
        /// </summary>
        public void Mute()
        {
            if(_bgTrack != -1)
                sounds[_bgTrack].ToggleMute();
        }

        /// <summary>
        ///     Mutes the track based on true or false.
        /// </summary>
        /// <param name="mute">True: mute, False: unmute.</param>
        public void Mute(bool mute)
        {
            if(_bgTrack != -1)
                sounds[_bgTrack].ToggleMute(mute);
        }
        
        /// <summary>
        ///     Resets the bg track.
        /// </summary>
        public void RestartBgTrack()
        {
            sounds[_bgTrack].audioSource.Stop();
            sounds[_bgTrack].audioSource.Play();
        }
    }
}
