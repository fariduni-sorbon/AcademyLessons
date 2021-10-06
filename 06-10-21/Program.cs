using System;
using System.Collections.Generic;

namespace _06_10_21
{
    class Program
    {
        public delegate T3 OperationDelegate<T1,T2,T3>(T1 left , T2 right);
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("A = "); int a = int.Parse(Console.ReadLine());
                    Console.Write("B = "); int b = int.Parse(Console.ReadLine());
                    OperationDelegate<int, int, int> operation = null;
                    Console.WriteLine("1. +\t2. -\t3. *\t4. /"); int op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            operation = Plus<int, int, int>;
                            break;
                        case 2:
                            operation = Minus<int, int, int>;
                            break;
                        case 3:
                            operation = Multiply<int, int, int>;
                            break;
                        case 4:
                            operation = Div<int, int, int>;
                            break;
                    }
                    Console.WriteLine(operation(a, b));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
               
                Console.ReadLine();
                Console.Clear();
            }
        }
        static T3 Minus<T1, T2, T3>(T1 left, T2 right)
        {
            dynamic d1 = left;
            dynamic d2 = right;
            return (T3)(d1 - d2);
        }
        static T3 Plus<T1, T2, T3>(T1 left, T2 right)
        {
            dynamic d1 = left;
            dynamic d2 = right;
            return (T3)(d1 + d2);
        }
        static T3 Multiply<T1, T2, T3>(T1 left, T2 right)
        {
            dynamic d1 = left;
            dynamic d2 = right;
            return (T3)(d1 * d2);
        }
        static T3 Div<T1, T2, T3>(T1 left, T2 right)
        {
            dynamic d1 = left;
            dynamic d2 = right;
            return (T3)(d1 / d2);
        }
    }
}
