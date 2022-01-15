using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSP;

namespace Interview
{
    class Program
    {
        static void foo(string s) { Console.WriteLine("ss"); }
        static void foo(int i) { Console.WriteLine("ii"); }
        static void foo(object s) { Console.WriteLine("oo"); }

        static void Main(string[] args)
        {
            //******* Test Deep Clone ************/
            // var testClone = new SomeComxClass();
            // testClone.Prop = 2;
            // testClone.ComxProp = new Codec();
            // var decMap = new Dictionary<string, string>();
            // decMap.Add("k1","v1");
            // testClone.ComxProp.decodeMap = decMap;
            // var cloned = testClone.DeepClone();

            // Console.WriteLine(cloned.Prop);
            // Console.WriteLine(cloned.ComxProp.decodeMap.Keys.ElementAt(0));

            //******* Test log transaction ************/
            // List<List<int>> ratings = new List<List<int>>();
            // ratings.Add(new List<int>() { 4, 4 });
            // ratings.Add(new List<int>() { 1, 2 });
            // ratings.Add(new List<int>() { 3, 6 });
            // int threshold = 77;
            // var logs = new string[] { "345366 89921 45", "029323 38239 23", "38239 345366 15", "029323 38239 77", "345366 38239 23", "029323 345366 13", "38239 38239 23" };
            //****************************************/

            var asa = new ArrayString();
            // Console.WriteLine(asa.UniqSub("aaa"));

            // var newInterval = new int[]{2,5};
            // var intv1 =  new int[]{1,3};
            // var intv2 =  new int[]{6,9};
            // int[][] existingInter = new int[][]{ intv1, intv2 };
            // asa.InsertInterval(existingInter, newInterval);

            // asa.TimeFrameConverter("mon 11:30 am, mon 3:40 pm");
            //Console.WriteLine( asa.rotationalCipher("Zebra-493?", 3));
            //asa.MinRemoveToMakeValid2("lee(t(c)o)de)");
            
            // var arrUnsort = new int[5] { 3, 34, 30, 5, 9 };
            // asa.SortingArrayIntoOneNum(arrUnsort);
            // asa.FradulentActivity(logs, 3);

            // Console.WriteLine(asa.five_stars_needed(ratings, threshold));

            // int kk = 2;
            // string[] keywords = new string[]{"deltacellular","anacell", "betacellular", "cetracular", "eurocell"};
            // string[] reviews = new string[]{
            //   "I love anacell Best services; Best services provided by anacdell",
            //   "betacellular has great services",
            //   "deltacellular provides much better services than betacellular",
            //   "cetracular is worse than anacell",
            //   "Betacellular is better than deltacellular."
            // };
            //             asa.kFrequenctly(keywords, reviews, kk);

            // List<PairString> itemAssociation1 = new  List<PairString>()
            // {
            //     new PairString("item1", "item2"),
            //     new PairString("item3", "item4"),
            //     new PairString("item4", "item5")
            // };

            // List<PairString> itemAssociation2 = new  List<PairString>()
            // {
            //     new PairString("item1", "item2"),
            //     new PairString("item4", "item5"),
            //     new PairString("item3", "item4"),
            //     new PairString("item1", "item4")
            // };

            // Console.Write("tesla: ");
            // Console.WriteLine(asa.folllowupThreeIdenticalConseLetters("aaa", new int[] { 1, 2, 3 }));

            // var dp = new DynamicProgramming();
            // asa.solution01(2, "1A 2F 1C");
            // asa.Compress(new char[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c' });

            // var enstr = asa.encode2(new List<string> { "str456#7/890", "str326#3/7890/123", "seffc" });
            // asa.decode2(enstr);

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
            // Console.WriteLine("doesn't block main thread");
            // foreach (var x in asa.MajorityElement2(new int[5] { 2, 2, 2, 4, 4 }))
            //     Console.WriteLine(x);


            //var ast = new AsyncTest();
            //ast.TestAsync();

            //Console.WriteLine("Press any key to exit...");
            //Console.ReadLine();

            //********** Test closure **********/
            // var C = new Circle();
            // Console.WriteLine("circumference:" + C.Calculate((x) => { return x * 2 * 3.14; }));
            //**********************************/

            // UInt16 uii = 0xFFFF;
            // var byt = BitConverter.GetBytes(uii);
            // Int32 data = BitConverter.ToInt16(byt, 0);
            // Console.WriteLine(data);

            // var bs = new BinarySearch();
            // bs.MyPow(5.0, -4);

            // var bm = new BitManipulate();

            // //Console.WriteLine("4th bit:" + bm.FourthBit(-23));
            // bm.CountOneBit(4294967295);

            var bk = new BackTracking();
            bk.Subsets(new []{1,2,3});

            //bk.SubarraysDivByK(new int[] { 4, 5, 0, -2, -3, 1 }, 5);

            // Console.WriteLine("Permutation:########");

            // var perret = bk.Permute(new int[3] { 1, 2, 3 });
            // foreach (var x in perret)
            // {
            //     foreach (var y in x)
            //     {
            //         Console.Write(y);
            //         Console.Write(",");
            //     }
            //     Console.WriteLine();
            // }

            // bk.GetFactors(12);

            // TreeNode root = new TreeNode(1);
            // TreeNode n2 = new TreeNode(2);
            // TreeNode n3 = new TreeNode(3);
            // root.left = n2;
            // root.right = n3;
            // n2.left = new TreeNode(4);
            // n2.right = new TreeNode(5);
            // n3.left = new TreeNode(6);
            // n3.right = new TreeNode(7);

            // var bt = new BTree();
            // Console.WriteLine("********************************MPS");
            // bt.MaxPathSum(root);

            // bt.deserialize(bt.serialize(root));
            // var ret = bt.FindLeaves(root);

            // bt.VerticalTraversal(root);

            var ht = new HashTable();
            //Console.WriteLine(ht.LongestSubstringEvenCount("abzabcddc"));
            // ht.PartitionLabels("ababcbacadefegdehijhklij");

            // Console.WriteLine("find anagrams:");
            // foreach (var x in ht.FindAnagrams2("cbaerwwbac", "abc"))
            // {
            //     Console.Write(x + ',');
            // }

            var dfs = new DFS_BFS();
            Console.WriteLine(dfs.alienOrder(new string[]{"wrt","wrf","er","ett","rftt"}));

            //dfs.LadderLength("a", "c", new string[]{"a","b","c"});

            // var flights = new int[3][];
            // flights[0] = new int[]{0,1,100};
            // flights[1] = new int[]{1,2,100};
            // flights[2] = new int[]{0,2,500};
            //dfs.FindCheapestPriceBFS(3, flights, 0, 2, 0);

            //dfs.ValidPalindrome2("cbbcc");
            // var parentChildPairs = new List<int[]>() {
            // new int[]{1, 3},
            // new int[]{2, 3},
            // new int[]{3, 6},
            // new int[]{5, 6},
            // new int[]{5, 7},
            // new int[]{4, 5},
            // new int[]{4, 8},
            // new int[]{8, 10}
            //     };

            // Console.Write("find CA:");
            // Console.WriteLine(dfs.findCA(parentChildPairs, 10, 7));

            //var chlist = new char[6] { 'A', 'A', 'A', 'B', 'B', 'B' };
            //Console.WriteLine("tasks:" + dfs.LeastInterval(chlist, 0));

            // Console.WriteLine("word break");
            // Console.WriteLine(dfs.WordBreak("leetcode", new List<string> { "leet", "code" }));

            // Point pt = new Point() { x = 100, y = 100 };
            // Point pt2 = new Point() { x = 0, y = 0 };
            // dfs.refSwap(ref pt, ref pt2);
            // Console.WriteLine(pt.x + "," + pt.y);
            // Console.WriteLine(pt2.x + "," + pt2.y);

            // dfs.KillProcess(new List<int> { 1, 3, 10, 5 }, new List<int> { 3, 0, 5, 3 }, 5);

            // //asa.GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            // asa.Compress("AAABBCCCCCCAAAAA");
            // asa.LongestIncreasingSubArray(new int[] { 15, 14, 12, 11, 2 });
            // asa.ProductExceptSelf(new int[] { 1, 2, 3, 4, });
            // //asa.SearchRotatedSortedArray(new int[] { 2,2,2,0,2,2 }, 0);
            // asa.MaxSubArray(new int[] { 1, 2, -4, 4, 5, 6 });
            // asa.SortColors(new int[] { 1, 2, 0 });
            // asa.Equi(new int[] { -1, 3, -4, 5, 1, -6, 2, 1 });


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
