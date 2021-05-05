using Abilities;
using Abilities.Helpers;
using Misc._Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    ///     <c>FireTrigger</c> takes player input and triggers appropriate player attack actions.
    /// </summary>
    public class FireTrigger : MonoBehaviour
    {
        public GameObject guiManager;                       // Object that holds Pause script
        public Sprite firing, idle;
        public Ability shooty, altShooty1, altShooty2;      // Ability holders for main, alt1 and alt2 shots.
        public bool altShooty1Enabled, altShooty2Enabled;   // Flags for which abilities are enabled.
        public bool shootyOn, altShoot1On, altShoot2On;
        public BasicShot basicShot;
        private Lives _life;                                // Flag for checks necessary to see if player is alive.
        private Text _mainCd, _alt1Cd, _alt2Cd;             // Cooldown for abilities.
        private Pause _pause;                               // Necessary for checking if game is paused.
        private SpriteRenderer _s;

        /// <summary>
        ///     Gets components.
        /// </summary>
        private void Awake()
        {
            _alt1Cd = GameObject.FindGameObjectWithTag("Alt1Cd").GetComponent<Text>();
            _alt2Cd = GameObject.FindGameObjectWithTag("Alt2Cd").GetComponent<Text>();
            _pause = guiManager.GetComponent<Pause>();
            _s = GetComponent<SpriteRenderer>();
            shooty = GetComponent<Ability>();
            altShooty1 = GetComponent<LaserBlast>(); 
            altShooty2 = GetComponent<SlashAbility>();
            _life = GetComponent<Lives>();
        }

        /// <summary>
        ///     Gets player input and applies appropriate actions.
        /// </summary>
        private void Update()
        {
            _alt1Cd.text = string.Format("{0:0.0}/{1:0.0}", altShooty1.cooldownTimer, altShooty1.cooldown);
            _alt2Cd.text = string.Format("{0:0.0}/{1:0.0}", altShooty2.cooldownTimer, altShooty2.cooldown);
            if (_pause.isPaused) // Checks if game is paused before firing
                return;
            if (Input.GetButtonDown("fire") && !_life.IsDown)
            {
                shooty.Begin();
                _s.sprite = firing;
                shootyOn = true;
            }

            if (Input.GetButtonDown("altFire1") && !_life.IsDown)
            {
                altShooty1.Begin();
                _s.sprite = firing;
            }

            if (Input.GetButtonDown("altFire2") && !_life.IsDown)
            {
                altShooty2.Begin();
                _s.sprite = firing;
            }

            if (!Input.GetButton("fire") || _life.IsDown)
            {
                shooty.End();
                _s.sprite = idle;
            }
            else if (altShooty1Enabled)
            {
                if (!Input.GetButton("altFire1"))
                {
                    altShooty1.End();
                    _s.sprite = idle;
                }
            }
            else if (altShooty2Enabled)
            {
                if (!Input.GetButtonDown("altFire2"))
                {
                    altShooty2.End();
                    _s.sprite = idle;
                }
            }
        }

        /// <summary>
        ///     Changes main ability to nextAbility
        /// </summary>
        /// <param name="nextAbility">The ability to be changed to.</param>
        public void ChangeAbility(Ability nextAbility)
        {
            shooty.End();
            shooty = nextAbility;
            if (Input.GetButton("fire")) shooty.Begin();
        }

        /// <summary>
        ///     Disables other abilities that aren't currAbility. Note: does not change sprite. NOT YET IMPLEMENTED.
        /// </summary>
        /// <param name="currAbility">currAbility: the ability to be ignored when disabling.</param>
        private void DisableOthers(int currAbility)
        {
            // TODO: Finish DisableOthers.
        }
    }
}