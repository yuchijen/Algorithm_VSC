using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class Graph
    {
        //261. Graph Valid Tree
        // Given n nodes labeled from 0 to n-1 and a list of undirected edges (each edge is a pair of nodes), write a function to check whether these edges make up a valid tree.
        // Example 1: Input: n = 5, and edges = [[0,1], [0,2], [0,3], [1,4]] Output: true
        // Example 2: Input: n = 5, and edges = [[0,1], [1,2], [2,3], [1,3], [1,4]]  Output: false (cycle)
        // note: see if all nodes connected graph and without cycle
        bool validTree(int n, int[][] edges)
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