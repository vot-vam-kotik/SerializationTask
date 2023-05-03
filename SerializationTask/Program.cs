using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationTask
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRand List = new ListRand();
            List.Add("");
            List.Add("Element 1");
            List.Add("El 2");
            List.Add("Elem 3");
            List.Add("Element___4");

            List.GetNodeByIndex(1).Rand = List.GetNodeByIndex(1);
            List.GetNodeByIndex(2).Rand = List.GetNodeByIndex(4);
            List.GetNodeByIndex(3).Rand = List.GetNodeByIndex(0);

            //List.PrintList();

            using (FileStream Stream = new FileStream("test.bin", FileMode.OpenOrCreate))
            {
                List.Serialize(Stream);
            }
            
          
            ListRand ListFromFile = new ListRand();
            using (FileStream Stream = new FileStream("test.bin", FileMode.Open))
            {
                ListFromFile.Deserialize(Stream);
            }
            
            ListFromFile.PrintList();           
        }
    }
}
