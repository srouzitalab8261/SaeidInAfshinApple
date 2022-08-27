using System.Collections;

namespace MyCollections
{
    public class OddNumbers2:IEnumerable,IEnumerator
    {
        private int _limit;
        private int i = -1;
        public OddNumbers2(int limit) { _limit = limit; }
        public object Current { get { return i; } }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (i < _limit)
            {
                i += 2;
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            i = -1;
        }
    }
}
