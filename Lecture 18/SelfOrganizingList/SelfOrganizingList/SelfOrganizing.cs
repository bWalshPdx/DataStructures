using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfOrganizingList
{
    public class Node<T>
    {
        public T value;
        public Node<T> Child;
        public int Hits;

        public Node(T value)
        {
            this.value = value;
            Child = null;
        }

        public Node()
        {
            this.value = value;
        }
    }

    
    //If a item is selected, shift it to the front and push the others back on step:
    public class MoveToFront<T>
    {
        private MyLinkedList<T> _list;

        public IEnumerable<T> Walk {get { return _list.WalkValues(); }} 

        public MoveToFront(IEnumerable<T> input)
        {
            _list = new MyLinkedList<T>(input);
        }


        
        public bool Find(T value)
        {
            var index = _list.FindIndex(value);

            if (index != -1)
            {
                _list.RemoveValue(value);
                _list.AddToFront(value);

                return true;
            }

            return false;
        }   
    }


    //http://en.wikipedia.org/wiki/Self-organizing_list
    public class CountMethod<T>
    {
        private MyLinkedList<T> _list;

        public IEnumerable<T> Walk {get { return _list.WalkValues(); }}

        public CountMethod(IEnumerable<T> input)
        {
            _list = new MyLinkedList<T>(input);
        }

        public bool Find(T value)
        {
            var index = _list.FindIndex(value);

            //Order by hit count:
            if (index != -1)
            {
                _list.RemoveValue(value);
                
                return true;
            }

            return false;
        }
    }
}
