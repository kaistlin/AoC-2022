using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay15
{
    public static readonly string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day15input.txt";
    public static readonly string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day15sample.txt";
    public static readonly string[] InputStrings = File.ReadAllLines(InputPath);
    int LineOfInterest = (InputStrings.Length==14) ? 10 : 2000000;
    long max = (InputStrings.Length == 14) ? 20 : 4000000;
    [Benchmark]
    public void Part1(){ 
        Dictionary<(long,long),long> Sensors = new();
        HashSet<(long,long)> BeaconsOnLine = new();
        foreach(string Line in InputStrings)
        {
            string[] Sensor = Line.Split(new char[] { ' ','=',',',':'}, StringSplitOptions.RemoveEmptyEntries);
            long SensorX = int.Parse(Sensor[3]);
            long SensorY = int.Parse(Sensor[5]);
            long BeaconX = int.Parse(Sensor[11]);
            long BeaconY = int.Parse(Sensor[13]);
            if (BeaconY == LineOfInterest)
            {
                BeaconsOnLine.Add((BeaconX, BeaconY));
            }
            long Distance = Math.Abs(SensorX-BeaconX)+Math.Abs(SensorY-BeaconY);
            Sensors.Add((SensorX,SensorY), Distance);
        }
        HashSet<(long, long)> NoBeacons = new();
        foreach((long, long) Sensor in Sensors.Keys)
        {
            long Range = Sensors[Sensor];
            (long, long) CurrentNode = Sensor;
            PriorityQueue<(long, long),long> NodesToTest = new();
            NodesToTest.Enqueue(CurrentNode,0);
            Dictionary<(long, long), long> CostSoFar = new();
            CostSoFar.Add(CurrentNode, 0);
            long NewCost = 0;
            long RangeY = Math.Abs(CurrentNode.Item2 - LineOfInterest);
            long RangeX = Range - RangeY;
            if(Math.Abs(CurrentNode.Item2 - LineOfInterest) > Range)
            {
                continue;
            }
            long CurrX = Sensor.Item1 - RangeX;
            long MaxX = Sensor.Item1 + RangeX;
            do
            {
                NoBeacons.Add((CurrX, LineOfInterest));
           //     Console.WriteLine("Adding " + CurrX + "," + LineOfInterest);
            } while (CurrX++ < MaxX);
            /**do
            {
                CurrentNode = NodesToTest.Dequeue();
                NewCost = CostSoFar[CurrentNode]+1;
                
                if(NewCost-1==Range)
                {
                    //Console.WriteLine("Found an endnode");
                    continue;
                }
                else
                {
                    if ((CostSoFar.TryAdd((CurrentNode.Item1+1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)]) && RangeX <= Math.Abs(CurrentNode.Item2 + 1 - Sensor.Item1))
                    {

                        CostSoFar[(CurrentNode.Item1 + 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 + 1, CurrentNode.Item2), NewCost);
                        if (CurrentNode.Item2 == LineOfInterest)
                        {
                            NoBeacons.Add((CurrentNode.Item1 + 1, CurrentNode.Item2));
                        }
                    }
                    if ((CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)])&&CurrentNode.Item2<LineOfInterest)
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 + 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 + 1), NewCost);
                        if (CurrentNode.Item2+1 == LineOfInterest)
                        {
                            NoBeacons.Add((CurrentNode.Item1, CurrentNode.Item2 + 1));
                        }
                    }
                    if ((CostSoFar.TryAdd((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)])&& RangeX <= Math.Abs(CurrentNode.Item2 - 1 - Sensor.Item1))
                    {
                        CostSoFar[(CurrentNode.Item1 - 1, CurrentNode.Item2)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1 - 1, CurrentNode.Item2), NewCost);
                        if (CurrentNode.Item2 == LineOfInterest)
                        {
                            NoBeacons.Add((CurrentNode.Item1 - 1, CurrentNode.Item2));
                        }
                    }
                    if ((CostSoFar.TryAdd((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost) || NewCost < CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)])&&CurrentNode.Item2>LineOfInterest)
                    {
                        CostSoFar[(CurrentNode.Item1, CurrentNode.Item2 - 1)] = NewCost;
                        NodesToTest.Enqueue((CurrentNode.Item1, CurrentNode.Item2 - 1), NewCost);
                        if (CurrentNode.Item2 - 1 == LineOfInterest)
                        {
                            NoBeacons.Add((CurrentNode.Item1, CurrentNode.Item2 - 1));
                        }
                    }
                }

            } while (NodesToTest.Count > 0);**/
        }
        Debug.WriteLine("Done. Answer is " + (NoBeacons.Count-BeaconsOnLine.Count));
    }
     [Benchmark]
    public void Part2()
    {
        Dictionary<(long, long), long> Sensors = new();
        HashSet<(long, long)> PossibleBeacons = new();
        foreach (string Line in InputStrings)
        {
            string[] Sensor = Line.Split(new char[] { ' ', '=', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            long SensorX = int.Parse(Sensor[3]);
            long SensorY = int.Parse(Sensor[5]);
            long BeaconX = int.Parse(Sensor[11]);
            long BeaconY = int.Parse(Sensor[13]);
            long Distance = Math.Abs(SensorX - BeaconX) + Math.Abs(SensorY - BeaconY);
            long TopY = SensorY - Distance - 1;
            long BottomY = SensorY + Distance + 1;
            long RightX = SensorY + Distance - 1;
            long LeftX = SensorY - Distance + 1;
            //go down and to the right
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX + i) <= max && (SensorX + i) >= 0)
                {
                    if ((TopY + i) <= max && (TopY + i) >= 0)
                    {
                        PossibleBeacons.Add((SensorX + i, TopY + i));

                    }
                }
            }
            //go down and to the left
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX - i) <= max && (SensorX - i) >= 0)
                {
                    if ((TopY + i) <= max && (TopY + i) >= 0)
                    {
                        PossibleBeacons.Add((SensorX - i, TopY + i));

                    }
                }
            }
            //go up and to the right
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX + i) <= max && (SensorX + i) >= 0)
                {
                    if ((BottomY - i) <= max && (BottomY - i) >= 0)
                    {
                        PossibleBeacons.Add((SensorX + i, BottomY - i));

                    }
                }
            }
            //go up and to the left
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX - i) <= max && (SensorX - i) >= 0)
                {
                    if ((BottomY - i) <= max && (BottomY - i) >= 0)
                    {
                        PossibleBeacons.Add((SensorX - i, BottomY - i));

                    }
                }
            }


            Sensors.Add((SensorX, SensorY), Distance);
        }
        HashSet<(long, long)> NoBeacons = new();
        /**foreach ((long, long) Sensor in Sensors.Keys)
        {
            long Range = Sensors[Sensor];
            (long, long) CurrentNode = Sensor;
            PriorityQueue<(long, long), long> NodesToTest = new();
            NodesToTest.Enqueue(CurrentNode, 0);
            Dictionary<(long, long), long> CostSoFar = new();
            CostSoFar.Add(CurrentNode, 0);
            long NewCost = 0;
            long RangeY = Math.Abs(CurrentNode.Item2 - LineOfInterest);
            long RangeX = Range - RangeY;
            if (Math.Abs(CurrentNode.Item2 - LineOfInterest) > Range)
            {
                continue;
            }
            long CurrX = (Sensor.Item1 - RangeX);
            CurrX = (CurrX > 0) ? CurrX : 0;
            long MaxX = Sensor.Item1 + RangeX;
            MaxX = Math.Min(MaxX, 4000000);
            do
            {
                NoBeacons.Add((CurrX, LineOfInterest));
                //     Console.WriteLine("Adding " + CurrX + "," + LineOfInterest);
            } while (CurrX++ < MaxX);
        }**/

        Parallel.ForEach(PossibleBeacons, node =>
        {
            bool Found = false;
            foreach ((long, long) Sensor in Sensors.Keys)
            {
                if (Math.Abs(Sensor.Item1 - node.Item1) + Math.Abs(Sensor.Item2 - node.Item2) <= Sensors[Sensor])
                {
                    Found = false;
                    break;
                }
                else
                {
                    Found = true;
                }

            }
            if (Found)
            {
              //  Console.WriteLine("Found it at " + node.Item1 + "," + node.Item2);
                //Console.WriteLine((node.Item1 * 4000000 + node.Item2));
            }
        });
    }
    public void Part2NoCollection()
    {
        Dictionary<(long, long), long> Sensors = new();
        HashSet<(long, long)> PossibleBeacons = new();
        foreach (string Line in InputStrings)
        {
            string[] Sensor = Line.Split(new char[] { ' ', '=', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            long SensorX = int.Parse(Sensor[3]);
            long SensorY = int.Parse(Sensor[5]);
            long BeaconX = int.Parse(Sensor[11]);
            long BeaconY = int.Parse(Sensor[13]);
            long Distance = Math.Abs(SensorX - BeaconX) + Math.Abs(SensorY - BeaconY);
            
            Sensors.Add((SensorX, SensorY), Distance);
        }
        foreach ((long, long) TopSensor in Sensors.Keys)
        {
            long SensorX = TopSensor.Item1;
            long SensorY = TopSensor.Item2;
            long Distance = Sensors[TopSensor];
            long TopY = SensorY - Distance - 1;
            long BottomY = SensorY + Distance + 1;
            long RightX = SensorY + Distance - 1;
            long LeftX = SensorY - Distance + 1;
            //go down and to the right
            //node is SensorX+i,TopY+i
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX + i) <= max && (SensorX + i) >= 0)
                {
                    if ((TopY + i) <= max && (TopY + i) >= 0)
                    {
                        bool Found = false;
                        foreach ((long, long) Sensor in Sensors.Keys)
                        {
                            if (Math.Abs(Sensor.Item1 - SensorX + i) + Math.Abs(Sensor.Item2 - SensorY+i) <= Sensors[Sensor])
                            {
                                Found = false;
                                break;
                            }
                            else
                            {
                                Found = true;
                            }

                        }
                        if (Found)
                        {
                              Console.WriteLine("Found it at " + SensorX + "," + SensorY);
                            Console.WriteLine((SensorX * 4000000 + SensorY));
                        }
                    }
                }
            }
            //go down and to the left
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX - i) <= max && (SensorX - i) >= 0)
                {
                    if ((TopY + i) <= max && (TopY + i) >= 0)
                    {
                        bool Found = false;
                        foreach ((long, long) Sensor in Sensors.Keys)
                        {
                            if (Math.Abs(Sensor.Item1 - node.Item1) + Math.Abs(Sensor.Item2 - node.Item2) <= Sensors[Sensor])
                            {
                                Found = false;
                                break;
                            }
                            else
                            {
                                Found = true;
                            }

                        }
                        if (Found)
                        {
                            //  Console.WriteLine("Found it at " + node.Item1 + "," + node.Item2);
                            //Console.WriteLine((node.Item1 * 4000000 + node.Item2));
                        }
                    }
                }
            }
            //go up and to the right
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX + i) <= max && (SensorX + i) >= 0)
                {
                    if ((BottomY - i) <= max && (BottomY - i) >= 0)
                    {
                        bool Found = false;
                        foreach ((long, long) Sensor in Sensors.Keys)
                        {
                            if (Math.Abs(Sensor.Item1 - node.Item1) + Math.Abs(Sensor.Item2 - node.Item2) <= Sensors[Sensor])
                            {
                                Found = false;
                                break;
                            }
                            else
                            {
                                Found = true;
                            }

                        }
                        if (Found)
                        {
                            //  Console.WriteLine("Found it at " + node.Item1 + "," + node.Item2);
                            //Console.WriteLine((node.Item1 * 4000000 + node.Item2));
                        }
                    }
                }
            }
            //go up and to the left
            for (long i = 0; i < Distance; i++)
            {
                if ((SensorX - i) <= max && (SensorX - i) >= 0)
                {
                    if ((BottomY - i) <= max && (BottomY - i) >= 0)
                    {
                        bool Found = false;
                        foreach ((long, long) Sensor in Sensors.Keys)
                        {
                            if (Math.Abs(Sensor.Item1 - node.Item1) + Math.Abs(Sensor.Item2 - node.Item2) <= Sensors[Sensor])
                            {
                                Found = false;
                                break;
                            }
                            else
                            {
                                Found = true;
                            }

                        }
                        if (Found)
                        {
                            Console.WriteLine("Found it at " + node.Item1 + "," + node.Item2);
                            Console.WriteLine((node.Item1 * 4000000 + node.Item2));
                        }
                    }
                }
            }
        }


        HashSet<(long, long)> NoBeacons = new();
        /**foreach ((long, long) Sensor in Sensors.Keys)
        {
            long Range = Sensors[Sensor];
            (long, long) CurrentNode = Sensor;
            PriorityQueue<(long, long), long> NodesToTest = new();
            NodesToTest.Enqueue(CurrentNode, 0);
            Dictionary<(long, long), long> CostSoFar = new();
            CostSoFar.Add(CurrentNode, 0);
            long NewCost = 0;
            long RangeY = Math.Abs(CurrentNode.Item2 - LineOfInterest);
            long RangeX = Range - RangeY;
            if (Math.Abs(CurrentNode.Item2 - LineOfInterest) > Range)
            {
                continue;
            }
            long CurrX = (Sensor.Item1 - RangeX);
            CurrX = (CurrX > 0) ? CurrX : 0;
            long MaxX = Sensor.Item1 + RangeX;
            MaxX = Math.Min(MaxX, 4000000);
            do
            {
                NoBeacons.Add((CurrX, LineOfInterest));
                //     Console.WriteLine("Adding " + CurrX + "," + LineOfInterest);
            } while (CurrX++ < MaxX);
        }**/

        Parallel.ForEach(PossibleBeacons, node =>
        {
            bool Found = false;
            foreach ((long, long) Sensor in Sensors.Keys)
            {
                if (Math.Abs(Sensor.Item1 - node.Item1) + Math.Abs(Sensor.Item2 - node.Item2) <= Sensors[Sensor])
                {
                    Found = false;
                    break;
                }
                else
                {
                    Found = true;
                }

            }
            if (Found)
            {
                //  Console.WriteLine("Found it at " + node.Item1 + "," + node.Item2);
                //Console.WriteLine((node.Item1 * 4000000 + node.Item2));
            }
        });
    }

}
