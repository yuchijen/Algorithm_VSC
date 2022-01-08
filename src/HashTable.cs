using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class HashTable
    {
        // 第三题给一个string，让你求最长的substring里面所有的character出现次数是偶数，
        // ‍‌‍‍‌‌‍‌‌‌‍‍‍‍‌‌‍‍‍想到特别好的方法，直接暴力解，不知道有没有更好的方法
        // e.g. ababcd => 4 ; cababd => 4 ; ababdcdc => 8
        public string LongestSubstringEvenCount(string s){
            if(string.IsNullOrEmpty(s))
                return "";

            var map = new Dictionary<char, int>();
            int maxLen = 0;
            int stIdx = 0;
            for(int i=0; i<s.Length; i++){
                map.Clear();
                for(int j = i; j<s.Length; j++){
                    if(!map.ContainsKey(s[j])){
                        map.Add(s[j],1);
                    }else{
                        map[s[j]]+=1;
                    }
                    if(map.Values.All(x=> x%2 ==0)){
                        if(j-i+1 > maxLen){
                            maxLen = j-i +1;
                            stIdx = i;
                        }
                    }
                }
            }
            return s.Substring(stIdx, maxLen);
        }


        //155. Min Stack (get min in O(1))
        //Input ["MinStack","push","push","push","getMin","pop","top","getMin"] 
        //                    [-2],[0],[-3]
        //Output[null,null,null,null,-3,null,0,-2]
        //使用两个栈来实现，一个栈来按顺序存储 push 进来的数据，另一个用来存出现过的最小值
        public class MinStack
        {
            Stack<int> st;
            Stack<int> st2;
            public MinStack()
            {   
                st = new Stack<int>();                
                st2 = new Stack<int>();                
            }

            public void Push(int val)
            {
                st.Push(val);
                if(st2.Count ==0 || val <= st2.Peek())
                    st2.Push(val);
            }

            public void Pop()
            {
                if(st.Count > 0){
                    var temp = st.Pop();
                    if(st2.Count > 0 && temp == st2.Peek())
                        st2.Pop();
                }
            }

            public int Top()
            {
                return st.Count > 0 ? st.Peek() : int.MaxValue;
            }

            public int GetMin()
            {
                return st2.Count > 0 ? st2.Peek() : int.MaxValue;
            }
        }



        //1268. Search Suggestions System
        // Input: products = ["mobile","mouse","moneypot","monitor","mousepad"], searchWord = "mouse"
        // Output: [
        // ["mobile","moneypot","monitor"],
        // ["mobile","moneypot","monitor"],
        // ["mouse","mousepad"],
        // ["mouse","mousepad"],
        // ["mouse","mousepad"]
        // ]
        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            if (products == null || products.Length == 0 || string.IsNullOrEmpty(searchWord))
                return null;

            Array.Sort(products);

            int maxCnt = 3;
            var ret = new List<IList<string>>();

            for (int i = 1; i <= searchWord.Length; i++)
            {
                var cur = new List<string>();
                var curSearch = searchWord.Substring(0, i);

                foreach (var p in products)
                {
                    if (p.StartsWith(curSearch))
                    {
                        cur.Add(p);
                    }
                    if (cur.Count == maxCnt)
                        break;
                }
                ret.Add(cur);
            }
            return ret;
        }
        class Trie
        {
            public Dictionary<char, Trie> next = new Dictionary<char, Trie>();
            public List<string> Storage = new List<string>();
        }
        public IList<IList<string>> SuggestedProductsWithTrie(string[] products, string searchWord)
        {
            if (products == null || products.Length == 0 || string.IsNullOrEmpty(searchWord))
                return null;

            Array.Sort(products);

            //build trie graph
            var root = new Trie();
            for (int i = 0; i < products.Length; i++)
            {
                var pt = root;
                for (int j = 0; j < products[i].Length; j++)
                {
                    char key = products[i][j];
                    pt.next.TryAdd(key, new Trie());
                    if (pt.next[key].Storage.Count < 3)
                    {
                        pt = pt.next[key];
                        pt.Storage.Add(products[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //search
            var ret = new List<IList<string>>();
            for (int i = 0; i < searchWord.Length; i++)
            {
                //var cur = new List<string>();
                if (root.next.ContainsKey(searchWord[i]))
                {
                    root = root.next[searchWord[i]];
                    ret.Add(root.Storage);
                }
                else
                {
                    ret.Add(new List<string>());
                }
            }

            return ret;
        }

        //763. Partition Labels
        public IList<int> PartitionLabels(string S)
        {
            var map = new Dictionary<char, int>();
            for (int i = 0; i < S.Length; i++)
            {
                map.TryAdd(S[i], i);
                map[S[i]] = i;
            }
            // int windowHead = 0; 
            int windowLast = 0;
            int curLast = 0;
            var ret = new List<int>();
            int head = 0;
            while (windowLast < S.Length)
            {

                curLast = Math.Max(map[S[windowLast]], curLast);
                if (curLast == windowLast)
                {
                    ret.Add(windowLast - head + 1);
                    head = windowLast + 1;

                }
                windowLast++;
            }
            return ret;
        }

        //347 Top K Frequent Elements
        //Given a non-empty array of integers, return the k most frequent elements.
        // Example 1: Input: nums = [1,1,1,2,2,3], k = 2, Output: [1,2]
        // Example 2: Input: nums = [1], k = 1 Output: [1]
        public IList<int> TopKFrequent(int[] nums, int k)
        {
            if (nums == null || nums.Count() == 0 || nums.Count() < k)
                return null;

            var map = new Dictionary<int, int>();
            foreach (var x in nums)
            {
                if (map.ContainsKey(x))
                    map[x]++;
                else
                    map.Add(x, 1);
            }
            List<KeyValuePair<int, int>> myList = map.ToList();

            myList.Sort((kv1, kv2) =>
            {
                return kv2.Value.CompareTo(kv1.Value);
            });

            return myList.Select(kv => kv.Key).Take(k).ToList();
        }

        //36. Valid Sudoku, sudoku validator ,if a passed string containing all 81 numbers of the puzzle 
        //is a valid solution, false otherwise
        //note: The Sudoku board could be partially filled, where empty cells are filled
        // with the character '.'.
        public bool IsValidSudoku(char[,] board)
        {
            var set = new HashSet<string>();

            //herizontal check
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != '.')
                        if (!set.Add(board[i, j] + " in row " + i) ||
                                !set.Add(board[i, j] + " in column " + j) ||
                                    !set.Add(board[i, j] + " in block (" + i / 3 + "," + j / 3 + ")"))
                            return false;
                }
            }

            return true;
        }

        //37. Sudoku Solver
        public void SolveSudoku(char[][] board)
        {
            SudokuHelp(board, 0, 0);
        }
        // like N-Queens solution, each empty spot, try 1-9 possible answer, and assign possible answer
        // if next /future not valid, then try next possible number.
        bool SudokuHelp(char[][] board, int i, int j)
        {
            if (i == 9)  //scan to end of board
                return true;
            if (j == 9)
                return SudokuHelp(board, i + 1, 0);
            if (board[i][j] != '.')
            {
                return SudokuHelp(board, i, j + 1);
            }

            for (char v = '1'; v <= '9'; v++)
            { //try 1 to 9 possible answer in cur empty spot
                if (isSudokuValid(board, i, j, v))
                {
                    board[i][j] = v;
                }
                if (SudokuHelp(board, i, j + 1))
                { //if next is valid return true
                    return true;
                }
                board[i][j] = '.';  //if not, try next possible value
            }

            return false;  //try all possible 1-9, none of valid.
        }

        bool isSudokuValid(char[][] board, int i, int j, char val)
        {
            for (int a = 0; a < 9; a++)
            {  //check valid row 
                if (board[i][a] == val)
                    return false;
            }
            for (int a = 0; a < 9; a++)
            {  //check valid col
                if (board[a][j] == val)
                    return false;
            }

            //check sub area
            int areaRowStart = i - i % 3;
            int areaColStart = j - j % 3;
            for (int a = 0; a < 3; a++)
            {  //check sub area
                for (int b = 0; b < 3; b++)
                {
                    if (board[areaRowStart + a][areaColStart + b] == val)
                        return false;
                }
            }
            return true;
        }

        //387. First Unique Character in a String
        public int FirstUniqChar(string s)
        {
            if (string.IsNullOrEmpty(s))
                return -1;
            var map = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                    map.Add(s[i], 1);
                else
                    map[s[i]] += 1;
            }
            for (int i = 0; i < map.Count; i++)
            {
                if (map.ElementAt(i).Value == 1)
                    return s.IndexOf(map.ElementAt(i).Key);
            }
            return -1;
        }


        //359.  Logger Rate Limiter
        //Design a logger system that receive stream of messages along with its timestamps, each message should be printed if and only if it is not printed in the last 10 seconds.
        //Given a message and a timestamp(in seconds granularity), return true if the message should be printed in the given timestamp, otherwise returns false.
        //It is possible that several messages arrive roughly at the same time.
        //e.g.
        //Logger logger = new Logger();
        // logging string "foo" at timestamp 1
        //logger.shouldPrintMessage(1, "foo"); returns true; 
        // logging string "bar" at timestamp 2
        //logger.shouldPrintMessage(2,"bar"); returns true;
        // logging string "foo" at timestamp 3
        //logger.shouldPrintMessage(3,"foo"); returns false;
        //使用哈希映射记录各个时间点的消息，使用一个集合记录最近10秒的所有消息。
        bool shouldPrintMessage2(int timestamp, string message)
        {
            var map = new Dictionary<string, int>();

            if (!map.ContainsKey(message))
            {
                map.Add(message, timestamp);
                return true;
            }
            if (timestamp - map[message] > 10)
            {
                map[message] = timestamp;
                return true;
            }
            return false;
        }
        public class Logger
        {
            Dictionary<int, List<string>> mapSPM;
            HashSet<string> curSet;
            int from;
            public Logger()
            {
                mapSPM = new Dictionary<int, List<string>>();
                curSet = new HashSet<string>();
                from = 0;
            }

            bool shouldPrintMessage(int timestamp, string message)
            {
                //remove all element (in set and map) staying longer than 10
                for (int i = from; i <= timestamp - 10; i++)
                {
                    foreach (var str in mapSPM[i])
                    {
                        if (curSet.Contains(str))
                            curSet.Remove(str);
                    }
                    mapSPM.Remove(i);
                }
                //maintain curset can reduce time on searching
                if (curSet.Contains(message))
                    return false;
                curSet.Add(message);
                if (mapSPM.ContainsKey(timestamp) && mapSPM[timestamp] != null)
                    mapSPM[timestamp].Add(message);
                else if (mapSPM.ContainsKey(timestamp))
                    mapSPM[timestamp] = new List<string> { message };
                else
                    mapSPM.Add(timestamp, new List<string> { message });

                from = timestamp - 9;
                return true;
            }
        }

        //340. Find the longest substring with k unique characters in a given string 
        //Given a string you need to print longest possible substring that has exactly M unique characters. 
        //If there are more than one substring of longest possible length, then print any one of them.
        //Examples:
        //"aabbcc", k = 1
        //Max substring can be any one from {"aa" , "bb" , "cc"}.
        //"aabbcc", k = 2
        //Max substring can be any one from {"aabb" , "bbcc"}.
        //For example, Given s = “eceba” and k = 2,
        //T is "ece" which its length is 3.
        //"aabbcc", k = 3
        //There are substrings with exactly 3 unique characters
        //{"aabbcc" , "abbcc" , "aabbc" , "abbc" }
        //Max is "aabbcc" with length 6.
        //"aaabbb", k = 3
        //There are only two unique characters, thus show error message. 
        public int lengthOfLongestSubstringKDistinct(string s, int k)
        {
            //slide window
            var map = new Dictionary<char, int>();
            int ret = 0, j = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                {
                    map[s[i]] += 1;
                }
                else
                {
                    map.Add(s[i], 1);
                }

                while (map.Count > k && j <= i)
                {
                    if (--map[s[j]] == 0)
                        map.Remove(s[j]);

                    j++;
                }
                while (map.Count == k)
                {
                    ret = Math.Max(ret, map.Sum(kv => kv.Value));
                    if (map.ContainsKey(s[j]))
                    {
                        map[s[j]]--;
                    }
                    j++;
                }
            }
            return ret;
        }
        public int lengthOfLongestSubstringKDistinct3(string s, int k)
        {
            if (string.IsNullOrEmpty(s) || k == 0)
                return 0;

            var map = new Dictionary<char, int>();
            int ret = 0;
            int b = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                    map[s[i]] += 1;
                else
                    map.Add(s[i], 1);

                while (map.Count > k)
                {
                    if (map[s[b]] > 1)
                        map[s[b]]--;
                    else
                        map.Remove(s[b]);
                    b++;
                }
                ret = Math.Max(ret, i - b + 1);
            }
            return ret;
        }

        public int lengthOfLongestSubstringKDistinct2(string s, int k)
        {
            if (string.IsNullOrEmpty(s) || k == 0)
                return 0;

            var map = new Dictionary<char, int>();
            int ret = 0;
            int leftIdx = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                    map[s[i]] += 1;
                else
                    map.Add(s[i], 1);

                while (map.Count > k)
                {
                    if (--map[s[leftIdx]] == 0)
                        map.Remove(s[leftIdx]);

                    leftIdx++;
                }
                ret = Math.Max(ret, map.Sum(kv => kv.Value));
            }
            return ret;
        }

        //249. Group Shifted Strings
        //Given a string, we can "shift" each of its letter to its successive letter, 
        //for example: "abc" -> "bcd". We can keep "shifting" which forms the sequence:
        //"abc" -> "bcd" -> ... -> "xyz"
        //Given a list of non-empty strings which contains only lowercase alphabets, 
        //group all strings that belong to the same shifting sequence.
        //Input: ["abc", "bcd", "acef", "xyz", "az", "ba", "a", "z"],
        // Output: 
        // [
        //   ["abc","bcd","xyz"],
        //   ["az","ba"],
        //   ["acef"],
        //   ["a","z"]
        //]
        //利用偏移字符串的特点，那就是字符串的每个字母和首字符的相对距离都是相等的，比如 abc 和 efg 互为偏移，
        //对于 abc 来说，b和a的距离是1，c和a的距离是2，对于 efg 来说，f和e的距离是1，g和e的距离是2。
        //再来看一个例子，az 和 yx，z和a的距离是 25，x和y的距离也是 25 (直接相减是 -1，这就是要加 26 然后取余的原因)，
        //那么这样的话，所有互为偏移的字符串都有个 unique 的距离差，根据这个来建立映射就可以很好的进行单词分组了
        // O(n)/O(n)
        public IList<IList<string>> GroupStrings(string[] strings)
        {
            if (strings == null || strings.Length == 0)
                return null;

            var ret = new List<IList<string>>();
            var map = new Dictionary<string, List<string>>();

            for (int i = 0; i < strings.Length; i++)
            {
                string curStr = strings[i];
                string key = "";
                if (!string.IsNullOrEmpty(curStr))
                {
                    if (curStr.Length == 1)
                        key = "";
                    else
                    {
                        for (int j = 1; j < curStr.Length; j++)
                        {
                            key += ((curStr[j] - curStr[j - 1] + 26) % 26).ToString();
                        }
                    }
                }
                if (map.ContainsKey(key))
                {
                    map[key].Add(curStr);
                }
                else
                {
                    map.Add(key, new List<string> { curStr });
                }
            }
            ret.Concat(map.Values);
            return ret;
        }

        //380. Insert Delete GetRandom O(1)
        public class RandomizedSet
        {

            /** Initialize your data structure here. */
            HashSet<int> hs;
            public RandomizedSet()
            {
                hs = new HashSet<int>();
            }

            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val)
            {
                if (hs.Contains(val))
                    return false;
                else
                {
                    hs.Add(val);
                    return true;
                }
            }

            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val)
            {
                if (hs.Contains(val))
                {
                    hs.Remove(val);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /** Get a random element from the set. */
            public int GetRandom()
            {
                var len = hs.Count;
                var rd = new Random();
                return hs.ElementAt(rd.Next(len));
            }
        }

        //381 Insert Delete GetRandom O(1) with duplicated number
        public class RandomizedCollection
        {

            /** Initialize your data structure here. */
            public RandomizedCollection()
            {
                map = new Dictionary<int, HashSet<int>>();
                list = new List<int>();
            }
            private Dictionary<int, HashSet<int>> map;
            private List<int> list;
            /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
            public bool Insert(int val)
            {
                if (map.ContainsKey(val))
                {
                    map[val].Add(list.Count);
                    list.Add(val);
                    return false;
                }
                map.TryAdd(val, new HashSet<int>());
                map[val].Add(list.Count);
                list.Add(val);
                return true;
            }

            /** Removes a value from the collection. Returns true if the collection contained the specified element. */
            public bool Remove(int val)
            {
                if (!map.ContainsKey(val))
                {
                    return false;
                }

                int firstIdxRemoveVal = map[val].First();
                map[val].Remove(firstIdxRemoveVal);
                if (map[val].Count == 0)
                    map.Remove(val);
                if (list.Last() != val)
                    if (firstIdxRemoveVal != list.Count - 1)
                    {
                        int tailVal = list.Last();
                        list[firstIdxRemoveVal] = tailVal;
                        map[tailVal].Remove(list.Count - 1);
                        map[tailVal].Add(firstIdxRemoveVal);
                    }
                list.RemoveAt(list.Count - 1);

                return true;
            }

            /** Get a random element from the collection. */
            public int GetRandom()
            {
                var rd = new Random();
                return list[rd.Next(list.Count)];
            }
        }


        //825. Friends Of Appropriate Ages
        //Some people will make friend requests. The list of their ages is given and ages[i] is the age of the ith person. 
        //Person A will NOT friend request person B(B != A) if any of the following conditions are true:
        //age[B] <= 0.5 * age[A] + 7
        //age[B] > age[A]
        //age[B] > 100 && age[A] < 100
        //Otherwise, A will friend request B.
        //Note that if A requests B, B does not necessarily request A.Also, people will not friend request themselves.
        //How many total friend requests are made?
        //Example 1:  Input: [16,16]
        //Output: 2   Explanation: 2 people friend request each other.
        public int NumFriendRequests(int[] ages)
        {
            if (ages == null || ages.Length <= 1)
                return 0;

            int ret = 0;
            // map key is age, value is its friend's request
            var map = new Dictionary<int, int>();
            Array.Sort(ages);
            for (int i = 0; i < ages.Length; i++)
            {
                if (map.ContainsKey(ages[i]))
                {
                    ret += map[ages[i]];
                    continue;
                }
                int curSum = 0;
                for (int j = i; j < ages.Length; j++)
                {
                    if (i != j && isValidAge(ages[j], ages[i]))
                        curSum++;
                }
                map.Add(ages[i], curSum);
                ret += curSum;
            }
            return ret;
        }
        bool isValidAge(int a, int b)
        {
            if (b <= 0.5 * a + 7)
                return false;
            if (a < b)
                return false;
            if (a < 100 && b > 100)
                return false;
            return true;
        }


        //438. Find All Anagrams in a String
        //Given a string s and a non-empty string p, find all the start indices of p's anagrams in s.
        //Strings consists of lowercase English letters only and the length of both strings s and p will not be larger than 20,100.
        //The order of output does not matter. 
        //Example 1: Input: s: "cbaebabacd" p: "abc"
        //Output: [0, 6]
        //Explanation:
        //The substring with start index = 0 is "cba", which is an anagram of "abc".
        //The substring with start index = 6 is "bac", which is an anagram of "abc".
        public IList<int> FindAnagrams2(string s, string p)
        {
            var ret = new List<int>();
            if (string.IsNullOrEmpty(s) || s.Length < p.Length)
                return ret;

            var map = new int[26];
            for (int i = 0; i < p.Length; i++)
                map[p[i] - 'a'] += 1;

            int left = 0, right = 0, cnt = p.Length, n = s.Length;

            while (right < n)
            {
                if (map[s[right] - 'a'] >= 1)
                {
                    --cnt;
                }
                map[s[right] - 'a']--;
                right++;
                if (cnt == 0)
                    ret.Add(left);
                if (right - left == p.Length)
                {
                    if (map[s[left] - 'a'] >= 0)
                    {
                        ++cnt;
                    }
                    map[s[left] - 'a']++;
                    left++;
                }
            }

            return ret;

            // for(int i=0; i<s.Length-p.Length; i++){
            //     bool ok = true;
            //     for(int j = i; j< i+p.Length; j++){
            //         var copy = (int[])pattern.Clone();
            //         if(--copy[s[j]-'a'] < 0){
            //             ok = false;
            //             break;
            //         }
            //     }
            //     if(ok){
            //         ret.Add(i);
            //     }
            // }
            // return ret;
        }

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
            {
                arrP[p[i] - 'a']++;
            }

            for (int i = 0; i < n; i++)
            {
                if (i >= l)
                    arrS[s[i - l] - 'a']--;
                arrS[s[i] - 'a']++;
                if (arrP.SequenceEqual(arrS))
                    ret.Add(i - l + 1);
            }

            return ret;
            //too slow
            //string patterm = string.Concat( p.OrderBy(c => c));
            //for(int i=0; i<=s.Length-p.Length; i++)
            //{
            //    string curSubStr = s.Substring(i, p.Length);
            //    if (patterm.Equals(string.Concat(curSubStr.OrderBy(c => c))))
            //        ret.Add(i);

            //}
            //return ret;

        }


        //159. Given a string s, find the length of the longest substring t that contains at most 2 distinct characters.
        //Example 1:  Input: "eceba"  Output: 3
        //Explanation: t is "ece" which its length is 3.
        //Example 2: Input: "ccaabbb" Output: 5
        //Explanation: t is "aabbb" which its length is 5.
        public int lengthOfLongestSubstringTwoDistinct(String s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int ret = 0;
            int backIdx = 0;
            var map = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                    map.Add(s[i], 1);
                else
                    map[s[i]]++;

                if (map.Count <= 2)
                {
                    ret = Math.Max(ret, i - backIdx + 1);
                }
                else  //current char out of 2 distinct. backIdx start to run 
                {
                    while (map.Count > 2)
                    {
                        if (map[s[backIdx]] == 1)
                            map.Remove(s[backIdx]);
                        else
                            map[s[backIdx]]--;

                        backIdx++;
                    }
                }
            }
            return ret;
        }

        //76. Minimum Window Substring
        //Given a string S and a string T, find the minimum window in S which will contain all the characters in T in complexity O(n).
        //Example: Input: S = "ADOBECODEBANC", T = "ABC"
        //Output: "BANC"
        //O(n)
        public string MinWindow4(string s, string t) {        
            //build lookup map
            var lookup = t.GroupBy(c=>c).ToDictionary(g=>g.Key, g=>g.Count());
            var map = new Dictionary<char, int>();
            int backPtr = 0;
            int idxSt = 0;
            int len = int.MaxValue;

            for(int i=0; i< s.Length; i++){
                if(!map.ContainsKey(s[i])){
                    map.Add(s[i],1);
                }else{
                    map[s[i]]+=1;
                }
                // if match the map match lookup table, try to shrink tail to find min len
                while(lookup.All(kv => map.ContainsKey(kv.Key)) && lookup.All(kv => map[kv.Key] <= kv.Value)){
                    if(i-backPtr +1 < len){
                        len = i-backPtr+1;
                        idxSt = backPtr;
                    }
                    if(map.ContainsKey(s[backPtr])){
                        if(map[s[backPtr]]==1)
                            map.Remove(s[backPtr]);
                        else
                            map[s[backPtr]]-=1;    
                    }
                    backPtr--;
                }            
            }
            return len == int.MaxValue ? "" : s.Substring(idxSt, len);
        }

        public string minWindow3(string s, string t)
        {
            var map = t.GroupBy(c=>c).ToDictionary(g=>g.Key, g=>g.Count());
            var map2 = new Dictionary<char, int>();
            int len = int.MaxValue;
            int stIdx = 0;
            int backPtr = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (map2.ContainsKey(s[i]))
                    map2[s[i]]++;
                else
                    map2.Add(s[i], 1);

                while (map.All(kv => map2.ContainsKey(kv.Key)) && map.All(kv => map[kv.Key] <= map2[kv.Key]))
                {
                    if (i - backPtr + 1 < len)
                    {
                        stIdx = backPtr;
                        len = i - backPtr + 1;
                    }
                    // shrank back
                    if (map2.ContainsKey(s[backPtr]))
                    {
                        if (map2[s[backPtr]] == 1)
                            map2.Remove(s[backPtr]);
                        else
                            map2[s[backPtr]]--;
                    }
                    backPtr++;
                }
            }
            return len == int.MaxValue ? "" : s.Substring(stIdx, len);
        }

        public string minWindow2(string s, string t)
        {
            var map = new Dictionary<char, int>();

            for (int i = 0; i < t.Length; i++)
            {
                if (map.ContainsKey(t[i]))
                    map[t[i]]++;
                else
                    map.Add(t[i], 1);
            }
            var map2 = new Dictionary<char, int>();
            int len = int.MaxValue, cnt = 0;
            int stIdx = 0;
            int backPtr = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                {
                    // ** avoid extra same char to mess up map count
                    if (map[s[i]] > 0)
                        cnt++;

                    map[s[i]]--;
                }

                while (cnt == t.Length)
                {
                    if (i - backPtr + 1 < len)
                    {
                        stIdx = backPtr;
                        len = i - backPtr + 1;
                    }
                    if (map.ContainsKey(s[backPtr]))
                    {
                        if (map[s[backPtr]] >= 0)
                            cnt--;
                        map[s[backPtr]]++;
                    }
                    backPtr++;
                }
            }
            return len == int.MaxValue ? "" : s.Substring(stIdx, len);
        }


        //525. Contiguous Array
        //Given a binary array, find the maximum length of a contiguous subarray with equal number of 0 and 1.
        //Example 1:Input: [0,1]        
        //Output: 2  Explanation: [0, 1] is the longest contiguous subarray with equal number of 0 and 1.
        public int FindMaxLength(int[] nums)
        {
            if (nums == null)
                return 0;

            var map = new Dictionary<int, int>();
            map.Add(0, -1);
            int curSum = 0;
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i] == 0 ? -1 : 1;

                if (!map.ContainsKey(curSum))
                    map.Add(curSum, i);
                else
                {
                    ret = Math.Max(ret, i - map[curSum]);

                }
            }
            return ret;
        }

        //523. Continuous Subarray Sum
        //Given a list of non-negative numbers and a target integer k, write a function to check if the array 
        //has a continuous subarray of size at least 2 that sums up to the multiple of k, that is, sums up 
        //to n*k where n is also an integer.
        // Example 1:input: [23, 2, 4, 6, 7],  k=6
        //Output: True Explanation: Because[2, 4] is a continuous subarray of size 2 and sums up to 6. or 6xn
        //用了些技巧，那就是，若数字a和b分别除以数字c，若得到的余数相同，那么(a-b)必定能够整除c
        public bool CheckSubarraySum2(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return false;

            int curSum = 0;
            var map = new Dictionary<int, int>();  //key is Module, value is index
            map.Add(0, -1);
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];
                int modu = k == 0 ? curSum : curSum % k;

                if (map.ContainsKey(modu))
                {
                    if (i - map[modu] > 1)
                        return true;
                }
                else
                    map.Add(modu, i);

            }
            return false;
        }
        public bool CheckSubarraySum(int[] nums, int k)
        {
            if (nums == null)
                return false;

            for (int i = 0; i < nums.Length; i++)
            {
                int sum = nums[i];
                for (int j = i + 1; j < nums.Length; j++)
                {
                    sum += nums[j];
                    if (sum == k)  //case k =sum=0
                        return true;
                    if (k != 0 && sum % k == 0)
                        return true;
                }
            }
            return false;
        }

        //560. Subarray Sum Equals K
        //Given an array of integers and an integer k, you need to find the total number of 
        //continuous subarrays whose sum equals to k.
        //Example 1: Input:nums = [1,1,1], k = 2
        //Output: 2
        //Note:The length of the array is in range[1, 20, 000].
        //Note: The length of the array is in range[1, 20, 000].
        //The range of numbers in the array is [-1000, 1000] and the range of the integer k is [-1e7, 1e7].
        public int SubarraySum1(int[] nums, int k)
        {
            // brutal force create sum array and find sum and previous possible sum
            var sumList = new int[nums.Length];
            sumList[0] = nums[0];
            for (int i = 1; i < sumList.Length; i++)
                sumList[i] = sumList[i - 1] + nums[i];

            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (k == sumList[i])
                {
                    ret++;
                }
                for (int j = i - 1; j >= 0; j--)
                {
                    if (sumList[i] - sumList[j] == k)
                    {
                        ret++;
                    }
                }
            }
            return ret;
        }
        public int SubarraySum(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);

            int curSum = 0;
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];
                //sum2- sum1 =k correct results is between 2 parts of sum curSum = sum2 
                if (map.ContainsKey(curSum - k))
                    ret += map[curSum - k];

                if (!map.ContainsKey(curSum))
                    map.Add(curSum, 1);
                else
                    map[curSum] += 1;
            }
            return ret;
        }


        //325.Maximum Size Subarray Sum Equals k 
        //Given an array nums and a target value k, find the maximum length of a subarray that sums to k.
        //If there isn't one, return 0 instead.
        //Example 1: Given nums = [1, -1, 5, -2, 3], k = 3,
        //return 4. (because the subarray[1, -1, 5, -2] sums to 3 and is the longest)
        //Example 2: Given nums = [-2, -1, 2, 1], k = 1,
        //return 2. (because the subarray[-1, 2] sums to 1 and is the longest)
        public int maxSubArrayLen(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            var ht = new Dictionary<int, int>();
            ht.Add(0, -1);
            int sum = 0;
            int maxLength = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (!ht.ContainsKey(sum))
                {
                    ht.Add(sum, i);
                }
                if (ht.ContainsKey(sum - k))
                {
                    maxLength = Math.Max(maxLength, i - ht[sum - k]);
                }
            }
            return maxLength;
        }


        //leetcode 1. Two Sum
        //Given an array of integers, return indices of the two numbers such that they add up to a specific target.
        //You may assume that each input would have exactly one solution, and you may not use the same element twice.
        //Example: Given nums = [2, 7, 11, 15], target = 9,  Because nums[0] + nums[1] = 2 + 7 = 9,return [0, 1].       
        public int[] TwoSum(int[] nums, int target)
        {
            List<int> ret = new List<int>();
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (map.ContainsKey(target - nums[i]))
                {
                    ret.Add(map[target - nums[i]]);
                    ret.Add(i);
                    return ret.ToArray();
                }
                if (!map.ContainsKey(nums[i]))
                    map.Add(nums[i], i);
            }
            return ret.ToArray();
        }

        //167. Two Sum II - Input array is sorted
        public int[] TwoSum2(int[] nums, int target)
        {
            int i = 0, j = nums.Length - 1;
            var ret = new List<int>();
            while (i < j)
            {
                int temp = nums[i] + nums[j];
                if (temp == target)
                {
                    ret.Add(i);
                    ret.Add(j);
                    return ret.ToArray();
                }
                else if (temp > target)
                {
                    j--;
                }
                else
                {
                    i++;
                }
            }
            return ret.ToArray();
        }

        //409. Longest Palindrome
        //Given a string which consists of lowercase or uppercase letters, find the length of the longest palindromes 
        //that can be built with those letters.
        //This is case sensitive, for example "Aa" is not considered a palindrome here.
        // Note:Assume the length of given string will not exceed 1,010.
        //e.g. Input: "abccccdd"   Output:7
        //Explanation: One longest palindrome that can be built is "dccaccd", whose length is 7.
        public int LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;
            var map = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                    map[s[i]]++;
                else
                    map.Add(s[i], 1);
            }
            int ret = 0;
            bool singleUsed = false;
            foreach (var kv in map)
            {
                if (kv.Value % 2 == 0)
                    ret += kv.Value;
                else if (kv.Value > 2)
                {
                    ret += singleUsed ? kv.Value - 1 : kv.Value;
                    singleUsed = true;
                }
                else if (!singleUsed)
                {
                    ret += 1;
                    singleUsed = true;
                }
            }
            return ret;

        }


        //389. Find the Difference
        //Given two strings s and t which consist of only lowercase letters.
        //String t is generated by random shuffling string s and then add one more letter at a random position.
        //Find the letter that was added in t.
        //Input: s = "abcd"  t = "abcde"  ; Output:Explanation:'e' is the letter that was added.





        //349. Intersection of Two Arrays   (106/260)
        //Given two arrays, write a function to compute their intersection.
        //Example: Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2].
        //Note: Each element in the result must be unique. The result can be in any order.
        // O(n) / O(3n)
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            var l1 = nums1.ToList();
            var l2 = nums2.ToList();

            var cc = from item in l1
                     where (nums2.Contains(item))
                     select item;

            return new HashSet<int>(cc).ToArray();
            /*
                var c = from i in Enumerable.Range(0, l1.Count)
                        from j in Enumerable.Range(0, l2.Count)
                        where l1[i] == l2[j]
                        select l1[i];
            */
        }

        public int[] Intersection2(int[] nums1, int[] nums2)
        {
            //是将一个数组排序，然后遍历另一个数组，把遍历到的每个数字在排序号的数组中用二分查找法搜索，如果能找到则放入结果set中
            Array.Sort(nums1);
            var ret = new HashSet<int>();
            for (int i = 0; i < nums2.Length; i++)
            {
                if (BSearch(nums2[i], nums1))
                {
                    ret.Add(nums2[i]);
                }
            }
            return ret.ToArray();
        }
        bool BSearch(int t, int[] arr)
        {
            int i = 0, j = arr.Length - 1;
            while (i < j)
            {
                int piv = (i + j) / 2;
                if (arr[piv] == t)
                    return true;
                if (arr[piv] > t)
                    j = piv;
                else
                    i = piv + 1;
            }
            return false;
        }

        //350. Intersection of Two Arrays II
        //Given two arrays, write a function to compute their intersection.
        //Example: Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2, 2].
        //Note:Each element in the result should appear as many times as it shows in both arrays.
        //The result can be in any order.
        //Follow up:
        //What if the given array is already sorted? How would you optimize your algorithm?
        //What if nums1's size is small compared to nums2's size? Which algorithm is better?
        //What if elements of nums2 are stored on disk, and the memory is limited such that you cannot load all elements into the memory at once?
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums1.Length; i++)
            {
                if (!map.ContainsKey(nums1[i]))
                    map.Add(nums1[i], 1);
                else
                    map[nums1[i]]++;
            }

            var ret = new List<int>();

            for (int i = 0; i < nums2.Length; i++)
            {
                if (map.ContainsKey(nums2[i]) && map[nums2[i]] > 0)
                {
                    ret.Add(nums2[i]);
                    map[nums2[i]]--;
                }
            }
            return ret.ToArray();
        }
        //follow up: use sort and 2 pointer
        public int[] Intersect2(int[] nums1, int[] nums2)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);
            int i = 0;
            int j = 0;
            var ret = new List<int>();
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] == nums2[j])
                {
                    ret.Add(nums1[i]);
                    i++;
                    j++;
                }
                else if (nums1[i] > nums2[j])
                    j++;
                else
                    i++;
            }
            return ret.ToArray();
        }


    }
}
