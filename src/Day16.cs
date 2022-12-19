using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using System.Diagnostics;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay16
{
    public static readonly string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day16input.txt";
    public static readonly string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day16sample2.txt";
    public static readonly string[] InputLines = File.ReadAllLines(SamplePath);
    public static readonly int NodeCount = InputLines.Length;
    public Dictionary<string, Node> CaveMap;
    public ulong globalRelease;
    public List<Node> ClosedValves;
    public static readonly ulong TotalTime = 26;
    public string globalPaths;
    public HashSet<string> Crawler = new();
    [Benchmark]
    public void Part1(){
         CaveMap = new();
        ClosedValves = new();
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
        
        ulong Time=1;
        ulong CurrentRelease = 0;
        CurrentNode = CaveMap["AA"];
        //Pathfind from AA to all ClosedValves
        foreach (Node ClosedValve2 in ClosedValves)
        {
            PriorityQueue<Node, ulong> NodesToTest = new();
            Dictionary<Node, ulong> CostSoFar = new();
            CostSoFar.Clear();
            NodesToTest.Clear();
            NodesToTest.Enqueue(CurrentNode, 0);
            CostSoFar.Add(CurrentNode, 0);
            do
            {
                Node NextNode = NodesToTest.Dequeue();
                if (NextNode == ClosedValve2)
                {
                    if (TotalTime - CostSoFar[NextNode] < 0)
                    {
                        continue;
                    }
                    ulong TotalReleaseHere = (TotalTime - CostSoFar[NextNode]) * NextNode.Pressure;
                    //Debug.WriteLine("It would take " + (CostSoFar[NextNode] + 1) + " minutes to get to Node " + NextNode.NodeID + " from " + CurrentNode.NodeID + " and close the valve and would give an eventual total release of " + TotalReleaseHere);
                    CurrentNode.Paths.Add(ClosedValve2, CostSoFar[NextNode]);
                    // ReleaseTotals.Add(NextNode, TotalReleaseHere);
                }
                if (NextNode.Neighbors.Contains(ClosedValve2.NodeID))
                {
                    ulong NewPathCost = CostSoFar[NextNode] + 1;
                    if (CostSoFar.TryAdd(ClosedValve2, NewPathCost) || CostSoFar[ClosedValve2] > NewPathCost)
                    {
                        CostSoFar[ClosedValve2] = NewPathCost;
                        NodesToTest.Enqueue(ClosedValve2, NewPathCost);
                        continue;
                    }
                }
                foreach (string Neighbor in NextNode.Neighbors)
                {
                    ulong NewPathCost = CostSoFar[NextNode] + 1;
                    if (CostSoFar.TryAdd(CaveMap[Neighbor], NewPathCost) || CostSoFar[CaveMap[Neighbor]] > NewPathCost)
                    {
                        CostSoFar[CaveMap[Neighbor]] = NewPathCost;
                        NodesToTest.Enqueue(CaveMap[Neighbor], NewPathCost);
                    }
                }
            } while (NodesToTest.Count > 0);
        }
        //Pathfind between all valves that are closed
        foreach (Node ClosedValve1 in ClosedValves){
            CurrentNode = ClosedValve1;
            foreach (Node ClosedValve2 in ClosedValves)
            { 
                PriorityQueue<Node, ulong> NodesToTest = new();
                Dictionary<Node, ulong> CostSoFar = new();
                CostSoFar.Clear();
                NodesToTest.Clear();
                NodesToTest.Enqueue(CurrentNode, 0);
                CostSoFar.Add(CurrentNode, 0);
                do
                {
                    Node NextNode = NodesToTest.Dequeue();
                    if (NextNode == ClosedValve2)
                    {
                        if (TotalTime - CostSoFar[NextNode] < 0)
                        {
                            continue;
                        }
                        ulong TotalReleaseHere = (TotalTime - CostSoFar[NextNode]) * NextNode.Pressure;
                        //Debug.WriteLine("It would take " + (CostSoFar[NextNode] + 1) + " minutes to get to Node " + NextNode.NodeID + " from " + CurrentNode.NodeID + " and close the valve and would give an eventual total release of " + TotalReleaseHere);
                        ClosedValve1.Paths.Add(ClosedValve2, CostSoFar[NextNode]);
                       // ReleaseTotals.Add(NextNode, TotalReleaseHere);
                    }
                    if (NextNode.Neighbors.Contains(ClosedValve2.NodeID))
                    {
                        ulong NewPathCost = CostSoFar[NextNode] + 1;
                        if (CostSoFar.TryAdd(ClosedValve2, NewPathCost) || CostSoFar[ClosedValve2] > NewPathCost)
                        {
                            CostSoFar[ClosedValve2] = NewPathCost;
                            NodesToTest.Enqueue(ClosedValve2, NewPathCost);
                            continue;
                        }
                    }
                    foreach (string Neighbor in NextNode.Neighbors)
                    {
                        ulong NewPathCost = CostSoFar[NextNode] + 1;
                        if (CostSoFar.TryAdd(CaveMap[Neighbor], NewPathCost) || CostSoFar[CaveMap[Neighbor]] > NewPathCost)
                        {
                            CostSoFar[CaveMap[Neighbor]] = NewPathCost;
                            NodesToTest.Enqueue(CaveMap[Neighbor], NewPathCost);
                        }
                    }
                } while (NodesToTest.Count > 0);
            }
        }
        Console.WriteLine("Found paths between all closed valves");
        CurrentNode = CaveMap["AA"];
        PathFinder(Time, new List<Node>(), 0, CurrentNode,"",0);
        Console.WriteLine("Total release was " + globalRelease);
        
        
    }
    public void PathFinder(ulong CurrentTime,List<Node> OpenedValves,ulong CurrentTotalRelease,Node CurrentNode,string CurrentPath,ulong OpenValvePressure)
    {
        List<Node> StillClosedValves = ClosedValves.Except(OpenedValves).ToList();
       // Debug.WriteLine("The time is " + CurrentTime + " and we currently have " + OpenedValves.Count + " valves open releasing " + OpenValvePressure + " and have released a total of " + CurrentTotalRelease + " pressure");
        ulong ProjectedTotal = (CurrentTotalRelease + (31 - (ulong)CurrentTime)*OpenValvePressure);
        if (ProjectedTotal > globalRelease)
        {
            Debug.WriteLine("Found new projected high: " + ProjectedTotal);
            globalRelease = ProjectedTotal;
        }
        Dictionary<Node, int> ReleaseTotals = new();
        if (StillClosedValves.Count == 0 || CurrentTime > TotalTime)
        {
          //  Debug.WriteLine("Found total release of " + CurrentTotalRelease);
            
            return;
        }
        StillClosedValves.Remove(CurrentNode);
        
        /**hardcoding going to DD first to help debugging
        if (CurrentPath == "")
        {
            Debug.WriteLine("Moving to DD" + " to free up 560 more pressure and have followed the path " + CurrentPath + "DD for a total release so far of " + (CurrentTotalRelease + 560));
            OpenedValves.Add(CaveMap["DD"]);
            PathFinder(3, OpenedValves, CurrentTotalRelease + (1 * OpenValvePressure), CaveMap["DD"], (CurrentPath + "DD"), OpenValvePressure + CaveMap["DD"].Pressure);
            OpenedValves.Remove(CaveMap["DD"]);
        }//hardcoding going to BB Second to help debugging
        if (CurrentPath == "DD")
        {
            Debug.WriteLine("Moving to BB to free up 325 more pressure and have followed the path DDBB for a total release so far of " + (CurrentTotalRelease + 560+325));
            OpenedValves.Add(CaveMap["BB"]);
            PathFinder(CurrentTime + CaveMap["BB"].Time+1, OpenedValves, CurrentTotalRelease + ((CaveMap["BB"].Time+1)*OpenValvePressure), CaveMap["BB"], (CurrentPath + "BB"), OpenValvePressure + CaveMap["BB"].Pressure);
            OpenedValves.Remove(CaveMap["BB"]);
        }**/
        foreach (Node ReleasePossibility in StillClosedValves)
            {
            // Debug.WriteLine("Moving to " + ReleasePossibility.NodeID + " to free up " + ReleasePossibility.Pressure + " more pressure and have have followed the path " + CurrentPath + ReleasePossibility.NodeID + " for an eventual total release of " + (ProjectedTotal+(ReleasePossibility.Pressure*(31-CurrentTime-1-CurrentNode.Paths[ReleasePossibility]))));
            if (CurrentNode.Paths[ReleasePossibility] + CurrentTime < 29)
            {
                OpenedValves.Add(ReleasePossibility);
                PathFinder(CurrentTime + 1 + CurrentNode.Paths[ReleasePossibility], OpenedValves, CurrentTotalRelease + ((CurrentNode.Paths[ReleasePossibility] + (ulong)1) * OpenValvePressure), ReleasePossibility, CurrentPath + ReleasePossibility.NodeID, OpenValvePressure + ReleasePossibility.Pressure);
                OpenedValves.Remove(ReleasePossibility);
            }
                /**CurrentNode = ReleaseTotals.Values[ReleaseTotals.Count - 1];
                CurrentTotalRelease += ReleaseTotals.Keys[ReleaseTotals.Count - 1];
                StillClosedValves.Remove(CurrentNode);**/
            }
        
        
        return;
    }
    public void PathFinder2(ulong CurrentTime, List<Node> OpenedValves, ulong CurrentTotalRelease, Node CurrentNode, string CurrentPath, ulong OpenValvePressure)
    {
        List<Node> StillClosedValves = ClosedValves.Except(OpenedValves).ToList();
        ulong MaxTime = 27;
       //  Debug.WriteLine("The time is " + CurrentTime + " and we currently have " + OpenedValves.Count + " valves open releasing " + OpenValvePressure + " and have released a total of " + CurrentTotalRelease + " pressure");
        if (CurrentTotalRelease > globalRelease)
        {
            Debug.WriteLine("Found new projected high: " + CurrentTotalRelease);
            globalRelease = CurrentTotalRelease;
            globalPaths = CurrentPath;
        }
        Dictionary<Node, int> ReleaseTotals = new();
        if (StillClosedValves.Count == 0 || CurrentTime > MaxTime)
        {
            //  Debug.WriteLine("Found total release of " + CurrentTotalRelease);

            return;
        }
        StillClosedValves.Remove(CurrentNode);

        /**hardcoding going to DD first to help debugging
        if (CurrentPath == "")
        {
            Debug.WriteLine("Moving to DD" + " to free up 560 more pressure and have followed the path " + CurrentPath + "DD for a total release so far of " + (CurrentTotalRelease + 560));
            OpenedValves.Add(CaveMap["DD"]);
            PathFinder(3, OpenedValves, CurrentTotalRelease + (1 * OpenValvePressure), CaveMap["DD"], (CurrentPath + "DD"), OpenValvePressure + CaveMap["DD"].Pressure);
            OpenedValves.Remove(CaveMap["DD"]);
        }//hardcoding going to BB Second to help debugging
        if (CurrentPath == "DD")
        {
            Debug.WriteLine("Moving to BB to free up 325 more pressure and have followed the path DDBB for a total release so far of " + (CurrentTotalRelease + 560+325));
            OpenedValves.Add(CaveMap["BB"]);
            PathFinder(CurrentTime + CaveMap["BB"].Time+1, OpenedValves, CurrentTotalRelease + ((CaveMap["BB"].Time+1)*OpenValvePressure), CaveMap["BB"], (CurrentPath + "BB"), OpenValvePressure + CaveMap["BB"].Pressure);
            OpenedValves.Remove(CaveMap["BB"]);
        }**/
        foreach (Node ReleasePossibility in StillClosedValves)
        {
             if (CurrentNode.Paths[ReleasePossibility] + CurrentTime < MaxTime-1)
            {
                ulong TotalToBeReleased = (MaxTime - CurrentNode.Paths[ReleasePossibility] - CurrentTime-1) * ReleasePossibility.Pressure;
                //Debug.WriteLine("Moving to " + ReleasePossibility.NodeID + " to free up " +TotalToBeReleased+ " more pressure and have have followed the path " + CurrentPath + ReleasePossibility.NodeID + " for an eventual total release of " + (CurrentTotalRelease + (ReleasePossibility.Pressure * (31 - CurrentTime - 1 - CurrentNode.Paths[ReleasePossibility]))));
                OpenedValves.Add(ReleasePossibility);
                PathFinder2(CurrentTime + 1 + CurrentNode.Paths[ReleasePossibility], OpenedValves, CurrentTotalRelease + TotalToBeReleased, ReleasePossibility, CurrentPath + ReleasePossibility.NodeID, OpenValvePressure + ReleasePossibility.Pressure);
                OpenedValves.Remove(ReleasePossibility);
            }
            /**CurrentNode = ReleaseTotals.Values[ReleaseTotals.Count - 1];
            CurrentTotalRelease += ReleaseTotals.Keys[ReleaseTotals.Count - 1];
            StillClosedValves.Remove(CurrentNode);**/
        }


        return;
    }
    public void ElephantPathFinder(ulong HumanTime, ulong ElephantTime, List<Node> OpenedValves, ulong CurrentTotalRelease, Node HumanNode, Node ElephantNode,string HumanPath, string ElephantPath,ulong HumanValvePressure,ulong ElephantValvePressure)
    {
        Crawler.Clear();
        List<Node> StillClosedValves = ClosedValves.Except(OpenedValves).ToList();
        if(Crawler.Contains((HumanPath+ElephantPath)))
        {
            Debug.WriteLine("Found duplicate");
            return;
        }
        Crawler.Add((HumanPath+ElephantPath));
        if (HumanTime>=ElephantTime)
        {
            CurrentTotalRelease += ElephantValvePressure;
            ElephantValvePressure = 0;
        }
        if(HumanTime<=ElephantTime)
        {
            CurrentTotalRelease += HumanValvePressure;
            HumanValvePressure = 0;
        }
        ulong CurrentTime = Math.Min(HumanTime, ElephantTime);
       //  Debug.WriteLine("The time is " + CurrentTime + " and we currently have " + OpenedValves.Count + " valves open and have released a total of " + CurrentTotalRelease + " pressure");
       // ulong ProjectedTotal = (CurrentTotalRelease + (31 - HumanTime) * HumanValvePressure) + (31 - ElephantTime * ElephantValvePressure); 
        if (CurrentTotalRelease > globalRelease)
        {
//            Debug.WriteLine("Found new projected high: " + CurrentTotalRelease + " and the human took path " + HumanPath+ " and the elephant took path " + ElephantPath);
            globalRelease = CurrentTotalRelease;
            globalPaths = (HumanPath + "-" + ElephantPath);
        }
        if (StillClosedValves.Count == 0 || CurrentTime >= 26)
        {

            CurrentTotalRelease += HumanValvePressure;
            CurrentTotalRelease += ElephantValvePressure;
            if (CurrentTotalRelease > globalRelease)
            {
                Debug.WriteLine("Found new projected high: " + CurrentTotalRelease + " and the human took path " + HumanPath + " and the elephant took path " + ElephantPath);
                globalRelease = CurrentTotalRelease;
                globalPaths = (HumanPath + "-" + ElephantPath);
            }
          //  Debug.WriteLine("Found total release of " + CurrentTotalRelease);

            return;
        }
        StillClosedValves.Remove(HumanNode);
        StillClosedValves.Remove(ElephantNode);

       
        int RemainingMoves = 0;
        if (HumanTime == ElephantTime)
        {
            foreach (Node HumanGoal in StillClosedValves)
            {
            HumanTime = CurrentTime + HumanNode.Paths[HumanGoal] + 1;
            if (HumanTime < 27)
            {
                OpenedValves.Add(HumanGoal);
                HumanValvePressure = (27 - HumanTime) * HumanGoal.Pressure;
               // Debug.WriteLine("The human is moving to " + HumanGoal.NodeID + " and will be done opening at the start of " + HumanTime + " to free up " + HumanValvePressure + " more pressure and have have followed the path " + HumanPath + HumanGoal.NodeID);
                foreach (Node ElephantGoal in StillClosedValves)
                {
                    if (ElephantGoal == HumanGoal)
                    {
                            
                            continue;
                    }
                   
                        ElephantTime = CurrentTime + ElephantNode.Paths[ElephantGoal] + 1;
                        if (ElephantTime < 27)
                        {
                            RemainingMoves++;
                            ElephantValvePressure = (27 - ElephantTime) * ElephantGoal.Pressure;
                            OpenedValves.Add(ElephantGoal);
                 //              Debug.WriteLine("Both are moving to " + ElephantGoal.NodeID + " and " + HumanGoal.NodeID+" and will be done opening at the start of " + ElephantTime + " to free up " + ElephantValvePressure + " more pressure and have have followed the path " + HumanPath+HumanGoal.NodeID+ElephantPath + ElephantGoal.NodeID);

                            ElephantPathFinder(HumanTime, ElephantTime, OpenedValves, CurrentTotalRelease, HumanGoal, ElephantGoal, HumanPath + HumanGoal.NodeID, ElephantPath + ElephantGoal.NodeID, HumanValvePressure, ElephantValvePressure);
                            OpenedValves.Remove(ElephantGoal);
                        }
                    
                }
                    RemainingMoves++;
                    OpenedValves.Remove(HumanGoal);
             }
            }
        }
        else if (HumanTime < ElephantTime)
        {
            foreach (Node HumanGoal in StillClosedValves)
            {
                HumanTime = CurrentTime + HumanNode.Paths[HumanGoal] + 1;
                if (HumanTime < 27)
                {
                    RemainingMoves++;
                    HumanValvePressure = (27 - HumanTime) * HumanGoal.Pressure;
                   // Debug.WriteLine("The human is moving to " + HumanGoal.NodeID + " and will be done opening at the start of " + HumanTime + " to free up " + HumanValvePressure + " more pressure and have have followed the path " + HumanPath + HumanGoal.NodeID+ElephantPath);
                    OpenedValves.Add(HumanGoal);
                    ElephantPathFinder(HumanTime, ElephantTime, OpenedValves, CurrentTotalRelease, HumanGoal, ElephantNode, HumanPath + HumanGoal.NodeID, ElephantPath, HumanValvePressure, ElephantValvePressure);
                    OpenedValves.Remove(HumanGoal);

                }
            }
        }
        else if (HumanTime > ElephantTime)
        {
            foreach (Node ElephantGoal in StillClosedValves)
            {
                ElephantTime = CurrentTime + ElephantNode.Paths[ElephantGoal] + 1;
                if (ElephantTime < 27)
                {
                    RemainingMoves++;
                    ElephantValvePressure = (27 - ElephantTime) * ElephantGoal.Pressure;
                  //  Debug.WriteLine("Elephant is moving to " + ElephantGoal.NodeID + " and will be done opening at the start of " + ElephantTime + " to free up " + ElephantValvePressure + " more pressure and have have followed the path " + HumanPath+ElephantPath + ElephantGoal.NodeID);
                    OpenedValves.Add(ElephantGoal);
                    ElephantPathFinder(HumanTime, ElephantTime, OpenedValves, CurrentTotalRelease, HumanNode, ElephantGoal, HumanPath, ElephantPath + ElephantGoal.NodeID, HumanValvePressure, ElephantValvePressure);
                    OpenedValves.Remove(ElephantGoal);

                }
            }
        }
        
        
    }
    public class Node
    {
        public Node? Parent;
        public string NodeID;
        public ulong Pressure;
        public HashSet<string> Neighbors;
        public Dictionary<Node, ulong> Paths;
        public ulong Time;
        public Node(Node ParentNode, string NodeID, int Flow, HashSet<string> Neighbors)
        {
            this.Parent = ParentNode;
            this.NodeID = NodeID;
            this.Pressure = (ulong)Flow;
            this.Neighbors = Neighbors;
            this.Time = 0;
            this.Paths = new Dictionary<Node, ulong>();
        }
        public Node(string NodeID, int Flow)
        {
            this.NodeID = NodeID;
            this.Pressure = (ulong)Flow;
            this.Neighbors = new HashSet<string>();
            this.Parent = null;
            this.Time = 0;
            this.Paths = new Dictionary<Node, ulong>();
        }
    }
    [Benchmark]
    public void Part2(){
        CaveMap = new();
        ClosedValves = new();
        Node CurrentNode;
        foreach (string Line in InputLines)
        {

            string[] LineParts = Line.Split(new char[] { ' ', '=', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            CurrentNode = new Node(LineParts[1], int.Parse(LineParts[5]));
            for (int i = 10; i < LineParts.Length; i++)
            {
                CurrentNode.Neighbors.Add(LineParts[i]);
            }
            if (CurrentNode.Pressure > 0)
            {
                ClosedValves.Add(CurrentNode);
            }
            CaveMap.Add(LineParts[1], CurrentNode);
        }
        Debug.WriteLine("Done adding Nodes");

        ulong Time = 1;
        ulong CurrentRelease = 0;
        CurrentNode = CaveMap["AA"];
        //Pathfind from AA to all ClosedValves
        foreach (Node ClosedValve2 in ClosedValves)
        {
            PriorityQueue<Node, ulong> NodesToTest = new();
            Dictionary<Node, ulong> CostSoFar = new();
            CostSoFar.Clear();
            NodesToTest.Clear();
            NodesToTest.Enqueue(CurrentNode, 0);
            CostSoFar.Add(CurrentNode, 0);
            do
            {
                Node NextNode = NodesToTest.Dequeue();
                if (NextNode == ClosedValve2)
                {
                    if (TotalTime - CostSoFar[NextNode] < 0)
                    {
                        continue;
                    }
                    ulong TotalReleaseHere = (TotalTime - CostSoFar[NextNode]) * NextNode.Pressure;
                    //Debug.WriteLine("It would take " + (CostSoFar[NextNode] + 1) + " minutes to get to Node " + NextNode.NodeID + " from " + CurrentNode.NodeID + " and close the valve and would give an eventual total release of " + TotalReleaseHere);
                    CurrentNode.Paths.Add(ClosedValve2, CostSoFar[NextNode]);
                    // ReleaseTotals.Add(NextNode, TotalReleaseHere);
                }
                if (NextNode.Neighbors.Contains(ClosedValve2.NodeID))
                {
                    ulong NewPathCost = CostSoFar[NextNode] + 1;
                    if (CostSoFar.TryAdd(ClosedValve2, NewPathCost) || CostSoFar[ClosedValve2] > NewPathCost)
                    {
                        CostSoFar[ClosedValve2] = NewPathCost;
                        NodesToTest.Enqueue(ClosedValve2, NewPathCost);
                        continue;
                    }
                }
                foreach (string Neighbor in NextNode.Neighbors)
                {
                    ulong NewPathCost = CostSoFar[NextNode] + 1;
                    if (CostSoFar.TryAdd(CaveMap[Neighbor], NewPathCost) || CostSoFar[CaveMap[Neighbor]] > NewPathCost)
                    {
                        CostSoFar[CaveMap[Neighbor]] = NewPathCost;
                        NodesToTest.Enqueue(CaveMap[Neighbor], NewPathCost);
                    }
                }
            } while (NodesToTest.Count > 0);
        }
        //Pathfind from current Node to all other Valves that are still closed
        foreach (Node ClosedValve1 in ClosedValves)
        {
            CurrentNode = ClosedValve1;
            foreach (Node ClosedValve2 in ClosedValves)
            {
                PriorityQueue<Node, ulong> NodesToTest = new();
                Dictionary<Node, ulong> CostSoFar = new();
                CostSoFar.Clear();
                NodesToTest.Clear();
                NodesToTest.Enqueue(CurrentNode, 0);
                CostSoFar.Add(CurrentNode, 0);
                do
                {
                    Node NextNode = NodesToTest.Dequeue();
                    if (NextNode == ClosedValve2)
                    {
                        if (TotalTime - CostSoFar[NextNode] < 0)
                        {
                            continue;
                        }
                        ulong TotalReleaseHere = (TotalTime - CostSoFar[NextNode]) * NextNode.Pressure;
                        //Debug.WriteLine("It would take " + (CostSoFar[NextNode] + 1) + " minutes to get to Node " + NextNode.NodeID + " from " + CurrentNode.NodeID + " and close the valve and would give an eventual total release of " + TotalReleaseHere);
                        ClosedValve1.Paths.Add(ClosedValve2, CostSoFar[NextNode]);
                        // ReleaseTotals.Add(NextNode, TotalReleaseHere);
                    }
                    if (NextNode.Neighbors.Contains(ClosedValve2.NodeID))
                    {
                        ulong NewPathCost = CostSoFar[NextNode] + 1;
                        if (CostSoFar.TryAdd(ClosedValve2, NewPathCost) || CostSoFar[ClosedValve2] > NewPathCost)
                        {
                            CostSoFar[ClosedValve2] = NewPathCost;
                            NodesToTest.Enqueue(ClosedValve2, NewPathCost);
                            continue;
                        }
                    }
                    foreach (string Neighbor in NextNode.Neighbors)
                    {
                        ulong NewPathCost = CostSoFar[NextNode] + 1;
                        if (CostSoFar.TryAdd(CaveMap[Neighbor], NewPathCost) || CostSoFar[CaveMap[Neighbor]] > NewPathCost)
                        {
                            CostSoFar[CaveMap[Neighbor]] = NewPathCost;
                            NodesToTest.Enqueue(CaveMap[Neighbor], NewPathCost);
                        }
                    }
                } while (NodesToTest.Count > 0);
            }
        }
        Console.WriteLine("Found paths between all closed valves");
        CurrentNode = CaveMap["AA"];
        PathFinder2(Time,new List<Node>(), 0, CurrentNode,"",0);
        Console.WriteLine("Total release was " + globalRelease + " along paths " + globalPaths);
        globalRelease = 0;
        for (int i = 0; i < globalPaths.Length - 2; i++)
        {
            ClosedValves.Remove(CaveMap[globalPaths.Substring(i++, 2)]);
        }
        PathFinder2(Time, new List<Node>(), 0, CurrentNode, "", 0);

    }
}
