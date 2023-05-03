using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTask
{
    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка
        public string Data;

        public ListNode(ref string Data)
        {
            this.Prev = null;
            this.Next = null;
            this.Rand = null;
            this.Data = Data;
        }
    }
}
