using BenchmarkDotNet.Attributes;
using System.Diagnostics;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay16
{
    public static readonly string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day16input.txt";
    public static readonly string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day16sample.txt";
    public static readonly string[] InputLines = File.ReadAllLines(SamplePath);
    public static readonly int NodeCount = InputLines.Length;
    [Benchmark]
    public void Part1(){
        Dictionary<string,Node> CaveMap = new();
        List<Node> ClosedValves = new();
        Node CurrentNode;
        foreach (string Line in InputLines)
        {
            
            string[] LineParts = Line.Split(new char[] { ' ', '=', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            CurrentNode = new Node(LineParts[1], int.Parse(LineParts[5]));
            for (int i=10; i < LineParts.Length; i++)
            {
                CurrentNode.Neighbors.Add(LineParts[i]);
            }
            if (CurrentNode.Pressure > 0)
            {
                ClosedValves.Add(CurrentNode);
            }
            CaveMap.Add(LineParts[1],CurrentNode);
        }
        Debug.WriteLine("Done adding Nodes");
        int TimeLeft = 30;
        int CurrentRelease = 0;
        CurrentNode = CaveMap["AA"];

        foreach (Node ClosedValve in ClosedValves)
        {
            PriorityQueue<Node,int> NodesToTest = new();
            Dictionary<Node, int> CostSoFar = new();
            NodesToTest.Enqueue(CurrentNode,0);
            CostSoFar.Add(CurrentNode, 0);
            do
            {
                Node NextNode = NodesToTest.Dequeue();
                if(NextNode == ClosedValve)
                {

                }
                foreach (string Neighbor in CurrentNode.Neighbors)
                {
                    int NewPathCost = CostSoFar[CurrentNode] + 1;
                    if (CostSoFar.TryAdd(CaveMap[Neighbor],NewPathCost)|| CostSoFar[CaveMap[Neighbor]]>NewPathCost)
                    {
                        CostSoFar[CaveMap[Neighbor]] = NewPathCost;
                        NodesToTest.Enqueue(CaveMap[Neighbor], NewPathCost);
                    }
                }
                TimeLeft--;
            } while (NodesToTest.Count>0&&TimeLeft>0);
        }
    }
    class Node
    {
        public Node? Parent;
        public string NodeID;
        public int Pressure;
        public HashSet<string> Neighbors;
        public uint Release;
        public Node(Node ParentNode, string NodeID, int Flow, HashSet<string> Neighbors)
        {
            this.Parent = ParentNode;
            this.NodeID = NodeID;
            this.Pressure = Flow;
            this.Neighbors = Neighbors;
            this.Release = 0;
        }
        public Node(string NodeID, int Flow)
        {
            this.NodeID = NodeID;
            this.Pressure = Flow;
            this.Neighbors = new HashSet<string>();
            this.Parent = null;
            this.Release = 0;
        }
    }
    [Benchmark]
    public void Part2(){

    }
}
