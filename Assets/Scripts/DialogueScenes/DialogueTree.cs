namespace DialogueScenes
{
    /// <summary>
    ///     Tree list class of cutscene messages.
    /// </summary>
    public class DialogueTree
    {
        public Message head, tail, curr;
        public int size;                // Number of elements in list

        /// <summary>
        ///     Adds message element to list
        /// </summary>
        /// <param name="msg">The Message object to be added to the list.</param>
        /// <param name="choiceCount">The number of choices the player will have in the scene. One means the scene will
        /// proceed without prompting player for a choice.</param>
        public void Add(Message msg, int choiceCount = 1)
        {
            msg.cMessage = new Message[choiceCount];
            if (size == 0)
            {
                head = msg;
                tail = msg;
                curr = msg;
            }

            else if (size == 1)
            {
                tail = msg;
                head.cMessage[0] = tail;
                tail.pMessage = head;
            }
            else
            {
                tail.cMessage[0] = msg;
                msg.pMessage = tail;
                tail = msg;
            }

            size++;
        }

        /// <summary>
        ///     Adds an option to a Message element in the list.
        /// </summary>
        /// <param name="msg">The Message object to be added to the list as an option.</param>
        /// <param name="pos">The position the option will be added to</param>
        public void AddOption(Message msg, int pos)
        {
            tail.isChoiceTop = true;
            tail.pMessage.cMessage[pos] = msg;
        }

        /// <summary>
        ///     Gets next element in the list.
        /// </summary>
        /// <param name="choice">The choice at the current message.</param>
        /// <returns></returns>
        public Message GetNext(int choice = 0)
        {
            if (curr == tail || size == 0)
            {
                return null;
            }

            curr = curr.cMessage[choice];
            return curr;
        }

        /// <summary>
        ///     Gets previous message.
        /// </summary>
        /// <returns>The parent message of the current element.</returns>
        public Message GetPrev()
        {
            if (curr == head || size == 0)
            {
                return null;
            }

            curr = curr.pMessage;
            return curr;
        }
        
        /// <summary>
        ///     Resets current message in the dialogue tree to the top.
        /// </summary>
        /// <returns>Head of tree.</returns>
        public Message Reset()
        {
            curr = head;
            return curr;
        }
    }
}