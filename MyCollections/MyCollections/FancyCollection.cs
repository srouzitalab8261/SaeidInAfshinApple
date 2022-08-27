using System.Collections;
using System.Collections.Generic;

namespace MyCollections
{
    public class FancyCollection<T> : IEnumerable<T>, IEnumerator<T>  //where T:class
    {
        public T this[int index]
        {
            get { return _array[index]; }
            set { _array[index]=value; }
        }
        private readonly int _limit;
        T[] _array;
        int _index = -1;

        public T Current => _array[_index];

        object IEnumerator.Current => _array[_index];

        public FancyCollection(int limit) {
            _limit = limit;
            _array = new T[_limit];
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_index < _limit-1)
            {
                _index++;
                return true;
            }
            Reset();
            return false;
        }

        public void Reset()
        {
            _index = -1;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            _array = null;
        }

    }
}
