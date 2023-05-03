using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace SerializationTask
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public ListRand()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        ~ListRand()
        {
            Clear();
        }

        /** Добавление элемента в конец списка */
        public void Add(string Data)
        {
            ListNode NewNode = new ListNode(ref Data);
            NewNode.Data = Data;
            NewNode.Next = null;

            if (Head == null)
            {
                Head = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Prev = Tail;
            }

            Tail = NewNode;
            Count++;

        }

        /** Удаление всех элементов списка*/
        public void Clear()
        {
            if (Head == null)
            {
                return;
            }

            ListNode Node = Head;
            while (Head != null)
            {
                Head = Head.Next;
                Node = null;
                Node = Head;
            }

            Head = null;
            Tail = null;
            Count = 0;
        }

        /** Поиск элемета по индексу, используется только для дебага, поэтому ввод не проверяется */
        public ListNode GetNodeByIndex(int Id, ListNode Node = null)
        {
            if (Node == null)
            {
                Node = Head;
            }

            if (Id == 0)
            {
                return Node;
            }

            return GetNodeByIndex(Id - 1, Node.Next);
        }

        /** Печать списка, используется только для дебага */
        public void PrintList()
        {
            for (int i = 0; i < this.Count; ++i)
            {
                ListNode Node = this.GetNodeByIndex(i);
                bool Flag = Node.Rand == null;
                Console.WriteLine("Data: " + Node.Data + "\nRandData: ");
                if (Flag)
                {
                    Console.WriteLine("null \n");
                }
                else
                {
                    Console.WriteLine(Node.Rand.Data + "\n");
                }
            }
        }

        /** Записывает все элементы списка в словарь с их индексами */
        public void FillIndexedDict(ref Dictionary<ListNode, int> IndexedDict)
        {
            IndexedDict.Clear();

            ListNode Node = Head;
            for (int i = 0; i < Count; ++i)
            {
                IndexedDict.Add(Node, i);
                Node = Node.Next;
            }
        }

        //* Сериализация в бинарный файл */
        public void Serialize(FileStream Stream)
        {
            Dictionary<ListNode, int> IndexedDict = new Dictionary<ListNode, int> { };
            FillIndexedDict(ref IndexedDict);

            ListNode Node = Head;
            using (BinaryWriter writer = new BinaryWriter(Stream, Encoding.UTF8))
            {
                writer.Write(Count);

                while (Node != null)
                {
                    // Не записываем длину строки, так как BinaryWriter делает это сам
                    writer.Write(Node.Data);

                    if (Node.Rand == null)
                    {
                        // -1 будет значить, что ссылки на rand нет
                        int FillerNumber = -1;
                        writer.Write(FillerNumber);
                    }
                    else
                    {
                        writer.Write(IndexedDict[Node.Rand]);
                    }

                    Node = Node.Next;
                }
            }
            Console.WriteLine("List serialized");

        }

        //* Десериализация из бинарного файла */
        public void Deserialize(FileStream Stream)
        {
            Clear();
            
            using(BinaryReader br = new BinaryReader(Stream))
            {
                int DeserializedCount;
                DeserializedCount = br.ReadInt32();
                Console.WriteLine("\nNumber of elements: " + DeserializedCount + "\n");

                List<ListNode> Nodes = new List<ListNode>();

                // Создание элементов, создаем в отдельном цикле для удобства записи rand элементов
                for (int i = 0; i < DeserializedCount; ++i)
                {

                    Add("");
                    Nodes.Add(Tail);
                }

                // Заполнение данных элементов
                foreach (ListNode Node in Nodes)
                {
                    Node.Data = br.ReadString();

                    int RandIndex = br.ReadInt32();
                    if (RandIndex != -1)
                    {
                        Node.Rand = Nodes.ElementAt(RandIndex);
                    }

                }
            }
            
        }                                                   
    }
}
