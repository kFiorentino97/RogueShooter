namespace DialogueScenes
{
    public class SceneTest : Cutscene
    {
        public override DialogueTree dS { get; protected set; }

        private void Start()
        {
            dS = new DialogueTree();
            dS.Add(new Message("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                "Cherry", "",
                Message.DialogueSprite.Cherry, Message.DialogueSprite.NegativeCherry));
            dS.Add(new Message(
                "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation", "",
                "Negative Cherry",
                Message.DialogueSprite.Cherry, Message.DialogueSprite.NegativeCherryMad, false));
            dS.Add(new Message("ullamco laboris nisi ut aliquip ex ea commodo consequat.", "Cherry", "",
                Message.DialogueSprite.CherryMad, Message.DialogueSprite.NegativeCherry));
            dS.Add(new Message("Duis aute irure dolor in reprehenderit in voluptate velit esse cillum", "",
                "Negative Cherry",
                Message.DialogueSprite.Cherry, Message.DialogueSprite.NegativeCherry, false));
            dS.Add(new Message("dolore eu fugiat nulla pariatur.", "Cherry", "",
                Message.DialogueSprite.CherryMad, Message.DialogueSprite.NegativeCherry));
            dS.Add(new Message(
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                "", "Negative Cherry",
                Message.DialogueSprite.Cherry, Message.DialogueSprite.NegativeCherryMad, false));
        }
    }
}