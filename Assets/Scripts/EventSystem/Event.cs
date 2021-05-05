using System;
using System.Collections.Generic;
using DialogueScenes;
using Enemy;
using Misc._Mechanics;
using UnityEngine;

namespace EventSystem
{
    /// <summary>
    ///     <c>Event</c> holds the information for each event. Events are the composition of enemies within a scene.
    /// </summary>
    public class Event : MonoBehaviour
    {
        public bool hasIntro, hasOutro;
        
        public GameObject nextEvent;
        public Cutscene intro, outro;
        public string bgTrack;
        public bool playDuringCutscene;
        public bool hasTransition;
        public string nextBackground;
        
        private AudioManager _audioManager;
        private bool _started, _ended, _outroStarted;
        private int _enemyCount;

        public List<GameObject> enemies;
        private List<EnemyHealth> _enemyHealth;
        private CutsceneHandler _cutsceneHandler;
        private LevelTransition _levelTransition;
        
        private void Start()
        {
            GetComponents();
            SetupIntro();
        }

        /// <summary>
        ///     Checks for end of cutscene, then checks for enemy health and proceeds to end cutscene
        /// </summary>
        private void Update()
        {
            if (!_started && hasIntro && _cutsceneHandler.ended)
            {
                _started = true;
                gameObject.SetActive(true);
                
                Debug.Log("Hey!");
                _audioManager.Mute(false);
                _audioManager.RestartBgTrack();
            }

            EnemyHealthCheck();
            
            if (!_ended)
                return;
            SetupOutro();
            _outroStarted = true;
            if (hasTransition)
            {
                _levelTransition.FadeIn();
                _levelTransition.nextBackground = nextBackground;
            }
            StartNextEvent();
        }
        
        /// <summary>
        ///     Sets up cutscene handler for intro cutscene.
        /// </summary>
        private void SetupIntro()
        {
            if (!hasIntro)
            {
                _cutsceneHandler.DisableCutscene();
                _started = true;
                return;
            }

            if (!playDuringCutscene)
                _audioManager.Mute(true);
            _cutsceneHandler.scn = intro;
            _cutsceneHandler.hasScene = true;
            _cutsceneHandler.gameObject.SetActive(true);
            _cutsceneHandler.ended = false;
            _cutsceneHandler.started = false;
        }

        /// <summary>
        ///     Sets up cutscene handler for outro cutscene.
        /// </summary>
        private void SetupOutro()
        {
            if (hasOutro && !_outroStarted)
            {
                if (!playDuringCutscene)
                    _audioManager.Mute(true);
                _cutsceneHandler.scn = outro;
                _cutsceneHandler.gameObject.SetActive(true);
                _cutsceneHandler.ended = false;
                _cutsceneHandler.started = false;
                _outroStarted = true;
            }
        }
        
        /// <summary>
        ///     Gets components of the event.
        /// </summary>
        private void GetComponents()
        {
            _levelTransition = GameObject.Find("FadeCanvas").GetComponentInChildren<LevelTransition>();
            _cutsceneHandler = GameObject.FindGameObjectWithTag("CursceneHandler").GetComponent<CutsceneHandler>();
            _enemyHealth = new List<EnemyHealth>();
            _enemyCount = enemies.Count;
            for (var i = 0; i < enemies.Count; i++)
            {
                _enemyHealth.Add(enemies[i].GetComponent<EnemyHealth>());
                _enemyHealth[i].fromEvent = true;
            }

            _audioManager = FindObjectOfType<AudioManager>();
            _audioManager.SetBgTrack(bgTrack);
            if (playDuringCutscene || !hasIntro)
                _audioManager.Mute(false);
        }

        /// <summary>
        ///     Checks the health of each enemy, if the enemy is below 0, the enemy will be destroyed. If no enemies are
        ///     left, the event will be flagged as ended, which will cause ending cutscene to start.
        /// </summary>
        private void EnemyHealthCheck()
        {
            for (var i = 0; i < _enemyHealth.Count; i++)
            {
                if (_enemyHealth[i].ToBeDestroyed)
                {
                    _enemyCount -= 1;
                    _enemyHealth[i].Explode();
                    enemies.RemoveAt(i);
                    _enemyHealth.RemoveAt(i);
                }
            }
            
            if (_enemyCount <= 0 && !_ended)
            {
                Debug.Log("enemies defeated!");
                _ended = true;
            }
        }
        
        /// <summary>
        ///     Starts the next event if conditions are met.
        /// </summary>
        private void StartNextEvent()
        {
            if (_outroStarted && _cutsceneHandler.ended && nextEvent != null || !hasOutro && nextEvent != null)
            {
                Instantiate(nextEvent);
                Destroy(gameObject);
            }
        }
    }
}