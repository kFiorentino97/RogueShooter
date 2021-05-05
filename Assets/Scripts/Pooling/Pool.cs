using System.Collections.Generic;

namespace Pooling
{
    public class Pool<T>
    {
        public LinkedList<T> active;
        public LinkedList<T> inactive;
    }
}