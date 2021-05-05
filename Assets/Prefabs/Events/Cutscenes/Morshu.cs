namespace DialogueScenes
{
    public class Morshu : Cutscene
    {
        public override DialogueTree dS { get; protected set; }

        private void Start()
        {
            dS = new DialogueTree();
            dS.Add(new Message("Lamp oil? Rope? Bombs?", "Cherry", "",
                Message.DialogueSprite.Cherry, Message.DialogueSprite.NegativeCherry));
            dS.Add(new Message("You want it?", "Cherry", "", Message.DialogueSprite.Cherry,
                Message.DialogueSprite.Cherry));
            dS.Add(new Message("It's yours my friend, as long as you have enough rupees.", "Cherry", "",
                Message.DialogueSprite.Cherry,
                Message.DialogueSprite.Cherry));
        }
    }
}