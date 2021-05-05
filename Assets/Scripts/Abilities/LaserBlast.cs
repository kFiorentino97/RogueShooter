using Abilities.Helpers;
using Player;
using UnityEngine;

namespace Abilities
{
    /// <summary>
    ///     <c>LaserBlast</c> is a player ability that lets the player fire a laser that scales across the screen until
    ///     it hits an enemy type object.
    /// </summary>
    public class LaserBlast : Ability
    {
        public float lifespan;
        public Vector3 offset;
        
        public GameObject laserShaft; // Reference to the main body of the laser.
        private GameObject[] _laserParts; // References to the separate parts of the laser. Laser head, laser base, and
                                          // laser shaft.

        private Lives _lives; // The lives script of the player. Used to check if player is dead.
        
        private bool _started, _finished, _onCooldown;
        private float _timeAlive;

        /// <summary>
        /// Gets scripts and components.
        /// </summary>
        private void Start()
        {
            _lives = GetComponent<Lives>();
            laserShaft = Instantiate(laserShaft, transform.position, transform.rotation);
            SetLaserActive(false);
        }

        /// <summary>
        ///     Increases the laser's size each update period. Also handles decrementing cooldown timer and resetting.
        /// </summary>
        private void Update()
        {
            if (_lives.IsDown)
            {
                _started = false;
                _finished = true;
                _timeAlive = 0;
                SetLaserActive(false);
            }

            if (_started && !_finished)
            {
                _timeAlive += Time.deltaTime;
                laserShaft.transform.position = gameObject.transform.position + offset;
                if (_timeAlive >= lifespan)
                {
                    _started = false;
                    _finished = true;
                    _timeAlive = 0;
                    SetLaserActive(false);
                }
            }

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0)
                cooldownTimer = 0;
            if (cooldownTimer <= 0) _onCooldown = false;
        }
        
        public override void Begin()
        {
            if (!_onCooldown)
            {
                SetLaserActive(true);
                laserShaft.transform.position = gameObject.transform.position + offset;
                cooldownTimer = cooldown;
                _started = true;
                _finished = false;
                _onCooldown = true;
            }
        }

        /// <summary>
        ///     End is not used, as releasing fire will have no affect.
        /// </summary>
        public override void End()
        {
            base.End();
        }

        /// <summary>
        ///     Sets each laser object to active or inactive.
        /// </summary>
        /// <param name="active">true = All components are active, false = All components are inactive.</param>
        private void SetLaserActive(bool active)
        {
            laserShaft.gameObject.SetActive(active);
            foreach (Transform part in laserShaft.transform) part.gameObject.SetActive(active);
        }
    }
}