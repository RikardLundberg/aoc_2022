using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_12
{
    public class Node
    {
        public Node(char val) { value = val; }
        public List<Node> neighbours { get; set; } = new List<Node>();
        public char value { get; set; }
        public bool isEnd { get; set; } = false;
        public int index { get; set; }
        public int distance { get; set; } = int.MaxValue;
    }

    class Program
    {
        static List<Node> allNodes = new List<Node>();
        static Node start = null;
        static List<Node> possibleStarts = new List<Node>();
        static int endIndex = 0;
        static void Main(string[] args)
        {
            first();
            second();
        }

        public static void second()
        {
            int shortestHike = int.MaxValue;
            foreach(var node in possibleStarts)
            {
                var path = BFS(node, allNodes, endIndex);
                if (path < shortestHike && path != -1)
                    shortestHike = path;
            }
            Console.WriteLine("Shortest hike is: " + shortestHike);
        }

        public static void first()
        {
            ParseInput();
            Console.WriteLine("Shortest path is: " + BFS(start, allNodes, endIndex));
        }

        private static void ParseInput()
        {
            var lines = File.ReadAllLines("../../input.txt");
            var lineCount = 0;
            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Node newNode = new Node(line[i]);
                    newNode.index = allNodes.Count;
                    if (newNode.value == 'S')
                    {
                        newNode.value = 'a';
                        start = newNode;
                    }
                    else if (newNode.value == 'E')
                    {
                        endIndex = newNode.index;
                        newNode.isEnd = true;
                        newNode.value = 'z';
                    }
                    if (newNode.value == 'a')
                        possibleStarts.Add(newNode);

                    List<Node> potentialNeighbours = new List<Node>();
                    if (i > 0)
                        potentialNeighbours.Add(allNodes[(lineCount * line.Length) + i - 1]);
                    if (lineCount > 0)
                        potentialNeighbours.Add(allNodes[((lineCount - 1) * line.Length) + i]);
                    foreach (var potNeighbour in potentialNeighbours)
                    {
                        if (potNeighbour.value <= newNode.value + 1)
                            newNode.neighbours.Add(potNeighbour);
                        if (newNode.value <= potNeighbour.value + 1)
                            potNeighbour.neighbours.Add(newNode);
                    }
                    allNodes.Add(newNode);
                }
                lineCount++;
            }
        }

        private static int BFS(Node start, List<Node> allNodes, int endIndex)
        {
            List<Node> queue = new List<Node>();
            bool[] visited = new bool[allNodes.Count];
            int[] dist = new int[allNodes.Count];
            Node[] pred = new Node[allNodes.Count];

            for (int i = 0; i < allNodes.Count; i++)
            {
                visited[i] = false;
                dist[i] = int.MaxValue;
                pred[i] = null;
            }
            visited[start.index] = true;
            dist[start.index] = 0;
            queue.Add(start);

            while (queue.Count != 0)
            {
                var node = queue[0];
                queue.RemoveAt(0);

                for (int i = 0; i < node.neighbours.Count; i++)
                {
                    var neighbour = node.neighbours[i];
                    if (visited[neighbour.index] == false)
                    {
                        visited[neighbour.index] = true;
                        dist[neighbour.index] = dist[node.index] + 1;
                        pred[neighbour.index] = node;
                        queue.Add(neighbour);

                        if (neighbour.index == endIndex)
                            return dist[endIndex];
                    }
                }
            }
            return -1;
        }
    }
}
