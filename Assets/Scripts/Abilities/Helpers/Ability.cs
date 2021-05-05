using UnityEngine;

namespace Abilities.Helpers
{
    /// <summary>
    ///     <c>Ability</c> is the model class for player abilities.
    /// </summary>
    public class Ability : MonoBehaviour
    {
        public float cooldown; // The initial cooldown
        public float cooldownTimer { get; protected set; } // The amount of time left before cooldown period is over
    

        /// <summary>
        ///     The function called by <c>FireTrigger</c> when player presses the corresponding firing button
        /// </summary>
        public virtual void Begin()
        {
        }

        /// <summary>
        ///     The function called by <c>FireTrigger</c> when player releases the corresponding firing button.
        /// </summary>
        public virtual void End()
        {
        }
    }
}