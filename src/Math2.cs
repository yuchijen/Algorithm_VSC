﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview
{
    public class Math2
    {
        //190. Reverse Bits
        public uint reverseBits(uint n)
        {
            uint ret = 0;

            for (int i = 0; i < 32; i++)
            {
                ret <<= 1;
                ret += (n&1);
                n >>= 1;    
            }

            return ret;
        }

        //55. Jump Game
        //Input: nums = [2,3,1,1,4] Output: true
        //Explanation: Jump 1 step from index 0 to 1, then 3 steps to the last index.
        // greedy approach, 
        public bool CanJump(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return false;
            int reach = 0;
            int len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                // if idx over reach, means impossible to reach, if reach >= len-1 means reach the end
                if (reach < i || reach >= len - 1)
                {
                    break;
                }
                reach = Math.Max(reach, i + nums[i]);
            }

            return reach >= len - 1;
        }

        //973. K Closest Points to Origin
        //We have a list of points on the plane.  Find the K closest points to the origin (0, 0).
        //Input: points = [[1,3],[-2,2]], K = 1 ;Output: [[-2,2]]
        public int[][] KClosest(int[][] points, int K)
        {
            return points.OrderBy(p => p[0] * p[0] + p[1] * p[1]).Take(K).ToArray();
            // Array.Sort(points, (p1, p2) => (p1[0]*p1[0] + p1[1]*p1[1] - p2[0]*p2[0] - p2[1]*p2[1]));
            // return points.Take(K).ToArray();
        }

        //152. Maximum Product Subarray
        public int MaxProduct3(int[] nums)
        {
            // 1->2->3->4
            int ret = 1;
            int prod = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                prod *= nums[i];
                ret = Math.Max(ret, prod);
                if (nums[i] == 0)
                {
                    prod = 1;
                }
            }

            prod = 1;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                prod *= nums[i];
                ret = Math.Max(ret, prod);
                if (nums[i] == 0)
                {
                    prod = 1;
                }
            }

            return ret;
        }
        // timout ...
        public int MaxProduct2(int[] nums)
        {
            if (nums == null)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int len = nums.Length;
            var dp = new int[len, len];
            int ret = nums.Max();

            for (int i = 0; i < len; i++)
            {
                dp[i, i] = nums[i];
            }

            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    dp[i, j] = nums[j] * dp[i, j - 1];
                    ret = Math.Max(ret, dp[i, j]);
                }
            }
            return ret;
        }
        public int MaxProduct(int[] nums)
        {
            int max = nums[0];
            int min = nums[0];
            int ret = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] >= 0)
                {
                    max = Math.Max(nums[i], nums[i] * max);
                    min = Math.Min(nums[i], nums[i] * min);
                }
                else
                {
                    int temp = max;
                    max = Math.Max(nums[i], nums[i] * min);
                    min = Math.Min(nums[i], nums[i] * temp);
                }
                ret = Math.Max(ret, max);
            }
            return ret;
        }

        //204. Count Primes
        public int CountPrimes(int n)
        {
            int c = 0;
            var hs = new HashSet<int>() { 2, 3 };
            var primes = new bool[n];
            for (int i = 0; i < primes.Length; i++)
            {
                primes[i] = true;
            }

            for (int i = 2; i < n; i++)
            {
                if (!primes[i])
                    continue;
                else
                {
                    c++;
                    for (int j = 2; j * i < n; j++)
                    {
                        primes[j * i] = false;
                    }
                }
            }
            return c;
        }

        bool isPrime(int x, HashSet<int> hs)
        {
            if (hs.Contains(x))
                return true;
            if (x <= 1)
                return false;
            // if (x == 2 || x == 3)
            //     return true;
            for (int i = 2; i * i <= x; i++)
            {
                if (x % i == 0)
                    return false;
            }
            hs.Add(x);
            return true;
        }
        
        //50. Pow(x, n)
        //Implement pow(x, n). in log(n)
        //Example 1:Input: 2.00000, 10
        //Output: 1024.00000
        //Example 2:Input: 2.10000, 3
        //Output: 9.26100
        //Example 3: Input: 2.00000, -2
        //Output: 0.25000
        //Explanation: 2^-2 = 1/2^2 = 1/4 = 0.25
        //Note: -100.0 < x< 100.0
        //n is a 32-bit signed integer, within the range[−2^31, 2^31 − 1]
        public double MyPow(double x, int n)
        {
            if (n == 0)
                return 1;
            if (n < 0)
                return (1 / x) * MyPow((1 / x), -(n + 1));
            if (n % 2 == 0)
                return MyPow(x * x, n >> 1);
            else
                return x * MyPow(x * x, n >> 1);
        }


        //leetcode  273. Integer to English Words
        //Convert a non-negative integer to its english words representation. Given input is guaranteed to 
        //be less than 2^31 - 1.
        // For example, 123 -> "One Hundred Twenty Three"  12345 -> "Twelve Thousand Three Hundred Forty Five"
        //1234567 -> "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"
        public string NumberToWords(int num)
        {
            string[] digitStr1 = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] digitStrTeen = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] digitStr2 = new string[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", "Hundred" };
            string[] digiStr3 = new string[] { "", "Thousand", "Million", "Billion" };
            if (num <= 0)
                return "Zero";

            string ret = "";

            int level = 0;

            while (num % 1000 != 0 || num / 1000 != 0)
            {
                int curSet = num % 1000;
                string curSetStr = curSet.ToString();

                string curRet = "";
                for (int i = curSetStr.Length - 1; i >= 0; i--)
                {
                    if (curSetStr.Length - i == 1 && curSetStr[i] - '0' != 0)
                    {
                        curRet = " " + digitStr1[curSetStr[i] - '0'];
                    }
                    else if (curSetStr.Length - i == 2 && curSetStr[i] - '0' != 0)
                    {
                        if (curSetStr[i] - '0' == 1)
                            curRet = " " + digitStrTeen[curSetStr[i + 1] - '0'];
                        else
                            curRet = " " + digitStr2[curSetStr[i] - '0'] + curRet;
                    }
                    else if (curSetStr.Length - i == 3 && curSetStr[i] - '0' != 0)
                        curRet = " " + digitStr1[curSetStr[i] - '0'] + " " + digitStr2[10] + curRet;
                }

                if (level == 0)
                    ret = curRet;
                else if (!string.IsNullOrWhiteSpace(curRet))
                    ret = curRet + " " + digiStr3[level] + ret;

                num /= 1000;
                level += 1;
            }
            return ret.Trim();
        }

        //71. Simplify Path
        //Given an absolute path for a file (Unix-style), simplify it. 
        //       For example,
        //      path = "/home/", => "/home"
        //path = "/a/./b/../../c/", => "/c"
        //path = "/a/../../b/../c//.//", => "/c"
        //path = "/a//b////c/d//././/..", => "/a/b/c"
        public string SimplifyPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            string ret = "";

            string[] segPath = path.Split('/');
            var st = new Stack<string>();
            for (int i = 0; i < segPath.Length; i++)
            {
                if (segPath[i] == ".." && st.Count > 0)
                    st.Pop();
                else if (segPath[i] == "." || segPath[i] == "")
                    continue;
                else
                    st.Push(segPath[i]);
            }
            while (st.Count > 0)
            {
                ret = "/" + st.Pop() + ret;
            }
            return ret == "" ? "/" : ret;
        }

        //leetcode 171. Excel Sheet Column Number
        //Given a column title as appear in an Excel sheet, return its corresponding column number.
        // For example:  A -> 1, B -> 2, ... Z -> 26,  AA -> 27,    AB -> 28 
        public int TitleToNumber(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int level = 0;
            double ret = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                level = s.Length - 1 - i;
                int letter = (s[i] - 'A') + 1;
                ret += letter * Math.Pow(26, level);
            }
            return Convert.ToInt32(ret);
        }


        //168. Excel Sheet Column Title
        //Given a positive integer, return its corresponding column title as appear in an Excel sheet.
        //For example:
        //    1 -> A
        //    2 -> B
        //    3 -> C
        //...
        //26 -> Z
        //27 -> AA
        //28 -> AB
        public string ConvertToTitle(int n)
        {
            if (n < 1)
                return "";

            string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string ret = "";
            while (n > 0)
            {
                if ((n % 26) > 0)
                {
                    ret = charSet[(n % 26) - 1] + ret;
                    n = n / 26;
                }
                else
                {
                    ret = 'Z' + ret;
                    n = (n / 26) - 1;
                }
            }
            return ret;

        }

    }
}
