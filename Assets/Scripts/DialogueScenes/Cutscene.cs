using UnityEngine;

namespace DialogueScenes
{
    /// <summary>
    ///     <c>Cutscene</c> is the holder for a dialogue scene.
    /// </summary>
    public class Cutscene : MonoBehaviour
    {
        public virtual DialogueTree dS { get; protected set; }
    }
}