using System.Collections;
using System.Collections.Generic;

namespace consoleApp1
{
    public class MyList<T> : IEnumerable<T>
    {
        T[] myArray ;
        int index = -1;
        public MyList(int capacity)
        {
            myArray = new T[capacity];
        }
        public void Add(T item) {
            myArray[++index] = item;
        }
        public IEnumerator GetEnumerator()
        {
            return myArray.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return (T)GetEnumerator();
        }
    }
}
