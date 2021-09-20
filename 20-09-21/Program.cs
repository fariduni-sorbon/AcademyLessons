using System;

namespace _20_09_21
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strArr = { "one", "two", "three", "four", "five" };
            int[] intArr = { 1, 2, 3, 4, 5 };
            
            Console.WriteLine("Before ArrayHelper.Pop");
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }

            Console.WriteLine("\nAfter ArrayHelper.Pop");
            ArrayHelper<int>.Pop(ref intArr);
            ArrayHelper<string>.Pop(ref strArr);

            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }
            Console.WriteLine("\n\n");
            //-------------------------------------------------

            Console.WriteLine("Before ArrayHelper.Push");
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }

            Console.WriteLine("\nAfter ArrayHelper.Push");
            ArrayHelper<int>.Push(ref intArr,5);
            ArrayHelper<string>.Push(ref strArr,"five");

            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }
            Console.WriteLine("\n\n");
            //-------------------------------------------------

            Console.WriteLine("Before ArrayHelper.Shift");
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }

            Console.WriteLine("\nAfter ArrayHelper.Shift");
            ArrayHelper<int>.Shift(ref intArr);
            ArrayHelper<string>.Shift(ref strArr);

            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }
            Console.WriteLine("\n\n");
            //-------------------------------------------------

            Console.WriteLine("Before ArrayHelper.UnShift");
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }

            Console.WriteLine("\nAfter ArrayHelper.UnShift");
            ArrayHelper<int>.Push(ref intArr, 1);
            ArrayHelper<string>.Push(ref strArr, "one");

            for (int i = 0; i < strArr.Length; i++)
            {
                Console.Write($"{strArr[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < intArr.Length; i++)
            {
                Console.Write($"{intArr[i]} ");
            }
            Console.WriteLine("\n\n");
            //-------------------------------------------------

            int[] newIntArr;
            string[] newStrArr;

            //Console.WriteLine("ArrayHelper.Slice:");
            //ArrayHelper<int>.Slice(intArr, 2, 4,out newIntArr);
            //ArrayHelper<string>.Slice(strArr, 2, 4, out newStrArr);
            //for (int i = 0; i < newIntArr.Length; i++)
            //{
            //    Console.Write(newIntArr[i]+ " ");
            //}
            //Console.WriteLine();
            //for (int i = 0; i < newStrArr.Length; i++)
            //{
            //    Console.Write(newStrArr[i] + " ");
            //}
            //Console.WriteLine();

            Console.WriteLine("ArrayHelper.Slice:");
            ArrayHelper<int>.Slice(intArr, 2, out newIntArr);
            ArrayHelper<string>.Slice(strArr, 2, out newStrArr);
            for (int i = 0; i < newIntArr.Length; i++)
            {
                Console.Write(newIntArr[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < newStrArr.Length; i++)
            {
                Console.Write(newStrArr[i] + " ");
            }
            Console.WriteLine();
        }
    }

    static class ArrayHelper<T>
    {
         public static T Pop(ref T[] arr)
         {
            T result = arr[arr.Length - 1];
            T[] newArr = new T[arr.Length-1];
            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = arr[i];
            }
            arr = newArr;
            return result;
         }

        public static int Push(ref T[] arr, T num)
        {
            T[] newArr = new T[arr.Length + 1];
            for (int i = 0; i < newArr.Length - 1; i++)
            {
                newArr[i] = arr[i];
            }
            newArr[newArr.Length - 1] = num;
            arr = newArr;
            return newArr.Length;
        }

        public static T Shift(ref T[] arr)
        {
            T result = arr[0];
            T[] newArr = new T[arr.Length - 1];
            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = arr[i + 1];
            }
            arr = newArr;
            return result;
        }

        public static int UnShift(ref T[] arr, T num)
        {
            T[] newArr = new T[arr.Length + 1];
            newArr[0] = num;
            for (int i = 1; i < newArr.Length; i++)
            {
                newArr[i] = arr[i - 1];
            }
            arr = newArr;
            return arr.Length;
        }

       public static void Slice(T[]arr,int beginIdx,int endIdx, out T[]newArr)
        {
            newArr = new T[endIdx-beginIdx+1];
            for (int i = beginIdx; i <= endIdx; i++)
            {
                newArr[i-beginIdx] = arr[i];
            }
        }

        public static void Slice(T[] arr, int beginIdx, out T[] newArr)
        {
            newArr = new T[arr.Length-beginIdx];
            for (int i = beginIdx; i < arr.Length; i++)
            {
                newArr[i-beginIdx] = arr[i];
            }
        }
    }
}
