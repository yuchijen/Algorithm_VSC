using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class Point
    {
        public Point(int i, int j)
        {
            x = i;
            y = j;
        }
        public Point()
        {
        }
        public int x { get; set; }
        public int y { get; set; }
        public int dist { get; set; }
    }

    public class DFS_BFS
    {    
        //787. Cheapest Flights Within K Stops (FB) 
        int curMin = int.MaxValue;
        public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
        {
            var map = new Dictionary<int, List<List<int>>>();
            //flights.GroupBy(g => g[0]).ToDictionary<
            for (int i = 0; i < n; i++)
            {
                map.Add(i, new List<List<int>>());
            }
            for (int i = 0; i < flights.Length; i++)
            {
                if (map.ContainsKey(flights[i][0]))
                {
                    map[flights[i][0]].Add(new List<int>() { flights[i][1], flights[i][2] });
                }
            }

            var visited = new bool[n];
            findDFSPricePath(src, dst, 0, 0, map, k, visited);

            return curMin == int.MaxValue ? -1 : curMin;
        }

        // DFS: O(V+E) where V is all vertices, E is all number of edges 
        void findDFSPricePath(int src, int dst, int cost, int pathCnt, Dictionary<int, List<List<int>>> map, int k, bool[] visited)
        {
            if (src == dst)
            {
                curMin = Math.Min(curMin, cost);
                return;
            }

            if (visited[src] || pathCnt > k)
                return;

            visited[src] = true;

            for (int i = 0; i < map[src].Count; i++)
            {
                findDFSPricePath(map[src][i][0], dst, cost + map[src][i][1], pathCnt + 1, map, k, visited);
            }
            visited[src] = false;
        }

        // BFS: O(V+E) where V is all vertices, E is all number of edges 
        // each node get into queue (V); and each node's adj edges will enqueue too (E)
        public int FindCheapestPriceBFS(int n, int[][] flights, int src, int dst, int k)
        {
            var map = new Dictionary<int, List<List<int>>>();
            //flights.GroupBy(g => g[0]).ToDictionary<
            for (int i = 0; i < n; i++)
            {
                map.Add(i, new List<List<int>>());
            }
            for (int i = 0; i < flights.Length; i++)
            {
                if (map.ContainsKey(flights[i][0]))
                {
                    map[flights[i][0]].Add(new List<int>() { flights[i][1], flights[i][2] });
                }
            }

            var q = new Queue<List<int>>();
            q.Enqueue(new List<int> { src, 0 });
            int curMin = 0;
            //int cost =0;
            int pathCnt = 0;
            while (q.Count > 0)
            {
                for (int i = 0; i < q.Count; i++)
                {
                    var curNode = q.Dequeue();
                    if (curNode[0] == dst)
                    {
                        curMin = Math.Min(curMin, curNode[1]);
                        //return curMin;
                    }
                    if (curNode[2] > k)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (var child in map[curNode[0]])
                        {
                            if (curNode[1] + child[1] > curMin)
                                continue;
                            q.Enqueue(new List<int> { child[0], curNode[1] + child[1] });
                        }
                    }
                }

                if (pathCnt > k)
                    break;
                pathCnt += 1;
            }
            return curMin == int.MaxValue ? -1 : curMin;
        }

        //Dijkstra: is similar with BFS, the difference is using priority queue instand of queue. 
        //The priority will always pop the element with heigher priority. In this case, the cost is lower the 
        //priority is heigher. Once the pop src is equal to destination we will have the answer.
        public int FindCheapestPriceDijkstra(int n, int[][] flights, int src, int dst, int k)
        {
            var map = new Dictionary<int, List<List<int>>>();
            for (int i = 0; i < n; i++)
            {
                map.Add(i, new List<List<int>>());
            }
            for (int i = 0; i < flights.Length; i++)
            {
                if (map.ContainsKey(flights[i][0]))
                {
                    map[flights[i][0]].Add(new List<int>() { flights[i][1], flights[i][2] });
                }
            }

            var q = new Queue<List<int>>();
            q.Enqueue(new List<int> { src, 0, 0 });
            int curMin = 0;
            while (q.Count > 0)
            {
                q = new Queue<List<int>>(q.OrderBy(Node => Node[1]));
                var curNode = q.Dequeue();
                if (curNode[0] == dst)
                {
                    return curNode[1];
                }
                if (curNode[2] > k)
                {
                    continue;
                }
                else
                {
                    foreach (var child in map[curNode[0]])
                    {
                        if (curNode[1] + child[1] > curMin)
                            continue;
                        q.Enqueue(new List<int> { child[0], curNode[1] + child[1], curNode[2] + 1 });
                    }
                }
            }
            return curMin == int.MaxValue ? -1 : curMin;
        }


        //127. Word Ladder
        //Given two words(beginWord and endWord), and a dictionary's word list, find the length of shortest transformation sequence from beginWord to endWord, such that:
        //Only one letter can be changed at a time.
        //Each transformed word must exist in the word list.Note that beginWord is not a transformed word.
        //Note:
        //Return 0 if there is no such transformation sequence.
        //All words have the same length.
        //All words contain only lowercase alphabetic characters.
        //You may assume no duplicates in the word list.
        //You may assume beginWord and endWord are non-empty and are not the same.
        //Example 1:Input:
        //beginWord = "hit",
        //endWord = "cog",
        //wordList = ["hot","dot","dog","lot","log","cog"]
        //Output: 5
        //Explanation: As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
        //return its length 5.
        //跟迷宫遍历很像啊，你想啊，迷宫中每个点有上下左右四个方向可以走，而这里有26个字母，就是二十六个方向可以走，
        //本质上没有啥区别啊！如果熟悉迷宫遍历的童鞋们应该知道，应该用BFS来求最短路径的长度，这也不难理解啊，
        //DFS相当于一条路走到黑啊，你走的那条道不一定是最短的啊。而BFS相当于一个小圈慢慢的一层一层扩大
        public int LadderLengthBFS(string beginWord, string endWord, IList<string> wordList)
        {
            var hs = new HashSet<string>(wordList);
            if (!hs.Contains(endWord))
                return 0;
            var q = new Queue<string>();
            if (beginWord == endWord)
                return 0;

            int ret = 0;
            q.Enqueue(beginWord);

            while (q.Count > 0)
            {
                ret += 1;
                int cnt = q.Count;
                //each leyer (possible candidates in hashset on last layer search)
                for (int l = 0; l < cnt; l++)
                {
                    var str = q.Dequeue();
                    if (str == endWord)
                        return ret;

                    char[] curStr = str.ToCharArray();
                    for (int i = 0; i < curStr.Length; i++)
                    {
                        char c = curStr[i];
                        for (char j = 'a'; j <= 'z'; j++)
                        {
                            if (j == c)
                                continue;
                            curStr[i] = j;
                            string newStr = new string(curStr);

                            if (hs.Contains(newStr))
                            {
                                q.Enqueue(newStr);
                                hs.Remove(newStr);
                            }
                        }
                        curStr[i] = c;
                    }
                }
            }
            return 0;
        }

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if (string.IsNullOrEmpty(beginWord)
                    || string.IsNullOrEmpty(endWord)
                    || wordList == null
                    || wordList.Count == 0
                    || !wordList.Contains(endWord))
                return 0;

            var chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            var hs = new HashSet<string>(wordList);
            hs.Add(beginWord);
            var map = new Dictionary<string, bool>();
            foreach (var str in hs)
                map.Add(str, false);

            var ret = new int[1] { 0 };
            DFSLadderHelper(beginWord, endWord, ret, chars, hs, map);
            return ret[0];
        }

        void DFSLadderHelper(string st, string end, int[] ret, char[] chs, HashSet<string> hs, Dictionary<string, bool> visited)
        {
            if (st == end)
                return;

            if (!visited.ContainsKey(st))
                return;

            if (visited.All(x => x.Equals(true)))
            {
                ret[0] = 0;
                return;
            }
            if (visited[st])
            {
                ret[0] = ret[0] > 0 ? --ret[0] : ret[0];
                return;
            }

            visited[st] = true;

            int maxLen = st.Length == 1 ? 1 : st.Length - 1;
            for (int i = 0; i < maxLen; i++)
            {
                foreach (var c in chs)
                {
                    string possiStr = st.Substring(0, i) + c + st.Substring(i + 1);
                    if (st != possiStr && hs.Contains(possiStr) && !visited[possiStr])
                    {
                        ret[0] += 1;
                        DFSLadderHelper(possiStr, end, ret, chs, hs, visited);
                    }
                }
            }
            visited[st] = false;
        }

        //126. Word Ladder II
        // public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList) {
        //     var ret = new List<IList<string>>();

        //     if (string.IsNullOrEmpty(beginWord) 
        //             || string.IsNullOrEmpty(endWord) 
        //             || wordList == null 
        //             || wordList.Count == 0 
        //             || !wordList.Contains(endWord))
        //         return ret;

        //     var hs = new HashSet<string>(wordList);
        //     Dictionary<string, bool> map = wordList.GroupBy(x=>x).ToDictionary(x=>x.Key, x=> false);
        //     map.Add(beginWord, false);

        //     var q = new Queue<string>();
        //     q.Enqueue(beginWord);
        //     int qLev = 0;
        //     while(q.Count > 0){
        //         int qCnt = q.Count;
        //         if(ret[qLev].Count <= qLev){
        //             ret.Add(new List<string>());
        //         }
        //         qLev += 1;

        //         for(int l =0; l< qCnt; l++){
        //             var cur = q.Dequeue();
        //             map[cur] = true;

        //             char[] curArr = cur.ToCharArray();
        //             for(int i=0; i< curArr.Length; i++){
        //                 char originChar = curArr[i];                        

        //                 for(var j = 'a'; j<='z'; j++){
        //                     if(j== originChar)
        //                         continue;
        //                     curArr[i] = j;    
        //                     string newWord = curArr.ToString();     
        //                     if(hs.Contains(newWord)){
        //                         q.Enqueue(newWord);
        //                     }
        //                 }                    
        //                 curArr[i] = originChar;
        //             }
        //         }
        //     }

        // }

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
            if (string.IsNullOrEmpty(s))
                return 0;

            var ret = new int[1] { 0 };
            for (int i = 0; i < s.Length; i++)
            {
                helper(i, i, ret, s);
                helper(i, i + 1, ret, s);
            }
            return ret[0];
        }
        void helper(int i, int j, int[] ret, string s)
        {
            while (i >= 0 && j < s.Length && s[i] == s[j])
            {
                ret[0] += 1;
                i--;
                j++;
            }
        }

        //416. Partition Equal Subset Sum
        //Given a non-empty array containing only positive integers, find if the array can be partitioned into two subsets such that the sum of elements in both subsets is equal.
        //Note: Each of the array element will not exceed 100.
        //The array size will not exceed 200.
        //Example 1:Input: [1, 5, 11, 5]
        //Output: true
        //Explanation: The array can be partitioned as [1, 5, 5] and[11].
        //DFS approach : time : O(2^n)
        public bool CanPartition(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;

            return CanPartitionDPS(nums, nums.Length, sum / 2);

        }
        bool CanPartitionDPS(int[] nums, int n, int target)
        {
            if (target == 0)
                return true;
            if (n == 0 && target != 0)
                return false;

            return CanPartitionDPS(nums, n - 1, target - nums[n - 1]) || CanPartitionDPS(nums, n - 1, target);
        }


        public class UndirectedGraphNode
        {
            public int label;
            public IList<UndirectedGraphNode> neighbors;
            public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
        }

        public class Node2
        {
            public int val;
            public IList<Node2> neighbors;

            public Node2()
            {
                val = 0;
                neighbors = new List<Node2>();
            }

            public Node2(int _val)
            {
                val = _val;
                neighbors = new List<Node2>();
            }

            public Node2(int _val, List<Node2> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        //133. Clone Graph
        //Given the head of a graph, return a deep copy(clone) of the graph.Each node in the graph 
        //contains a label(int) and a list(List[UndirectedGraphNode]) of its neighbors.There is an 
        //edge between the given node and each of the nodes in its neighbors.
        public Node2 CloneGraph2(Node2 node)
        {
            if (node == null)
                return null;

            var map = new Dictionary<Node2, Node2>();
            map.Add(node, new Node2(node.val));

            return CloneGraphDFS(node, map);
        }
        public Node2 CloneGraphBFS(Node2 node)
        {
            if (node == null)
                return null;

            var map = new Dictionary<Node2, Node2>();
            map.Add(node, new Node2(node.val));

            var q = new Queue<Node2>();
            q.Enqueue(node);

            while (q.Count > 0)
            {
                var curNode = q.Dequeue();
                if (!map.ContainsKey(curNode))
                {
                    var cloneNode = new Node2(curNode.val);
                    map.Add(curNode, cloneNode);
                }
                foreach (var n in curNode.neighbors)
                {
                    if (!map.ContainsKey(n))
                    {
                        map.Add(n, new Node2(n.val));
                        q.Enqueue(n);
                    }
                    map[curNode].neighbors.Add(map[n]);
                }
            }
            return map[node];
        }


        public Node2 CloneGraphDFS(Node2 node, Dictionary<Node2, Node2> map)
        {
            if (node == null)
                return null;

            if (map.ContainsKey(node))
            {
                return map[node];
            }
            var newNode = new Node2(node.val);
            map.Add(node, newNode);
            foreach (var n in node.neighbors)
            {
                newNode.neighbors.Add(CloneGraphDFS(n, map));
            }
            return newNode;
        }

        public UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
        {
            if (node == null)
                return null;

            Dictionary<UndirectedGraphNode, UndirectedGraphNode> dic = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
            dic.Add(node, new UndirectedGraphNode(node.label));
            Queue<UndirectedGraphNode> qu = new Queue<UndirectedGraphNode>();
            qu.Enqueue(node);

            while (qu.Count != 0)
            {
                var curNode = qu.Dequeue();

                foreach (var nei in curNode.neighbors)
                {
                    if (!dic.ContainsKey(nei))
                    {
                        qu.Enqueue(nei);
                        dic.Add(nei, new UndirectedGraphNode(nei.label));
                    }
                    dic[curNode].neighbors.Add(dic[nei]);
                }
            }
            return dic[node];
        }


        //953. Verifying an Alien Dictionary
        //In an alien language, surprisingly they also use english lowercase letters, but possibly in a different order. The order of the alphabet is some permutation of lowercase letters.
        //Given a sequence of words written in the alien language, and the order of the alphabet, return true if and only if the given words are sorted lexicographicaly in this alien language.
        //Example 1: Input: words = ["hello","leetcode"], order = "hlabcdefgijkmnopqrstuvwxyz"
        //Output: true
        //Explanation: As 'h' comes before 'l' in this language, then the sequence is sorted.
        //Example 2:
        //Input: words = ["word","world","row"], order = "worldabcefghijkmnpqstuvxyz"
        //Output: false
        //Explanation: As 'd' comes after 'l' in this language, then words[0] > words[1], hence the sequence is unsorted.
        //Input: words = ["apple","app"], order = "abcdefghijklmnopqrstuvwxyz"
        //Output: false
        public bool IsAlienSorted2(string[] words, string order)
        {
            if (words == null || words.Length == 0)
                return true;

            for (int i = 0; i < words.Length - 1; i++)
            {
                if (!inorder(words[i], words[i + 1], order))
                    return false;
            }
            return true;
        }
        public bool inorder(String w1, String w2, string order)
        {
            for (int i = 0; i < w1.Length; i++)
            {
                if (i >= w2.Length)
                    return false;
                if (order.IndexOf(w1[i]) < order.IndexOf(w2[i]))
                    return true;
                else if (order.IndexOf(w1[i]) > order.IndexOf(w2[i]))
                    return false;
            }
            return true;
            //return w1.Length <= w2.Length;
        }


        //636. Exclusive Time of Functions
        //Given the running logs of n functions that are executed in a nonpreemptive single threaded CPU, find the exclusive time of these functions.
        //Each function has a unique id, start from 0 to n-1. A function may be called recursively or by another function.
        //A log is a string has this format : function_id:start_or_end:timestamp. For example, "0:start:0" means function 0 starts from the very beginning of time 0. "0:end:0" means function 0 ends to the very end of time 0.
        //Exclusive time of a function is defined as the time spent within this function, the time spent by calling other functions should not be considered as this function's exclusive time. You should return the exclusive time of each function sorted by their function id.
        //Functions could be called recursively, and will always end.
        //0start0 1start2 0end5 1end6 impossible 
        //0start0 1start2 1end5 0end6
        public int[] ExclusiveTime(int n, IList<string> logs)
        {
            if (logs == null)
                return new int[] { };
            var ret = new int[n];

            var st = new Stack<int>();
            int pre = 0;
            for (int i = 0; i < logs.Count; i++)
            {
                int id = int.Parse(logs[i].Split(':')[0]);
                string stOrEnd = logs[i].Split(':')[1];
                int time = int.Parse(logs[i].Split(':')[2]);

                if (stOrEnd.Equals("start"))
                {
                    if (st.Count > 0)
                    {
                        ret[st.Peek()] += time - pre;
                    }
                    pre = time;
                    st.Push(id);
                }
                else
                {
                    if (st.Count > 0)
                    {
                        ret[st.Peek()] += time - pre + 1;
                        pre = time + 1;   //be aware +1
                        st.Pop();
                    }
                }

            }
            return ret;
        }


        //886. Possible Bipartition
        public bool PossibleBipartition(int N, int[][] dislikes)
        {
            if (N == 0 || dislikes == null)
                return false;
            var visited = new int[N + 1];
            int color = 1;
            for (int i = 1; i <= N; i++)
            {
                if (visited[i] == 0 && !PossibleBipartitionDFS(color, dislikes, visited, i))
                    return false;
            }

            return true;
        }
        bool PossibleBipartitionDFS(int color, int[][] graph, int[] visited, int i)
        {
            visited[i] = color;

            for (int j = 0; j < graph[i].Length; j++)
            {
                if (visited[graph[i][j]] == color)
                    return false;

                if (visited[graph[i][j]] == 0 && !PossibleBipartitionDFS(-color, graph, visited, visited[graph[i][j]]))
                    return false;
            }

            return true;
        }


        //785. Is Graph Bipartite?
        //Given an undirected graph, return true if and only if it is bipartite.
        //Recall that a graph is bipartite if we can split it's set of nodes into two independent subsets A and B such that every edge in the graph has one node in A and another node in B.
        //The graph is given in the following form: graph[i] is a list of indexes j for which the edge between nodes i and j exists.Each node is an integer between 0 and graph.length - 1.  There are no self edges or parallel edges: graph[i] does not contain i, and it doesn't contain any element twice.
        //e.g. Input: [[1,3], [0,2], [1,3], [0,2]]
        //Output: true
        //hint: every node's neighber has to hold different color (only 2 colors bipartite) 
        // Time: O(V+E)  numbers of node and connections 
        //space: O(V+E)
        public bool IsBipartite(int[][] graph)
        {
            if (graph == null)
                return false;

            // 0 means not visited yet, 1 means visited as 1 party color, -1 means visited as another party color
            var visited = new int[graph.Length];

            for (int i = 0; i < visited.Length; i++)
            {
                if (visited[i] == 0 && !BipartiteDFSHelper(visited, graph, i, 1))
                    return false;
            }
            return true;
        }
        bool BipartiteDFSHelper(int[] visited, int[][] graph, int key, int color)
        {
            visited[key] = color;
            for (int i = 0; i < graph[key].Length; i++)// traversal neighbors and check 
            {
                if (visited[graph[key][i]] == color) //if neighbor has the same color, false
                    return false;
                //if not visited, put into DFS with another color 
                if (visited[graph[key][i]] == 0 && !BipartiteDFSHelper(visited, graph, graph[key][i], -color))
                    return false;
            }
            return true;
        }

        public bool IsBipartiteBFS(int[][] graph)
        {
            if (graph == null)
                return false;

            // 0 means not visited yet, 1 means visited as 1 party color, -1 means visited as another party color
            var visited = new int[graph.Length];
            var q = new Queue<int>();


            for (int i = 0; i < graph.Length; i++)
            {
                if (visited[i] != 0)
                    continue;
                q.Enqueue(i);
                visited[i] = 1;
                while (q.Count > 0)
                {
                    var node = q.Dequeue();
                    int curColor = visited[node];

                    for (int j = 0; j < graph[node].Length; j++)
                    {
                        if (visited[graph[node][j]] == curColor)
                            return false;
                        if (visited[graph[node][j]] == 0)
                        {
                            q.Enqueue(graph[node][j]);
                            visited[graph[node][j]] = -curColor;
                        }
                    }
                }
            }
            return true;
        }

        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            // build graph 
            var g = new List<int>[numCourses];

            for (int i = 0; i < prerequisites.Length; i++)
            {
                if (g[prerequisites[i][0]] == null)
                    g[prerequisites[i][0]] = new List<int>();

                g[prerequisites[i][0]].Add(prerequisites[i][1]);
            }
            var visit = new int[numCourses];
            for (int i = 0; i < numCourses; i++)
            {
                if (hasCycle(i, g, visit))
                {
                    return false;
                }
            }

            return true;
        }

        bool hasCycle(int n, List<int>[] g, int[] visit)
        {
            if (visit[n] == 1) //in middle of path
                return true;

            if (visit[n] == 2)  // this node is the end of graph
                return false;

            visit[n] = 1;
            if (g[n] != null)
            {
                foreach (var i in g[n])
                {
                    if (hasCycle(i, g, visit))
                        return true;
                }
            }

            visit[n] = 2;
            return false;
        }

        //269. Alien Dictionary (topological sort)
        //There is a new alien language which uses the latin alphabet. However, the order among letters 
        //are unknown to you. You receive a list of non-empty words from the dictionary, where words 
        //are sorted lexicographically by the rules of this new language. Derive the order of letters 
        //in this language.
        //Example 1:  
        //Input:[
        //      "wrt",
        //      "wrf",
        //      "er",
        //      "ett",
        //      "rftt"
        //      ]    Output: "wertf"
        //e.g.2 Input:
        //[
        //  "z",
        //  "x",
        //  "z"
        //]   Output: ""   cycle not allowed
        //Note:
        //You may assume all letters are in lowercase.
        //You may assume that if a is a prefix of b, then a must appear before b in the given dictionary.
        //If the order is invalid, return an empty string.
        //There may be multiple valid order of letters, return any one of them is fine.
        //space O(V+E), time: DFS which is O(V+E)
        public string alienOrder(string[] words)
        {
            if (words == null)
                return null;
            //build graph
            var map = new Dictionary<char, HashSet<char>>();
            for (int i = 0; i < words.Length - 1; i++)
            {
                int idx = 0;
                int len = Math.Min(words[i].Length, words[i + 1].Length);
                while (idx < len)
                {
                    if (words[i][idx] != words[i + 1][idx])
                    {
                        if (map.ContainsKey(words[i][idx]))
                            map[words[i][idx]].Add(words[i + 1][idx]);
                        else
                            map.Add(words[i][idx], new HashSet<char> { words[i + 1][idx] });

                        break;
                    }
                    idx++;
                }
                if (idx == len && words[i].Length > words[i + 1].Length)
                    return "";
            }
            // Console.WriteLine("alien graph:");
            // foreach(var kv in map){
            //     Console.Write(kv.Key +":");
            //     foreach(var v in kv.Value)
            //         Console.Write(v+" ");

            //     Console.WriteLine("");                    
            // }
            var allSet = new String(string.Join("", words).Distinct().ToArray());

            var visited = new int[26];

            var ret = new HashSet<char>();
            foreach (var c in allSet)
            {
                if (FindCycle(c, ret, map, visited)) //if cycle happened
                    return "";
            }
            return string.Join("", ret.ToArray().Reverse());
        }

        //return true if found cycle
        bool FindCycle(char c, HashSet<char> ret, Dictionary<char, HashSet<char>> map, int[] visited)
        {
            if (visited[c - 'a'] == 1)
                return true;
            if (visited[c - 'a'] == 2)
                return false;

            visited[c - 'a'] = 1;
            if (map.ContainsKey(c))
            {
                foreach (var cc in map[c])
                {
                    if (FindCycle(cc, ret, map, visited))
                        return true;
                }
            }

            visited[c - 'a'] = 2;
            ret.Add(c);
            return false;
        }

        //by Hanrey Liu
        public String alienOrderBFS(String[] words)
        {
            if (words.Length == 0) return "";
            var map = new Dictionary<char, HashSet<char>>();
            var indegree = new Dictionary<char, int>();

            //Map<Character, Integer> indegree = new HashMap<>();

            foreach (String word in words)
            {
                foreach (char c in word)
                {
                    indegree.Add(c, 0);
                }
            }
            for (int i = 0; i < words.Length - 1; i++)
            {
                String curr = words[i];
                String next = words[i + 1];
                addDependency(curr, next, words, indegree, map);
            }
            Queue<char> queue = new Queue<char>();
            StringBuilder sb = new StringBuilder();
            foreach (char c in indegree.Keys)
            {
                if (indegree[c] == 0)
                {
                    queue.Enqueue(c);
                }
            }
            while (queue.Count > 0)
            {
                char curr = queue.Dequeue();
                sb.Append(curr);
                HashSet<char> child = map[curr];
                if (child != null)
                {
                    foreach (char node in child)
                    {
                        indegree.Add(node, indegree[node] - 1);
                        if (indegree[node] == 0)
                        {
                            queue.Enqueue(node);
                        }
                    }
                }
            }
            return sb.Length == indegree.Count ? sb.ToString() : "";
        }
        private void addDependency(String curr, String next, String[] words, Dictionary<char, int> indegree,
                                   Dictionary<char, HashSet<char>> map)
        {
            int len = Math.Min(curr.Length, next.Length);
            for (int i = 0; i < len; i++)
            {
                char c1 = curr[i];
                char c2 = next[i];
                if (c1 != c2)
                {
                    if (!map.ContainsKey(c1))
                    {
                        map.Add(c1, new HashSet<char>());
                    }
                    if (!map[c1].Add(c2))
                    {
                        break;
                    }
                    indegree[c2] += 1;
                    //indegree.Add(c2, indegree[c2] + 1);
                    break;
                }
            }
        }

        //44. Wildcard Matching  (DFS not passed yet)
        //Given an input string (s) and a pattern (p), implement wildcard pattern matching with support for '?' and '*'.
        // '?' Matches any single character.
        // '*' Matches any sequence of characters (including the empty sequence).
        // The matching should cover the entire input string (not partial).
        // Note:
        // s could be empty and contains only lowercase letters a-z.
        // p could be empty and contains only lowercase letters a-z, and characters like ? or *.
        //Input:
        // s = "aa"
        // p = "*"
        // Output: true
        // Explanation: '*' matches any sequence.
        // Example 3:
        // Input:
        // s = "cb"
        // p = "?a"
        // Output: false
        // Explanation: '?' matches 'c', but the second letter is 'a', which does not match 'b'.
        // Example 4:
        // Input:
        // s = "adceb"
        // p = "*a*b"
        // Output: true
        // Explanation: The first '*' matches the empty sequence, while the second '*' matches the substring "dce".
        // isMatch("aa","a") → false
        // isMatch("aa", "a*") → true
        // isMatch("ab", "?*") → true
        // isMatch("aab", "c*a*b") → false
        // isMatch("aaaa","***a") -> true
        public bool IsMatch(string s, string p)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(p))
                return false;
            if (s == p)
                return true;
            return isMatchHelper(s, p, 0, 0);

        }
        bool isMatchHelper(string s, string p, int si, int pi)
        {
            if (pi == p.Length)
                return si == s.Length;
            if (si == s.Length)
                return pi == p.Length || (pi == p.Length - 1 && p[pi] == '*');

            if (s[si] == p[pi] || p[pi] == '?')
            {
                return isMatchHelper(s, p, si + 1, pi + 1);
            }
            else if (p[pi] == '*')
            {
                if (pi + 1 < p.Length && p[pi + 1] == '*')
                    return isMatchHelper(s, p, si + 1, pi + 1);
                else if (pi + 1 < p.Length && s[si] != p[pi + 1])
                    return isMatchHelper(s, p, si + 1, pi);
                else if (pi + 1 < p.Length && s[si] == p[pi + 1])
                    return isMatchHelper(s, p, si + 1, pi + 2) || isMatchHelper(s, p, si + 1, pi);
                else
                    return true;
            }
            else
                return false;
        }


        //10. Regular Expression Matching
        //Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.
        //'.' Matches any single character.'*' Matches zero or more of the preceding element.
        //The matching should cover the entire input string (not partial).
        //Note: s could be empty and contains only lowercase letters a-z.
        //p could be empty and contains only lowercase letters a-z, and characters like.or*.
        //Example 1: Input: s = "aa"  p = "a" Output: false
        //Explanation: "a" does not match the entire string "aa".
        //Example 2: Input: s = "aa"  p = "a*"  Output: true
        //Explanation: '*' means zero or more of the precedeng element, 'a'. Therefore, by repeating 'a' 
        //once, it becomes "aa".
        //Example 3: Input:s = "ab" p = ".*" Output: true
        //Explanation: ".*" means "zero or more (*) of any character (.)".
        //Example 4: Input: s = "aab" p = "c*a*b" Output: true
        //Explanation: c can be repeated 0 times, a can be repeated 1 time.Therefore it matches "aab".
        public bool IsMatch2(string s, string p)
        {
            if (s == null || p == null)
                return false;
            if (s == p)
                return true;
            return isMatch(s, p, 0, 0);
        }
        bool isMatch(String s, String p, int i, int j)
        {
            if (j == p.Length)
            {
                return s.Length == i;
            }
            bool isFirstMatch = i < s.Length && (s[i] == p[j] || p[j] == '.');

            //next char is *, then if first char not match, the * is 0, go check next in j and i remain the same
            if (j + 1 < p.Length && p[j + 1] == '*')
            {
                return isMatch(s, p, i, j + 2) || (isFirstMatch && isMatch(s, p, i + 1, j));
            }
            else
            {
                return isFirstMatch && isMatch(s, p, i + 1, j + 1);
            }
        }

        //680. Valid Palindrome II
        //Given a non-empty string s, you may delete at most one character. Judge whether you can make it a palindrome.
        // Example 1:Input: "aba"   Output: True
        //Example 2:   Input: "abca"  Output: True
        //Explanation: You could delete the character 'c'.
        public bool ValidPalindrome2(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            return ValidPalindromeHelper2(s, 0, s.Length - 1, false);
        }
        bool ValidPalindromeHelper2(string s, int i, int j, bool skip)
        {
            while (i < j)
            {
                if (s[i] != s[j] && skip)
                    return false;
                else if (s[i] != s[j] && !skip)
                {
                    return ValidPalindromeHelper2(s, i + 1, j, true) || ValidPalindromeHelper2(s, i, j - 1, true);
                }
                i++;
                j--;
            }
            return true;
        }

        public bool ValidPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            return ValidPalindromeHelper(s, true);
        }
        bool ValidPalindromeHelper(string s, bool firstTime)
        {
            int st = 0;
            int end = s.Length - 1;

            while (st < end)
            {
                if (s[st] != s[end] && !firstTime)
                {
                    return false;
                }
                else if (s[st] != s[end] && firstTime)
                {
                    return ValidPalindromeHelper(s.Remove(st, 1), false) || ValidPalindromeHelper(s.Remove(end, 1), false);
                }
                else
                {
                    st++;
                    end--;
                }
            }
            return true;
        }

        //426. Convert Binary Search Tree to Sorted Doubly Linked List
        //Convert a BST to a sorted circular doubly-linked list in-place. Think of the left and right 
        //pointers as synonymous to the previous and next pointers in a doubly-linked list.
        //Let's take the following BST as an example, it may help you understand the problem better
        public class Node
        {
            public int val;
            public Node left;
            public Node right;

            public Node() { }
            public Node(int _val, Node _left, Node _right)
            {
                val = _val;
                left = _left;
                right = _right;
            }
        }

        public Node treeToDoublyListRecursive(Node root)
        {
            if (root == null)
                return null;
            Node head = null;
            Node pre = null;
            treeToNodeInOrderHelper(root, pre, head);
            pre.right = head;
            head.left = pre;
            return head;
        }

        void treeToNodeInOrderHelper(Node r, Node pre, Node head)
        {
            if (r == null)
                return;
            treeToNodeInOrderHelper(r.left, pre, head);
            if (head == null)
            {
                head = r;
            }
            else
            {
                pre.right = r;
                r.left = pre;
            }
            pre = r;
            treeToNodeInOrderHelper(r.right, pre, head);
        }

        public Node treeToDoublyList(Node root)
        {
            Node head = null;
            Node prev = null;
            Node curNode = root;
            var st = new Stack<Node>();
            while (curNode != null || st.Count > 0)
            {
                if (curNode != null) //push sub-left tree nodes
                {
                    st.Push(curNode);
                    curNode = curNode.left;
                }
                else
                {
                    curNode = st.Pop();
                    if (prev == null)
                    {
                        head = curNode;
                    }
                    else
                    {
                        prev.right = curNode;  //connect double links
                        curNode.left = prev;
                    }
                    prev = curNode;
                    curNode = curNode.right;
                }
            }
            prev.right = head;
            head.left = prev;
            return head;
        }


        //621. Task Scheduler
        //Given a char array representing tasks CPU need to do. It contains capital letters A to Z where 
        //different letters represent different tasks.Tasks could be done without original order. 
        //Each task could be done in one interval. For each interval, CPU could finish one task or just be idle.
        //However, there is a non-negative cooling interval n that means between two same tasks, there must 
        //be at least n intervals that CPU are doing different tasks or just be idle.
        //You need to return the least number of intervals the CPU will take to finish all the given tasks.
        //Example:    Input: tasks = ["A","A","A","B","B","B"], n = 2
        //Output: 8   Explanation: A -> B -> idle -> A -> B -> idle -> A -> B.
        //http://www.cnblogs.com/grandyang/p/7098764.html
        //ref:https://zxi.mytechroad.com/blog/greedy/leetcode-621-task-scheduler/
        public int LeastInterval(char[] tasks, int n)
        {
            if (tasks == null || tasks.Length == 0)
                return 0;

            int[] vector = new int[26];
            for (int i = 0; i < tasks.Length; i++)
                vector[tasks[i] - 'A']++;

            Array.Sort(vector);

            //find max count char 
            int maxCount = vector[25];
            int ret = (maxCount - 1) * (n + 1);

            //find how many char has max count
            int p = 0;
            p = vector.Where(i => i == maxCount).Count();

            return Math.Max(tasks.Length, ret + p);
        }

        //282. Expression Add Operators
        //Given a string that contains only digits 0-9 and a target value, return all possibilities to add binary operators(not unary) +, -, or* between the digits so they evaluate to the target value.
        //Example 1:
        //Input: num = "123", target = 6
        //Output: ["1+2+3", "1*2*3"]
        //ref:https://zxi.mytechroad.com/blog/searching/leetcode-282-expression-add-operators/
        public IList<string> AddOperators(string num, int target)
        {
            if (string.IsNullOrEmpty(num))
                return null;

            var ret = new List<string>();

            //time complex O( 4^(n-1)) = O(4^n)  (n= length of num, 4 possible operator (including none) put between numbers (n-1) between space 


            operatorDFS("", 0, 0, 0, num, target, ret);
            return ret;
        }

        void operatorDFS(string expr, int position, int prev, int curSum, string num, int target, List<string> ret)
        {
            if (position == num.Length && curSum == target)
            {
                ret.Add(expr);
                return;
            }

            for (int i = 1; position + i < num.Length; i++)
            {
                int n = int.Parse(num.Substring(position, i));

                operatorDFS(expr + '+', position + i, n, curSum + n, num, target, ret);
                operatorDFS(expr + '-', position + i, -n, curSum - n, num, target, ret);
                operatorDFS(expr + '*', position + i, prev * n, curSum + prev * n - prev, num, target, ret);
            }

        }

        //301. Remove Invalid Parentheses
        //Remove the minimum number of invalid parentheses in order to make the input string valid.Return all possible results.
        //Note: The input string may contain letters other than the parentheses (and ).
        //Example 1: Input: "()())()"  Output: ["()()()", "(())()"]
        //Example 2: Input: "(a)())()" Output: ["(a)()()", "(a())()"]
        //DFS time: O(n!) worst case, or O(2^(l+r))
        //space: O((l+r)^2) or O(n^2)
        public IList<string> RemoveInvalidParenthesesBFS(string s)
        {
            var q = new Queue<string>();
            q.Enqueue(s);
            var ret = new List<string>();
            var visited = new HashSet<string>();
            bool isValid = false;

            while (q.Count > 0)
            {
                //isValid = false; just require remove min number of ()
                var cur = q.Dequeue();
                if (isValidParenthses(cur))
                {
                    ret.Add(cur);
                    isValid = true;
                }
                if (isValid)
                    continue;

                //enqueue all possible remove 1 parenthesis string
                for (int i = 0; i < cur.Length - 1; i++)
                {
                    if (cur[i] == '(' || cur[i] == ')')
                    {
                        string newStr = cur.Substring(0, i) + cur.Substring(i + 1);
                        if (!visited.Contains(newStr))
                        {
                            visited.Add(newStr);
                            q.Enqueue(newStr);
                        }
                    }
                }
            }
            return ret;
        }

        public IList<string> RemoveInvalidParentheses(string s)
        {
            int removeLeft = 0;
            int removeRight = 0;

            for (int i = 0; i < s.Length; i++)
            {
                removeLeft += s[i] == '(' ? 1 : 0;
                if (removeLeft == 0)
                    removeRight += s[i] == ')' ? 1 : 0;
                else
                    removeLeft -= s[i] == ')' ? 1 : 0;
            }

            var ret = new List<string>();
            dfsFindValidParentheses(s, 0, removeLeft, removeRight, ret);
            return ret;
        }
        bool isValidParenthses(string s)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    count++;
                else if (s[i] == ')' && --count < 0)
                    return false;
            }
            return count == 0;
        }
        void dfsFindValidParentheses(string s, int startIdx, int left, int right, List<string> ret)
        {
            if (left == 0 && right == 0)
            {
                if (isValidParenthses(s))
                    ret.Add(s);

                return;
            }

            for (int i = startIdx; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == ')')
                {
                    //just remove the first one if have repeated 
                    //对于多个相同的半括号在一起，只删除第一个，比如 "())"，这里有两个右括号，不管删第一个还是删第二个右括号都会得到 "()"，没有区别，所以只用算一次就行了
                    //otherwise you will get duplicated results, because every recursive layer, we only have 2 choice (remove this char or not)
                    //so for repeated parenthese, delete any one it will get same string on next recursive layer.
                    if (i != startIdx && s[i] == s[i - 1])
                        continue;

                    var curStr = s.Remove(i, 1);
                    if (left > 0 && s[i] == '(')
                        dfsFindValidParentheses(curStr, i, left - 1, right, ret);
                    else if (right > 0 && s[i] == ')')
                        dfsFindValidParentheses(curStr, i, left, right - 1, ret);
                }
            }
        }
        //follow up : just return 1 possible result , which means remove invalid ( and )
        //正着删一次 :remove extra ')' , 反着删一次:remove extra '('
        public string RemoveInvalidParentheses2(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            string temp = "";
            int left = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    left++;
                    temp += s[i];
                }
                else if (s[i] == ')')
                {
                    if (left > 0)
                    {
                        left--;
                        temp += s[i];
                    }
                }
            }
            int right = 0;
            string ret = "";
            for (int j = temp.Length - 1; j >= 0; j--)
            {
                if (temp[j] == ')')
                {
                    right++;
                    ret = temp[j] + ret;
                }
                else if (temp[j] == '(')
                {
                    if (right > 0)
                    {
                        right--;
                        ret = temp[j] + ret;
                    }
                }
            }
            return ret;
        }


        //921. Minimum Add to Make Parentheses Valid
        //Given a string S of '(' and ')' parentheses, we add the minimum number of parentheses ( '(' or ')', and in any positions ) so that the resulting parentheses string is valid.
        //Formally, a parentheses string is valid if and only if:
        //It is the empty string, or It can be written as AB(A concatenated with B), where A and B are valid strings, or
        //It can be written as (A), where A is a valid string.
        //Given a parentheses string, return the minimum number of parentheses we must add to make the resulting string valid.
        public int MinAddToMakeValid(string S)
        {
            if (string.IsNullOrEmpty(S))
                return 0;

            int stackR = 0;
            int StackL = 0;

            for (int i = 0; i < S.Length; i++)
            {
                if (S[i] == '(')
                    StackL += 1;
                else if (S[i] == ')' && StackL > 0)
                    StackL--;
                else if (S[i] == ')')
                    stackR++;
            }
            return stackR + StackL;
        }

        //79. Word Search
        //Given a 2D board and a word, find if the word exists in the grid.
        //The word can be constructed from letters of sequentially adjacent cell, where "adjacent" cells are 
        //those horizontally or vertically neighboring.The same letter cell may not be used more than once.
        //Example:board =
        //[
        //  ['A','B','C','E'],
        //  ['S','F','C','S'],
        //  ['A','D','E','E']
        //]
        //Given word = "ABCCED", return true.
        //Given word = "SEE", return true.
        //Given word = "ABCB", return false.
        public bool Exist2(char[][] board, string word)
        {
            int lenR = board.Length;
            int lenC = board[0].Length;
            var visited = new bool[lenR, lenC];

            for (int i = 0; i < lenR; i++)
            {
                for (int j = 0; j < lenC; j++)
                {
                    if (searchHelper(board, word, i, j, visited, 0))
                        return true;
                }
            }
            return false;
        }

        bool searchHelper(char[][] board, string word, int i, int j, bool[,] visited, int curIdx)
        {
            if (curIdx == word.Length)
                return true;

            if (i >= board.Length ||
                j >= board[0].Length ||
                i < 0 || j < 0 ||
                visited[i, j] ||
                word[curIdx] != board[i][j])
            {
                return false;
            }

            visited[i, j] = true;
            bool ret = searchHelper(board, word, i + 1, j, visited, curIdx + 1) ||
                        searchHelper(board, word, i, j + 1, visited, curIdx + 1) ||
                        searchHelper(board, word, i - 1, j, visited, curIdx + 1) ||
                        searchHelper(board, word, i, j - 1, visited, curIdx + 1);
            visited[i, j] = false;
            return ret;
        }

        //212. Word Search II
        //Given a 2D board and a list of words from the dictionary, find all words in the board.
        //Each word must be constructed from letters of sequentially adjacent cell, where "adjacent" 
        //cells are those horizontally or vertically neighboring.The same letter cell may not be used 
        //more than once in a word.
        //Input:  words = ["oath","pea","eat","rain"]
        //and board =[        
        //  ['o', 'a', 'a', 'n'],        
        //  ['e', 't', 'a', 'e'],        
        //  ['i', 'h', 'k', 'r'],        
        //  ['i', 'f', 'l', 'v']
        //  ]
        //        Output: ["eat","oath"]
        public IList<string> FindWords(char[,] board, string[] words)
        {
            var ret = new HashSet<string>();
            int row = board.GetLength(0);
            int col = board.GetLength(1);

            for (int k = 0; k < words.Length; k++)
            {
                var visited = new bool[row, col];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (findWordsHelper(board, words[k], visited, i, j, 0))
                            ret.Add(words[k]);
                    }
                }
            }
            return ret.ToList();
        }
        bool findWordsHelper(char[,] board, string word, bool[,] visited, int i, int j, int count)
        {
            if (word.Length == count)
                return true;
            if (i < 0 || j < 0 || i >= board.GetLength(0) || j >= board.GetLength(1))
                return false;

            if (visited[i, j] || word[count] != board[i, j])
                return false;

            visited[i, j] = true;
            if (findWordsHelper(board, word, visited, i + 1, j, count + 1) ||
                findWordsHelper(board, word, visited, i, j + 1, count + 1) ||
                findWordsHelper(board, word, visited, i - 1, j, count + 1) ||
                findWordsHelper(board, word, visited, i, j - 1, count + 1))
                return true;
            visited[i, j] = false;
            return false;
        }

        //419. Battleships in a Board
        //Given an 2D board, count how many battleships are in it. The battleships are represented with 'X's, 
        //empty slots are represented with '.'s. You may assume the following rules:
        //  You receive a valid board, made of only battleships or empty slots.
        //  Battleships can only be placed horizontally or vertically.In other words, they can only be made of 
        //the shape 1xN (1 row, N columns) or Nx1(N rows, 1 column), where N can be of any size.
        //At least one horizontal or vertical cell separates between two battleships - there are no adjacent battleships.        
        public int CountBattleships(char[,] board)
        {
            if (board == null)
                return 0;
            int ret = 0;
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == 'X')
                    {
                        ret++;
                        CountBattleshipsHelper(x, y, board);
                    }
                }
            }
            return ret;
        }
        void CountBattleshipsHelper(int x, int y, char[,] board)
        {
            if (x >= board.GetLength(0) || x < 0 || y >= board.GetLength(1) || y < 0 || board[x, y] == '.')
                return;
            board[x, y] = '.';
            CountBattleshipsHelper(x + 1, y, board);
            CountBattleshipsHelper(x, y + 1, board);
            CountBattleshipsHelper(x - 1, y, board);
            CountBattleshipsHelper(x, y - 1, board);
        }


        //ref Swap 
        public void refSwap(ref Point pt1, ref Point pt2)
        {
            Point temp = pt1;
            pt1 = pt2;
            pt2 = temp;

            Console.WriteLine(pt1.x + "," + pt1.y);
            Console.WriteLine(pt2.x + "," + pt2.y);
        }

        //amazon OA Shortest path in a Binary Maze , to point 9 
        public int PathBinaryMaze2(int[,] mat, Point source, Point dest)
        {
            if (mat == null || mat.GetLength(0) == 0 || mat.GetLength(1) == 0)
                return -1;

            var q = new Queue<Point>();

            q.Enqueue(source);
            var visited = new bool[mat.GetLength(0), mat.GetLength(1)];
            var adjx = new int[] { 1, 0, -1, 0 };
            var adjy = new int[] { 0, -1, 0, 1 };

            while (q.Count != 0)
            {
                var curP = q.Dequeue();
                if (mat[curP.x, curP.y] == mat[dest.x, dest.y])
                    return curP.dist;

                for (int i = 0; i < adjx.Length; i++)
                {
                    var adjRow = curP.x + adjx[i];
                    int adjCol = curP.y + adjy[i];

                    if (isValidP(adjRow, adjCol, mat) && !visited[adjRow, adjCol] && mat[adjRow, adjCol] == 1)
                    {
                        var adjP = new Point();
                        adjP.x = adjRow;
                        adjP.y = adjCol;
                        adjP.dist = curP.dist + 1;
                        visited[adjRow, adjCol] = true;
                        q.Enqueue(adjP);
                    }
                }
            }
            return -1;
        }
        bool isValidP(int x, int y, int[,] mx)
        {
            if (x < 0 || y < 0 || x >= mx.GetLength(0) || y >= mx.GetLength(1))
                return false;
            return true;
        }

        //Amazon OA, NVIDIA Shortest path in a Binary Maze
        //Given a MxN matrix where each element can either be 0 or 1. We need to find the shortest 
        //path between a given source cell to a destination cell.
        //The path can only be created out of a cell if its value is 1.
        // Expected time complexity is O(MN).
        //Input:
        //        mat[ROW][COL]  = {{1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
        //                          {1, 0, 1, 0, 1, 1, 1, 0, 1, 1 },
        //                          {1, 1, 1, 0, 1, 1, 0, 1, 0, 1 },
        //                          {0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
        //                          {1, 1, 1, 0, 1, 1, 1, 0, 1, 0 },
        //                          {1, 1, 0, 0, 0, 0, 1, 0, 0, 1 }};
        //Source = {0, 0};
        //Destination = {3, 4};   Output: Shortest Path is 11 
        public int PathBinaryMaze(int[,] mat, Point source, Point dest)
        {
            if (mat == null || mat.GetLength(0) == 0 || mat.GetLength(1) == 0)
                return -1;

            Queue<Point> q = new Queue<Point>();
            q.Enqueue(source);

            bool[,] visited = new bool[mat.GetLength(0), mat.GetLength(1)];

            int[] adjRow = new int[4] { 1, 0, 0, -1 };
            int[] adjCol = new int[4] { 0, 1, -1, 0 };

            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                if (cur.x == dest.x && cur.y == dest.y)
                    return cur.dist;

                for (int i = 0; i < 4; i++)
                {
                    int adjX = cur.x + adjRow[i];
                    int adjY = cur.y + adjCol[i];
                    if (validPoint(mat, adjX, adjY) && !visited[adjX, adjY] && mat[adjX, adjY] == 1)
                    {
                        Point adjPoint = new Point() { x = adjX, y = adjY, dist = cur.dist + 1 };
                        visited[adjX, adjY] = true;
                        q.Enqueue(adjPoint);
                    }
                }
            }
            return -1;
        }
        bool validPoint(int[,] mat, int adjX, int adjY)
        {
            if (adjX >= 0 && adjX < mat.GetLength(0) && adjY >= 0 && adjY < mat.GetLength(1))
                return true;
            return false;
        }

        //139. Word Break
        //Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, 
        //determine if s can be segmented into a space-separated sequence of one or more dictionary words. 
        //You may assume the dictionary does not contain duplicate words.
        //For example, given s = "leetcode", dict = ["leet", "code"].
        //Return true because "leetcode" can be segmented as "leet code".
        public bool WordBreak(string s, IList<string> wordDict)
        {
            //time takes O(n!) 
            if (wordDict == null || s == null)
                return false;
            if (s.Length == 0)
                return true;

            for (int i = 0; i <= s.Length; i++)
            {
                string front = s.Substring(0, i);

                if (wordDict.Contains(front) && WordBreak(s.Substring(i), wordDict))
                {
                    return true;
                }
            }
            return false;
        }
        bool WordBreakBetterDFS(string s, IList<string> wordDict)
        {
            var map = new Dictionary<string, bool>();
            var hs = new HashSet<string>(wordDict);

            return wbHelpDFS(s, hs, map);

        }
        bool wbHelpDFS(string s, HashSet<string> dict, Dictionary<string, bool> map)
        {
            if (s.Length == 0)
                return true;

            if (map.ContainsKey(s))
                return map[s];

            if (dict.Contains(s))
                map.TryAdd(s, true);
            else
                map.TryAdd(s, false);

            for (int i = 0; i <= s.Length; i++)
            {
                string front = s.Substring(0, i);
                if (dict.Contains(front) && wbHelpDFS(s.Substring(i), dict, map))
                    return true;
            }
            return false;
        }

        bool WordBreakBFS(string s, IList<string> wordDict)
        {
            var visited = new bool[s.Length];
            var hs = new HashSet<string>(wordDict);

            var q = new Queue<int>();
            q.Enqueue(0);

            while (q.Count > 0)
            {
                var curIdx = q.Dequeue();
                if (!visited[curIdx])
                {
                    for (int i = curIdx + 1; i <= s.Length; i++)
                    {
                        if (hs.Contains(s.Substring(curIdx, i - curIdx)))
                        {
                            q.Enqueue(i);
                            if (i == s.Length)
                                return true;
                        }
                    }
                    visited[curIdx] = true;
                }
            }
            return false;
        }

        public bool WordBreakBetter(string s, IList<string> wordDict)
        {
            var map = new Dictionary<string, bool>();
            var hs = new HashSet<string>(wordDict);
            if (s == null || wordDict == null)
                return false;
            return WordBreakHelper(s, hs, map);
        }

        public bool WordBreakHelper(string s, HashSet<string> wordDict, Dictionary<string, bool> map)
        {
            if (map.ContainsKey(s))
                return map[s];

            if (wordDict.Contains(s))
            {
                map.Add(s, true);
                return true;
            }

            for (int i = 0; i < s.Length; i++)
            {
                string left = s.Substring(0, i);
                string right = s.Substring(i);

                if (wordDict.Contains(left) && WordBreakHelper(right, wordDict, map))
                {
                    map.TryAdd(s, true);
                    return true;
                }
            }
            map.TryAdd(s, false);
            return false;
        }

        //140. Word Break II
        //Given a non-empty string s and a dictionary wordDict containing a list of non-empty words, add spaces in s to construct a sentence where each word is a valid dictionary word. Return all such possible sentences.
        //Note: The same word in the dictionary may be reused multiple times in the segmentation.
        //You may assume the dictionary does not contain duplicate words.
        //Example 1: Input: s = "catsanddog"  wordDict = ["cat", "cats", "and", "sand", "dog"]
        //Output: [  "cats and dog",   "cat sand dog" ]
        //public IList<string> WordBreakII(string s, IList<string> wordDict)
        //{
        //    var ret = new List<string>();
        //    var hs = new HashSet<string>(wordDict);
        //    var map = new Dictionary<string, List<string>>();

        //    return WordBreakIIHelper(s, hs, map,new List<string>(), ret);

        //}

        //public IList<string> WordBreakIIHelper(string s, HashSet<string> wordDict, Dictionary<string, List<string>> map, List<string ret)
        //{
        //    //if (!string.IsNullOrEmpty(cur) && s == "")
        //    //    ret.Add(cur);

        //    if (map.ContainsKey(s))
        //        return map[s];

        //    if (wordDict.Contains(s))
        //    {   
        //        if(map.ContainsKey(s))
        //            cur.Add(s);
        //    }
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        string left = s.Substring(0, i);
        //        string right = s.Substring(i);
        //        if (!wordDict.Contains(right))
        //            continue;

        //        List<string> left_ans = new List<string>(WordBreakIIHelper(left, wordDict, map, cur, ret));
        //        left_ans.Add(right);

        //        //cur = left_ans.Concat(cur);                
        //    }
        //    map.Add(s, cur);
        //    return map[s];
        //}

        //547. friend circle
        public int FindCircleNum(int[][] M)
        {
            if (M == null)
                return 0;
            int len = M.Length;
            var visited = new bool[len];
            var map = new Dictionary<int, List<int>>();
            //build graph
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    if (M[i][j] == 1)
                    {
                        map.TryAdd(i, new List<int>());
                        map.TryAdd(j, new List<int>());
                        map[i].Add(j);
                        map[j].Add(i);
                    }
                }
            }
            foreach (var kv in map)
            {
                Console.Write(kv.Key.ToString() + ":");
                foreach (var va in kv.Value)
                {
                    Console.Write(va + ",");
                }
            }
            int ret = 0;
            foreach (var kv in map)
            {
                if (!visited[kv.Key])
                {
                    ret += 1;
                    DFSFindCircleNum(kv.Key, visited, map);
                }
            }
            return ret + visited.Count(n => n == false);
        }
        void DFSFindCircleNum(int key, bool[] visited, Dictionary<int, List<int>> map)
        {
            if (visited[key])
                return;
            visited[key] = true;
            foreach (var n in map[key])
            {
                DFSFindCircleNum(n, visited, map);
            }
        }

        //323. Number of Connected Components in an Undirected Graph
        public int CountComponents(int n, int[][] edges)
        {
            var ret = new int[1];
            var visited = new bool[n];
            var map = new Dictionary<int, List<int>>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                map.TryAdd(edges[i][0], new List<int>());
                map.TryAdd(edges[i][1], new List<int>());
                map[edges[i][0]].Add(edges[i][1]);
                map[edges[i][1]].Add(edges[i][0]);
            }

            foreach (var kv in map)
            {
                if (!visited[kv.Key])
                {
                    ret[0] += 1;
                    DFSComponentHelper(visited, map, kv.Key);
                }
            }
            ret[0] += visited.Count(c => c == false);
            return ret[0];
        }
        void DFSComponentHelper(bool[] visited, Dictionary<int, List<int>> map, int key)
        {
            if (visited[key])
                return;
            visited[key] = true;
            foreach (var node in map[key])
            {
                DFSComponentHelper(visited, map, node);
            }
        }

        //200. Number of Islands
        //Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. 
        //An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. 
        //You may assume all four edges of the grid are all surrounded by water.
        public int NumIslandsBFS(char[][] grid)
        {   // with visited table , no need to change orignal grid
            if (grid == null)
                return 0;
            int lenR = grid.Length;
            int lenC = grid[0].Length;
            int ret = 0;
            var visited = new bool[lenR, lenC];
            var q = new Queue<Point>();

            for (int i = 0; i < lenR; i++)
            {
                for (int j = 0; j < lenC; j++)
                {
                    if (!visited[i, j] && grid[i][j] == '1')
                    {
                        ret += 1;
                        q.Enqueue(new Point(i, j));

                        while (q.Count > 0)
                        {
                            var curNode = q.Dequeue();

                            if (curNode.x < 0 || curNode.x >= lenR || curNode.y < 0 || curNode.y >= lenC || grid[curNode.x][curNode.y] == '0' || visited[curNode.x, curNode.y])
                                continue;

                            visited[curNode.x, curNode.y] = true;
                            q.Enqueue(new Point(curNode.x + 1, curNode.y));
                            q.Enqueue(new Point(curNode.x - 1, curNode.y));
                            q.Enqueue(new Point(curNode.x, curNode.y + 1));
                            q.Enqueue(new Point(curNode.x, curNode.y - 1));
                        }
                    }
                }
            }
            return ret;
        }

        public int NumIslandsDFS(char[][] grid)
        {   // with visited table , no need to change orignal grid
            if (grid == null)
                return 0;
            int lenR = grid.Length;
            int lenC = grid[0].Length;
            int ret = 0;
            var visited = new bool[lenR, lenC];

            for (int i = 0; i < lenR; i++)
            {
                for (int j = 0; j < lenC; j++)
                {
                    if (grid[i][j] == '1' && !visited[i, j])
                    {
                        ret += 1;
                        islandHelp(i, j, grid, visited);
                    }
                }
            }
            return ret;
        }

        void islandHelp(int i, int j, char[][] grid, bool[,] visited)
        {
            int lenR = grid.Length;
            int lenC = grid[0].Length;
            if (i < 0 || j < 0 || i >= lenR || j >= lenC || grid[i][j] == '0' || visited[i, j])
                return;

            visited[i, j] = true;
            islandHelp(i + 1, j, grid, visited);
            islandHelp(i - 1, j, grid, visited);
            islandHelp(i, j + 1, grid, visited);
            islandHelp(i, j - 1, grid, visited);
            //visited[i,j]=false;
        }
        public int NumIslands(char[,] grid)
        {
            int ret = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == '1')
                    {
                        drownIslandDfsHelper(grid, i, j);
                        ret++;
                    }
                }
            }
            return ret;
        }

        public void drownIslandDfsHelper(char[,] grid, int i, int j)
        {
            if (i >= grid.GetLength(0) || i < 0 || j >= grid.GetLength(1) || j < 0 || grid[i, j] == '0')
                return;
            grid[i, j] = '0';
            drownIslandDfsHelper(grid, i + 1, j);
            drownIslandDfsHelper(grid, i, j + 1);
            drownIslandDfsHelper(grid, i - 1, j);
            drownIslandDfsHelper(grid, i, j - 1);
        }


        //207. Course Schedule
        //There are a total of n courses you have to take, labeled from 0 to n - 1.
        //Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
        // Given the total number of courses and a list of prerequisite pairs, is it possible for you to finish all courses?        
        public bool CanFinish(int numCourses, int[,] prerequisites)
        {
            if (numCourses == 0 || prerequisites == null || prerequisites.GetLength(0) == 0)
                return true;

            var visitedList = new int[numCourses];

            List<int>[] map = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                map[i] = new List<int>();

            for (int i = 0; i < prerequisites.GetLength(0); i++)
                map[prerequisites[i, 0]].Add(prerequisites[i, 1]);

            for (int i = 0; i < numCourses; i++)
            {
                if (isCircle(map, i, visitedList))
                    return false;
            }
            return true;
        }
        bool isCircle(List<int>[] map, int key, int[] visitedList)
        {
            if (visitedList[key] == 1)  //visiting in dfs stack
                return true;
            if (visitedList[key] == 2) // already visited, out of dfs stack
                return false;

            visitedList[key] = 1;

            foreach (var k in map[key])
            {
                if (isCircle(map, k, visitedList))
                    return true;
            }

            visitedList[key] = 2;
            return false;
        }


        //210 Course Schedule II
        //There are a total of n courses you have to take, labeled from 0 to n - 1.
        //Some courses may have prerequisites, for example to take course 0 you have to first take course 1, which is expressed as a pair: [0,1]
        //Given the total number of courses and a list of prerequisite pairs, return the ordering of courses you should take to finish all courses.
        //There may be multiple correct orders, you just need to return one of them.If it is impossible to finish all courses, return an empty array.
        // For example: 2, [[1,0]]
        //There are a total of 2 courses to take.To take course 1 you should have finished course 0. So the correct course order is [0,1]
        //4, [[1,0],[2,0],[3,1],[3,2]]
        //There are a total of 4 courses to take.To take course 3 you should have finished both courses 1 and 2. Both courses 1 and 2 should be 
        //taken after you finished course 0. So one correct course order is [0,1,2,3]. Another correct ordering is[0,2,1,3].
        //Note: The input prerequisites is a graph represented by a list of edges, not adjacency matrices.Read more about how a graph is represented.
        //You may assume that there are no duplicate edges in the input prerequisites.
        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            if (numCourses == 0)
                return null;

            List<int> ret = new List<int>();
            //build graph
            List<int>[] graph = new List<int>[numCourses];
            for (int i = 0; i < numCourses; i++)
                graph[i] = new List<int>();

            for (int i = 0; i < prerequisites.GetLength(0); i++)
                graph[prerequisites[i, 0]].Add(prerequisites[i, 1]);

            var visit = new int[numCourses];

            for (int i = 0; i < numCourses; i++)
            {
                if (IsCourseCycle(graph, ret, visit, i))
                    return new int[] { };
            }
            return ret.ToArray();
        }
        bool IsCourseCycle(List<int>[] graph, List<int> ret, int[] visit, int idx)
        {
            if (visit[idx] == 1)  //if visiting node 
                return true;

            if (visit[idx] == 2)  //if visited node 
                return false;

            visit[idx] = 1;

            foreach (int x in graph[idx])
            {
                if (IsCourseCycle(graph, ret, visit, x))
                    return true;
            }

            visit[idx] = 2;  //already visited
            ret.Add(idx);
            return false;
        }


        //339. Nested List Weight Sum
        //Example 1: Given the list[[1, 1],2,[1,1]], return 10. (four 1's at depth 2, one 2 at depth 1)
        //Example 2: Given the list[1,[4,[6]]], return 27. (one 1 at depth 1, one 4 at depth 2, and one 6 at depth 3; 1 + 4*2 + 6*3 = 27)
        // This is the interface that allows for creating nested lists.
        public interface NestedInteger
        {     // @return true if this NestedInteger holds a single integer, rather than a nested list.
            bool IsInteger();
            // @return the single integer that this NestedInteger holds, if it holds a single integer
            // Return null if this NestedInteger holds a nested list
            int GetInteger();
            // @return the nested list that this NestedInteger holds, if it holds a nested list
            // Return null if this NestedInteger holds a single integer
            IList<NestedInteger> GetList();
        }
        public int DepthSum(IList<NestedInteger> nestedList)
        {
            if (nestedList == null)
                return 0;
            return DepthSumHelper(nestedList, 1);
        }
        int DepthSumHelper(IList<NestedInteger> nestedList, int depth)
        {
            int ret = 0;

            for (int i = 0; i < nestedList.Count; i++)
            {
                ret += nestedList[i].IsInteger() ? depth * nestedList[i].GetInteger() : DepthSumHelper(nestedList[i].GetList(), depth + 1);
            }
            return ret;
        }

        //364. Nested List Weight Sum II  (inverse weight)
        //Given a nested list of integers, return the sum of all integers in the list weighted by their depth.
        //Each element is either an integer, or a list -- whose elements may also be integers or other lists.
        //Different from the previous question where weight is increasing from root to leaf, now the weight is defined from bottom up.i.e., the leaf level integers have weight 1, and the root level integers have the largest weight.
        //Example 1: Given the list[[1, 1],2, [1,1]], return 8. (four 1's at depth 1, one 2 at depth 2)
        //Example 2:Given the list[1,[4,[6]]], return 17. (one 1 at depth 3, one 4 at depth 2, and one 6 at depth 1; 1*3 + 4*2 + 6*1 = 17)
        public int DepthSumInverse(IList<NestedInteger> nestedList)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;

            int depth = depthHelper(nestedList);
            return sumHelper(nestedList, depth);
        }
        int sumHelper(IList<NestedInteger> nestedList, int depth)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;
            int ret = 0;

            for (int i = 0; i < nestedList.Count(); i++)
            {
                if (nestedList[i].IsInteger())
                    ret += nestedList[i].GetInteger() * depth;
                else
                    ret += sumHelper(nestedList[i].GetList(), depth - 1);
            }
            return ret;
        }
        int depthHelper(IList<NestedInteger> nestedList)
        {
            if (nestedList == null || nestedList.Count == 0)
                return 0;
            int depth = 0;
            for (int i = 0; i < nestedList.Count(); i++)
            {
                if (nestedList[i].IsInteger())
                    depth = Math.Max(depth, 1);
                else
                    depth = Math.Max(depth, 1 + depthHelper(nestedList[i].GetList()));
            }
            return depth;
        }



        //582. Kill Process My SubmissionsBack To Contest
        //Given n processes, each process has a unique PID(process id) and its PPID(parent process id).
        //Input:  pid =  [1, 3, 10, 5]  ppid = [3, 0, 5, 3]   kill = 5   Output: [5,10]
        //Explanation:   Kill 5 will also kill 10.
        //      3
        //    /   \
        //   1     5
        //        /
        //      10
        public IList<int> KillProcess2(IList<int> pid, IList<int> ppid, int kill)
        {
            if (pid == null || ppid == null || pid.Count == 0 || ppid.Count == 0)
                return null;

            var ret = new HashSet<int>();

            dfs(pid, ppid, kill, ret);
            return ret.ToList();
        }
        void dfs(IList<int> pid, IList<int> ppid, int kill, HashSet<int> ret)
        {
            if (ppid.Contains(kill))
            {
                ret.Add(kill);
                int idx = -1;
                for (int i = 0; i < ppid.Count; i++)
                {
                    if (ppid[i] == kill)
                    {
                        idx = i;
                        dfs(pid, ppid, pid[idx], ret);
                    }
                }
            }
            else
                ret.Add(kill);
        }

        public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            //save relationship in map
            for (int i = 0; i < ppid.Count; i++)
            {
                if (!map.ContainsKey(ppid[i]))
                    map.Add(ppid[i], new List<int>() { pid[i] });
                else
                    map[ppid[i]].Add(pid[i]);
            }

            List<int> ret = new List<int>();
            Queue<int> q = new Queue<int>();
            q.Enqueue(kill);

            while (q.Count != 0)
            {
                int parent = q.Dequeue();
                ret.Add(parent);
                if (map.ContainsKey(parent))
                {
                    foreach (var x in map[parent])
                        q.Enqueue(x);
                }
            }
            return ret;
        }
    }
}
