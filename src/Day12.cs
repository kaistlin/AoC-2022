using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay12
{
    public readonly static string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day12input.txt";
    public readonly static string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day12sample.txt";
    byte[] InputBytes = File.ReadAllBytes(InputPath);
    int[,] Elevation;
    (int,int) Endpoint;
    int modulus = 96;
    

    [Benchmark]
    public void part1()
    {
        (int, int) Startpoint=(0,0);
        
        int modulus = 96;
        int height = ((InputBytes.Length+1) / modulus + 2);
       
        Elevation = new int[height, modulus+1];
        for (int i = 0; i< modulus+1; i++)
        {
            Elevation[0,i] = 126;
            Elevation[i%height, 0] = 126;
            Elevation[height-1, i] = 126;
            Elevation[i%height, modulus] = 126;
        }

        for (int i = 0; i < InputBytes.Length-1; i++)
        {
            if (InputBytes[i] == 10) { continue; }
            Elevation[(i / modulus) + 1, (i+1) % modulus] = InputBytes[i];
            
            if (InputBytes[i] == 69) {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 122;
                Endpoint = ((i / modulus) + 1, (i % modulus) + 1);
            }
            if(InputBytes[i] == 83)
            {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 97;
                Startpoint = ((i / modulus) + 1, (i % modulus) + 1);
            }
        }
        
        //Console.WriteLine("Done reading maps");
       
        //int? cost = DijkstrasSearch(Startpoint.Item1,Startpoint.Item2);
        //Console.WriteLine(cost);
        PriorityQueue<(int, int), int> NodesToTest = new();
        NodesToTest.Enqueue((Startpoint.Item1, Startpoint.Item2), 0);
        Dictionary<(int, int), (int, int)> CameFrom = new();
        Dictionary<(int, int), int> CostSoFar = new();
        CostSoFar[(Startpoint.Item1, Startpoint.Item2)] = 0;
        List<(int, int)> PossibleTrim = new();
        int cost = 0;
        do
        {
            (int, int) CurrentNode = NodesToTest.Dequeue();
            if (CurrentNode == Endpoint)
            {
                cost = CostSoFar[CurrentNode];
                break;
            }
            int NewCost = CostSoFar[CurrentNode] + 1;
            //Check if Down is a possible neighbor
            if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 + 1, CurrentNode.Item2] >= -1)
            {
                //check to make sure we haven't already visisted this node
                if (CostSoFar.TryAdd((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)])
                {
                    CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = NewCost;
                    NodesToTest.Enqueue((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                    CameFrom[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                }
            }
            //Check if Up is a possible neighbor
            if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 - 1, CurrentNode.Item2] >= -1)
            {
                //check to make sure we haven't already visisted this node
                if (CostSoFar.TryAdd((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)])
                {
                    CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = NewCost;
                    NodesToTest.Enqueue((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                    CameFrom[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                }
            }
            //Check if Right is a possible neighbor
            if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 + 1] >= -1)
            {
                //check to make sure we haven't already visisted this node
                if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)])
                {
                    CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = NewCost;
                    NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost); //add Neighbor to Queue
                    CameFrom[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                }
            }
            //Check if Left is a possible neighbor
            if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 - 1] >= -1)
            {
                //check to make sure we haven't already visisted this node
                if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)])
                {
                    CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = NewCost;
                    NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost); //add Neighbor to Queue
                    CameFrom[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                }
            }

        } while (NodesToTest.Count > 0);
       // Console.WriteLine(cost);
    }
    [Benchmark]
    public void part2()
    {
        int modulus = 96;
        int height = ((InputBytes.Length + 1) / modulus + 2);
        List<(int, int)> StartPoints = new();
        Elevation = new int[height, modulus + 1];
        for (int i = 0; i < modulus + 1; i++)
        {
            Elevation[0, i] = 126;
            Elevation[i % height, 0] = 126;
            Elevation[height - 1, i] = 126;
            Elevation[i % height, modulus] = 126;
        }

        for (int i = 0; i < InputBytes.Length - 1; i++)
        {
            if (InputBytes[i] == 10) { continue; }
            Elevation[(i / modulus) + 1, (i + 1) % modulus] = InputBytes[i];

            if (InputBytes[i] == 69)
            {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 122;
                Endpoint = ((i / modulus) + 1, (i % modulus) + 1);
            }
            if (InputBytes[i] == 83 || InputBytes[i] == 97)
            {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 97;
                StartPoints.Add(((i / modulus) + 1, (i % modulus) + 1));
            }
        }

        //Console.WriteLine("Done reading maps");
        int? currentMin = 1000;
        
        do
        {
            (int, int) Startpoint = StartPoints[StartPoints.Count - 1];
            StartPoints.Remove(Startpoint);
            PriorityQueue<(int, int), int> NodesToTest = new();
            NodesToTest.Enqueue((Startpoint.Item1, Startpoint.Item2), 0);
            Dictionary<(int, int), (int, int)> CameFrom = new();
            Dictionary<(int, int), int> CostSoFar = new();
            CostSoFar[(Startpoint.Item1, Startpoint.Item2)] = 0;
            List<(int, int)> PossibleTrim = new();
            int? cost = null;
            do
            {
                (int, int) CurrentNode = NodesToTest.Dequeue();
                if (CurrentNode == Endpoint)
                {
                    cost = CostSoFar[CurrentNode];
                    break;
                }
                int NewCost = CostSoFar[CurrentNode] + 1;
                //Check if Down is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 + 1, CurrentNode.Item2] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)])
                    {
                        CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Up is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 - 1, CurrentNode.Item2] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)])
                    {
                        CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Right is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 + 1] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)])
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Left is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 - 1] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)])
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }

            } while (NodesToTest.Count > 0);
            if (cost < currentMin)
            {
                currentMin = cost;
            }
        } while (StartPoints.Count > 0);
       //  Console.WriteLine(currentMin);
    }
    [Benchmark]
    public void part2Trim()
    {
        int modulus = 96;
        int height = ((InputBytes.Length + 1) / modulus + 2);


        List<(int, int)> StartPoints= new ();
        Elevation = new int[height, modulus + 1];
        for (int i = 0; i < modulus + 1; i++)
        {
            Elevation[0, i] = 126;
            Elevation[i % height, 0] = 126;
            Elevation[height - 1, i] = 126;
            Elevation[i % height, modulus] = 126;
        }

        for (int i = 0; i < InputBytes.Length - 1; i++)
        {
            if (InputBytes[i] == 10) { continue; }
            Elevation[(i / modulus) + 1, (i + 1) % modulus] = InputBytes[i];

            if (InputBytes[i] == 69)
            {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 122;
                Endpoint = ((i / modulus) + 1, (i % modulus) + 1);
            }
            if (InputBytes[i] == 83 || InputBytes[i] == 97)
            {
                Elevation[(i / modulus) + 1, (i % modulus) + 1] = 97;
                StartPoints.Add(((i / modulus) + 1, (i % modulus) + 1));
            }
        }

        //Console.WriteLine("Done reading maps");
        int? currentMin = 1000;
        do
        {
            (int, int) Startpoint = StartPoints[StartPoints.Count - 1];
            StartPoints.Remove(Startpoint);
            PriorityQueue<(int, int), int> NodesToTest = new();
            NodesToTest.Enqueue((Startpoint.Item1, Startpoint.Item2), 0);
            Dictionary<(int, int), (int, int)> CameFrom = new();
            Dictionary<(int, int), int> CostSoFar = new();
            CostSoFar[(Startpoint.Item1, Startpoint.Item2)] = 0;
            List<(int, int)> PossibleTrim = new();
            int? cost = null;
            do
            {
                (int, int) CurrentNode = NodesToTest.Dequeue();
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] == 97)
                {
                    PossibleTrim.Add(CurrentNode);
                }
                if (CurrentNode == Endpoint)
                {
                    cost = CostSoFar[CurrentNode];
                    break;
                }
                int NewCost = CostSoFar[CurrentNode] + 1;
                //Check if Down is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 + 1, CurrentNode.Item2] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)])
                    {
                        CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Up is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1 - 1, CurrentNode.Item2] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)])
                    {
                        CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Right is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 + 1] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)])
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }
                //Check if Left is a possible neighbor
                if (Elevation[CurrentNode.Item1, CurrentNode.Item2] - Elevation[CurrentNode.Item1, CurrentNode.Item2 - 1] >= -1)
                {
                    //check to make sure we haven't already visisted this node
                    if (CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)])
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost); //add Neighbor to Queue
                        CameFrom[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = CurrentNode; //set Neighbor's path length to curent node +1
                    }
                }

            } while (NodesToTest.Count > 0);
            if (cost == null)
            {
                foreach (var node in PossibleTrim)
                {
                    StartPoints.Remove((node.Item1, node.Item2));
                }
            }
            if (cost < currentMin)
            {
                currentMin = cost;
            }
        } while (StartPoints.Count > 0);
         //Console.WriteLine(currentMin);
    }
    public int Heuristic (int node1, int node2)
    {
        return Math.Abs(node1%modulus - node2%modulus) + Math.Abs(node1/modulus - node2/modulus);
    }
   
    class Node
    {
        public Node? Parent;
        public int NodeID;
        public char Value;
        public HashSet<int> Neighbors;
        public int PathCost;
        public Node(Node ParentNode, int NodeID, char Value, HashSet<int> Neighbors)
        {
            this.Parent = ParentNode;
            this.NodeID = NodeID;
            this.Value = Value;
            this.Neighbors = Neighbors;
            this.PathCost = 0;
        }
        public Node(int NodeID, char Value)
        {
            this.NodeID = NodeID;
            this.Value = Value;
            this.Neighbors = new HashSet<int>();
            this.Parent = null;
            this.PathCost = 0;
        }
    }
    [Benchmark]
    public void Part2Graph()
    {
        Node[] Nodes = new Node[3985];
        int modulus = 96;
        int EndNodeID = 0;
        List<Node> StartNodes = new();
        int i = 0;
        int NodeIndex = 0;
        int? currentMin = 10000;

        do
        {
            Node CurrentNode;
            if (InputBytes[i] == 10) { continue; }
            else if (InputBytes[i] == 69)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                EndNodeID = NodeIndex;

            }
            else if (InputBytes[i] == 83 || InputBytes[i] == 97)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                StartNodes.Add(CurrentNode);
                InputBytes[i] = 97;
            }
            else
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
            }
            //check the four possible neighbors to see if they are actual neighbors
            if (InputBytes[i] - InputBytes[i + 1] >= -1 && InputBytes[i + 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex + 1);
            }
            //check left, but only if i>0 and if Left isn't the \n character
            if (i > 0 && InputBytes[i] - InputBytes[i - 1] >= -1 && InputBytes[i - 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex - 1);
            }
            //check above
            if (i >= modulus && InputBytes[i] - InputBytes[i - modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex - modulus + 1);
            }
            //check below
            if (i < InputBytes.Length - modulus && InputBytes[i] - InputBytes[i + modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex + modulus - 1);
            }
            Nodes[NodeIndex] = (CurrentNode);
            NodeIndex++;
        } while (++i < InputBytes.Length);
        do
        {
            Queue<Node> Trimmable = new();
            PriorityQueue<Node, int> NodesToTest = new();
            Node CurrentNode = StartNodes[StartNodes.Count - 1];
            Dictionary<Node, int> CostSoFar = new();
            NodesToTest.Enqueue(CurrentNode, 0);
            CostSoFar.Add(CurrentNode, 0);
            StartNodes.Remove(CurrentNode);

            int? cost = null;
            do
            {
                CurrentNode = NodesToTest.Dequeue();

                if (CurrentNode.NodeID == EndNodeID)
                {
                    cost = CostSoFar[CurrentNode];
                    break;
                }
                if (CurrentNode.Value == 'a')
                {
                    Trimmable.Enqueue(CurrentNode);
                }
                foreach (int next in CurrentNode.Neighbors)
                {
                    int NewCost = CostSoFar[CurrentNode] + 1;
                    if (CostSoFar.TryAdd(Nodes[next], NewCost) || CostSoFar[Nodes[next]] > NewCost)
                    {
                        CostSoFar[Nodes[next]] = NewCost;
                        NodesToTest.Enqueue(Nodes[next], NewCost);
                        Nodes[next].Parent = CurrentNode;
                    }
                }
            } while (NodesToTest.Count > 0);
            if (cost == null)
            {
                foreach (Node ThrowAway in Trimmable)
                {
                    StartNodes.Remove(ThrowAway);
                }
            }
            Trimmable.Clear();
            if (cost < currentMin)
            {
                currentMin = cost;
            }
        } while (StartNodes.Count > 0);
        //  Console.WriteLine(currentMin);
    }
    [Benchmark]
    public void Part2GraphHeuristic()
    {
        Node[] Nodes = new Node[3985];
        int modulus = 96;
        int EndNodeID = 0;
        List<Node> StartNodes = new();
        int i = 0;
        int NodeIndex = 0;
        int? currentMin = 10000;

        do
        {
            Node CurrentNode;
            if (InputBytes[i] == 10) { continue; }
            else if (InputBytes[i] == 69)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                EndNodeID = NodeIndex;

            }
            else if (InputBytes[i] == 83 || InputBytes[i] == 97)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                StartNodes.Add(CurrentNode);
                InputBytes[i] = 97;
            }
            else
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
            }
            //check the four possible neighbors to see if they are actual neighbors
            if (InputBytes[i] - InputBytes[i + 1] >= -1 && InputBytes[i + 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex + 1);
            }
            //check left, but only if i>0 and if Left isn't the \n character
            if (i > 0 && InputBytes[i] - InputBytes[i - 1] >= -1 && InputBytes[i - 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex - 1);
            }
            //check above
            if (i >= modulus && InputBytes[i] - InputBytes[i - modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex - modulus + 1);
            }
            //check below
            if (i < InputBytes.Length - modulus && InputBytes[i] - InputBytes[i + modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex + modulus - 1);
            }
            Nodes[NodeIndex] = (CurrentNode);
            NodeIndex++;
        } while (++i < InputBytes.Length);
        do
        {
            Queue<Node> Trimmable = new();
            PriorityQueue<Node, int> NodesToTest = new();
            Node CurrentNode = StartNodes[StartNodes.Count - 1];
            Dictionary<Node, int> CostSoFar = new();
            NodesToTest.Enqueue(CurrentNode, Math.Abs(CurrentNode.NodeID % modulus - EndNodeID % modulus) + Math.Abs(CurrentNode.NodeID / modulus - EndNodeID / modulus));
            CostSoFar.Add(CurrentNode, 0);
            StartNodes.Remove(CurrentNode);

            int? cost = null;
            do
            {
                CurrentNode = NodesToTest.Dequeue();

                if (CurrentNode.NodeID == EndNodeID)
                {
                    cost = CostSoFar[CurrentNode];
                    break;
                }
                if (CurrentNode.Value == 'a')
                {
                    Trimmable.Enqueue(CurrentNode);
                }
                foreach (int next in CurrentNode.Neighbors)
                {
                    int NewCost = CostSoFar[CurrentNode] + 1;
                    if (CostSoFar.TryAdd(Nodes[next], NewCost) || CostSoFar[Nodes[next]] > NewCost)
                    {
                        CostSoFar[Nodes[next]] = NewCost;
                        NodesToTest.Enqueue(Nodes[next], NewCost + Math.Abs(CurrentNode.NodeID % modulus - EndNodeID % modulus) + Math.Abs(CurrentNode.NodeID / modulus - EndNodeID / modulus));
                        Nodes[next].Parent = CurrentNode;
                    }
                }
            } while (NodesToTest.Count > 0);
            if (cost == null)
            {
                foreach (Node ThrowAway in Trimmable)
                {
                    StartNodes.Remove(ThrowAway);
                }
            }
            Trimmable.Clear();
            if (cost < currentMin)
            {
                currentMin = cost;
            }
        } while (StartNodes.Count > 0);
        //  Console.WriteLine(currentMin);
    }
    public void Part2DFS()
    {
        Node[] Nodes = new Node[3985];
        byte[] InputBytes = File.ReadAllBytes(InputPath);
        int modulus = 96;
        int EndNodeID = 0;
        List<Node> StartNodes = new();
        int i = 0;
        int NodeIndex = 0;
        int? currentMin = 10000;

        do
        {
            Node CurrentNode;
            if (InputBytes[i] == 10) { continue; }
            else if (InputBytes[i] == 69)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                EndNodeID = NodeIndex;

            }
            else if (InputBytes[i] == 83 || InputBytes[i] == 97)
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
                StartNodes.Add(CurrentNode);
                InputBytes[i] = 97;
            }
            else
            {
                CurrentNode = new Node(NodeIndex, (char)InputBytes[i]);
            }
            //check the four possible neighbors to see if they are actual neighbors
            if (InputBytes[i] - InputBytes[i + 1] >= -1 && InputBytes[i + 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex + 1);
            }
            //check left, but only if i>0 and if Left isn't the \n character
            if (i > 0 && InputBytes[i] - InputBytes[i - 1] >= -1 && InputBytes[i - 1] != 10)
            {
                CurrentNode.Neighbors.Add(NodeIndex - 1);
            }
            //check above
            if (i >= modulus && InputBytes[i] - InputBytes[i - modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex - modulus + 1);
            }
            //check below
            if (i < InputBytes.Length - modulus && InputBytes[i] - InputBytes[i + modulus] >= -1)
            {
                CurrentNode.Neighbors.Add(NodeIndex + modulus - 1);
            }
            Nodes[NodeIndex] = (CurrentNode);
            NodeIndex++;
        } while (++i < InputBytes.Length);
        do
        {
            Queue<Node> Trimmable = new();
            PriorityQueue<Node, int> NodesToTest = new();
            Node CurrentNode = StartNodes[StartNodes.Count - 1];
            Dictionary<Node, int> CostSoFar = new();
            NodesToTest.Enqueue(CurrentNode, Math.Abs(CurrentNode.NodeID % modulus - EndNodeID % modulus) + Math.Abs(CurrentNode.NodeID / modulus - EndNodeID / modulus));
            CostSoFar.Add(CurrentNode, 0);
            StartNodes.Remove(CurrentNode);

            int? cost = null;
            do
            {
                CurrentNode = NodesToTest.Dequeue();

                if (CurrentNode.NodeID == EndNodeID)
                {
                    cost = CostSoFar[CurrentNode];
                    break;
                }
                if (CurrentNode.Value == 'a')
                {
                    Trimmable.Enqueue(CurrentNode);
                }
                foreach (int next in CurrentNode.Neighbors)
                {
                    int NewCost = CostSoFar[CurrentNode] + 1;
                    if (CostSoFar.TryAdd(Nodes[next], NewCost) || CostSoFar[Nodes[next]] > NewCost)
                    {
                        CostSoFar[Nodes[next]] = NewCost;
                        NodesToTest.Enqueue(Nodes[next], NewCost + Math.Abs(CurrentNode.NodeID % modulus - EndNodeID % modulus) + Math.Abs(CurrentNode.NodeID / modulus - EndNodeID / modulus));
                        Nodes[next].Parent = CurrentNode;
                    }
                }
            } while (NodesToTest.Count > 0);
            if (cost == null)
            {
                foreach (Node ThrowAway in Trimmable)
                {
                    StartNodes.Remove(ThrowAway);
                }
            }
            Trimmable.Clear();
            if (cost < currentMin)
            {
                currentMin = cost;
            }
        } while (StartNodes.Count > 0);
        //  Console.WriteLine(currentMin);
    }
}
