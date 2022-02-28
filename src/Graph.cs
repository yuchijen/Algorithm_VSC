using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class Graph
    {
        //MS (1/25/22) (undirect graph, find the shortest path from st to dst)
        // Y->C->A->S , Y->C->B, Y->C->A->S, A->S  ; find shortest path from Y-> A->S; not Y-> C-> A-> S 
        public List<char> ShortestPath(Dictionary<char, List<char>> g)
        {
            var ret = new List<char>();
            int len = g.Count;
            var visit = new HashSet<char>();

            // shortestHelper(ret, visit, g, 'Y', 'S', new List<char>());
            var pathMap = shortestBFS(visit, g, 'Y', 'S');

            ret.Add('S');
            char vInPath = 'S';
            while (pathMap[vInPath] != '#')
            {
                ret.Insert(0, pathMap[vInPath]);
                vInPath = pathMap[vInPath];
            }

            return ret;
        }

        public Dictionary<char, char> shortestBFS(HashSet<char> visit, Dictionary<char, List<char>> g, char st, char end)
        {
            var ret = new Dictionary<char, char>();
            var distance = new Dictionary<char, int>();
            foreach (var k in g.Keys)
                ret.Add(k, '#');
            foreach (var k in g.Keys)
                distance.Add(k, int.MaxValue);
            distance[st] = 0;

            var q = new Queue<char>();
            q.Enqueue(st);
            visit.Add(st);

            while (q.Count > 0)
            {
                var curNode = q.Dequeue();
                foreach (char c in g[curNode])
                {
                    if (!visit.Contains(c))
                    {
                        ret[c] = curNode;
                        distance[c] = distance[curNode] + 1;
                        if (c == end)
                            return ret;
                        visit.Add(c);
                        q.Enqueue(c);
                    }
                }
            }
            return ret;
        }

        public void shortestDFS(List<char> ret, HashSet<char> visit, Dictionary<char, List<char>> g, char st, char end, List<char> curPath)
        {
            if (visit.Contains(st))
                return;

            visit.Add(st);
            curPath.Add(st);
            Console.Write("curPath:");
            foreach (var x in curPath)
            {
                Console.Write(x + ",");
            }
            if (curPath.Count > 0 && curPath[curPath.Count - 1] == end)
            {
                if (ret.Count == 0)
                    ret = new List<char>(curPath);
                else
                {
                    if (curPath.Count < ret.Count)
                        ret = new List<char>(curPath);
                }
            }

            foreach (var child in g[st])
            {
                shortestDFS(ret, visit, g, child, end, curPath);
                //curPath.Remove(child);
            }
            curPath.Remove(st);
            visit.Remove(st);
        }

        //261. Graph Valid Tree (undirected graph)
        // Given n nodes labeled from 0 to n-1 and a list of undirected edges (each edge is a pair of nodes), write a function to check whether these edges make up a valid tree.
        // Example 1: Input: n = 5, and edges = [[0,1], [0,2], [0,3], [1,4]] Output: true
        // Example 2: Input: n = 5, and edges = [[0,1], [1,2], [2,3], [1,3], [1,4]]  Output: false (cycle)
        // note: see if all nodes connected graph and without cycle
        public bool validTree(int n, int[][] edges)
        {
            if (n == 1 && edges.Length == 0)
                return true;

            if ((edges == null || edges.Length == 0) && n > 1)
                return false;

            var graph = new Dictionary<int, List<int>>();
            for (int i = 0; i < edges.Length; i++)
            {
                graph.TryAdd(edges[i][0], new List<int>());
                graph.TryAdd(edges[i][1], new List<int>());
                graph[edges[i][0]].Add(edges[i][1]);
                graph[edges[i][1]].Add(edges[i][0]);
            }

            var visit = new bool[n];

            if (hasCycleTreeDFS(graph, visit, -1, edges[0][0]))
            {
                return false;
            }

            return visit.All(x => x);
        }
        bool hasCycleTreeDFS(Dictionary<int, List<int>> g, bool[] visit, int parent, int node)
        {
            if (visit[node])
                return true;

            visit[node] = true;

            foreach (var child in g[node])
            {
                if (child != parent)
                {
                    if (hasCycleTreeDFS(g, visit, node, child))
                        return true;
                }
            }
            return false;
        }

        //207. Course Schedule (directed graph)
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

        //269. Alien Dictionary (topological sort) (directed graph)
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

        //323. Number of Connected Components in an Undirected Graph
        public int CountComponents(int n, int[][] edges)
        {
            int ret = 0;
            var visited = new bool[n];
            var map = new Dictionary<int, List<int>>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                map.TryAdd(edges[i][0], new List<int>());
                map.TryAdd(edges[i][1], new List<int>());
                map[edges[i][0]].Add(edges[i][1]);
                map[edges[i][1]].Add(edges[i][0]);
            }

            foreach (var k in map.Keys)
            {
                if (!visited[k])
                {
                    ret += 1;
                    DFSComponentHelper(visited, map, k);
                }
            }
            ret += visited.Count(c => c == false);
            return ret;
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

        //[[s1,s2]
        // [s4,s5]
        // [s3,s4]
        // [s1,s4]]  => [s1,s2,s3,s4,s5]
        public List<string> largestItemAssociation(List<PairString> itemAssociation)
        {

            //build graph
            var map = new Dictionary<string, List<string>>();
            foreach (var item in itemAssociation)
            {
                map.TryAdd(item.first, new List<string>());
                map.TryAdd(item.second, new List<string>());
                map[item.first].Add(item.second);
                map[item.second].Add(item.first);
            }

            var visited = new HashSet<string>();
            var ret = new List<List<string>>();

            // go DFS 
            foreach (var k in map.Keys)
            {
                var curList = new List<string>();
                DfsItems(visited, k, map, curList);
                ret.Add(curList);
            }
            ret.ForEach(x => x.Sort());
            return ret.OrderByDescending(list => list.Count).FirstOrDefault();
        }

        void DfsItems(HashSet<string> visited, string key, Dictionary<string, List<string>> map, List<string> curList)
        {
            if (visited.Contains(key))
                return;
            visited.Add(key);
            curList.Add(key);
            foreach (var neighborItem in map[key])
            {
                DfsItems(visited, neighborItem, map, curList);
            }
        }

        public class PairString
        {
            public String first;
            public String second;

            public PairString(String first, String second)
            {
                this.first = first;
                this.second = second;
            }
        }

        //zume given 2 node in graph, find any common ancestor 
        public bool findCA(List<int[]> input, int n1, int n2)
        {
            if (input == null || input.Count == 0)
                return false;

            var total = new HashSet<int>();
            var map = new Dictionary<int, List<int>>();
            var visited = new Dictionary<int, int>();

            foreach (var tu in input)
            {
                total.Add(tu[0]);
                total.Add(tu[1]);

                if (map.ContainsKey(tu[1]))
                    map[tu[1]].Add(tu[0]);
                else
                    map.Add(tu[1], new List<int>() { tu[0] });
            }

            //trace back to root from n1 and n2 
            var path1 = new List<int>();
            var path2 = new List<int>();

            pathHelper(map, visited, n1, path1);
            visited = new Dictionary<int, int>();
            pathHelper(map, visited, n2, path2);
            //see any node in path is the same
            return path1.Any(p1 => (path2.Contains(p1)));
        }
        void pathHelper(Dictionary<int, List<int>> map, Dictionary<int, int> visited, int n, List<int> path)
        {
            path.Add(n);
            if (visited.ContainsKey(n))
                return;
            visited.Add(n, 1);
            if (map.ContainsKey(n))
            {
                foreach (var parent in map[n])
                {
                    pathHelper(map, visited, parent, path);
                }
            }
        }

        //zume  find the nodes have 0 parent or 1 parent 
        // space: O(n) time: O(n+v)
        public List<List<int>> findZeroOrOneParent(List<int[]> input)
        {
            if (input == null || input.Count == 0)
                return null;
            var total = new HashSet<int>();
            var map = new Dictionary<int, List<int>>();
            foreach (var tu in input)
            {
                total.Add(tu[0]);
                total.Add(tu[1]);

                if (map.ContainsKey(tu[1]))
                    map[tu[1]].Add(tu[0]);
                else
                    map.Add(tu[1], new List<int>() { tu[0] });
            }
            var ret = new List<List<int>>();
            var retZeroP = new List<int>();
            var retOneP = new List<int>();

            foreach (int node in total)
            {
                if (!map.ContainsKey(node))
                    retZeroP.Add(node);
            }

            foreach (var kv in map)
            {
                if (kv.Value.Count == 1)
                    retOneP.Add(kv.Key);
            }
            ret.Add(retZeroP);
            ret.Add(retOneP);
            return ret;
        }
    }
}