using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Interview
{
    public class ArrayString
    {

        //271	Encode and Decode String
        //Design an algorithm to encode a list of strings to a string. 
        //The encoded string is then sent over the network and is decoded back to the original list of strings.
        // Machine 1 (sender) has the function:
        // string encode(vector<string> strs) {
        //   // ... your code
        //   return encoded_string;
        // }
        // Machine 2 (receiver) has the function:
        // vector<string> decode(string s) {
        //   //... your code
        //   return strs;
        // }
        public string encode(List<string> strs) {
            string res = "";
            foreach (var str in strs) 
                res += str + '\0';
            return res;
        }
        //use length of each item as prefix, then put one character, then string item
        public string encode2(List<string> strs) {
            string res = "";
            foreach (var str in strs) 
                res +=  "#" + str.Length + "/" + str;
            
            Console.WriteLine(res);    
            return res;
        }
        // Decodes a single string to a list of strings.
        public List<string> decode(string s) {
            List<string> res = new List<string>();
            var ret = s.Split('\0').ToList();

            foreach(var x in ret)
                Console.Write(x+',');
        
            return ret;
        }

        public List<string> decode2(string s) {
            List<string> ret = new List<string>();
            int i = 0;
            while(i < s.Length){
                if(s[i]=='#'){
                    int idx = s.IndexOf('/',i);
                    int len = 0;
                    
                    if(idx!=-1){
                        len = int.Parse(s.Substring(i+1, idx-i -1));
                        ret.Add(s.Substring(idx+1,len));
                        i = len+idx;
                    }

                }
                                    i++;
            }

            foreach(var x in ret)
                Console.Write(x+',');
        
            return ret;
        }
        public int solution01(int N, string S)
        {
            if (N == 0)
                return 0;

            string[] siteSet = S.Split(' ');
            var occupySites = new List<string>[N];

            for (int i = 0; i < siteSet.Length; i++)
            {
                if (string.IsNullOrEmpty(siteSet[i]))
                    continue;

                var row = Int32.Parse(Regex.Match(siteSet[i], @"\d+").Value);
                if (row > N)
                    continue;
                if (occupySites[row - 1] == null)
                    occupySites[row - 1] = new List<string>() { };

                occupySites[row - 1].Add(siteSet[i].Last().ToString());
            }
            int ret = 0;
            foreach (var row in occupySites)
            {
                if (row == null)
                {
                    ret += 3;
                    continue;
                }
                var rowStr = string.Join("", row);
                if (Regex.IsMatch(rowStr, @"[^ABC]"))
                    ret += 1;
                if (Regex.IsMatch(rowStr, @"[^H-K]"))
                    ret += 1;
                if (Regex.IsMatch(rowStr, @"[^DEF]") || Regex.IsMatch(rowStr, @"[^EFG]"))
                    ret += 1;
            }
            return ret;
        }


        //442. Find All Duplicates in an Array (in-space , not easy)
        //Given an array of integers, 1 ≤ a[i] ≤ n (n = size of array), some elements appear twice and others appear once.
        //Find all the elements that appear twice in this array.
        //Could you do it without extra space and in O(n) runtime?
        //Example:Input:[4,3,2,7,8,2,3,1]
        //Output:[2,3]
        public IList<int> FindDuplicates(int[] nums)
        {
            List<int> ret = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int pos = Math.Abs(nums[i]) - 1;

                if (nums[pos] < 0)
                    ret.Add(Math.Abs(nums[i]));
                else
                    nums[pos] = -nums[pos];
            }
            return ret;
        }


        //443. String Compression
        //Input: ["a","a","b","b","c","c","c"]
        //Output: Return 6, and the first 6 characters of the input array should be: ["a","2","b","2","c","3"]
        //Explanation:"aa" is replaced by "a2". "bb" is replaced by "b2". "ccc" is replaced by "c3".
        //e.g.2 Input:["a"]
        //Output:Return 1, and the first 1 characters of the input array should be: ["a"]
        //Explanation:Nothing is replaced.
        public int Compress(char[] chars)
        {
            int n = chars.Length, cur = 0;
            for (int i = 0, j = 0; i < n; i = j)
            {
                while (j < n && chars[j] == chars[i])
                    ++j;
                chars[cur++] = chars[i];
                if (j - i == 1)
                    continue;
                for (int c = 0; c < (j - i).ToString().Length; c++)
                    chars[cur++] = (j - i).ToString()[c];
            }
            return cur;

        }


        //expedia OA
        //974. Subarray Sums Divisible by K
        //Input: A = [4,5,0,-2,-3,1], K = 5
        //Output: 7
        //Explanation: There are 7 (contiduous) subarrays with a sum divisible by K = 5:
        //[4, 5, 0, -2, -3, 1], [5], [5, 0], [5, 0, -2, -3], [0], [0, -2, -3], [-2, -3]
        public int SubarraysDivByK(int[] A, int K)
        {
            var c = new int[K];
            c[0] = 1;
            int ans = 0;
            int sum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                sum = (sum + A[i] % K + K) % K;
                ans += c[sum];
                c[sum]++;
            }
            return ans;

        }
        public int SubarraysDivByK_(int[] A, int K)
        {
            var preSum = new int[A.Length];

            for (int i = 0; i < A.Length; i++)
            {
                preSum[i] = i == 0 ? A[0] : preSum[i - 1] + A[i];
            }
            int ret = 0;
            for (int i = 0; i < preSum.Length; i++)
            {
                if (preSum[i] % K == 0)
                    ret++;
                for (int j = 0; j < i; j++)
                {
                    if ((preSum[i] - preSum[j]) % K == 0)
                        ret++;
                }
            }
            return ret;
        }

        //expedia OA 
        //Given [1,2,3,4,1,2,2] 
        //output[2, 2, 2, 1, 1, 3, 4]
        //规则是先sort by freq, 同样的freq 就sort by value
        public int[] sortByFreqValue(int[] num)
        {

            var map = new Dictionary<int, int>();
            Array.Sort(num);
            //1,1,2,2,2,3,4
            for (int i = 0; i < num.Length; i++)
            {
                if (map.ContainsKey(num[i]))
                    map[num[i]] += 1;
                else
                    map.Add(num[i], 1);
            }
            var ret = new int[num.Length];
            int idx = 0;
            foreach (var item in map.OrderByDescending(kv => (kv.Value)))
            {
                for (int j = 0; j < item.Value; j++)
                {
                    ret[idx] = item.Key;
                    idx++;
                }
            }
            foreach (var ii in ret)
                Console.Write(ii + ",");

            return ret;
        }

        //zume phone screen 04232019
        //user1: ["abc","bef","ghi","jkl","lbj"] 
        //user2: ["abc","cf","dpi","ghi","jkl","lbj"]
        //return "ghi","jkl","lbj"
        //input are sorted with alphabet
        public List<string> LongestCommonStrArr(string[] user1, string[] user2)
        {
            int idx1 = 0;
            int idx2 = 0;
            int stIdx = 0;
            int maxLen = 0;
            int tempIdx = 0;
            int cnt = 0;
            while (idx1 < user1.Length && idx2 < user2.Length)
            {
                if (user1[idx1] == user2[idx2])
                {
                    if (cnt == 0)
                        tempIdx = idx1;
                    cnt++;
                    if (cnt > maxLen)
                    {
                        stIdx = tempIdx;
                        maxLen = cnt;
                    }

                    idx1++;
                    idx2++;
                }
                else
                {
                    cnt = 0;
                    if (user1[idx1][0] > user2[idx2][0])
                        idx2++;
                    else
                        idx1++;
                }
            }
            var ret = new List<string>();
            for (int i = stIdx; i < stIdx + maxLen; i++)
                ret.Add(user1[i]);

            return ret;
        }

        //239. Sliding Window Maximum
        //Given an array nums, there is a sliding window of size k which is moving from the very 
        //left of the array to the very right. You can only see the k numbers in the window. Each time 
        //the sliding window moves right by one position. Return the max sliding window.
        //Input: nums = [1,3,-1,-3,5,3,6,7], and k = 3
        //Output: [3,3,5,5,6,7]
        //Explanation: 
        //Window position                Max
        //---------------               -----
        //[1  3  -1] -3  5  3  6  7       3
        // 1 [3  -1  -3] 5  3  6  7       3
        // 1  3 [-1  -3  5] 3  6  7       5
        // 1  3  -1 [-3  5  3] 6  7       5
        // 1  3  -1  -3 [5  3  6] 7       6
        // 1  3  -1  -3  5 [3  6  7]      7
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return nums;

            var ret = new List<int>();
            var kList = new List<int>();
            for (int i = 0; i < k; i++)
            {
                kList.Add(nums[i]);
            }
            kList.Sort();
            ret.Add(kList.Last());

            for (int stIdx = 1; stIdx <= nums.Length - k; stIdx++)
            {
                kList.Remove(nums[stIdx - 1]);
                if (nums[stIdx + k - 1] > ret.Last()) //if this cur new item greater than previous max, add it
                {
                    ret.Add(nums[stIdx + k - 1]);
                    kList.Add(nums[stIdx + k - 1]);
                    //kList.Sort();
                    continue;
                }
                //else find max in cur k range
                kList.Add(nums[stIdx + k - 1]);
                kList.Sort();
                ret.Add(kList.Last());
            }
            return ret.ToArray();
        }

        //986. Interval List Intersections
        //Given two lists of closed intervals, each list of intervals is pairwise disjoint and in sorted order.
        //Return the intersection of these two interval lists.
        //Input: A = [[0,2],[5,10],[13,23],[24,25]], B = [[1,5],[8,12],[15,24],[25,26]]
        //Output: [[1,2],[5,5],[8,10],[15,23],[24,24],[25,25]]
        //Reminder: The inputs and the desired output are lists of Interval objects, and not arrays or lists.        
        public Interval[] IntervalIntersection(Interval[] A, Interval[] B)
        {
            //O(n^2) or O(nxm) 
            if (A == null || B == null)
                return null;
            var ret = new List<Interval>();

            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    if (A[i].end < B[j].start)
                        break;
                    else if (A[i].start > B[j].end)
                        continue;
                    else
                        ret.Add(new Interval(Math.Max(A[i].start, B[j].start), Math.Min(A[i].end, B[j].end)));
                }
            }
            return ret.ToArray();
        }

        //O(n+m)
        // j++    case 1                case 2   
        //    |---------|             |----|
        //      |----|             |----|         
        public Interval[] IntervalIntersection2(Interval[] A, Interval[] B)
        {
            if (A == null || B == null)
                return null;
            var ret = new List<Interval>();

            int i = 0, j = 0;

            while (i < A.Length && j < B.Length)
            {
                if (A[i].end < B[j].start)
                    i++;
                else if (A[i].start > B[j].end)
                    j++;
                else
                {
                    ret.Add(new Interval(Math.Max(A[i].start, B[j].start), Math.Min(A[i].end, B[j].end)));
                    if (A[i].end > B[j].end)
                        j++;
                    else
                        i++;
                }
            }
            return ret.ToArray();
        }


        //487. Max Consecutive Ones II
        //Find the max length of consecutive ones with one flip (0 => 1)
        //e.g. 11101001 ->  max = 5, if flip first 0 to 1
        int findMaxConsecutiveOnes(int[] nums)
        {
            int ret = 0, leftCnt = 0, rightCnt = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                    rightCnt++;
                if (nums[i] == 0)
                {
                    ret = Math.Max(ret, leftCnt + rightCnt);
                    leftCnt = rightCnt;
                    rightCnt = 0;
                }
            }
            if (rightCnt != 0)
                ret = Math.Max(ret, leftCnt + rightCnt);

            return ret + 1;  //plus flip 0 
        }
        // Follow up:
        // What if K (>=0) flips is allowed? 
        // 1. Then, we are interested in where the last K 0s are.
        // 2. By using a queue to track the positions of previous 0s. If the size of queue is more than K, we need immediately remove the front of queue, let's say p, then l = p + 1.
        // 3. In the code, I treat l as the previous slot of the window to get rid of +1 or -1.
        // 4. Note that the queue technique can deal with input stream, if it is not a fixed vector.
        // 5. But a long or long long or other big_integer is needed to track the ans, since it may be very large without MOD 1e9+7.
        int findMaxConsecutiveOnes(int[] nums, int K)
        {
            int l = -1, n = nums.Length;
            var que = new Queue<int>();

            int ans = 0;
            for (int i = 0; i < n; i++)
            {
                if (nums[i] == 0)
                {
                    que.Enqueue(i);
                }
                if (que.Count > K)
                {
                    l = que.Dequeue();
                }
                ans = Math.Max(ans, i - l);
            }
            return ans;
        }


        //189. Rotate Array
        //Given an array, rotate the array to the right by k steps, where k is non-negative.
        //Example 1:Input: [1,2,3,4,5,6,7]        and k = 3
        //Output: [5,6,7,1,2,3,4]
        //Explanation:
        //rotate 1 steps to the right: [7,1,2,3,4,5,6]
        //rotate 2 steps to the right: [6,7,1,2,3,4,5]
        //rotate 3 steps to the right: [5,6,7,1,2,3,4]
        //Example 2: Input: [-1,-100,3,99]    and k = 2
        //Output: [3,99,-1,-100]
        //Explanation: rotate 1 steps to the right: [99,-1,-100,3]
        //rotate 2 steps to the right: [3,99,-1,-100]
        //Note:Try to come up as many solutions as you can, there are at least 3 different ways to solve this problem.
        //Could you do it in-place with O(1) extra space?
        public void Rotate(int[] nums, int k)
        {
            if (nums == null)
                return;
            var ret = new int[nums.Length];

            Array.Copy(nums, ret, nums.Length);

            for (int i = 0; i < nums.Length; i++)
            {
                nums[(i + k) % nums.Length] = ret[i];
            }

        }
        //space: O(1)
        public void Rotate2(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || (k %= nums.Length) == 0)
                return;
            reverseParial(nums, 0, nums.Length - k - 1);
            reverseParial(nums, nums.Length - k, nums.Length - 1);
            reverseParial(nums, 0, nums.Length - 1);
        }

        void reverseParial(int[] nums, int st, int end)
        {
            while (st < end)
            {
                var temp = nums[st];
                nums[st] = nums[end];
                nums[end] = temp;
                st++;
                end--;
            }
        }

        //438. Find All Anagrams in a String
        //Given a string s and a non-empty string p, find all the start indices of p's anagrams in s.    
        // Strings consists of lowercase English letters only and the length of both strings s and p will not be larger than 20,100.
        // The order of output does not matter.
        // Example 1: Input: s: "cbaebabacd" p: "abc"
        // Output: [0, 6]
        public IList<int> FindAnagrams(string s, string p)
        {
            var ret = new List<int>();
            if (string.IsNullOrEmpty(s) || s.Length < p.Length)
                return ret;

            int n = s.Length;
            int l = p.Length;

            var arrP = new int[26];
            var arrS = new int[26];

            for (int i = 0; i < p.Length; i++)
                arrP[p[i] - 'a']++;

            for (int i = 0; i < n; i++)
            {
                if (i >= l)
                    arrS[s[i - l] - 'a']--;
                arrS[s[i] - 'a']++;
                if (arrP.SequenceEqual(arrS))
                    ret.Add(i - l + 1);
            }
            return ret;
        }


        //31. Next Permutation
        //Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.
        //If such arrangement is not possible, it must rearrange it as the lowest possible order(ie, sorted in ascending order).
        //The replacement must be in-place and use only constant extra memory.
        //Here are some examples.Inputs are in the left-hand column and its corresponding outputs are in the right-hand column.
        //1,2,3 → 1,3,2
        //3,2,1 → 1,2,3
        //1,1,5 → 1,5,1
        public void NextPermutation(int[] nums)
        {
            if (nums == null)
                return;

            //find from tail digits index until it becomes descendent (find first descendent index from tail) 
            int fDescIdx = int.MaxValue;

            for (int i = nums.Length - 1; i > 0; i--)
            {
                if (nums[i] > nums[i - 1])
                {
                    fDescIdx = i - 1;
                    break;
                }
            }
            if (fDescIdx == int.MaxValue)  //input is all ascedent from tail, sort whole nums
            {
                Array.Sort(nums);
                //int st = 0, end = nums.Length - 1;
                // while (st < end)
                // {   //swap 
                //     int temp = nums[st];
                //     nums[st] = nums[end];
                //     nums[end] = temp;
                //     st++;
                //     end--;
                // }
            }
            else
            {
                //swap fDescIdx with right min greater than fDescIdx value
                //find out right min greater than fDescIdx value (first greater than fDescIdx value from left)
                int exchangeIndex = -1;
                for (int i = nums.Length - 1; i > fDescIdx; i--)
                {
                    if (nums[i] > nums[fDescIdx])
                    {
                        exchangeIndex = i;
                        break;
                    }
                }
                int temp = nums[exchangeIndex];
                nums[exchangeIndex] = nums[fDescIdx];
                nums[fDescIdx] = temp;

                Array.Sort(nums, fDescIdx + 1, nums.Length - 1 - fDescIdx);
            }
        }


        //Envestnet simple data structure question with 2 sets of numbers. Which number is in list A, that is not in list B.
        List<int> inAnotInB(int[] A, int[] B)
        {
            var hs = new HashSet<int>();
            foreach (var x in B)
                hs.Add(x);

            var ret = new List<int>();
            foreach (var a in A)
            {
                if (!hs.Contains(a))
                    ret.Add(a); ;
            }
            return ret;
        }

        //402  Remove K Digits
        //Given a non-negative integer num represented as a string, remove k digits from the number so that the 
        //new number is the smallest possible.
        //Note:The length of num is less than 10002 and will be ≥ k.The given num does not contain any leading zero.
        //Example 1: Input: num = "1432219", k = 3   1412219
        //Output: "1219" Explanation: Remove the three digits 4, 3, and 2 to form the new number 1219 which is the smallest.
        //Example 2:
        //Input: num = "10200", k = 1
        //Output: "200"
        //Explanation: Remove the leading 1 and the number is 200. Note that the output must not contain leading zeroes.
        //Example 3:
        //Input: num = "10", k = 2
        //Output: "0"
        //Explanation: Remove all the digits from the number and it is left with nothing which is 0.
        string removeKdigits(string num, int k)
        {
            if (string.IsNullOrEmpty(num) || k == num.Length)
                return "0";

            string ret = "";
            int len = k;
            for (int i = 0; i < num.Length; i++)
            {
                while (ret.Length > 0 && k > 0 && (num[i] - ret.Last()) < 0)
                {
                    ret = ret.Remove(ret.Length - 1);
                    k--;
                }
                ret += num[i];

            }
            if (k > 0)
                ret = ret.Substring(0, num.Length - len);

            while (ret.Length > 0 && ret[0] == '0')
            {
                ret = ret.Remove(0, 1);
            }
            return ret.Length > 0 ? ret : "0";

        }
        //678. Valid Parenthesis String
        //Given a string containing only three types of characters: '(', ')' and '*', write a function to check whether this string is valid.We define the validity of a string by these rules:
        //Any left parenthesis '(' must have a corresponding right parenthesis ')'.
        //Any right parenthesis ')' must have a corresponding left parenthesis '('.
        //Left parenthesis '(' must go before the corresponding right parenthesis ')'.
        //'*' could be treated as a single right parenthesis ')' or a single left parenthesis '(' or an empty string.
        //An empty string is also valid.
        //Example 1:Input: "()" Output: True
        //Example 2:Input: "(*)"Output: True
        //Example 3:Input: "(*))"Output: True
        //正反各遍历一次，正向遍历的时候，我们把星号都当成左括号，此时用经典的验证括号的方法，即遇左括号计数器加1，遇右括号则自减1，如果中间某个时刻计数器小于0了，直接返回false。如果最终计数器等于0了，我们直接返回true，因为此时我们把星号都当作了左括号，可以跟所有的右括号抵消。而此时就算计数器大于0了，我们暂时不能返回false，因为有可能多余的左括号是星号变的，星号也可以表示空，所以有可能不多，我们还需要反向q一下，哦不，是反向遍历一下，这是我们将所有的星号当作右括号，遇右括号计数器加1，遇左括号则自减1，如果中间某个时刻计数器小于0了，直接返回false。遍历结束后直接返回true，这是为啥呢？此时计数器有两种情况，要么为0，要么大于0。为0不用说，肯定是true，为啥大于0也是true呢？因为之前正向遍历的时候，我们的左括号多了，我们之前说过了，多余的左括号可能是星号变的，也可能是本身就多的左括号。本身就多的左括号这种情况会在反向遍历时被检测出来，如果没有检测出来，说明多余的左括号一定是星号变的。而这些星号在反向遍历时又变做了右括号，最终导致了右括号有剩余，所以当这些星号都当作空的时候，左右括号都是对应的，即是合法的。
        public bool CheckValidString2(string s)
        {
            if(string.IsNullOrEmpty(s))
                return true;
            int left = 0;
            int right = 0;
            
            //iterate from left, assume * as left 
            foreach(var c in s){
                if(c=='(' || c=='*')
                    left++;
                else
                    left--;
                if(left < 0)
                    return false;                    
            }
            if(left==0)
                return true;
            
            //iterate from right to left, assume * as right 
            for(int i = s.Length-1; i>=0; i--){
                if(s[i]==')' || s[i]=='*')
                    right++;
                else 
                    right--;
                if(right <0)
                    return false;                            
            }    
            return true;
        }

        public bool CheckValidString(string s)
        {
            if(string.IsNullOrEmpty(s))
                return true;
            //2 stacks saves left parenthesis and star, use int to remember index
            var st1 = new Stack<int>();
            var stStar = new Stack<int>();

            for(int i =0; i< s.Length; i++){
                if(s[i]=='(')
                    st1.Push('(');
                else if(s[i]=='*')
                    stStar.Push('*');
                else{
                    if(st1.Count > 0)
                        st1.Pop();
                    else if(stStar.Count >0)
                        stStar.Pop();
                    else if(st1.Count==0 && stStar.Count==0)
                        return false;        
                }
            }

            while(st1.Count!=0 && stStar.Count!=0){
                if(st1.Peek() > stStar.Peek())
                    return false;
                st1.Pop();    
                stStar.Pop();
            }
            return st1.Count ==0 || st1.Count<=stStar.Count;
        }


        //28. Implement strStr()
        //Input: haystack = "hello", needle = "ll"
        //Output: 2
        public int StrStr(string haystack, string needle)
        {
            if (haystack == null || needle == null || needle.Length > haystack.Length)
                return -1;
            if (haystack.Equals(needle))
                return 0;
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                if (needle == haystack.Substring(i, needle.Length))
                    return i;
            }
            return -1;
        }


        //692. Top K Frequent Words
        //Given a non-empty list of words, return the k most frequent elements.
        //Your answer should be sorted by frequency from highest to lowest.If two words have the same frequency, then the word with the lower alphabetical order comes first.
        //Example 1:    Input: ["i", "love", "leetcode", "i", "love", "coding"], k = 2
        //Output: ["i", "love"]
        //Explanation: "i" and "love" are the two most frequent words.
        //Note that "i" comes before "love" due to a lower alphabetical order.
        public IList<string> TopKFrequent(string[] words, int k)
        {
            var ret = new List<string>();
            if (words == null || words.Length == 0)
                return ret;

            var map = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (!map.ContainsKey(words[i]))
                    map.Add(words[i], 1);
                else
                    map[words[i]]++;
            }
            return map.OrderByDescending(pair => pair.Value).ThenBy(x => x.Key).Take(k).Select(x => x.Key).ToList();
        }


        //60. Permutation Sequence
        //The set [1,2,3,…,n] contains a total of n! unique permutations.
        //By listing and labeling all of the permutations in order,
        //We get the following sequence(ie, for n = 3):
        //"123"
        //"132"
        //"213"
        //"231"
        //"312"
        //"321"
        //Given n and k, return the kth permutation sequence.Note: Given n will be between 1 and 9 inclusive.
        public string GetPermutation(int n, int k)
        {
            if (n < 1 || k < 1)
                return "";
            //generate factorial array e.g. n=4 , [1,1,2,6] => [0!,1!,2!,3!] 
            var factorial = new int[n];
            factorial[0] = 1;
            for (int i = 1; i < n; i++)
                factorial[i] = factorial[i - 1] * i;

            //generate candidate array
            var candidateSet = new List<int>();
            for (int i = 1; i <= n; i++)
                candidateSet.Add(i);

            var ret = new StringBuilder();

            k--; // if k= 2 , will be 132 (the last element of prefix =1, 1(k-1) % 2 = 1 <-- index of candidateSet   
            //iterate start from 3! to 0!  
            for (int i = n - 1; i >= 0; i--)
            {
                ret.Append(candidateSet[k % factorial[i]]);
                candidateSet.RemoveAt(k % factorial[i]);
                k %= factorial[i];

            }
            return ret.ToString();
        }
        //hint:定ｎ个数字让求第k个序列．ｎ个数字总共的全排列最多有n!个，并且全排列有个规律．
        //给定一个n，以１开头的排列有(n-1)!个，同样对２和３也是，所以如果k小于等于(n-1)!，那么首位必为１，因为以１开头的全排列有(n-1)!个．
        //同样如果k大于(n-1)!，那么第一个数就应该为(k-1)/(n-1)! + 1．这样找到了首位数字应该是哪个，剩下了(n-1)个数字，
        //我们只需要再重复上面的步骤，不断缩小k即可．

        //38. Count and Say
        //1.     1
        //2.     11     1 is read off as "one 1" or 11
        //3.     21     11 is read off as "two 1s" or 21.
        //4.     1211   21 is read off as "one 2, then one 1" or 1211.
        //5.     111221
        public string CountAndSay(int n)
        {
            if (n <= 1)
                return "1";

            StringBuilder sb = new StringBuilder();
            int ini = 1;
            string curStr = "1";

            while (ini < n)
            {
                int count = 1;
                for (int i = 1; i < curStr.Length; i++)
                {
                    if (curStr[i] != curStr[i - 1])
                    {
                        sb.Append(count.ToString()).Append(curStr[i - 1].ToString());
                        count = 1;
                    }
                    else
                        count++;
                }
                sb.Append(count.ToString()).Append(curStr.Last().ToString());
                curStr = sb.ToString();
                sb.Clear();
                ini++;

            }
            return curStr;
        }


        //219. Contains Duplicate II
        //Given an array of integers and an integer k, find out whether there are two distinct indices i and j in the array such that nums[i] = nums[j] and the absolute difference between i and j is at most k.
        //Example 1: Input: nums = [1,2,3,1], k = 3  Output: true
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (nums == null && nums.Length == 0)
                return false;

            //var set = new HashSet<int>();
            var dMap = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dMap.ContainsKey(nums[i]))
                    dMap.Add(nums[i], new List<int>() { i });
                else
                    dMap[nums[i]].Add(i);
            }

            foreach (var pair in dMap)
            {
                if (pair.Value.Count <= 1)
                    continue;
                else
                {
                    for (int i = 0; i < pair.Value.Count - 1; i++)
                    {
                        if (pair.Value[i + 1] - pair.Value[i] == k)
                            return true;
                    }
                }
            }

            return false;
        }


        //220. Contains Duplicate III
        //Given an array of integers, find out whether there are two distinct indices i and j in the array 
        //such that the absolute difference between nums[i] and nums[j] is at most t and the absolute 
        //difference between i and j is at most k.
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            //use bucket in slide window range k, if any 2 in the same bucket or adjacent bucket within t is true
            // bucket[idx] idx = floor[num[i] / t]  
            //find min and (nums[i] - min) / t+1  as bucket index   (t+1 prevent t=0)
            if (nums == null || nums.Length == 0 || k <= 0 || t < 0)
                return false;
            //find min 
            int min = nums.Min();
            int diff = t + 1;
            var map = new Dictionary<long, int>(); // record bucket idx as key , nums[i] as value
            for (int i = 0; i < nums.Length; i++)
            {
                long bucketIdx = ((long)nums[i] - (long)min) / diff;

                //if this bucket already has value, then means another number is in the same range
                if (map.ContainsKey(bucketIdx))
                    return true;
                //adjacent bucket might have value in range too, so check left and right neighbors
                if (map.ContainsKey(bucketIdx + 1) && Math.Abs(map[bucketIdx + 1] - nums[i]) <= t)
                    return true;
                if (map.ContainsKey(bucketIdx - 1) && Math.Abs(map[bucketIdx - 1] - nums[i]) <= t)
                    return true;

                map.Add(bucketIdx, nums[i]);
                if (i >= k)
                {
                    map.Remove(((long)nums[i - k] - (long)min) / diff);
                }
            }
            return false;
        }

        //4. Median of Two Sorted Arrays
        //There are two sorted arrays nums1 and nums2 of size m and n respectively.
        //Find the median of the two sorted arrays.The overall run time complexity should be O(log (m+n)).
        //Example 1: nums1 = [1, 3]   nums2 = [2]    The median is 2.0
        //Example 2: nums1 = [1, 2]   nums2 = [3, 4] The median is (2 + 3)/2 = 2.5
        public double findMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;
            if (n1 > n2)
                return findMedianSortedArrays(nums2, nums1);

            int k = (n1 + n2 + 1) / 2;  //mid count of all nums
            int l = 0;       // left segment start   
            int r = n1;      // left segment end
            int m1 = 0;
            int m2 = 0;

            while (l < r)
            {
                m1 = l + (r - l) / 2;
                m2 = k - m1;
                if (nums1[m1] < nums2[m2 - 1])
                    l = m1 + 1;
                else
                    r = m1;
            }

            m1 = l;
            m2 = k - l;

            int c1 = Math.Max(m1 <= 0 ? int.MinValue : nums1[m1 - 1],
                              m2 <= 0 ? int.MinValue : nums2[m2 - 1]);

            if ((n1 + n2) % 2 == 1)
                return c1;

            int c2 = Math.Min(m1 >= n1 ? int.MaxValue : nums1[m1],
                              m2 >= n2 ? int.MaxValue : nums2[m2]);

            return (c1 + c2) * 0.5;
        }


        //209. Minimum Size Subarray Sum
        //Given an array of n positive integers and a positive integer s, find the minimal length of a contiguous 
        //subarray of which the sum ≥ s.If there isn't one, return 0 instead.
        //Example: Input: s = 7, nums = [2,3,1,2,4,3]
        //        Output: 2
        //Explanation: the subarray[4, 3] has the minimal length under the problem constraint.
        //Follow up:
        //If you have figured out the O(n) solution, try coding another solution of which the time complexity is O(n log n). 
        public int MinSubArrayLen(int s, int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int frontIdx = 0;
            int backIdx = 0;
            int sum = 0;
            int min = int.MaxValue;
            while (frontIdx < nums.Length)
            {
                sum += nums[frontIdx];
                frontIdx++;
                while (sum >= s && frontIdx > backIdx)
                {
                    min = Math.Min(min, frontIdx - backIdx);
                    sum -= nums[backIdx];
                    backIdx++;
                }
            }
            return min == int.MaxValue ? 0 : min;
        }


        //165. Compare Version Numbers
        //Compare two version numbers version1 and version2.
        //If version1 > version2 return 1, if version1<version2 return -1, otherwise return 0.
        //You may assume that the version strings are non-empty and contain only digits and the.character.
        //The.character does not represent a decimal point and is used to separate number sequences.
        //For instance, 2.5 is not "two and a half" or "half way to version three", it is the fifth second-level revision of the second first-level revision.
        //Here is an example of version numbers ordering: 0.1 < 1.1 < 1.2 < 13.37
        public int CompareVersion(string version1, string version2)
        {
            if (string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2))
                return 0;

            //check format
            var regex2 = new Regex("^[0-9]+([.]{1}[0-9]+)*$");

            var v1s = version1.Split('.');
            var v2s = version2.Split('.');

            int len = Math.Min(v1s.Length, v2s.Length);

            for (int i = 0; i < len; i++)
            {
                if (int.Parse(v1s[i]) > int.Parse(v2s[i]))
                    return 1;
                if (int.Parse(v1s[i]) < int.Parse(v2s[i]))
                    return -1;
            }

            if (v1s.Length < v2s.Length)
            {
                for (int i = v1s.Length; i < v2s.Length; i++)
                {
                    if (int.Parse(v2s[i]) != 0)
                        return -1;
                }
                return 0;
            }
            if (v1s.Length > v2s.Length)
            {
                for (int i = v2s.Length; i < v1s.Length; i++)
                {
                    if (int.Parse(v1s[i]) != 0)
                        return 1;
                }
                return 0;
            }
            return 0;
        }

        //415. Add Strings
        public string AddStrings(string num1, string num2) {
            int l1 = num1.Length-1;
            int l2 = num2.Length-1;
            int carry = 0;
            var ret = new StringBuilder();
            
            while(l1 >= 0 || l2 >= 0){
                int a = l1 >= 0 ? num1[l1--]-'0' : 0;
                int b = l2 >= 0 ? num2[l2--]-'0' : 0;
                ret.Insert(0,(carry + a + b) %10);
                carry=(carry + a + b)/10;
            }
            if(carry!=0)
                ret.Insert(0,carry);

            return ret.ToString();
        }

        //43. Multiply Strings
        //Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and 
        //num2, also represented as a string.
        //e.g. 1: Input: num1 = "2", num2 = "3" Output: "6"
        //e.g.2: Input: num1 = "123", num2 = "456"  Output: "56088"
        public string Multiply2(string num1, string num2)
        {
            if(string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
                return "0";
            int n = num1.Length;
            int m = num2.Length;    
            var ret = new int[n+m];

            for(int i = n-1; i>=0; i--){
                for(int j = m-1; j>=0; j--){                
                    ret[i+j] += (num1[i]*num2[j]) % 10;
                    if(i+j>0)
                        ret[i+j-1]+=(num1[i]*num2[j]) / 10;                
                }
            }
            string rr = "";
            int k =0;
            while(ret[k]==0){
                k++;
            }
            for(int  l=k; l< ret.Length; l++){
                rr+=ret[l];
            }
            return rr;
        }
        public string Multiply(string num1, string num2)
        {
            if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
                return "0";

            var retArr = new int[num1.Length + num2.Length];

            var sb = new StringBuilder();

            for (int i = num1.Length - 1; i >= 0; i--)
            {
                for (int j = num2.Length - 1; j >= 0; j--)
                {
                    retArr[i + j + 1] += (num1[i] - '0') * (num2[j] - '0');
                    retArr[i + j] += retArr[i + j + 1] / 10;
                    retArr[i + j + 1] = retArr[i + j + 1] % 10;
                }
            }
            for (int i = 0; i < retArr.Length; i++)
            {
                if (retArr[i] == 0 && sb.Length == 0)
                    continue;
                sb.Append(retArr[i]);
            }
            if (sb.Length == 0)
                return "0";
            return sb.ToString();
        }


        //MS onsite 
        //find out max and second max value in array
        public int[] FindMaxAndSecondMax(int[] nums)
        {
            if (nums == null || nums.Length < 2)
                return null;

            var ret = new List<int>();
            int max = int.MinValue;
            int smax = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > max)
                {
                    smax = max;
                    max = nums[i];
                }
                else if (nums[i] > smax && nums[i] != max)
                {
                    smax = nums[i];
                }
            }
            if (smax == int.MinValue)
                throw new Exception("no second largest element");
            ret.Add(max);
            ret.Add(smax);
            return ret.ToArray();
        }

        //75. Sort Colors (MS OA)
        //Given an array with n objects colored red, white or blue, sort them so that objects of the same 
        //color are adjacent, with the colors in the order red, white and blue.
        //Here, we will use the integers 0, 1, and 2 to represent the color red, white, and blue respectively.
        //Note:You are not suppose to use the library's sort function for this problem.
        public void SortColors(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return;

            int ptR = 0;
            int ptB = nums.Length - 1;

            for (int i = ptR; i <= ptB;)
            {
                if (nums[i] == 0)
                {
                    swapColor(nums, i, ptR);
                    ptR++;
                    i++;
                }
                else if (nums[i] == 2)
                {
                    swapColor(nums, i, ptB);
                    ptB--;
                }
                else
                    i++;
            }
        }

        void swapColor(int[] nums, int i, int j)
        {
            if (nums == null || nums.Length == 0 || i == j)
                return;
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        //125. Valid Palindrome
        //Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.
        //For example, "A man, a plan, a canal: Panama" is a palindrome. "race a car" is not a palindrome.
        // Note:Have you consider that the string might be empty? This is a good question to ask during an interview.
        // For the purpose of this problem, we define empty string as valid palindrome.
        public bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            int i = 0;
            int j = s.Length - 1;

            while (i < j)
            {
                while (i < s.Length && !(char.IsLetter(s[i]) || char.IsNumber(s[i])))
                    i++;

                while (j >= 0 && !(char.IsLetter(s[j]) || char.IsNumber(s[j])))
                    j--;

                if (i >= j)
                    return true;

                if (s[i].ToString().ToUpper() != s[j].ToString().ToUpper())
                    return false;
                i++;
                j--;
            }
            return true;
        }


        //161. One Edit Distance(substring)
        //Given two strings S and T, determine if they are both one edit distance apart.
        //如果字符串长度相等，那么判断对应位置不同的字符数是不是1即可。
        //如果字符串长度相差1，那么肯定是要在长的那个串删掉一个，所以两个字符串一起加加，一旦遇到一个不同，
        //那么剩下的子串就要是一样，否则就是不止一个不同，false。
        public bool isOneEditDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
                return false;
            if (Math.Abs(s.Length - t.Length) == 1)
                return true;
            if (s.Length == t.Length)
            {
                if (s == t)
                    return false;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != t[i])
                    {
                        if (i + 1 == s.Length)
                            return true;
                        return s.Substring(i + 1, s.Length - i) == t.Substring(i + 1, s.Length - i);
                    }
                }
            }
            return false;
        }


        //157. Read N Characters Given Read4  
        //The API: int read4(char *buf) reads 4 characters at a time from a file. 
        //The return value is the actual number of characters read.For example, it returns 3 
        //if there is only 3 characters left in the file.
        //By using the read4 API, implement the function int read(char* buf, int n) that reads n characters from the file.
        //Note:The read function will only be called once for each test case.
        int read(char[] buf, int n)
        {
            char[] buf4 = new char[4];
            int i = 0;
            while(i < n){
                int read_len = read4(buf4);
                if(read_len == 0)
                    return i;

                int addChar = Math.Min(n-i, read_len);
                for(int j =0; j<addChar; j++){
                    buf[i+j] = buf4[j];
                }
                i += addChar;
            }
            return i;
            
            // for(int i =0; i< count ; i++){
            //     res+=
            // }

            
        }
        int read4(char[] input){
            return 1;
        }

        //253 meeting room2  (groupon phone)
        //Given an array of meeting time intervals consisting of start and end times[[s1, e1],[s2, e2],...] 
        //(si<ei), find the minimum number of conference rooms required.
        //e.g. Given[[0, 30],[5, 10],[15, 20]], return 2.
        int minMeetingRooms(Interval[] intervals)
        {
            if (intervals == null || intervals.Length == 0)
                return 0;

            int len = intervals.Length;
            int[] starts = new int[len];
            int[] ends = new int[len];
            for (int i = 0; i < len; i++)
            {
                starts[i] = intervals[i].start;
                ends[i] = intervals[i].end;
            }
            Array.Sort(starts);
            Array.Sort(ends);


            int ret = 0;
            int endIdx = 0;
            for (int i = 0; i < len; i++)
            {
                if (starts[i] < ends[endIdx])
                    ret++;
                else
                    endIdx++;
            }
            return ret;
        }

        //alternative way not verified
        int MeetingRoomsII(Interval[] intervals)
        {
            if (intervals == null || intervals.Length == 0)
                return 0;
            if (intervals.Length == 1)
                return 1;

            int ret = 1;
            int len = intervals.Length;
            int[] starts = new int[len];
            int[] ends = new int[len];
            for (int i = 0; i < len; i++)
            {
                starts[i] = intervals[i].start;
                ends[i] = intervals[i].end;
            }
            Array.Sort(starts);
            Array.Sort(ends);
            //above is the same
            int endTimeIdx = 0;
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i].start < ends[endTimeIdx])
                    ret++;
                else
                    endTimeIdx++;
            }
            return ret;
        }

        //252. Meeting room
        //Given an array of meeting time intervals consisting of start and end times
        //[[s1, e1],[s2, e2],...] (si<ei), determine if a person could attend all meetings.
        //For example,
        //Given[[0, 30],[5, 10],[15, 20]],return false.
        public bool canAttendMeetings(Interval[] intervals)
        {
            if (intervals == null || intervals.Length == 0)
                return false;
            if (intervals.Length == 1)
                return true;

            Array.Sort(intervals, (Interval a, Interval b) => { return a.start.CompareTo(b.start); });

            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i].start < intervals[i - 1].end)
                    return false;
            }
            return true;
        }


        //56. Merge Intervals
        //Given a collection of intervals, merge all overlapping intervals.
        //For example,  Given[1, 3],[2, 6],[8, 10],[15, 18],  return [1,6],[8,10],[15,18].
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            List<Interval> ret = new List<Interval>();
            if (intervals == null || intervals.Count == 0)
                return ret;

            intervals = intervals.OrderBy(x => x.start).ToList();

            int curEnd = intervals[0].end;
            int curStart = intervals[0].start;

            for (int i = 1; i < intervals.Count; i++)
            {
                if (intervals[i].start <= curEnd)
                {
                    curEnd = Math.Max(curEnd, intervals[i].end);
                }
                else
                {
                    ret.Add(new Interval(curStart, curEnd));
                    curEnd = intervals[i].end;
                    curStart = intervals[i].start;
                }
            }
            ret.Add(new Interval(curStart, curEnd));
            return ret;
        }
        public class Interval
        {
            public int start;
            public int end;
            public Interval() { start = 0; end = 0; }
            public Interval(int s, int e) { start = s; end = e; }
        }


        //Flexe onsite: merge 2 people's time schedule and filter out both available time by given time frame 
        public List<Interval2> AvialbleTime(List<Interval2> c1, List<Interval2> c2, float start, float end, float duration)
        {
            var ret = new List<Interval2>();
            if (c1 == null && c2 == null)
                return ret;

            var merged = MergeTakenTime(c1, c2, start, end);
            var curAvialbelSt = start;
            var finalENd = end;
            // find available time 
            for (int i = 0; i < merged.Count; i++)
            {
                if (curAvialbelSt < merged[i].start && merged[i].start - curAvialbelSt >= duration)
                {
                    ret.Add(new Interval2(curAvialbelSt, merged[i].start));
                    curAvialbelSt = merged[i].end;
                }
            }
            if (ret.Count == 0)
                return ret;
            if (finalENd > ret.Last().end)
                ret.Add(new Interval2(curAvialbelSt, finalENd));
            else
                ret.Last().end = finalENd;
            
            return ret;
        }

        public IList<Interval2> MergeTakenTime(IList<Interval2> c1, IList<Interval2> c2, float start, float end)
        {
            List<Interval2> ret = new List<Interval2>();

            if (c1 == null || c1.Count == 0)
                return c2;
            if (c2 == null || c2.Count == 0)
                return c1;

            c1 = c1.OrderBy(x => x.start).ToList();
            c2 = c2.OrderBy(x => x.start).ToList();

            float curStart = Math.Min(c1[0].start, c2[0].start);
            float curEnd = Math.Max(c1[0].end, c2[0].end);

            int minLen = Math.Min(c1.Count, c2.Count);
            for (int i = 1; i < minLen; i++)
            {
                if (start > Math.Min(c1[i].start, c2[i].start))
                    continue;
                if (end < Math.Max(c1[i].end, c2[i].end))
                    break;

                if (Math.Min(c1[i].start, c2[i].start) <= curEnd)
                    curEnd = Math.Max(curEnd, Math.Max(c1[i].end, c2[i].end));
                else
                {
                    ret.Add(new Interval2(curStart, curEnd));
                    curEnd = Math.Max(c1[i].end, c2[i].end);
                    curStart = Math.Min(c1[i].start, c2[i].start);
                }
            }
            ret.Add(new Interval2(curStart, curEnd));

            if (c1.Count > minLen)
            {
                for (int i = minLen; i < c1.Count; i++)
                    if (c1[i].start >= ret.Last().end)
                        ret.Add(c1[i]);
            }
            if (c2.Count > minLen)
            {
                for (int i = minLen; i < c2.Count; i++)
                {
                    if (c2[i].start >= ret.Last().end)
                        ret.Add(c2[i]);
                }
            }
            return ret;
        }

        public class Interval2
        {
            public float start;
            public float end;
            public Interval2() { start = 0; end = 0; }
            public Interval2(float s, float e) { start = s; end = e; }
        }


        //22. Generate Parentheses
        //Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
        //        For example, given n = 3, a solution set is:
        //[
        //  "((()))",
        //  "(()())",
        //  "(())()",
        //  "()(())",
        //  "()()()"
        //]
        public IList<string> GenerateParenthesis(int n)
        {
            List<string> ret = new List<string>();
            if (n == 0)
                return ret;

            helper(ret, n, "", 0, 0);
            return ret;
        }


        void helper(List<string> ret, int n, string cur, int st, int end)
        {
            if (cur.Length == n * 2)
            {
                ret.Add(cur);
                return;
            }
            if (st < n)
            {
                helper(ret, n, cur + "(", st + 1, end);
            }
            if (end < st)
            {
                cur += ")";
                helper(ret, n, cur, st, end + 1);
            }
        }

        //67. Add Binary
        //Given two binary strings, return their sum (also a binary string).
        // For example,        a = "11"  b = "1", return "100".
        public string AddBinary(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return "0";
            if (string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
                return b;
            if (!string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return a;

            int idxA = a.Length - 1;
            int idxB = b.Length - 1;
            int carry = 0;
            string ret = "";

            while (idxA >= 0 || idxB >= 0 || carry > 0)
            {
                int valA = 0;
                int valB = 0;
                if (idxA >= 0)
                    valA = a[idxA] - '0';
                if (idxB >= 0)
                    valB = b[idxB] - '0';

                ret = (valA + valB + carry) % 2 + ret;
                carry = (valA + valB + carry) / 2;
                idxA--;
                idxB--;
            }

            return ret;
        }

        //17. Letter Combinations of a Phone Number
        //Given a digit string, return all possible letter combinations that the number could represent.        
        //Input:Digit string "23"
        //Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
        public IList<string> LetterCombinations(string digits)
        {
            string[] keymap = new string[10] { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

            List<string> ret = new List<string>();
            if (digits.Length == 0)
                return ret;

            List<string> keySets = new List<string>();
            for (int i = 0; i < digits.Length; i++)
            {
                if (!string.IsNullOrEmpty(keymap[digits[i] - '0']))
                    keySets.Add(keymap[digits[i] - '0']);
            }

            backtrackingHelp(ret, digits.Length, keySets, "", 0);
            return ret;
        }

        void backtrackingHelp(List<string> ret, int len, List<string> keySets, string cur, int idx)
        {
            if (cur.Length == len)
            {
                ret.Add(cur);
                return;
            }

            for (int i = idx; i < keySets.Count; i++)
            {
                for (int j = 0; j < keySets[i].Length; j++)
                {
                    cur += keySets[i][j];
                    backtrackingHelp(ret, len, keySets, cur, i + 1);
                    cur = cur.Remove(cur.Length - 1);
                }
            }
        }

        //242. Valid Anagram
        //For example,  s = "anagram", t = "nagaram", return true.
        //s = "rat", t = "car", return false.
        public bool IsAnagram(string s, string t)
        {
            if (s == null || t == null || s.Length != t.Length)
                return false;
            if (s.Length == 0)
                return true;

            var map = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                    map[s[i]]++;
                else
                    map.Add(s[i], 1);
            }
            for (int i = 0; i < t.Length; i++)
            {
                if (map.ContainsKey(t[i]))
                {
                    if (map[t[i]] == 0)
                        return false;
                    map[t[i]]--;
                }
                else
                    return false;
            }

            return !map.Any(x => x.Value != 0);
        }

        //49. Group Anagrams  (Amazon onsite)
        //Given an array of strings, group anagrams together.
        //For example, given: ["eat", "tea", "tan", "ate", "nat", "bat"], 
        //Return:
        //  [  ["ate", "eat","tea"], ["nat","tan"], ["bat"] ]        
        public IList<List<string>> GroupAnagrams2(string[] strs)
        {
            var ret = new List<IList<string>>();
            if (strs == null)
                return null;

            var map = new Dictionary<string, List<string>>();
            for (int i = 0; i < strs.Length; i++)
            {
                var keyStr = new string(strs[i].OrderBy(c => c).ToArray());

                if (!map.ContainsKey(keyStr))
                    map.Add(keyStr, new List<string>() { strs[i] });
                else
                    map[keyStr].Add(strs[i]);

            }
            return map.Values.ToList();
        }

        //14. Longest Common Prefix
        //Write a function to find the longest common prefix string amongst an array of strings
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return "";
            if (strs.Length == 1)
                return strs[0];

            int len = strs.Min(x => x.Length);

            string ret = "";

            for (int strIdx = 0; strIdx < len; strIdx++)
            {
                char curCH = strs[0][strIdx];
                for (int arrIdx = 1; arrIdx < strs.Length; arrIdx++)
                {
                    if (strs[arrIdx][strIdx] == curCH && arrIdx == strs.Length - 1)
                    {
                        ret += curCH;
                    }
                    else if (strs[arrIdx][strIdx] == curCH)
                    {
                        continue;
                    }
                    else if (strs[arrIdx][strIdx] != curCH)
                    {
                        return ret;
                    }
                }

            }
            return ret;
        }


        //151. Reverse Words in a String
        //Given an input string, reverse the string word by word.
        //For example,Given s = "the sky is blue",return "blue is sky the".
        //For C programmers: Try to solve it in-place in O(1) spac  (NOW IS NOT IN-SPACE SOL!)
        public string ReverseWords(string s)
        {
            string[] temp = s.Split(' ');

            int st = 0;
            int end = temp.Length - 1;
            while (st < end)
            {
                swap(st, end, temp);
                st++;
                end--;
            }
            string ret = "";

            foreach (var x in temp)
            {
                if (x == " " || x == "")
                    continue;
                else
                    ret += x + " ";
            }
            return ret.Trim();
        }
        void swap(int i, int j, string[] strs)
        {
            string temp = strs[i].Trim();
            strs[i] = strs[j].Trim();
            strs[j] = temp.Trim();
        }


        //NVIDIA Put 1-9 into 3x3 matrix, and have the same sum in every rows and columns and two diagonal


        //NVIDIA Compress string such as 'AAABBCCCCCCAAAAA' to '3A2B6C5A'  
        public string Compress(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            char[] ch = str.ToArray();
            StringBuilder sb = new StringBuilder();

            int count = 1;
            char curCh = ch[0];
            for (int i = 1; i < ch.Length; i++)
            {
                if (curCh == ch[i])
                    count++;
                else
                {
                    sb.Append(count.ToString());
                    sb.Append(curCh);

                    curCh = ch[i];
                    count = 1;
                }
            }
            if (count >= 1)
            {
                sb.Append(count.ToString());
                sb.Append(curCh);
            }
            return sb.ToString();
        }


        //NVIDIA round up number by multiple
        public int roundUp(int numToRound, int multiple)
        {
            if (multiple == 0)
                return numToRound;

            int remainder = Math.Abs(numToRound) % multiple;

            if (numToRound > 0)
                return numToRound - remainder + multiple;
            else
                return -(Math.Abs(numToRound) - remainder + multiple);
        }

        //5. Longest Palindromic Substring
        //Given a string s, find the longest palindromic substring in s.You may assume that the maximum length of s is 1000.
        //Example 1:
        //Input: "babad"
        //Output: "bab"
        //Note: "aba" is also a valid answer.
        int startIdx = 0;
        int maxLen = 0;
        public string LongestPalindrome(string s)
        {
            if (s.Length == 0)
                return "";
            if (s.Length == 1)
                return s;

            for (int i = 0; i < s.Length - 1; i++)
            {
                maxCheck(s, i, i);
                maxCheck(s, i, i + 1);
            }
            return s.Substring(startIdx, maxLen);
        }
        void maxCheck(string s, int st, int ed)
        {
            while (st >= 0 && ed < s.Length && s[st] == s[ed])
            {
                st--;
                ed++;
            }
            if (maxLen < ed - st - 1)
            {
                startIdx = st + 1;
                maxLen = ed - st - 1;
            }
        }


        //(amazon) Find the longest unbroken series of increasing numbers in a list of random numbers 
        //i.e. if given[15, 2, 38, 71, 2, 524, 98], return [2, 38, 71] (longest increasing sub array)
        public int[] LongestIncreasingSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return null;

            int st = 0;
            int end = 0;
            int max = 0;
            int maxStart = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    end++;
                    if (end - st > max)
                    {
                        max = end - st;
                        maxStart = st;
                    }
                }
                else
                {
                    st = i;
                    end = st;
                }
            }

            if (max > 0)
            {
                int[] ret = new int[max + 1];
                for (int i = maxStart; i <= maxStart + max; i++)
                    ret[i - maxStart] = nums[i];
                return ret;
            }
            else
                return new int[] { };
        }

        //1428. Leftmost Column with at Least a One
        public int LeftMostColumnWithOne(BinaryMatrix binaryMatrix) {
            //matrix的右上角出发，首先往下走，如果遇到1的时候，我们就往左走。直到遇到0为止
            int row = binaryMatrix.Dimensions()[0];
            int col = binaryMatrix.Dimensions()[1];
        
            int i = 0, j = col-1, ret =-1;
            while(i<row && j>=0){
                if(binaryMatrix.Get(i,j)==0){
                    //go down search this col
                    i++;
                } else{
                    // go left
                    ret=j;
                    j--;
                }
            }
            return ret;
        }

        public int LeftMostColumnWithOne2(BinaryMatrix binaryMatrix) {
            int row = binaryMatrix.Dimensions()[0];
            int col = binaryMatrix.Dimensions()[1];
            //int ret = -1;
            for(int j =0; j<col; j++){
                //Bseearch by col to see have 1
                int i = 0, k = row-1; 
                while(i<k){
                    int piv = (i+k)/2;
                    if(binaryMatrix.Get(piv,j)==1){
                        return j;
                    }
                    else{
                        i = piv+1;
                    }

                }
            }    
            return -1;    
            // int i = 0, j = col-1, ret =-1;
            // while(i<row && j>=0){
            //     if(binaryMatrix.Get(i,j)==0){
            //         //go down search this col
            //         i++;
            //     } else{
            //         // go left
            //         ret=j;
            //         j--;
            //     }
            // }
            //return ret;
        }

        public class BinaryMatrix {
            public int Get(int row, int col) {return 1;}
            public IList<int> Dimensions() {return null;}
        }
 

        //238. Product of Array Except Self
        //Given an array of n integers where n > 1, nums, return an array output such that output[i] is equal to 
        //the product of all the elements of nums except nums[i].
        //Solve it without division and in O(n).
        //For example, given[1, 2, 3, 4], return [24,12,8,6].       
        public int[] ProductExceptSelf2(int[] nums)
        {
            //我们知道其前面所有数字的乘积，同时也知道后面所有的数乘积，那么二者相乘就是我们要的结果，
            //所以我们只要分别创建出这两个数组即可，分别从数组的两个方向遍历就可以分别创建出乘积累积数组
            
            int[] fwd = new int[nums.Length];
            int[] bwd = new int[nums.Length];
            bwd[0]=1;
            fwd[nums.Length-1]=1;

            for(int i=1; i< nums.Length; i++){
                bwd[i]= bwd[i-1]*nums[i-1];
            }

            for(int i=nums.Length-2; i>=0; i--){
                fwd[i]= fwd[i+1] *nums[i+1];
            }

            for(int i=0; i< nums.Length; i++){
                fwd[i]*= bwd[i];
            }
            return fwd;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int[] result = new int[nums.Length];
            //go from left multiply
            for (int i = 0, tmp = 1; i < nums.Length; i++)
            {
                result[i] = tmp;
                tmp *= nums[i];
            }
            //go from right to left 
            for (int i = nums.Length - 1, tmp = 1; i >= 0; i--)
            {
                result[i] *= tmp;
                tmp *= nums[i];
            }
            return result;
        }

        //35. Search Insert Position
        //Given a sorted array and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.
        //You may assume no duplicates in the array.  Here are few examples.
        //[1, 3, 5, 6], 5 → 2
        //[1,3,5,6], 2 → 1
        //[1,3,5,6], 7 → 4
        //[1,3,5,6], 0 → 0
        public int SearchInsert(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int piv = (st + end) / 2;
                if (nums[piv] == target)
                    return piv;

                if (target > nums[piv])
                    st = piv + 1;
                else
                    end = piv - 1;
            }
            if (end < 0)
                return 0;
            if (st == nums.Length)
                return nums.Length;

            return st;
        }

        //66. Plus One
        //Given a non-negative integer represented as a non-empty array of digits, plus one to the integer.
        //You may assume the integer do not contain any leading zero, except the number 0 itself.
        //The digits are stored such that the most significant digit is at the head of the list.
        public int[] PlusOne(int[] digits)
        {
            if (digits == null || digits.Length == 0)
                return digits;

            if (digits[digits.Length - 1] < 9)
            {
                digits[digits.Length - 1] += 1;
                return digits;
            }
            else
            {
                for (int i = digits.Length - 1; i >= 0; i--)
                {
                    if (digits[i] == 9)
                        digits[i] = 0;
                    else
                    {
                        digits[i] += 1;
                        return digits;
                    }
                }
            }
            int[] ret = new int[digits.Length + 1];
            ret[0] = 1;
            return ret;
        }


        //277. Find the Celebrity
        //Suppose you are at a party with n people (labeled from 0 to n - 1) and among them, there may 
        //exist one celebrity. The definition of a celebrity is that all the other n - 1 people know 
        //him/her but he/she does not know any of them.
        //Note: There will be exactly one celebrity if he/she is in the party. Return the celebrity's 
        //label if there is a celebrity in the party. If there is no celebrity, return -1.
        public int FindCelebrity(int n)
        {
            int maybeCelerity = 0;

            for (int i = 1; i < n; i++)
            {
                if (Knows(maybeCelerity, i))
                    maybeCelerity = i;
            }
            for (int i = 0; i < n; i++)
            {
                if (i != maybeCelerity && (Knows(maybeCelerity, i) || !Knows(i, maybeCelerity)))
                    return -1;
            }

            return maybeCelerity;
        }
        //mock API you can call
        bool Knows(int a, int b)
        {
            return true;
        }


        //311. Sparse Matrix Multiplication
        //Given two sparse matrices A and B, return the result of A*B.        
        public int[,] Multiply(int[,] A, int[,] B)
        {
            int rowA = A.GetLength(0);
            int colA = A.GetLength(1);
            int colB = B.GetLength(1);

            int[,] ret = new int[rowA, colB];

            for (int i = 0; i < rowA; i++)
            {
                for (int j = 0; j < colA; j++)
                {
                    if (A[i, j] != 0)
                    {
                        for (int k = 0; k < colB; k++)
                        {
                            if (B[j, k] != 0)
                                ret[i, k] += A[i, j] * B[j, k];
                        }
                    }
                }
            }
            return ret;
        }


        //205. Isomorphic Strings
        //Given two strings s and t, determine if they are isomorphic.
        //For example, Given "egg", "add", return true.  Given "foo", "bar", return false.  Given "paper", "title", return true.
        //You may assume both s and t have the same length.
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, List<int>> map1 = new Dictionary<char, List<int>>();
            Dictionary<char, List<int>> map2 = new Dictionary<char, List<int>>();

            for (int i = 0; i < s.Length; i++)
            {
                if (map1.ContainsKey(s[i]))
                    map1[s[i]].Add(i);
                else
                    map1.Add(s[i], new List<int>() { i });

                if (map2.ContainsKey(t[i]))
                    map2[t[i]].Add(i);
                else
                    map2.Add(t[i], new List<int>() { i });
            }

            for (int j = 0; j < map1.Count; j++)
            {
                if (!map1.ElementAt(j).Value.SequenceEqual(map2.ElementAt(j).Value))
                    return false;
            }
            return true;
        }


        //243. Shortest Word Distance
        //For example, Assume that words = ["practice", "makes", "perfect", "coding", "makes"].
        //Given word1 = “coding”, word2 = “practice”, return 3.
        //Given word1 = "makes", word2 = "coding", return 1.
        //Note:You may assume that word1 does not equal to word2, and word1 and word2 are both in the list.
        public int ShortestDistance(string[] words, string word1, string word2)
        {
            if (words == null)
                return 0;

            int k = -1;
            int j = -1;
            int ret = int.MaxValue;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word1)
                    k = i;
                if (words[i] == word2)
                    j = i;

                if (k != -1 && j != -1)
                    ret = Math.Min(Math.Abs(k - j), ret);
            }
            return ret;
        }
        //244. Shortest Word Distance II
        //This is a follow up of Shortest Word Distance. The only difference is now you are given the list 
        //of words and your method will be called repeatedly many times with different parameters. How would you optimize it?
        public class WordDistance
        {
            Dictionary<string, List<int>> map;
            public WordDistance(string[] words)
            {
                map = new Dictionary<string, List<int>>();

                for (int i = 0; i < words.Length; i++)
                {
                    if (map.ContainsKey(words[i]))
                        map[words[i]].Add(i);
                    else
                        map.Add(words[i], new List<int>() { i });
                }
            }
            public int Shortest(string word1, string word2)
            {
                int ret = int.MaxValue;
                for (int i = 0, j = 0; i < map[word1].Count && j < map[word2].Count;)
                {
                    if (map[word1][i] < map[word2][j])
                    {
                        ret = Math.Min(ret, Math.Abs(map[word2][j] - map[word1][i]));
                        i++;
                    }
                    else
                    {
                        ret = Math.Min(ret, Math.Abs(map[word1][i] - map[word2][j]));
                        j++;
                    }
                }
                return ret;
            }
        }


        //283. Move Zeroes  (NVIDIA)
        //Given an array nums, write a function to move all 0's to the end of it while maintaining the relative order of the non-zero elements.
        //For example, given nums = [0, 1, 0, 3, 12], after calling your function, nums should be[1, 3, 12, 0, 0].
        //Note:You must do this in-place without making a copy of the array.Minimize the total number of operations.
        public void MoveZeroes(int[] nums)
        { //smart solution~
            if (nums == null || nums.Length == 0)
                return;

            int zIdxx = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    zIdxx = i;
                    break;
                }
            }
            if (zIdxx == -1)
                return;
            for (int j = zIdxx + 1; j < nums.Length; j++)
            {
                if (nums[j] != 0)
                {
                    swap(nums, j, zIdxx);
                    zIdxx++;
                }
            }
        }


        //153. Find Minimum in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).Find the minimum element.
        //You may assume no duplicate exists in the array.
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            if (nums.Length == 1)
                return nums[0];

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                    return nums[i + 1];
            }
            return nums[0];
        }

        //191. Number of 1 Bits
        //Write a function that takes an unsigned integer and returns the number of ’1' bits it has (also known as the Hamming weight).
        //For example, the 32-bit integer ’11' has binary representation 00000000000000000000000000001011, so the function should return 3.
        public int HammingWeight(uint n)
        {
            if (n == 0)
                return 0;

            int ret = 0;
            while (n != 0)
            {
                if ((n & 1) == 1)
                    ret++;

                n >>= 1;
            }

            //while (n > 0)
            //{
            //    if (n % 2 != 0)
            //    {
            //        n = n - 1;
            //        ret += 1;
            //    }
            //    else
            //        n /= 2;
            //}
            return ret;
        }

        //26. Remove Duplicates from Sorted Array  (zillow)
        //do it in-space and put duplicate to tail, return non-repeated length
        public int RemoveDuplicates(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int ret = 1;

            int jumpCount = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                    jumpCount++;
                else
                {
                    nums[i - jumpCount] = nums[i];//or swap
                    ret += 1;
                }
            }
            return ret;
        }

        //Codelity 3. Rotate string by any index, see if it is the same as original one.
        //return how many index can satisfy         
        public int RotateStringAreTheSame(string S)
        {
            if (string.IsNullOrEmpty(S))
                return 0;

            if (S.Length == 1)
                return 1;

            int ret = 1;
            for (int i = S.Length - 1; i > 0; i--)
            {
                string ss = S.Substring(i) + S.Substring(0, i);
                if (S == ss)
                    ret += 1;
            }
            return ret;
        }

        //Codelity 2. input is binary 0/1 string, if see 1, minus 1, if see 0, divid by 2 
        //see how many steps to make it to zero
        //e.g. 00011100 (28)  -> 7 step to zero. 
        public int StepsNeedToZero(string S)
        {
            if (S.Length == 0)
                return 0;

            //prefix remove 0      
            string ss = "";
            bool flag = false;
            for (int i = 0; i < S.Length; i++)
            {
                if (S[i] != '0' || flag)
                {
                    ss += S[i];
                    flag = true;
                }
                else if (S[i] == '0' && !flag)
                    continue;
            }
            if (ss.Length == 0 || ss == "0")
                return 0;

            int step = 0;
            for (int i = ss.Length - 1; i >= 0;)
            {
                if (ss[i] == '0')
                {
                    step += 1;
                    i--;
                }
                else if (ss[i] == '1' && i > 0)
                {
                    step += 2;
                    i--;
                }
                else if (ss[i] == '1' && i == 0)
                {
                    step += 1;
                    i--;
                }
            }
            return step;
        }

        //Find use 1 or 2 digit only to show time, e.g. 15:15:15, 12:11:11, 11:11:31 
        //find interesting point in period of time
        //not passed
        public int FindInterestingPointsTime(string S, string T)
        {
            string[] STIme = S.Split(':');
            string[] TTime = T.Split(':');

            //find all possible in 1 day 
            var list = findSortedAllTimeLength();
            int SLength = CalLength(STIme);
            int TLength = CalLength(TTime);
            int ss = 0;
            int tt = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > SLength)
                {
                    ss = i;
                    break;
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > TLength)
                {
                    tt = i;
                    break;
                }
            }
            return tt - ss;
        }
        int CalLength(string[] s)
        {
            return 60 * Convert.ToInt32(s[0]) + 60 * Convert.ToInt32(s[1]) + Convert.ToInt32(s[2]);
        }
        List<int> findSortedAllTimeLength()
        {

            HashSet<int> hsRet = new HashSet<int>();  //record intersting time length
            HashSet<int> hs = new HashSet<int>();

            for (int m = 0; m < 60; m++)
            {
                int mFirst = m / 10;
                int mSecd = m % 10;
                hs.Add(mFirst);
                hs.Add(mSecd);

                //first = mFirst;
                //secd = mSecd;

                for (int s = 0; s < 60; s++)
                {
                    int sFirst = s / 10;
                    int sSecd = s % 10;

                    if (hs.Count == 1)  //use only 1 digit
                    {
                        if (hs.Add(sFirst))  //use 2 digits
                        {
                            if (hs.Contains(sFirst) && hs.Contains(sSecd))
                            {
                                for (int h = 0; h < 24; h++)
                                {
                                    int hFirst = h / 10;
                                    int hSecd = h % 10;

                                    if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                    {
                                        int val = (h * 60) + (m * 60) + s;
                                        hsRet.Add(val);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (hs.Add(sSecd))  //use 2 digits
                            {
                                if (hs.Contains(sFirst) && hs.Contains(sSecd))
                                {
                                    for (int h = 0; h < 24; h++)
                                    {
                                        int hFirst = h / 10;
                                        int hSecd = h % 10;

                                        if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                        {
                                            int val = (h * 60) + (m * 60) + s;
                                            hsRet.Add(val);
                                        }
                                    }
                                }
                            }
                            else  //only 1 
                            {
                                if (hs.Contains(sSecd))
                                {
                                    for (int h = 0; h < 24; h++)
                                    {
                                        int hFirst = h / 10;
                                        int hSecd = h % 10;

                                        if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                        {
                                            int val = (h * 60) + (m * 60) + s;
                                            hsRet.Add(val);
                                        }
                                    }
                                }
                            }
                        }

                        //hs.Add(sSecd);
                        if (hs.Count == 2)  //use 2 disits
                        {
                            //if (!((sFirst != mFirst && sFirst != sSecd) && (sSecd != mFirst && sSecd != sFirst)))
                            if (hs.Contains(sFirst) && hs.Contains(sSecd))
                            {
                                for (int h = 0; h < 24; h++)
                                {
                                    int hFirst = h / 10;
                                    int hSecd = h % 10;

                                    if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                    {
                                        int val = (h * 60) + (m * 60) + s;
                                        hsRet.Add(val);
                                    }
                                }
                            }
                        }
                    }
                    else if (hs.Count == 2) //use 2 digits
                    {
                        //if ((sFirst == mFirst || sFirst == mSecd) && (sSecd == mFirst || sSecd == mSecd))
                        if (hs.Contains(sFirst) && hs.Contains(sSecd))
                        {
                            for (int h = 0; h < 24; h++)
                            {
                                int hFirst = h / 10;
                                int hSecd = h % 10;

                                if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                {
                                    int val = (h * 60) + (m * 60) + s;
                                    hsRet.Add(val);
                                }
                            }
                        }
                    }
                }
            }

            var ret = hsRet.OrderBy(x => x).ToList();
            return ret;
        }


        //229. Majority Element II
        //Given an integer array of size n, find all elements that appear more than ⌊ n/3 ⌋ times.
        //The algorithm should run in linear time and in O(1) space.
        public IList<int> MajorityElement2(int[] nums)
        {
            var ret = new HashSet<int>();

            if (nums == null || nums.Length == 0)
                return ret.ToList();


            int count1 = 0, count2 = 0;
            int maj1 = int.MinValue;
            int maj2 = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (count1 == 0)
                {
                    maj1 = nums[i];
                    count1++;
                }
                else if (count2 == 0)
                {
                    maj2 = nums[i];
                    count2++;
                }
                else if (nums[i] == maj1)
                    count1++;
                else if (nums[i] == maj2)
                    count2++;
                else
                {
                    count1--;
                    count2--;
                }
            }


            if (nums.Where(x => x == maj1).Count() > (nums.Length / 3))
                ret.Add(maj1);
            if (nums.Where(x => x == maj2).Count() > (nums.Length / 3))
                ret.Add(maj2);

            return ret.ToList();
        }


        //169. Majority Element  (in space O(1))
        //Given an array of size n, find the majority element. The majority element is the element that appears more than ⌊ n/2 ⌋ times.
        //You may assume that the array is non-empty and the majority element always exist in the array.
        public int MajorityElement(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return int.MinValue;

            int major = nums[0], count = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (count == 0)
                {
                    count++;
                    major = nums[i];
                }
                else if (major == nums[i])
                    count++;
                else
                    count--;
            }
            return major;
        }

        //Codility1. Equi
        //Find an index in an array such that its prefix sum equals its suffix sum.
        //int[] { -1, 3, -4, 5, 1, -6, 2, 1 }
        //P = 1 is an equilibrium index of this array, because:
        //   A[0] = −1 = A[2] + A[3] + A[4] + A[5] + A[6] + A[7]
        //P = 3 is an equilibrium index of this array, because:
        //   A[0] + A[1] + A[2] = −2 = A[4] + A[5] + A[6] + A[7]
        //P = 7 is also an equilibrium index, because:
        //  A[0] + A[1] + A[2] + A[3] + A[4] + A[5] + A[6] = 0
        //time & space both in O(n) 
        public int Equi(int[] A)
        {
            if (A == null || A.Length == 0)
                return -1;
            Dictionary<int, int> map = new Dictionary<int, int>(); //idex: cur sum
            map.Add(-1, 0);
            int curSum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                curSum += A[i];
                map.Add(i, curSum);
            }
            int ret = -1;
            for (int j = 0; j < map.Count; j++)
            {
                if (map[j - 1] == map.Last().Value - map[j])
                    return j;
            }
            return ret;
        }

        //581. Shortest Unsorted Continuous Subarray  leetcode contest 32
        //Given an integer array, you need to find one continuous subarray that if you only sort this subarray in ascending order, then the whole array will be sorted in ascending order, too.
        //You need to find the shortest such subarray and output its length.
        //Example 1: Input: [2, 6, 4, 8, 10, 9, 15]  Output: 5
        //Explanation: You need to sort [6, 4, 8, 10, 9] in ascending order to make the whole array sorted in ascending order.
        public int FindUnsortedSubarray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int len = nums.Length;
            int[] copy1 = new int[len];
            for (int i = 0; i < nums.Length; i++)
            {
                copy1[i] = nums[i];
            }
            Array.Sort(copy1);

            int st = 0;
            int end = len - 1;

            while (st < len && copy1[st] == nums[st])
                st++;

            while (end > st && copy1[end] == nums[end])  //key note: end > st
                end--;

            return end - st + 1;
        }

        //leetcode contest 32 
        //public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        //{
        //    HashSet<int> ret = new HashSet<int>();

        //    if (!ppid.Any(p => p == kill))
        //    {
        //        if (pid.Any(p => p == kill))
        //            return new List<int>() { kill };
        //    }
        //    else
        //    {
        //        for (int i = 0; i < ppid.Count; i++)
        //        {
        //            if (ppid[i] == kill)
        //            {
        //                ret.Add(kill);
        //                findAll(pid, ppid, ret, kill, i);
        //            }
        //        }

        //    }
        //    return ret.ToList();
        //}
        //void findAll(IList<int> pid, IList<int> ppid, HashSet<int> ret, int kill, int ppidKilledIdx)
        //{

        //    if (ppid.Any(p => p == kill))
        //    {
        //        //ret.Add(kill);    
        //        int pidIdx = -1;

        //        for (int i = ppidKilledIdx; i < pid.Count; i++)
        //        {
        //            if (pid[i] == kill)
        //                pidIdx = i;
        //        }
        //        if (pidIdx == -1)
        //            return;

        //        ret.Add(pid[pidIdx]);
        //        findAll(pid, ppid, ret, pid[pidIdx], pidIdx);
        //    }
        //    else
        //        return;
        //}

        //return sqaure 
        private void Run(int[] testData)
        {
            if (testData == null || testData.Length == 0)
                return;
            for (int i = 0; i < testData.Length; i++)
            {
                var target = testData[i];
                //Square(target);
                if (target >= Math.Pow(int.MaxValue, 0.5) || target <= -1 * Math.Pow(int.MaxValue, 0.5))
                {
                    Console.WriteLine(int.MaxValue);
                    return;
                }
                target *= target;
                Console.WriteLine(target);
            }
        }
        void StairCase(int n)
        {
            if (n == 0)
                return;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n; i++)
            {
                int j = 0;
                for (j = 0; j < n - i; j++)
                    sb.Append(" ");
                for (int k = j; k < n; k++)
                    sb.Append("#");

                Console.WriteLine();
            }
        }

        //1249. Minimum Remove to Make Valid Parentheses
        //Given a string s of '(' , ')' and lowercase English characters. 
        //Your task is to remove the minimum number of parentheses ( '(' or ')', in any positions ) 
        //so that the resulting parentheses string is valid and return any valid string.
        //Input: s = "lee(t(c)o)de)"  Output: "lee(t(c)o)de"
        //Explanation: "lee(t(co)de)" , "lee(t(c)ode)" would also be accepted.
        public string MinRemoveToMakeValid2(string s) {
            if(string.IsNullOrEmpty(s))
                return "";
            int leftP = 0;
            string temp = "";    
            for(int i = 0; i< s.Length; i++){
                 if(s[i] == '('){
                     leftP++;
                 }
                 else if(s[i] == ')'){
                     if(leftP == 0){
                        continue;
                     }
                     leftP--;   
                 }
                 temp+=s[i];
            }
            StringBuilder ret = new StringBuilder();

            for(int i =temp.Length-1; i>=0; i--){
                if(temp[i]=='(' && leftP>0){
                    leftP--;
                }
                else{
                    ret.Insert(0,temp[i]);
                }    
            }
            return ret.ToString();
        }
        public string MinRemoveToMakeValid(string s) {
            // OK, but over time, O(n) / O(n)
            if(string.IsNullOrEmpty(s))
                return "";

             var stackL = new Stack<int>();
             var stackR = new Stack<int>(); 
             for(int i = 0; i< s.Length; i++){
                 if(s[i] == '('){
                     stackL.Push(i);
                 }
                 else if(s[i]==')' && stackL.Count>0){
                     stackL.Pop();
                 }
                 else if(s[i]==')'){
                     stackR.Push(i);
                 }
             }
             if(stackL.Count==0 && stackR.Count ==0 )
                return s;

             string ret ="";
             for(int i = 0; i<s.Length; i++){
                if(!(stackL.Contains(i) || stackR.Contains(i))){
                    ret+= s[i];
                }    
             }
             
            return ret;
        }

        //20. Valid Parentheses
        //Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
        //The brackets must close in the correct order, "()" and "()[]{}" are all valid but "(]" and "([)]" are not.
        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            Stack<char> st = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '{' || s[i] == '[' || s[i] == '(')
                {
                    st.Push(s[i]);
                }
                else if (s[i] == '}')
                {
                    if (st.Count > 0 && st.Peek() == '{')
                        st.Pop();
                    else
                        return false;
                }
                else if (s[i] == ']')
                {
                    if (st.Count > 0 && st.Peek() == '[')
                        st.Pop();
                    else
                        return false;
                }
                else if (s[i] == ')')
                {
                    if (st.Count > 0 && st.Peek() == '(')
                        st.Pop();
                    else
                        return false;
                }
            }
            return st.Count == 0;
        }

        //13. Roman to Integer
        public int RomanToInt(string s)
        {
            int ret = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == 'I') //*1
                {
                    ret += ret >= 5 ? -1 : 1;
                }
                if (s[i] == 'V') //5
                {
                    ret += 5;
                }
                if (s[i] == 'X') //*10
                {
                    ret += ret >= 50 ? -10 : 10;
                }
                if (s[i] == 'L') //50
                {
                    ret += 50;
                }
                if (s[i] == 'C') //*100
                {
                    ret += ret >= 500 ? -100 : 100;
                }
                if (s[i] == 'D') //500
                {
                    ret += 500;
                }
                if (s[i] == 'M') //*1000
                {
                    ret += 1000;
                }
            }
            return ret;
        }


        //88. Merge Sorted Array  
        //Given two sorted integer arrays nums1 and nums2, merge nums2 into nums1 as one sorted array.
        //Note:You may assume that nums1 has enough space(size that is greater or equal to m + n) to 
        //hold additional elements from nums2.The number of elements initialized in nums1 and nums2 are m and n respectively.        
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 == null || nums2 == null || n <= 0)
                return;

            int k = m + n - 1;
            int i = m - 1;
            int j = n - 1;

            while (i >= 0 && j >= 0)
            {
                if (nums2[j] > nums1[i])
                {
                    nums1[k] = nums2[j];
                    j--;
                }
                else
                {
                    nums1[k] = nums1[i];
                    i--;
                }
                k--;
            }
            while (j >= 0)
            {
                nums1[k] = nums2[j];
                j--;
                k--;
            }
        }

        //follow up , Merge K sorted arrays
        //https://www.geeksforgeeks.org/merge-k-sorted-arrays-set-2-different-sized-arrays/
        //use priority queue
        // O(nlog(m))  n is total numbers m is how many rows  


        public int LengthOfLongestSubstring2(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;
            int max = 0;
            var map = new HashSet<int>();
            int left = 0;
            int i = 0;
            while (i < s.Length)
            {
                if (!map.Contains(s[i]))
                {
                    map.Add(s[i]);
                    i++;
                    max = Math.Max(max, map.Count);
                }
                else
                {
                    map.Remove(s[left]);
                    left++;
                }
            }
            return max;
        }

        //3. Longest Substring Without Repeating Characters
        //Examples:  Given "abcabcbb", the answer is "abc", which the length is 3.
        //Given "bbbbb", the answer is "b", with the length of 1.
        //Given "pwwkew", the answer is "wke", with the length of 3. Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            Dictionary<char, int> map = new Dictionary<char, int>();
            int max = 0;

            int i = 0;
            while (i < s.Length)
            {
                if (!map.ContainsKey(s[i]))
                {
                    map.Add(s[i], i);
                    max = Math.Max(max, map.Count);
                    i++;
                }
                else
                {
                    int repeatedIdx = map[s[i]];
                    i = repeatedIdx + 1;
                    map.Clear();
                }
            }
            return max;
        }


        //54. Spiral Matrix
        //Given a matrix of m x n elements (m rows, n columns), return all elements of the matrix in spiral order.
        //For example,  Given the following matrix:
        //[ [ 1, 2, 3 ],
        //  [ 4, 5, 6 ],
        //  [ 7, 8, 9 ]
        //]                        You should return [1,2,3,6,9,8,7,4,5].
        public IList<int> SpiralOrder(int[,] matrix)
        {
            int leftFlag = 0;
            int downFlag = matrix.GetLength(0) - 1;
            int upFlag = 0;
            int rightFlag = matrix.GetLength(1) - 1;

            List<int> ret = new List<int>();

            if (matrix.Length == 0)
                return ret;

            while (leftFlag <= rightFlag && upFlag <= downFlag)
            {
                //visit through most up row 
                for (int i = leftFlag; i <= rightFlag; i++)
                {
                    ret.Add(matrix[upFlag, i]);
                }
                upFlag++;

                //visit through most right col
                for (int i = upFlag; i <= downFlag; i++)
                {
                    ret.Add(matrix[i, rightFlag]);
                }
                rightFlag--;

                //visit back through down row
                if (upFlag <= downFlag)  //need to check is it no row left
                {
                    for (int i = rightFlag; i >= leftFlag; i--)
                    {
                        ret.Add(matrix[downFlag, i]);
                    }
                }
                downFlag--;
                //visit back through up col
                if (leftFlag <= rightFlag) //need to check is it no col left
                {
                    for (int i = downFlag; i >= upFlag; i--)
                    {
                        ret.Add(matrix[i, leftFlag]);
                    }
                }
                leftFlag++;
            }
            return ret;

        }



        //186. Reverse Words in a String II  
        //Given an input string, reverse the string word by word. A word is defined as a sequence of non-space characters.
        //The input string does not contain leading or trailing spaces and the words are always separated by a single space.
        //For example, Given s = "the sky is blue", return "blue is sky the".
        // Could you do it in-place without allocating extra space?        
        public void ReverseWords(char[] s)
        {
            if (s.All(c => c != ' '))
                return;
            //reverse whole 
            reverseParial(s, 0, s.Length - 1);
            //reverse each word
            int stIdx = 0;
            for (int i = 0; i < s.Length;)
            {
                if (s[i] == ' ')
                {
                    if (i > 0)
                        reverseParial(s, stIdx, i - 1);

                    stIdx = i + 1;
                }
                i++;
            }
            //reverse last part
            reverseParial(s, stIdx, s.Length - 1);

        }

        void reverseParial(char[] s, int start, int end)
        {
            while (start < end)
            {
                char temp = s[start];
                s[start] = s[end];
                s[end] = temp;
                start++;
                end--;
            }
        }


        //leetcode 201705 53. Maximum Subarray
        //Find the contiguous subarray within an array (containing at least one number) which has the largest sum.
        //For example, given the array[-2, 1, -3, 4, -1, 2, 1, -5, 4],
        //the contiguous subarray[4, -1, 2, 1] has the largest sum = 6.                
        public int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int curSum = 0;
            int curmax = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];

                if (curSum < nums[i])
                    curSum = nums[i];

                curmax = Math.Max(curSum, curmax);
            }
            return curmax;
        }


        //leetcode 201705 121. Best Time to Buy and Sell Stock
        //Say you have an array for which the ith element is the price of a given stock on day i.
        //If you were only permitted to complete at most one transaction(ie, buy one and sell one share of the stock), design an algorithm to find the maximum profit.
        //Example 1: Input: [7, 1, 5, 3, 6, 4]  Output: 5
        // max.difference = 6 - 1 = 5(not 7 - 1 = 6, as selling price needs to be larger than buying price)
        //Input: [7, 6, 4, 3, 1]        Output: 0              
        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            int min = prices[0];
            int max = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                max = Math.Max(prices[i] - min, max);
                min = Math.Min(prices[i], min);
            }
            return max;
        }

        //leetcode 122. Best Time to Buy and Sell Stock II
        //Say you have an array for which the ith element is the price of a given stock on day i.
        //Design an algorithm to find the maximum profit.You may complete as many transactions as 
        //you like (ie, buy one and sell one share of the stock multiple times). 
        //However, you may not engage in multiple transactions at the same time(ie, you must sell the stock before you buy again).
        public int MaxProfit2(int[] prices)
        {
            //better one
            int ret = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                ret += prices[i] > prices[i - 1] ? prices[i] - prices[i - 1] : 0;
            }
            return ret;

            //int ret = 0;
            //int curMax = 0;

            //for (int i = 0; i < prices.Length; i++)
            //{
            //    while (i + 1 < prices.Length && prices[i + 1] > prices[i])
            //    {
            //        curMax += prices[i + 1] - prices[i];
            //        i++;
            //    }
            //    ret += curMax;
            //    curMax = 0;
            //}
            //return ret;
        }

        //123. Best Time to Buy and Sell Stock III
        //Design an algorithm to find the maximum profit. You may complete at most two transactions.
        //Note: You may not engage in multiple transactions at the same time (i.e., you must sell the stock before you buy again).
        //Example 1: Input: [3,3,5,0,0,3,1,4]
        //Output: 6
        //Explanation: Buy on day 4 (price = 0) and sell on day 6 (price = 3), profit = 3-0 = 3.
        //Then buy on day 7 (price = 1) and sell on day 8 (price = 4), profit = 4-1 = 3.
        //O(n)空间复杂度的动态规划：由于最多进行两次交易，所以第一次交易和第二次交易的分割点就比较关键。
        //我们定义left_max[i]表示在[0, i]区间内完成一笔交易所能获得的最利润，而right_max[i]则表示在[i, prices.size() - 1]区间内完成
        //一笔交易所能获得的最大利润。显然，left_max和right_max都可以通过线性扫描，采用贪心策略正确计算出来，最后线性扫描，
        //取得最佳分割点即可。不过，最后别忘了将交易两次的获利和只交易一次的最大获利相比较，并取最大值（left_max[prices.size() - 1]或者right_max[0]）。        
        //原文：https://blog.csdn.net/magicbean2/article/details/71045903 
        //space : O(n)
        public int MaxProfit3(int[] prices)
        {
            if (prices.Length <= 1)
                return 0;
            var left_max = new int[prices.Length];
            var right_max = new int[prices.Length];
            int lowest_price = prices[0];
            for (int i = 1; i < prices.Length; ++i)
            {
                if (prices[i] < lowest_price)
                    lowest_price = prices[i];
                left_max[i] = Math.Max(left_max[i - 1], prices[i] - lowest_price);
            }
            int highest_price = prices[prices.Length - 1];
            for (int i = prices.Length - 2; i >= 0; --i)
            {
                if (prices[i] > highest_price)
                    highest_price = prices[i];
                right_max[i] = Math.Max(right_max[i + 1], highest_price - prices[i]);
            }
            int max_profit = 0;
            for (int i = 0; i < prices.Length - 1; ++i)
            {
                int sum_price = left_max[i] + right_max[i];
                if (max_profit < sum_price)
                    max_profit = sum_price;
            }
            return Math.Max(max_profit, right_max[0]);
        }


        //amazon OA k nearest point，背景是一个城市有N个牛排馆，牛排馆的坐标都是存在allocations的list里面
        //(类型是List<List<Integer>>)，然后要求返回k个最近的牛排馆给用户，用户位置在坐标(0, 0)
        public List<List<int>> ClosestXdestinations(int numDestinations, int[,] allLocations, int numDeliveries)
        {
            // WRITE YOUR CODE HERE
            // considering edge case:
            var ret = new List<List<int>>();
            if (allLocations == null || allLocations.GetLength(0) == 0 || allLocations.GetLength(1) == 0 || numDeliveries > numDestinations)
                return ret;

            //get all delivery point into class, then we can find distance and sort it 
            var delList = new List<LFromOrigin>();

            for (int i = 0; i < allLocations.GetLength(0); i++)
            {
                delList.Add(new LFromOrigin(allLocations[i, 0], allLocations[i, 1]));
            }

            //sort by distance and just take numDeliveries
            delList = delList.OrderBy((d) => d.dist).Take(numDeliveries).ToList();

            for (int i = 0; i < delList.Count; i++)
            {
                ret.Add(new List<int>() { delList[i].x, delList[i].y });
            }
            return ret;
        }
        class LFromOrigin
        {
            public int x;
            public int y;
            public int dist;

            public LFromOrigin(int i, int j)
            {
                x = i;
                y = j;
                dist = (x * x) + (y * y);
            }
        }


        //amazon OA 背景是无人机送货，无人机有最大里程，然后给了两个list，分别是出发和返回的里程数，数据类型是List<List<Integer>>，
        //list里面只有id和里程两个值，要求找出所有出发和返回里程数之和最接近无人机最大里程的pair。比如，最大里程M = 10000，
        //forwarding = [[1, 1000],[2, 7000],[3, 12000]], retrun = [[1, 10000],[2, 9000],[3, 3000],[4, 2000]], 
        //最接近的里程和是10000，所以结果是[[1, 2],[2, 3]].
        public List<List<int>> ClosestPair(List<List<int>> forward, List<List<int>> returning, int target)
        {
            var ret = new List<List<int>>();
            if (forward == null || returning == null)
                return ret;

            forward = forward.OrderBy((x) => x[1]).ToList();
            returning = returning.OrderBy((r) => r[1]).ToList();

            int i = 0;
            int j = returning.Count - 1;
            int min = int.MaxValue;
            int targetSum = int.MinValue;
            while (i < forward.Count && j >= 0)
            {
                int curSum = forward[i][1] + returning[j][1];
                if (target - curSum > 0)
                {
                    i++;
                    if (target - curSum < min)
                    {
                        min = target - curSum;
                        targetSum = curSum;
                    }
                }
                else
                {
                    j--;
                    if (curSum - target < min)
                    {
                        min = curSum - target;
                        targetSum = curSum;
                    }
                }
            }
            //already find out closet sum value (max) , then go to and index combination
            //var fMap = new Dictionary<int, int>();
            //use 2 sum skill
            var rMap = new Dictionary<int, int>();
            foreach (var r in returning)
                rMap.Add(r[1], r[0]);

            for (int idx = 0; idx < forward.Count; idx++)
            {
                if (rMap.ContainsKey(targetSum - forward[idx][1]))
                {
                    ret.Add(new List<int>() { forward[idx][0], rMap[targetSum - forward[idx][1]] });
                }
            }

            return ret;
        }



        //leetcode 15. 3Sum
        //Given an array S of n integers, are there elements a, b, c in S such that a + b + c = 0? 
        //Find all unique triplets in the array which gives the sum of zero.
        //For example, given array S = [-1, 0, 1, 2, -1, -4],  A solution set is:
        //[  [-1, 0, 1],  [-1, -1, 2]  ]
        public IList<IList<int>> ThreeSum2(int[] nums)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (nums == null)
                return ret;

            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int j = i + 1;
                int k = nums.Length - 1;
                while (j < k)
                {
                    if (nums[i] + nums[j] + nums[k] == 0)
                    {
                        ret.Add(new List<int> { nums[i], nums[j], nums[k] });
                        j++;
                        k--;
                        while (j < k && nums[j] == nums[j - 1])
                            j++;
                        while (j < k && nums[k] == nums[k + 1])
                            k--;
                    }
                    else if (nums[i] + nums[j] + nums[k] > 0)
                        k--;
                    else
                        j++;
                }
            }
            return ret;
        }

        //backtracking
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> ret = new List<IList<int>>();

            if (nums == null || nums.Length == 0)
                return ret;
            Array.Sort(nums);
            //var hs = new HashSet<string>();

            ThreeSumHelp(nums, 0, ret, new List<int>());
            return ret;
        }

        void ThreeSumHelp(int[] nums, int curIdx, List<IList<int>> ret, List<int> curList)
        {
            //string checkRep = string.Join(",", curList);

            if (curList.Count == 3 && curList.Sum() == 0 && !ret.Any(item => item[0] == curList[0] && item[1] == curList[1] && item[2] == curList[2]))
            {
                //hs.Add(checkRep);
                ret.Add(new List<int>(curList));
                return;
            }

            for (int i = curIdx; i < nums.Length; i++)
            {
                curList.Add(nums[i]);
                ThreeSumHelp(nums, i + 1, ret, curList);
                curList.Remove(curList.Last());
            }
        }


        //259. 3sum smaller
        //Given an array of n integers nums and a target, find the number of index triplets i, j, k with 0 <= i<j<k<n that satisfy the condition nums[i] + nums[j] + nums[k] < target.
        //For example, given nums = [-2, 0, 1, 3], and target = 2.        
        //Return 2. Because there are two triplets which sums are less than 2:
        //[-2, 0, 1]
        //[-2, 0, 3]
        //Follow up:
        //Could you solve it in O(n^2) runtime?
        int threeSumSmaller(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            Array.Sort(nums);
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int j = i + 1;
                int k = nums.Length - 1;

                if (i > 0 && nums[i] == nums[i - 1])
                    continue;
                while (j < k)
                {
                    if (nums[i] + nums[j] + nums[k] < target)
                    {
                        ret++;
                        j++;
                        while (nums[j] == nums[j - 1])
                            j++;
                    }
                    else
                    {
                        k--;
                        while (nums[k] == nums[k + 1])
                            k--;
                    }
                }
            }
            return ret;
        }


        //268. Missing Number
        //Given an array containing n distinct numbers taken from 0, 1, 2, ..., n, find the one that is missing from the array.
        //For example, Given nums = [0, 1, 3] return 2.
        // Input: [3,0,1]    Output: 2
        //Your algorithm should run in linear runtime complexity. Could you implement it using only constant extra space complexity?        
        public int MissingNumber(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            if (nums.Length == 1 && nums[0] == 0)
                return 1;
            if (nums.Length == 1 && nums[0] == 1)
                return 0;

            int realsum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                realsum += nums[i];
            }
            int ideasum = ((nums.Length) * (1 + nums.Length)) / 2;

            return ideasum - realsum;
        }

        //[-2,0, 1,2] missing -1 
        int missingNumberII(int[] nums)
        {
            //find min first ; 
            int min = -2; //e.g. 

            int res = 0;

            for (int i = 0; i < nums.Length; ++i)
            {
                res ^= (i + min) ^ nums[i];
            }
            res ^= (min + nums.Length);
            return res;
        }

        //8. String to Integer (atoi) MS OA
        //Implement atoi to convert a string to an integer.
        //Hint: Carefully consider all possible input cases.If you want a challenge, please do not see below and ask yourself what are the possible input cases.
        //If no valid conversion could be performed, a zero value is returned.If the correct value is out of the range of representable values, INT_MAX (2147483647) or INT_MIN(-2147483648) is returned.
        //e.g. -12a445  ->  -12 
        public int MyAtoi(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            str = str.Trim();
            int len = str.Length;
            double ret = 0;
            int comp = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (i == 0 && str[0] == '+')
                    break;
                if (i == 0 && str[0] == '-')
                {
                    ret = ret * -1;
                    break;
                }
                if (str[i] == ' ')
                {
                    ret = 0;
                    comp = len - i + 1;
                    continue;
                }
                int num = (int)(str[i] - '0');
                if (num > 9 || num < 0)
                {
                    ret = 0;
                    comp = len - i + 1;
                }
                else
                    ret += num * Math.Pow(10, len - i - comp);
            }
            if (ret > int.MaxValue)
                return int.MaxValue;
            if (ret < int.MinValue)
                return int.MinValue;

            return (int)ret;
        }

        public void SortColors2(int[] nums)
        {
            int rIdx = 0;
            int bIdx = nums.Length - 1;

            for (int i = rIdx; i <= bIdx;)
            {
                if (nums[i] == 0)
                {
                    swap(nums, i, rIdx);
                    rIdx += 1;
                    i++;
                }
                else if (nums[i] == 2)
                {
                    swap(nums, i, bIdx);
                    bIdx -= 1;
                }
                else
                    i++;
            }
        }
        void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }


        //48. Rotate Image
        //You are given an n x n 2D matrix representing an image.Rotate the image by 90 degrees(clockwise).
        //Follow up: Could you do this in-place?
        /* clockwise rotate
        * first reverse up to down, then swap the symmetry 
        * 1 2 3     7 8 9     7 4 1
        * 4 5 6  => 4 5 6  => 8 5 2
        * 7 8 9     1 2 3     9 6 3
        */
        public void Rotate(int[,] matrix)
        {
            if (matrix == null)
                return;

            int len = matrix.GetLength(0);
            // swap up down
            int st = 0;
            int end = len - 1;
            while (st < end)
            {
                int[] temp = new int[len];
                for (int i = 0; i < len; i++)
                {
                    temp[i] = matrix[st, i];
                    matrix[st, i] = matrix[end, i];
                    matrix[end, i] = temp[i];
                }
                st++;
                end--;
            }
            //swap symmetric 
            for (int i = 0; i < len; ++i)
            {
                for (int j = i + 1; j < len; ++j)
                    swap(i, j, matrix);
            }
        }
        void swap(int i, int j, int[,] matrix)
        {
            int temp = matrix[i, j];
            matrix[i, j] = matrix[j, i];
            matrix[j, i] = temp;
        }

        //73. Set Matrix Zeroes
        //Given a m x n matrix, if an element is 0, set its entire row and column to 0. Do it in place.
        public void SetZeroes2(int[,] matrix)
        {
            //space complex : O(1)
            //use 1st row and 1st col to record zero
            if (matrix == null)
                return;

            bool isRowZero = false;
            bool isColZero = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 0] == 0)
                {
                    isColZero = true;
                    break;
                }
            }
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[0, j] == 0)
                {
                    isRowZero = true;
                    break;
                }
            }
            //use 1st row and 1st col to record zero
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        matrix[i, 0] = 0;
                        matrix[0, j] = 0;
                    }
                }
            }
            //assign 0 according to 1st row and col
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, 0] == 0 || matrix[0, j] == 0)
                        matrix[i, j] = 0;
                }
            }
            //taking care 1st row and col
            if (isRowZero)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[0, j] = 0;
                }
            }
            if (isColZero)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    matrix[i, 0] = 0;
                }
            }

        }

        public void SetZeroes(int[,] matrix)
        {
            //space complex : O(m+n) 
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            HashSet<int> zRow = new HashSet<int>();
            HashSet<int> zCol = new HashSet<int>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        zRow.Add(i);
                        zCol.Add(j);
                    }
                }
            }

            for (int i = 0; i < zRow.Count; i++)
            {
                for (int j = 0; j < cols; j++)
                    matrix[zRow.ElementAt(i), j] = 0;
            }
            for (int j = 0; j < zCol.Count; j++)
            {
                for (int i = 0; i < rows; i++)
                    matrix[i, zCol.ElementAt(j)] = 0;
            }
        }

        //MS tech screen, 2 arrays , one is n and another is n+1, find out extra char
        //....
        char findExtraChar(char[] a, char[] b)
        {
            if (a == null || b == null)
                return '\0';
            if (a.Length == 0 && b.Length == 1)
                return b[0];
            if (a.Length == 1 && b.Length == 0)
                return a[0];

            var map = new Dictionary<char, int>();

            for (int i = 0; i < a.Length; i++)
            {
                if (!map.ContainsKey(a[i]))
                    map.Add(a[i], 1);
                else
                    map[a[i]] += 1;
            }

            for (int i = 0; i < b.Length; i++)
            {
                if (map.ContainsKey(b[i]))
                    map[a[i]] -= 1;
                else
                    return b[i];
            }
            return map.Where(p => p.Value != 0).FirstOrDefault().Key;
        }

    }
}
