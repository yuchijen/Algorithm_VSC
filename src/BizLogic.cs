using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BizLogic
    {
        
        //Design an algorithm and write code to remove the duplicate characters in a string 
        //without using any additional buffer. NOTE: One or two additional variables are fine. An extra copy of the array is not.
        public static void RemoveDuplicate(string input)
        {

            var dup = input.ToList<char>();
            for (int i = dup.Count - 1; i >= 0; i--)
            {
                for (int j = dup.Count - 1; j >= 0; j--)
                {
                    if (i == j)
                        break;
                    if (dup[i] == dup[j])
                    {
                        dup.RemoveAt(j);

                    }
                }
            }
            foreach (var x in dup)
            {
                Console.Write(x);
            }

        }


        //Implement an algorithm to determine if a string has all unique characters. What if you can not use additional data structures?
        public static bool IsUniqueChar(string input)
        {
            char[] charStr = input.ToArray<char>();

            HashSet<char> hs = new HashSet<char>();

            foreach (char x in charStr)
            {
                if (!hs.Add(x))
                    return false;
            }
            return true;
        }

        //Write code to reverse a C-Style String. (C-String means that “abcd” is represented 
        //as five characters, including the null character
        public static string ReverseString(string input)
        {
            char[] reverse = input.ToArray<char>();

            int i = 0;
            int j = reverse.Length - 1;
            int mid = (reverse.Length - 1) / 2;
            // in-space swap
            while (i < j)
            {
                if (i > mid)
                {
                    break;
                }
                char temp = reverse[j];
                reverse[j] = reverse[i];
                reverse[i] = temp;
                i++;
                j--;
            }
            StringBuilder ret = new StringBuilder();
            foreach (var x in reverse)
            {
                ret.Append(x);
            }
            return ret.ToString();

        }

        //fibonacci  n
        public static int Fibonacci(int n)
        {
            if (n <= 0)
                return 0;

            if (n == 1)
                return 1;

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // *11/16 = 1.454545...  output: 1.(45), find repetition if any, string divide(int n, int m)
        // see if has infinite repeat
        public static void Divide(int divisor, int dividend)
        {
            List<int> quotient = new List<int>();
            bool isRepeat = true;

            int firstQuotient = dividend / divisor;

            HashSet<int> modHashset = new HashSet<int>();

            while (true)
            {
                int mod = dividend % divisor;

                if (mod == 0)
                {
                    isRepeat = false;
                    break;
                }
                else
                {
                    //repeat happens
                    if (!modHashset.Add(mod))
                    {
                        break;
                    }
                    else
                    {
                        dividend = mod * 10;
                    }
                }
            }

            StringBuilder quotientSeries = new StringBuilder();

            foreach (var x in modHashset)
            {
                int quote = (x * 10) / divisor;
                quotientSeries.Append(quote.ToString());
            }


            if (isRepeat)
            {
                //String.Format("repeat:{0}",)
                Console.WriteLine("repeat: {0}.({1})", firstQuotient.ToString(), quotientSeries.ToString());
            }
            else
            {
                Console.WriteLine("not repeat: {0}.{1}", firstQuotient.ToString(), quotientSeries.ToString());
            }


        }


        public static void BitShift()
        {
            int i = 10;
            long lg = 1;
            // Shift i one bit to the left. The result is 2.
            Console.WriteLine("0x{0:x}", i >> 1);
            // In binary, 33 is 100001. Because the value of the five low-order 
            // bits is 1, the result of the shift is again 2. 
            Console.WriteLine("0x{0:x}", i << 33);
            // Because the type of lg is long, the shift is the value of the six (c# int is 32 bits)
            // low-order bits. In this example, the shift is 33, and the value of 
            // lg is shifted 33 bits to the left. 
            //     In binary:     10 0000 0000 0000 0000 0000 0000 0000 0000  
            //     In hexadecimal: 2    0    0    0    0    0    0    0    0
            Console.WriteLine("0x{0:x}", lg << 33);

        }

    }
}
