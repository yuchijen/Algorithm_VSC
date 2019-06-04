using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSP;

namespace Interview
{
    class Program
    {
        static void Main(string[] args)
        {

            var asa = new ArrayString();
            var dp = new DynamicProgramming();

            asa.Compress(new char[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c' });

            //Console.WriteLine( dp.longestRepeatedSubstring("geeksforgeeks"));

            //var arr = new int[] { 1, 2, 3, 4, 1, 2, 2 };
            //asa.sortByFreqValue(arr);
            //string[] user1 = new string[] { "abc", "def", "ghi", "jkl", "lbj","mmm","nnn","oo" };
            //var user2 = new string[] { "abc","cf","dpi","ghi","jkl","lbj", "mge", "nnn", "oo" };

            //foreach (var x in asa.LongestCommonStrArr(user1, user2))
            //    Console.WriteLine(x);

            //asa.FindAnagrams("cbaebabacd", "abc");
            //foreach (var x in asa.MajorityElement2(new int[5] { 2, 2, 2, 4, 4 }))
            //    Console.WriteLine(x);
            // //forwarding = [[1, 1000],[2, 7000],[3, 12000]], retrun = [[1, 10000],[2, 9000],[3, 3000],[4, 2000]]
            //var f = new List<List<int>>();
            //var r = new List<List<int>>();
            //f.Add(new List<int>() { 1, 1000 });
            //f.Add(new List<int>() { 2, 7000 });
            //f.Add(new List<int>() { 3, 12000 });
            //r.Add(new List<int>() { 1, 10000 });
            //r.Add(new List<int>() { 2, 9000 });
            //r.Add(new List<int>() { 3, 3000 });
            //r.Add(new List<int>() { 4, 2000 });

            //var cpair = asa.ClosestPair(f, r, 10000);
            //foreach(var x in cpair)
            //{
            //    foreach (var y in x)
            //        Console.Write(y+',');
            //    Console.WriteLine();
            //}


            var ast = new AsyncTest();
            ast.TestAsync();
            Console.WriteLine("doesn't block main thread");

            foreach (var x in asa.MajorityElement2(new int[5] { 2, 2, 2, 4, 4 }))
                Console.WriteLine(x);


            //var ast = new AsyncTest();
            //ast.TestAsync();

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadLine();

            var C = new Circle();
            Console.WriteLine("circumference:" + C.Calculate((x) => { return x * 2 * 3.14; }));

            UInt16 uii = 0xFFFF;
            var byt = BitConverter.GetBytes(uii);
            Int32 data = BitConverter.ToInt16(byt, 0);
            Console.WriteLine(data);

            var bs = new BinarySearch();
            bs.MyPow(5.0, -4);

            var bm = new BitManipulate();

            //Console.WriteLine("4th bit:" + bm.FourthBit(-23));
            bm.CountOneBit(4294967295);

            var bk = new BackTracking();


            //bk.SubarraysDivByK(new int[] { 4, 5, 0, -2, -3, 1 }, 5);

            Console.WriteLine("Permutation:########");
            var perret = bk.Permute(new int[3] { 1, 2, 3 });
            foreach (var x in perret)
            {
                foreach(var y in x)
                {
                    Console.Write(y);
                    Console.Write(",");
                }
                Console.WriteLine();    
            }

            bk.GetFactors(12);

            TreeNode root = new TreeNode(1);
            TreeNode n2 = new TreeNode(2);
            TreeNode n3 = new TreeNode(3);
            root.left = n2;
            root.right = n3;
            n3.left = new TreeNode(4);
            n3.right = new TreeNode(5);

            var bt = new BTree();
           
            bt.deserialize(bt.serialize(root));
            var ret =bt.FindLeaves(root);

            //var tt = new Thailand();
            //tt.Execute(10, 10);

            var dfs = new DFS_BFS();

            var parentChildPairs = new List<int[]>() {
            new int[]{1, 3},
            new int[]{2, 3},
            new int[]{3, 6},
            new int[]{5, 6},
            new int[]{5, 7},
            new int[]{4, 5},
            new int[]{4, 8},
            new int[]{8, 10}
                };

            Console.Write("find CA:");
            Console.WriteLine(dfs.findCA(parentChildPairs, 10, 7));

            //var chlist = new char[6] { 'A', 'A', 'A', 'B', 'B', 'B' };
            //Console.WriteLine("tasks:" + dfs.LeastInterval(chlist, 0));

            Console.WriteLine("word break");
            Console.WriteLine(dfs.WordBreak("leetcode", new List<string> { "leet", "code" }));

            Point pt = new Point() { x=100,y=100};
            Point pt2 = new Point() { x = 0, y = 0 };
            dfs.refSwap(ref pt, ref pt2);
            Console.WriteLine(pt.x + "," + pt.y);
            Console.WriteLine(pt2.x + "," + pt2.y);

            dfs.KillProcess(new List<int> { 1, 3, 10, 5 }, new List<int> { 3, 0, 5, 3 }, 5);

            asa.GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            asa.Compress("AAABBCCCCCCAAAAA");
            asa.LongestIncreasingSubArray(new int[] { 15, 14, 12, 11, 2});
            asa.ProductExceptSelf(new int[] { 1, 2, 3, 4, });
            //asa.SearchRotatedSortedArray(new int[] { 2,2,2,0,2,2 }, 0);
            asa.MaxSubArray(new int[] { 1, 2, -4, 4, 5, 6 });
            asa.SortColors(new int[] { 1, 2, 0 });
            asa.Equi(new int[] { -1, 3, -4, 5, 1, -6, 2, 1 });


            #region traveler problem
            ////Nearest neighbour algorithm  (Only one messanger)
            //int[,] adjacency_matrix =
            //{
            //    { 0,0, 0, 0, 0, 0 },
            //    { 0,0,50,30,100,10 },
            //    { 0,50,0,5, 20,99999},
            //    { 0,30,5,0, 50,99999 },
            //    { 0,100,20,50,0,10 },
            //    { 0,10,99999,99999,10,0 }
            //};
            //Console.WriteLine("the citys are visited as follows");
            //ImperialMessengersTSPNearestNeighbour tspNearestNeighbour = new ImperialMessengersTSPNearestNeighbour();
            //tspNearestNeighbour.tsp(adjacency_matrix);

            //dijkstra methods 
            //int numberNode = 5;
            //string[] shortedPath = new string[numberNode];
            //int[,] G =
            //{
            //    { 0,50,30,100,10 },
            //    { 50,0,5, 20,99999},
            //    { 30,5,0, 50,99999 },
            //    { 100,20,50,0,10 },
            //    { 10,99999,99999,10,0 }
            //};
            //string[] PathResult = new string[numberNode];
            //int[] path1 = new int[numberNode];
            ////int[,] path2 = new int[numberNode, numberNode];
            //int[] distance2 = new int[numberNode];

            //var dijkstra = new Dijkstra();
            //int dist1 = Dijkstra.getShortedPath(G, 0, 1, path1, numberNode);

            //int goThroughAllCities = int.MinValue;
            //for (int i = 1; i < numberNode; i++)
            //{
            //    goThroughAllCities = Math.Max(goThroughAllCities, Dijkstra.getShortedPath(G, 0, i, path1, numberNode));
            //}
            //Console.WriteLine("The min cost of going through all cities is: " + goThroughAllCities);

            //Console.WriteLine("Node 0 To 1:");
            //for (int i = 0; i < path1.Length; i++)
            //    Console.Write(path1[i].ToString() + " ");
            //Console.WriteLine("Length:" + dist1);

            //int[] pathdist = Dijkstra.getShortedPath(G, 0, path2, 5);
            //Console.WriteLine("\nNode 0 To other:");
            //for (int j = 1; j < pathdist.Length; j++)
            //{
            //    Console.WriteLine("Node 0 to " + j + " path:");
            //    for (int i = 0; i < numberNode; i++)
            //    {
            //        Console.Write(path2[j, i].ToString() + " ");
            //    }
            //    Console.WriteLine("length:" + pathdist[j]);
            //}
        #endregion

        }

    }
}
