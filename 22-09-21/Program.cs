using System;
using System.Collections;
using System.Collections.Generic;

namespace _22_09_21
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>(0);
            try
            {
                Console.WriteLine("List item count: " + list.ItemCount);
                Console.WriteLine("Adding items.");
                list.Add(5);
                list.Add(10);
                list.Add(520);
                list.Add(15);
                list.Add(21);
                list.Add(3);
                list.ToList();


                Console.WriteLine("List item count after adding: " + list.ItemCount);
                Console.WriteLine("Deleting item by index 2:");
                list.Delete(2);
                Console.WriteLine("List item count: " + list.ItemCount);
                list.ToList();
                Console.WriteLine();
                Console.WriteLine($"Select the value List[3] = {list[3]}");
                Console.WriteLine("Changing the value of List[3].");
                list[3] = 22;
                Console.WriteLine($"List[3] = {list[3]}");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
      
            Console.WriteLine("---------------------------------");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("---------------------------------");


            MyList<int, string> myList = new MyList<int, string>(0);
            try
            {
                Console.WriteLine("List item count: " + myList.ItemCount);
                Console.WriteLine("Adding items");
                myList.Add(1, "Tom");
                myList.Add(2, "Jerry");
                myList.Add(3, "John");
                myList.Add(4, "Jack");
                myList.Add(5, "Mark");
                myList.ToList();
                Console.WriteLine("List item count After Adding: " + myList.ItemCount);

                Console.WriteLine("Delete items by index 3 and 2:");
                myList.Delete(3);
                myList.Delete(2);

                myList.ToList();
                Console.WriteLine("List item count: " + myList.ItemCount);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            Console.ReadLine();
        }
    }

    class MyList<T>
    {
        private T[] list;
        public MyList(int listSize)
        {
            list = new T[listSize];
        }

      public T this[int index]
      {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
      }

        public int ItemCount
        {
            get {return list.Length; }
        }
       
        public void ToList()
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"list[{i}] = {list[i]}");
            }
        }

        public void Add(T item)
        {
            Array.Resize(ref list, list.Length + 1);
            list[list.Length - 1] = item;
        }

        public void Delete(int index)
        {
            for (int i = index; i < list.Length-1; i++)
            {
                list[i] = list[i + 1];
            }
            Array.Resize(ref list, list.Length - 1);
        }
    }

    class MyList<TKey, TValue>
    {
        private TValue[] list;
        private TKey[] key;

        public MyList(int size)
        {
            list = new TValue[size];
            key = new TKey[size];
        }

        public int ItemCount
        {
            get { return list.Length; }
        }

        public string this[int index]
        {
            get
            {
                string result = $"{key[index -1]}. {list[index-1]}";
                return result;
            }
        }

        public void ToList()
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"{key[i]}. {list[i]}");
            }
        }

        public void Add(TKey itemKey, TValue item)
        {
            Array.Resize(ref list, list.Length + 1);
            list[list.Length - 1] = item;

            Array.Resize(ref key, key.Length + 1);
            key[key.Length - 1] = itemKey;
        }

        public void Delete(int index)
        {
            for (int i = index-1; i < list.Length - 1; i++)
            {
                list[i] = list[i+1 ];
            }
            Array.Resize(ref list, list.Length - 1);

            for (int i = index-1; i < key.Length - 1; i++)
            {
                key[i] = key[i + 1];
            }
            Array.Resize(ref key, key.Length - 1);
        }
    }
}
