namespace DialogueScenes
{
    /// <summary>
    ///     <c>Message</c> is the linked list element for a cutscene containing the dialogue and sprite info.
    /// </summary>
    public class Message
    {
        public enum DialogueSprite
        {
            Cherry,
            CherryMad,
            NegativeCherry,
            NegativeCherryMad
        }

        public string body, lName, rName;
        public Message[] cMessage; // Child Message, it is an array in case of branching dialogue.
        public Message pMessage; // parent message
        
        public bool isChoiceTop = false; // IMPORTANT NOTE: this will be true when the [0] position member is part of a
        // choice! not when one of the choices are.

        public bool leftSpeaking;
        public DialogueSprite lSprite, rSprite;

        public Message(string body, string lName, string rName, DialogueSprite lSprite, DialogueSprite rSprite,
            bool leftSpeaking = true)
        {
            this.body = body;
            this.lName = lName;
            this.rName = rName;
            this.lSprite = lSprite;
            this.rSprite = rSprite;
            this.leftSpeaking = leftSpeaking;
        }


        public override string ToString()
        {
            return base.ToString() + "[" + lName + "]: " + body + "\n" + "[" + rName + "]";
        }
    }
}