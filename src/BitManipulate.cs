using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BitManipulate
    {
        //Find 4th bit of a given num. 秒杀
        //Given num = 32(10000)  4th是0  **1st是最后一位**
        public int FourthBit(int num)
        {
            if (((1 << 3) & num) >= 1)
                return 1;

            return 0;

            //for (int i = 0; i < 3; i++)
            //    num =num >> 1;

            //return num % 2;
        }

        //371. Sum of Two Integers, add 2 numbers without + operator
        //https://www.cnblogs.com/grandyang/p/5631814.html
        public int add(int x, int y)
        {
            // Iterate till there is no carry 
            while (y != 0)
            {
                // carry now contains common 
                // set bits of x and y 
                //e.g. 11(3) + 11(3) => carry = 11 , x^y = 00=x, 110 =y; carry = 000,  x=110, y=0000  
                int carry = x & y;
                // Sum of bits of x and y where at least one  
                // of the bits is not set 
                x = x ^ y;
                // Carry is shifted by  
                // one so that adding it  
                // to x gives the required sum 
                y = carry << 1;
            }
            return x;
        }

        //29. Divide Two Integers
        //Divide two integers without using multiplication, division and mod operator.
        //If it is overflow, return MAX_INT.
        //举个例子，假设除数为3，被除数为16，那么商应该是5。我们从2的0次幂与除数的乘积也即2^0x3=3开始，
        //幂次逐渐增加，直至超过被除数。可以看出，当幂次达到2时刚好超过16（3x2^0+3x2^1+3x2^2=21>16）。
        //那么从3x2^2开始往下累加，3x2^2=12 < 16，那么记录下2^2=4。再看3x2^1，发现3x2^2+3x2^1= 18 > 16，因此略过2^1=2。再看3x2^0，
        //发现3x2^2+3x2^0=15 < 16，(16-15 < 除数3) 那么将2^0=1记录下。次幂累加结束，计算一下商，分别记录了4和1，
        //因此结果4+1=5，此答案也即为最终的商。
        public int Divide(int dividend, int divisor)
        {
            if (divisor == 0)  //edge case
                return dividend >= 0 ? int.MaxValue : int.MinValue;
            if (dividend == int.MinValue && divisor == -1) //aviod overflow in positive number
                return int.MaxValue;

            bool sign = true;
            if ((divisor > 0 && dividend < 0) || (divisor < 0 && dividend > 0))
                sign = false;

            uint dvd = (uint)Math.Abs(dividend);
            uint dvs = (uint)Math.Abs(divisor);

            int i = 0;
            while ((dvs << i) <= dvd)
                i++;

            int ret = 0;

            while (dvd >= dvs)
            {
                if (dvd >= dvs << i)
                {
                    dvd -= dvs << i;
                    ret += 1 << i;
                }
                i--;
            }
            return sign ? ret : 0 - ret;
        }

        //338. Counting Bits
        //Given an integer n, return an array ans of length n + 1 such that for each i (0 <= i <= n), ans[i] is the number of 1's in the binary representation of i.
        //Input: n = 5 Output: [0,1,1,2,1,2] =>  0, 1, 10, 11, 100, 101
        public int[] CountBits(int n)
        {
            var ret = new int[n+1];

            for(int i=1; i<=n; i++){
                //int tail = (i&1) == 1 ? 1:0;
                ret[i] = ret[i>>1] + (i%2);
            }
            return ret;
        }

        //NVIDIA Given a 32 bit unsigned integer, write a function(in C) that returns a count of how many bits are "1".
        public int CountOneBit(uint num)
        {
            int ret = 0;
            while (num != 0)
            {
                if ((num & 1) == 1)
                    ret++;

                num >>= 1;
            }
            return ret;
        }

        //NVIDIA: Swap even and odd bits of a 32 bit integer.  
        public uint SwapBits(uint x)
        {
            //get all even bit
            uint even = x & 0xAAAAAAAA;
            uint odd = x & 0x55555555;

            even >>= 1;
            odd <<= 1;
            return even | odd;
        }
    }
}