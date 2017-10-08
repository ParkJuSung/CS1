using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public static class HashCodeExtensionMethod
    {
        public static int GetHashCode(int input)
        {
            int hashCode = input % 10;
            return hashCode;
        }

        public static int GetHashCode2(int input)
        {
            int hasCode = input % 2;
            return hasCode;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] saveidx = new int[100];
            Random r = new Random();
            for (int i = 0; i < 12; i++)
            {
                Node node = new Node();
                node.value = 2;
                node.hashNext = null;
                Hash.AddHashData(node.value, node);
                saveidx[i] = node.value;
            }
            Hash.PrintHashData();

            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));
            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));
            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));
            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));
            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));
            Console.WriteLine("이값 삭제:{0}", Hash.DeleteHashData(2));

            Hash.PrintHashData();
        }
    }

    
    public class Node
    {
        public int value;
        public Node hashNext;
    }

   static public class Hash
    {
        public static Node[] hashTable = new Node[10];

        public  static void AddHashData(int value, Node node)
        {
            int hashKey = HashCodeExtensionMethod.GetHashCode(value);

            if (hashTable[hashKey] == null)
            {
                hashTable[hashKey] = node;
                return;
            }
            else
            {
                if (NodeCount(value,hashKey) >= 5)
                {
                   int hashKey2 = HashCodeExtensionMethod.GetHashCode2(value);
                    if (hashTable[hashKey2] == null)
                    {
                        hashTable[hashKey2] = node;
                        return;
                    }
                    else
                    {
                        if (NodeCount(value, hashKey2) >= 5)
                        {
                            Console.WriteLine("Node is Full");
                            return;
                        }
                        node.hashNext = hashTable[hashKey2];
                        hashTable[hashKey2] = node;
                        return;
                    }
                }
                node.hashNext = hashTable[hashKey];
                hashTable[hashKey] = node;
            }
        }

       public static int NodeCount(int value, int key)
       {
           int count = 1 ;
           Node node = hashTable[key];
           Node next = node.hashNext;

           if (node == null)
               return 0;
           else
               while (next != null)
               {
                   count++;
                   next = next.hashNext;
               }
           return count;
       }
        public static  int? DeleteHashData(int value)
        {
            int hashKey = HashCodeExtensionMethod.GetHashCode(value);
            if (hashTable[hashKey] == null)
            {
                hashKey = HashCodeExtensionMethod.GetHashCode2(value);
                if (hashTable[hashKey] == null)
                    return null;
            }

            Node deleteNode = null;
            if (hashTable[hashKey].value == value)
            {
                deleteNode = hashTable[hashKey];
                hashTable[hashKey] = hashTable[hashKey].hashNext;
            }
            else
            {
                Node node = hashTable[hashKey];
                Node next = node.hashNext;
                while (node != null)
                {
                    if (next.value == value)
                    {
                        node.hashNext = next.hashNext;
                        deleteNode = next;
                        break;
                    }
                    node = next;
                    next = node.hashNext;
                }
            }

            return deleteNode.value;
        }

        public  static void PrintHashData()
        {
            Console.WriteLine("Print Hash Data");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("idx : {0}", i);
                if (hashTable[i] != null)
                {
                    Node node = hashTable[i];
                    while (node.hashNext != null)
                    {
                        Console.WriteLine("{0}", node.value);
                        node = node.hashNext;
                    }
                    Console.WriteLine("{0}", node.value);
                }
            }
        }

        public static Node search(int key)
        {
            Node node = hashTable[key];

            if(node != null)
            {
                return node;
            }
            else
                return null;
        }

    }

    
}
