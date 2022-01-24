﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview
{
    public class DynamicProgramming
    {
        //1048. Longest String Chain
        // time O(NlogN + NxM) M is average length of word
        public int LongestStrChain(string[] words)
        {
            int len = words.Length;
            if (len == 1)
                return 1;

            words = words.OrderBy(x => x.Length).ToArray();
            var dp = new Dictionary<string, int>();

            // map's key是一个词，value是从小到大能变换到这个词的最大长度。

            foreach (var s in words)
            {
                for (int i=0; i<s.Length; i++)
                {
                    string prev = s.Substring(0, i)+s.Substring(i+1);
                    int dpPrev = dp.ContainsKey(prev) ? dp[prev]:0;
                    dp.TryAdd(s,1);
                    int val = Math.Max(dp[s], dpPrev+1);
                    dp[s] = val;
                }
            }
            return dp.Values.Max();
        }

        //64. Minimum Path Sum
        public int MinPathSum(int[][] grid)
        {
            int rLen = grid.Length;
            int cLen = grid[0].Length;
            //dp[i][j] 表示到达当前位置的最小路径和
            int[,] dp = new int[rLen + 1, cLen + 1];
            //但是有些特殊情况要提前赋值，比如起点位置，直接赋值为 grid[0][0]，还有就是第一行和第一列，其中第一行的位置只能从左边过来
            //initialize 1st col 
            for (int i = 1; i <= rLen; i++)
            {
                dp[i, 1] = grid[i - 1][0] + dp[i - 1, 1];
            }
            //initialize 1st row 
            for (int j = 1; j <= cLen; j++)
            {
                dp[1, j] = grid[0][j - 1] + dp[1, j - 1];
            }
            for (int i = 2; i <= rLen; i++)
            {
                for (int j = 2; j <= cLen; j++)
                {
                    dp[i, j] = grid[i - 1][j - 1] + Math.Min(dp[i - 1, j], dp[i, j - 1]);
                }
            }

            return dp[rLen, cLen];
        }

        //304. Range Sum Query 2D - Immutable
        public class NumMatrix
        {
            private int[,] dp;
            public NumMatrix(int[][] matrix)
            {
                int rLen = matrix.Length;
                int cLen = matrix[0].Length;
                dp = new int[rLen + 1, cLen + 1];

                for (int i = 1; i <= rLen; i++)
                {
                    for (int j = 1; j <= cLen; j++)
                    {
                        dp[i, j] = matrix[i - 1][j - 1] + dp[i - 1, j] + dp[i, j - 1] - dp[i - 1, j - 1];
                    }
                }
            }

            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                //int minLeft = Math.Min()
                return dp[row2 + 1, col2 + 1] - dp[row2 + 1, col1] - dp[row1, col2 + 1] + dp[row1, col1];

            }
        }
        //300. Longest Increasing Subsequence
        //Given an integer array nums, return the length of the longest strictly increasing subsequence.
        //Input: nums = [0,1,0,3,2,3] Output: 4
        public int LengthOfLIS(int[] nums)
        {
            int ret = 0;
            int len = nums.Length;
            int[] dp = new int[len]; //dp[i] is max LIS until i idx 
            for (int i = 0; i < len; i++)
                dp[i] = 1;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                }

                ret = Math.Max(dp.Max(), ret);
            }
            return ret;
        }

        //62. Unique Paths
        public int UniquePaths(int m, int n)
        {
            if (m < 2 || n < 2)
                return 1;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 1; i <= m; i++)
            {
                dp[i, 1] = 1;
            }
            for (int j = 1; j <= n; j++)
            {
                dp[1, j] = 1;
            }

            for (int i = 2; i <= m; i++)
            {
                for (int j = 2; j <= n; j++)
                {
                    // final step depends on previous step from up 1 or left 1
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
            return dp[m, n];
        }

        //5. Longest Palindromic Substring
        //Given a string s, find the longest palindromic substring in s.You may assume that the maximum length of s is 1000.
        //Example 1:
        //Input: "babad"
        //Output: "bab"
        //Note: "aba" is also a valid answer.
        // O(n^2)
        public string LongestPalindromeDp(string s)
        {
            if (s.Length < 2)
            {
                return s;
            }

            int len = s.Length;
            int left = 0;
            int maxLen = 1;
            var dp = new bool[len, len];
            // transform function:
            // dp[i, j] = 1  if i == j , (from i to j is palindrom)
            // s[i] == s[j]  && j-i <2
            // s[i] == s[j] && dp[i + 1][j - 1]     

            for (int j = 0; j < len; j++)
            {
                dp[j, j] = true;
                for (int i = 0; i < j; i++)
                {
                    dp[i, j] = (s[i] == s[j] && dp[i + 1, j - 1]) || (s[i] == s[j] && (j - i < 2));
                    if (dp[i, j] && j - i + 1 > maxLen)
                    {
                        maxLen = j - i + 1;
                        left = i;
                    }
                }
            }
            return s.Substring(left, maxLen);
        }

        //1235. Maximum Profit in Job Scheduling
        //dp[i]: max profit from 0 to ith jobs
        //dp[i] = Math.max(dp[i - 1], max profit from 0 to ith jobs, including the ith job)
        class job
        {
            public int StartTime;
            public int EndTime;
            public int Profit;

            public job(int startTime, int endTime, int profit)
            {
                StartTime = startTime;
                EndTime = endTime;
                Profit = profit;
            }
        }

        public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
        {
            // set dp[i] i is from 0 to i max profit
            // so it depends on (pick last non overlapping profit + current profit) or dp[n-1]
            // dp[i] = max(dp[i-1], dp[lastNonOverlapIdx](if any)+ current profit)
            int n = profit.Length;
            var jobs = new job[n];
            for (int i = 0; i < n; i++)
            {
                jobs[i] = new job(startTime[i], endTime[i], profit[i]);
            }

            jobs = jobs.OrderBy(x => x.EndTime).ToArray();

            int[] dp = new int[profit.Length];
            dp[0] = profit[0];

            for (int i = 1; i < n; i++)
            {
                int lastNonOverlapJob = findLastNonOverlap(jobs, 0, i - 1, jobs[i].StartTime);
                int curProfit = lastNonOverlapJob == -1 ? 0 + jobs[i].Profit : dp[lastNonOverlapJob] + jobs[i].Profit;
                dp[i] = Math.Max(dp[i - 1], curProfit);
            }
            return dp[n - 1];
        }

        int findLastNonOverlap(job[] jobs, int left, int right, int nonOverlapStart)
        {

            while (left < right)
            {
                int mid = (left + right) / 2;
                if (jobs[mid].EndTime <= nonOverlapStart)
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }
            if (jobs[right].EndTime <= nonOverlapStart)
            {
                return right;
            }
            if (jobs[left].EndTime <= nonOverlapStart)
            {
                return left;
            }
            return -1;
        }

        //1062. Longest Repeating Substring
        //Given a string S, find out the length of the longest repeating substring(s). Return 0 if no 
        //repeating substring exists.
        //Input: “aabcaabdaab”
        //Output: 3
        //Explanation: The longest repeating substring is “aab”, which occurs 3 times.
        //先定义dp[i][j]为分别以第i个字符和第j个字符结尾的substring有相同共同后缀的最大长度。
        //stores length of the matching substrings ending 
        //with i'th and j'th characters.
        //因此，我们也要求i>j。
        //我们注意到，当S[i] != S[j], 那么dp[i][j] = 0， 否则dp[i][j] = dp[i-1][j-1] + 1。这就是我们的状态转移方程。
        // dp[i][j] = dp[i-1][j-1] + 1 ----------- S[i] == S[j]
        // dp[i][j] = 0 ----------- S[i] != S[j]
        // 我们更新dp[i][j]的最大值，就可以得到最后的答案。
        public int longestRepeatingSubstring(string S)
        {
            int ans = int.MinValue;
            int[,] dp = new int[S.Length + 1, S.Length + 1];

            for (int i = 1; i <= S.Length; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    if (S[i - 1] == S[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    ans = Math.Max(ans, dp[i, j]);
                }
            }
            return ans;
        }




        //1335 Minimum Difficulty of a Job Schedule
        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            int n = jobDifficulty.Length;
            if (d > n)
                return -1;
            var dp = new int[n + 1, d + 1];
            for (int i = 0; i < dp.GetLength(0); i++)
            {
                for (int j = 0; j < dp.GetLength(1); j++)
                {
                    dp[i, j] = int.MaxValue / 2;  // if set max, will be overflow
                }
            }
            dp[0, 0] = 0;

            for (int i = 1; i <= n; i++)
            {
                for (int k = 1; k <= d; k++)
                {
                    int minDay = 0;
                    for (int j = i - 1; j >= k - 1; j--)
                    {
                        //for(int j= k-1; j<=i ; j++){

                        minDay = Math.Max(minDay, jobDifficulty[j]);
                        dp[i, k] = Math.Min(dp[i, k], dp[j, k - 1] + minDay);
                    }
                }
            }
            return dp[n, d];

        }

        string createShortestPalindrome(string s)
        {
            string str = s;

            //reverse (str);
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);
            str = s + "#" + str;
            int len1 = s.Length, len2 = str.Length;


            var vec = new int[len2];
            for (int i = 1; i < len2; i++)
            {
                int k = vec[i - 1];
                while (k > 0 && str[k] != str[i])
                    k = vec[k - 1];

                vec[i] = (k += str[i] == str[k] ? 1 : 0);
            }
            return str.Substring(len1 + 1, len1 - vec[len2 - 1]) + s;
        }


        //expeida OA
        //Longest repeating and non-overlapping substring
        //Input : str = "geeksforgeeks"
        //Output : geeks
        //Input : str = "aab"
        //Output : a
        //LCSRe(i, j) stores length of the matching and
        //non-overlapping substrings endin with i'th and j'th characters.
        //If str[i - 1] == str[j - 1] && (j-i) > LCSRe(i-1, j-1)
        //LCSRe(i, j) = LCSRe(i-1, j-1) + 1, 
        //Else
        //LCSRe(i, j) = 0

        //Where i varies from 1 to n and
        //j varies from i+1 to n
        public string longestRepeatedSubstring(string str)
        {
            var dp = new int[str.Length + 1, str.Length + 1];

            dp[0, 0] = 0;
            int stIdx = 0;
            int maxLen = 0;

            for (int i = 1; i <= str.Length; i++)
            {
                for (int j = i + 1; j <= str.Length; j++)
                {
                    if (str[i - 1] == str[j - 1] && (j - i) > dp[i - 1, j - 1])
                    {
                        dp[i, j] = 1 + dp[i - 1, j - 1];
                        if (dp[i, j] > maxLen)
                        {
                            stIdx = i;
                            maxLen = dp[i, j];
                        }
                    }
                    else
                    {
                        dp[i, j] = 0;
                    }
                }
            }
            string ret = "";

            if (maxLen > 0)
            {
                for (int i = stIdx - maxLen + 1; i <= stIdx; i++)
                    ret += str[i - 1];
            }
            return ret;
        }


        //198. House Robber
        //You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security system connected and it will automatically contact the police if two adjacent houses were broken into on the same night.
        //Given a list of non-negative integers representing the amount of money of each house, determine the maximum amount of money you can rob tonight without alerting the police.
        //Example 1: Input: [1,2,3,1]
        //Output: 4
        //Explanation: Rob house 1 (money = 1) and then rob house 3 (money = 3).
        //Total amount you can rob = 1 + 3 = 4.
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            //dp i is cur max value if rob this house or not  
            var dp = new int[nums.Length];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(dp[i - 1], nums[i] + dp[i - 2]);
            }
            return dp[nums.Length - 1];
        }

        //213. House Robber II
        //All houses at this place are arranged in a circle. That means the first house is 
        //the neighbor of the last one.
        //hint: remove 1st or remove last then compare 2 conditions
        public int Rob2(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int len = nums.Length;
            var arr1 = new int[len - 1];
            var arr2 = new int[len - 1];

            Array.Copy(nums, 0, arr1, 0, len - 1);
            Array.Copy(nums, 1, arr2, 0, len - 1);

            return Math.Max(Rob(arr1), Rob(arr2));
        }

        //647. Palindromic Substrings 
        //Given a string, your task is to count how many palindromic substrings in this string.
        //The substrings with different start indexes or end indexes are counted as different substrings even they consist of same characters.
        //Example 1: Input: "abc"  Output: 3
        //Explanation: Three palindromic strings: "a", "b", "c".        
        //Example 2: Input: "aaa"
        //Output: 6
        //Explanation: Six palindromic strings: "a", "a", "a", "aa", "aa", "aaa".
        public int CountSubstrings(string s)
        {
            var dp = new bool[s.Length, s.Length];
            //init dp with i==j :true , i to j means from index i to index j in string is Palindrom
            int ret = 0;

            for (int j = 0; j < s.Length; j++)
            {
                for (int i = 0; i <= j; i++)
                {
                    if (s[i] == s[j] && (j - i <= 2 || dp[i + 1, j - 1]))
                    {
                        dp[i, j] = true;
                        ret++;
                    }
                }
            }
            return ret;
        }

        //583. Delete Operation for Two Strings
        //Given two words word1 and word2, find the minimum number of steps required to make word1 and word2 the same, where in each step you can delete one character in either string.
        //Example 1: Input: "sea", "eat"  Output: 2
        //Explanation: You need one step to make "sea" to "ea" and another step to make "eat" to "ea".
        //hint :  length word1 + length word2 - (most common string length) x2 
        //其中dp[i][j]表示word1的前i个字符和word2的前j个字符组成的两个单词的最长公共子序列的长度。
        //下面来看递推式dp[i][j]怎么求，首先来考虑dp[i][j]和dp[i-1][j-1]之间的关系，我们可以发现，
        //如果当前的两个字符相等，那么dp[i][j] = dp[i-1][j-1] + 1，这不难理解吧，因为最长相同子序列又多了一个相同的字符，
        //所以长度加1。由于我们dp数组的大小定义的是(n1+1) x (n2+1)，所以我们比较的是word1[i-1]和word2[j-1]。
        //那么我们想如果这两个字符不相等呢，难道我们直接将dp[i-1][j-1]赋值给dp[i][j]吗，当然不是，我们还要错位相比，
        //比如就拿题目中的例子来说，"sea"和"eat"，当我们比较第一个字符，发现's'和'e'不相等，下一步就要错位比较，
        //比较sea中第一个's'和eat中的'a'，sea中的'e'跟eat中的第一个'e'相比，这样我们的dp[i][j]就要取dp[i-1][j]跟dp[i][j-1]中的较大值了，最后我们求出了最大共同子序列的长度，就能直接算出最小步数了
        public int MinDistance(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1))
                return word2.Length;
            if (string.IsNullOrEmpty(word2))
                return word1.Length;

            int l1 = word1.Length;
            int l2 = word2.Length;
            //dp i to j means  longest common length of before i in word1 and before j in word2
            var dp = new int[l1 + 1, l2 + 1];

            for (int i = 1; i <= l1; i++)
            {
                for (int j = 1; j <= l2; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    else
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
            return l1 + l2 - 2 * dp[l1, l2];
        }


        //44. Wildcard Matching
        //Given an input string (s) and a pattern (p), implement wildcard pattern matching with support for '?' and '*'.
        //'?' Matches any single character.
        //'*' Matches any sequence of characters (including the empty sequence).
        //The matching should cover the entire input string (not partial).
        //Note:s could be empty and contains only lowercase letters a-z.
        //p could be empty and contains only lowercase letters a-z, and characters like? or *.
        //Example 1:
        //Input:
        //s = "aa"
        //p = "a"
        //Output: false
        //Explanation: "a" does not match the entire string "aa".
        //Example 2:
        //Input:
        //s = "aa"
        //p = "*"
        //Output: true
        //Explanation: '*' matches any sequence.
        //Time O(NxM) space O(NxM) 
        public bool IsMatch(string s, string p)
        {
            if (s == null || p == null)
                return false;

            // empty pattern can only match with 
            // empty string 
            if (p.Length == 0)
                return (s.Length == 0);

            // lookup table for storing results of 
            // subproblems i, j presents dp[i-1, j-1]  
            bool[,] lookup = new bool[s.Length + 1, p.Length + 1];

            // empty pattern can match with empty string 
            lookup[0, 0] = true;

            // Only '*' can match with empty string 
            for (int j = 1; j <= p.Length; j++)
                if (p[j - 1] == '*')
                    lookup[0, j] = lookup[0, j - 1];

            // fill the table in bottom-up fashion 
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= p.Length; j++)
                {
                    // Two cases if we see a '*' 
                    // a) We ignore '*'' character and move. 
                    //    to next  character in the pattern, 
                    //     i.e., '*' indicates an empty sequence.
                    //     e.g. dp[i,j-1] => s[i-1]==p[j-2], e.g. "C" match "C*", i=2, j=2 
                    // b) '*' character matches with ith 
                    //     character in input 
                    //     e.g. dp[i-1,j] => s[i-2]==p[j-1], e.g. "AC" match "*", i = 2, j = 1 
                    if (p[j - 1] == '*')
                        lookup[i, j] = lookup[i, j - 1] || lookup[i - 1, j];
                    // Current characters are considered as 
                    // matching in two cases 
                    // (a) current character of pattern is '?' 
                    // (b) characters actually match 
                    else if (p[j - 1] == '?' || s[i - 1] == p[j - 1])
                        lookup[i, j] = lookup[i - 1, j - 1];
                    // If characters don't match 
                    else lookup[i, j] = false;
                }
            }
            return lookup[s.Length, p.Length];
        }


        //416. Partition Equal Subset Sum
        //Given a non-empty array containing only positive integers, find if the array can be partitioned into two subsets such that the sum of elements in both subsets is equal.
        //Note:Each of the array element will not exceed 100.
        //The array size will not exceed 200. Example 1:
        //Input: [1, 5, 11, 5]
        //Output: true
        //Explanation: The array can be partitioned as [1, 5, 5] and[11].
        //hint: https://www.geeksforgeeks.org/partition-problem-dp-18/
        //space: O(sum*N) , time: O(sum*N)
        public bool CanPartition(int[] nums)
        {
            if (nums == null)
                return false;
            int sum = nums.Sum();

            if (sum % 2 != 0)
                return false;

            var dp = new bool[sum / 2 + 1, nums.Length + 1];   // i is all possible sub-sum to sum/2, j is 0 to j-1 sub-elements 

            //initialize top row = true
            for (int j = 0; j <= nums.Length; j++)
                dp[0, j] = true;
            //initialize first column = false (empty subset column)            
            for (int i = 1; i <= sum / 2; i++)
                dp[i, 0] = false;

            for (int i = 1; i <= sum / 2; i++)
            {
                for (int j = 1; j <= nums.Length; j++)
                {
                    dp[i, j] = dp[i, j - 1];
                    if (i >= nums[j - 1])
                        dp[i, j] = dp[i, j] || dp[i - nums[j - 1], j - 1];
                }
            }
            return dp[sum / 2, nums.Length];
        }


        //5.Longest Palindromic Substring
        //Given a string S, find the longest palindromic substring in S. You may assume that the 
        //maximum length of S is 1000, and there exists one unique longest palindromic substring.
        //Example 1: Input: "babad"
        //Output: "bab"
        //Note: "aba" is also a valid answer.
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length == 1)
                return s;

            int maxLen = 0;
            int startIdx = 0;
            //record i to j is palindrom or not
            var dpPalindrom = new bool[s.Length, s.Length];
            //for (int i = 0; i < s.Length; i++)
            //    dpPalindrom[i, i] = true;

            for (int j = 0; j < s.Length; j++)
            {
                for (int i = 0; i <= j; i++)
                {
                    if (s[i] == s[j] && (j - i <= 2 || dpPalindrom[i + 1, j - 1]))
                    {
                        dpPalindrom[i, j] = true;
                        if (maxLen < j - i + 1)
                        {
                            maxLen = j - i + 1;
                            startIdx = i;
                        }
                    }
                    else
                        dpPalindrom[i, j] = false;
                }
            }

            return s.Substring(startIdx, maxLen);
        }


        //322. Coin Change
        //You are given coins of different denominations and a total amount of money amount.
        //Write a function to compute the fewest number of coins that you need to make up that 
        //amount.If that amount of money cannot be made up by any combination of the coins, return -1.
        //Example 1:coins = [1, 2, 5], amount = 11
        //return 3 (11 = 5 + 5 + 1)
        //Example 2:coins = [2], amount = 3  return -1.
        //用dp存储硬币数量，dp[i] 表示凑齐钱数 i 需要的最少硬币数，那么凑齐钱数 amount 最少硬币数为：
        //固定钱数为 coins[j] 一枚硬币，另外的钱数为 amount - coins[j] 它的数量为dp[amount - coins[j]]，j 从0遍历到coins.length - 1：
        public int CoinChange(int[] coins, int amount)
        {
            if (coins == null || amount == 0)
                return 0;

            int[] ret = new int[amount + 1];
            for (int i = 0; i <= amount; i++)
                ret[i] = int.MaxValue;
            ret[0] = 0;

            //iterate coin value , (pick 1 of coin, the rest is ret[amount - this coin value] )
            for (int i = 0; i < coins.Length; i++)
            {
                for (int j = coins[i]; j <= amount; j++)
                {
                    if (ret[j - coins[i]] < int.MaxValue)
                        ret[j] = Math.Min(ret[j - coins[i]] + 1, ret[j]);
                }
            }

            return ret[amount] == int.MaxValue ? -1 : ret[amount];
        }


        //Reverse Fibonacci MS OTS
        //given 2 first number. 80 50 -> 80 50 30 20 10 10 0
        public int[] ReverseFibonacci(int i, int j)
        {
            //assume this is for positive 
            if (i <= 0 || j <= 0 || i < j)
                return null;

            var ret = new List<int>();
            ret.Add(i);
            ret.Add(j);
            int k = i - j;
            while (k >= 0)
            {
                ret.Add(k);
                k = ret.Last() - k;
            }
            return ret.ToArray();
        }


        //leetcode 201705 53. Maximum Subarray
        //Find the contiguous subarray within an array (containing at least one number) which has the largest sum.
        //For example, given the array[-2, 1, -3, 4, -1, 2, 1, -5, 4],
        //the contiguous subarray[4, -1, 2, 1] has the largest sum = 6.                
        public int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            //int curSum = nums[0];
            int curMax = nums[0];

            int[] dp = new int[nums.Length + 1];

            for (int i = 1; i <= nums.Length; i++)
            {
                dp[i] = dp[i - 1] + nums[i - 1] < nums[i - 1] ? nums[i - 1] : dp[i - 1] + nums[i - 1];
                curMax = Math.Max(curMax, dp[i]);
            }
            return curMax;
        }

        //91. Decode Ways
        //A message containing letters from A-Z is being encoded to numbers using the following mapping:
        //'A' -> 1,'B' -> 2,...'Z' -> 26
        //Given an encoded message containing digits, determine the total number of ways to decode it.
        //For example,Given encoded message "12", it could be decoded as "AB" (1 2) or "L" (12).
        //The number of ways decoding "12" is 2.
        //DP其中 dp[i] 表示s中前i个字符组成的子串的解码方法的个数，长度比输入数组长多多1，并将 dp[0] 初始化为1。
        public int NumDecodings2(string s)
        {
            if (s[0] == '0')
                return 0;

            int len = s.Length;
            var dp = new int[len + 1];
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= len; i++)
            {
                int prev2 = 0;
                int prev1 = 0;
                // if previous 2 digits in 1-26, prev2 is dp[i-2] ways;
                if (s[i - 2] - '0' > 0 && s[i - 2] - '0' <= 2 && s[i - 1] - '0' <= 6)
                {
                    prev2 = dp[i - 2];
                }
                // if previous 1 digit is Not 0, prev1 is to plus dp[i-1], otherwise should be 0 way
                // e.g. 10 
                if (s[i - 1] != '0')
                {
                    prev1 = dp[i - 1];
                }
                dp[i] = prev2 + prev1;
            }
            return dp[len];
        }

        public int NumDecodings(string s)
        {
            if (s == null || s.Length == 0 || s[0] == '0')
                return 0;

            int len = s.Length;
            int[] dp = new int[len + 1];
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= len; i++)
            {
                int digi2 = 0;
                int digi1 = 0;
                int prev2 = 0;
                int prev1 = 0;
                int.TryParse(s.Substring(i - 2, 2), out digi2);
                int.TryParse(s.Substring(i - 1, 1), out digi1);

                if (digi2 >= 10 && digi2 <= 26)
                    prev2 = dp[i - 2];

                if (digi1 != 0)
                    prev1 = dp[i - 1];

                dp[i] = prev2 + prev1;
            }
            return dp[len];
        }

        //70. Climbing Stairs
        //You are climbing a stair case. It takes n steps to reach to the top.
        // Each time you can either climb 1 or 2 steps.In how many distinct ways can you climb to the top?
        //       Note: Given n will be a positive integer.
        public int ClimbStairs(int n)
        {
            if (n < 3)
                return n;

            int[] ret = new int[n + 1];
            ret[0] = 0;
            ret[1] = 1;
            ret[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                ret[i] = ret[i - 1] + ret[i - 2];
            }
            return ret[n];
        }


        //256. Paint House
        //There are a row of n houses, each house can be painted with one of the three colors: red, blue or green. 
        //The cost of painting each house with a certain color is different. You have to paint all the houses such 
        //that no two adjacent houses have the same color.
        //The cost of painting each house with a certain color is represented by a n x 3 cost matrix.For example, 
        //costs[0][0] is the cost of painting house 0 with color red; costs[1][2] is the cost of painting house 1 
        //with color green, and so on...Find the minimum cost to paint all houses.
        public int MinCost(int[,] costs)
        {
            if (costs == null || costs.Length == 0)
                return 0;

            int rows = costs.GetLength(0);
            for (int r = 1; r < costs.GetLength(0); r++)
            {
                costs[r, 0] += Math.Min(costs[r - 1, 1], costs[r - 1, 2]);
                costs[r, 1] += Math.Min(costs[r - 1, 0], costs[r - 1, 2]);
                costs[r, 2] += Math.Min(costs[r - 1, 0], costs[r - 1, 1]);
            }

            return Math.Min(costs[rows - 1, 0], Math.Min(costs[rows - 1, 1], costs[rows - 1, 2]));
        }
    }


}
