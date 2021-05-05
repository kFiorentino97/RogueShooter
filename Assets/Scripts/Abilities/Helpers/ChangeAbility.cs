using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities.Helpers
{
    /// <summary>
    ///     <c>ChangeAbility</c> is the script attached to the player GameObject that allows for swapping abilities.
    /// </summary>
    public class ChangeAbility : MonoBehaviour
    {
    
        // The variables below are the scrips for each ability.
        public FireTrigger fireTrigger;
        public BasicShot basicShot;
        public SpiralShot spiralShot;
    
        // References to pause screen buttons, may be deleted later.
        public Button basicButt, spinnyButt;

        /// <summary>
        ///     Grabs Scripts and components.
        /// </summary>
        private void Awake()
        {
            fireTrigger = GetComponent<FireTrigger>();
            spiralShot = GetComponent<SpiralShot>();
            basicShot = GetComponent<BasicShot>();
            basicButt.onClick.AddListener(ToBasic);
            spinnyButt.onClick.AddListener(ToSpinny);
        }

        /// <summary>
        ///     Changes to basic firing type.
        /// </summary>
        public void ToBasic()
        {
            fireTrigger.ChangeAbility(basicShot);
        }

        /// <summary>
        ///     Changes to spinny firing type.
        /// </summary>
        public void ToSpinny()
        {
            fireTrigger.ChangeAbility(spiralShot);
        }
    }
}