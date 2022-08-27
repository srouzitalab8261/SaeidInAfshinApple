using System.Collections;
using System.Collections.Generic;

namespace consoleApp1
{
    public class GenericList<T>
    {
        List<T> _myList = new List<T>();
        public List<T> Mylist
        {
            get { return _myList; }
            set { _myList = Mylist; }
        }

        public void Addd(T input) {
            _myList.Add(input);
        }
        //public List<T> GetData()
        //{
        //    return _myList;
        //}
    }

    public class GenericListNew<T>: IEnumerable<T>
    {
        List<T> _myList = new List<T>();
        public void Add(T input)
        {
            _myList.Add(input);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _myList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)_myList;
        }
    }
}
    
