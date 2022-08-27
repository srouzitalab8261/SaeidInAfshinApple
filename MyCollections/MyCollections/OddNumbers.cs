using System.Collections;

namespace MyCollections
{
    public class MyEnumerator : IEnumerator{
        public MyEnumerator(int limit) {
            _limit = limit;
        }
        private int _limit;
        private int i = -1;
        public object Current { get { return i; } }
        public bool MoveNext()
        {
            if (i < _limit)
            {
                i+=2;
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
    public class OddNumbers : IEnumerable
    {
        MyEnumerator myEnumerator;
        public OddNumbers(int limit) {
            myEnumerator = new MyEnumerator(limit);

        }
        public IEnumerator GetEnumerator()
        {
            return myEnumerator;
        }

        
    }
}
