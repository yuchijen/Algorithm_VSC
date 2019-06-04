using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP
{

    public class Travelerproblem { }


    //  steps of TSP Nearest Neighbour algorithm:  For Only One Messager !!
    //  1.start on an arbitrary vertex as current vertex.
    //  2.find out the shortest edge connecting current vertex and an unvisited vertex V.
    //  3.set current vertex to V.
    //  4.mark V as visited.
    //  5.if all the vertices in domain are visited, then terminate, else Go to step 2.
    public class TSPNearestNeighbour
    {
        private int numberOfNodes;
        private Stack<int> stack;
        
        public TSPNearestNeighbour()
        {
            stack = new Stack<int>();
        }

        public void tsp(int[,] adjacencyMatrix)
        {
            numberOfNodes = adjacencyMatrix.GetLength(0) - 1;
            int[] visited = new int[numberOfNodes + 1];
            visited[1] = 1;
            stack.Push(1);
            
            int min = int.MaxValue;
            bool minFlag = false;
            List<int> path = new List<int>();
            path.Add(1);
            //Console.Write(1 + "->");
            int cost = 0;

            while (stack.Count > 0)
            {
                int currentNode, dst = 0;
                currentNode = stack.Peek();
                min = int.MaxValue;
                // traversal unvisited adjacent nodes to pick nearest one as next 
                for (int adj = 1; adj <= numberOfNodes; adj++)
                {
                    if (adjacencyMatrix[currentNode, adj] > 1 && visited[adj] == 0)
                    {
                        if (min > adjacencyMatrix[currentNode, adj])
                        {
                            min = adjacencyMatrix[currentNode, adj];
                            dst = adj;
                            minFlag = true;
                        }
                    }
                }
                if (minFlag)
                {
                    visited[dst] = 1;
                    stack.Push(dst);
                    path.Add(dst);
                    //Console.Write(dst + "->");
                    minFlag = false;
                    continue;
                }
                //if min not found , remove current node
                stack.Pop();
            }
            for (int i = 0; i < path.Count; i++)
            {
                if (i < path.Count - 1)
                    Console.Write(path[i] + "->");
                else
                    Console.Write(path[i]);
            }
            for (int i = 1; i < path.Count; i++)
            {
                cost += adjacencyMatrix[path[i - 1], path[i]];
            }

            Console.WriteLine();
            Console.WriteLine("min time required is: " + cost);
        }

    }


    //Dijkstra, find node to node shortest path
    public class Dijkstra
    {
        //find shortest path from origin to target
        public static int getShortedPath(int[,] G, int start, int end, int[] path, int numberVertex)
        {
            bool[] foundShortest = new bool[numberVertex]; //find shortest path from origin to current node  
            int min;  //current min dist 
            int curNode = 0; //temp node  
            int[] dist = new int[numberVertex];  // Unknown distance from source to vertax
            int[] prev = new int[numberVertex];  // Previous node in optimal path from source

            // initialize start vertex to its adjecent points distance.
            for (int v = 0; v < numberVertex; v++)
            {
                foundShortest[v] = false;
                dist[v] = G[start, v];  // record origin node's adjacent nodes              
            }
            path[0] = end;
            dist[start] = 0;
            foundShortest[start] = true;

            //Dijkstra 
            for (int i = 1; i < numberVertex; i++)
            {
                min = int.MaxValue;
                for (int j = 0; j < numberVertex; j++)
                {
                    if (!foundShortest[j] && dist[j] < min)
                    {
                        curNode = j;
                        min = dist[j];
                    }
                }
                //set adjecent min from origin
                foundShortest[curNode] = true;

                //update min path to all nodes from origin
                for (int j = 0; j < numberVertex; j++)
                {
                    if (!foundShortest[j] && min + G[curNode, j] < dist[j])
                    {
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;
                    }
                }
            }
            //output path
            int e = end, step = 0;
            while (e != start)
            {
                step++;
                path[step] = prev[e];
                e = prev[e];
            }
            for (int i = step; i > step / 2; i--)
            {
                int temp = path[step - i];
                path[step - i] = path[i];
                path[i] = temp;
            }
            return dist[end];
        }

    }


    public class Dijkstra2
    {
        //static int length = 6;
        //static string[] shortedPath = new string[length];
        //static int noPath = 2000;
        //static int MaxSize = 1000;
        //static int[,] G =
        //{
        // { noPath, noPath, 10, noPath, 30, 100 },
        // { noPath, noPath, 5, noPath, noPath, noPath },
        // { noPath, noPath, noPath, 50, noPath, noPath },
        // { noPath, noPath, noPath, noPath, noPath, 10 },
        // { noPath, noPath, noPath, 20, noPath, 60 },
        // { noPath, noPath, noPath, noPath, noPath, noPath }
        //};

        //static string[] PathResult = new string[length];
        //static int[] path1 = new int[length];
        //static int[,] path2 = new int[length, length];
        //static int[] distance2 = new int[length];

        //static void Main(string[] args)
        //{
        //    int dist1 = getShortedPath(G, 0, 5, path1);
        //    Console.WriteLine("Node 0 To 5:");
        //    for (int i = 0; i < path1.Length; i++)
        //        Console.Write(path1[i].ToString() + " ");
        //    Console.WriteLine("Length:" + dist1);

        //    int[] pathdist = getShortedPath(G, 0, path2);
        //    Console.WriteLine("\nNode 0 To other:");
        //    for (int j = 0; j < pathdist.Length; j++)
        //    {
        //        Console.WriteLine("Node 0 to " + j + " path:");
        //        for (int i = 0; i < length; i++)
        //        {
        //            Console.Write(path2[j, i].ToString() + " ");
        //        }
        //        Console.WriteLine("length:" + pathdist[j]);
        //    }
        //    Console.ReadKey();
        //}

        //find shortest path from origin to target
        public static int getShortedPath(int[,] G, int start, int end, int[] path, int numberNode)
        {
            int length = numberNode;
            bool[] foundShortest = new bool[length]; //find shortest path from origin to current node  表示找到起始结点与当前结点间的最短路径
            int min;  //current min dist 最小距离临时变量
            int curNode = 0; //temp node  临时结点，记录当前正计算结点
            int[] dist = new int[length];  // Unknown distance from source to vertax
            int[] prev = new int[length];  // Previous node in optimal path from source

            // initialize start vertex to its adjecent points distance.
            for (int v = 0; v < length; v++)
            {
                foundShortest[v] = false;
                dist[v] = G[start, v];  // record origin node's adjacent nodes              
            }
            path[0] = end;
            dist[start] = 0;
            foundShortest[start] = true;

            //Dijkstra 
            for (int i = 1; i < length; i++)
            {
                min = int.MaxValue;
                for (int j = 0; j < length; j++)
                {
                    if (!foundShortest[j] && dist[j] < min)
                    {
                        curNode = j;
                        min = dist[j];
                    }
                }

                foundShortest[curNode] = true;

                for (int j = 0; j < length; j++)
                {
                    if (!foundShortest[j] && min + G[curNode, j] < dist[j])
                    {
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;
                    }
                }
            }
            //输出路径结点
            int e = end, step = 0;

            while (e != start)
            {
                step++;
                path[step] = prev[e];
                e = prev[e];
            }
            for (int i = step; i > step / 2; i--)
            {
                int temp = path[step - i];
                path[step - i] = path[i];
                path[i] = temp;
            }
            return dist[end];
        }

        //find all shortest paths from origin 
        public static int[] getShortedPath(int[,] G, int start, int[,] path, int numberNode)
        {
            int length = numberNode;
            int[] PathID = new int[length];//all path 
            bool[] foundShortest = new bool[length]; //found shortest
            int min;  //min dist
            int curNode = 0;
            int[] dist = new int[length];  // Unknown distance from source to vertax
            int[] prev = new int[length];  // Previous node in optimal path from source

            // initialize start vertex to its adjecent points distance.
            for (int v = 0; v < length; v++)
            {
                foundShortest[v] = false;
                dist[v] = G[start, v];
                path[v, 0] = v;
            }

            dist[start] = 0;
            foundShortest[start] = true;

            //Dijkstra
            for (int i = 1; i < length; i++)
            {
                min = int.MaxValue;
                for (int j = 0; j < length; j++)
                {
                    if (!foundShortest[j] && dist[j] < min)
                    {
                        curNode = j;
                        min = dist[j];
                    }
                }

                foundShortest[curNode] = true;

                for (int j = 0; j < length; j++)
                {
                    if (!foundShortest[j] && min + G[curNode, j] < dist[j])
                    {
                        dist[j] = min + G[curNode, j];
                        prev[j] = curNode;
                    }
                }
            }
            //输出路径结点
            for (int k = 0; k < length; k++)
            {
                int e = k, step = 0;
                while (e != start)
                {
                    step++;
                    path[k, step] = prev[e];
                    e = prev[e];
                }
                for (int i = step; i > step / 2; i--)
                {
                    int temp = path[k, step - i];
                    path[k, step - i] = path[k, i];
                    path[k, i] = temp;
                }
            }
            return dist;

        }
    }


}
