using Abilities.Helpers;
using Enemy;
using Player;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    ///     An ability for the player where it swings a laser sword back and forth, destroying enemies and projectiles.
    ///     This is the script to create the laser sword object.
    /// </summary>
    public class SlashAbility : Ability
    {
        public float lifespan;
        public Vector3 offset;
        
        public GameObject sord; // Laser sword object.
        private Lives _lives; // The lives script of the player. Used to check if player is dead.

        private SwordScript _sordScript;
        private bool _started, _finished, _onCooldown;
        private float _timeAlive;
        
        /// <summary>
        /// Gets scripts and components.
        /// </summary>
        private void Start()
        {
            _lives = GetComponent<Lives>();
            sord = Instantiate(sord, transform.position, transform.rotation);
            sord.SetActive(false);
            _sordScript = sord.GetComponent<SwordScript>();
        }
        
        /// <summary>
        ///     Moves the sword based on the player's position. Handles cooldown timer.
        /// </summary>
        private void Update()
        {
            if (_lives.IsDown)
            {
                _started = false;
                _finished = true;
                _timeAlive = 0;
                sord.SetActive(false);
            }

            if (_started && !_finished)
            {
                _timeAlive += Time.deltaTime;
                sord.transform.position = gameObject.transform.position + offset;
                if (_timeAlive >= lifespan)
                {
                    _started = false;
                    _finished = true;
                    _timeAlive = 0;
                    sord.SetActive(false);
                }
            }

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
                cooldownTimer = 0;
            if (cooldownTimer <= 0) _onCooldown = false;
        }
        
        /// <summary>
        ///     Spawns the sword.
        /// </summary>
        public override void Begin()
        {
            if (!_onCooldown)
            {
                sord.SetActive(true);
                sord.transform.position = gameObject.transform.position + offset;
                cooldownTimer = cooldown;
                _started = true;
                _finished = false;
                _onCooldown = true;
            }
        }

        public override void End()
        {
            base.End();
        }
    }
}
