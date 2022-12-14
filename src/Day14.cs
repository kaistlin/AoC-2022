using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay14
{
    public readonly static string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14input.txt";
    public readonly static string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14sample.txt";
    string[] InputLines = File.ReadAllLines(InputPath);
    [Benchmark]
    public void Part1()
    {
        HashSet<(int, int)> Walls = new();
        int Abyss = 0;
        foreach (string line in InputLines)
        {
            string[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = int.Parse(Wall[i]);
                int Node1Y = int.Parse(Wall[i + 1]);
                int Node2X = int.Parse(Wall[i + 2]);
                int Node2Y = int.Parse(Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X, Node2Y + m));
                        }
                        if (Node1Y > Abyss)
                        {
                            Abyss = Node1Y;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X, Node1Y + m));
                        }
                        if (Node2Y > Abyss)
                        {
                            Abyss = Node2Y;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    if (Node2Y > Abyss)
                    {
                        Abyss = Node1Y;
                    }
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node2X + m, Node1Y));
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X + m, Node1Y));
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Console.WriteLine("Diagonal walls exist!");
                    break;
                }
            }
        }
#if DEBUG
        Console.WriteLine("Walls are added!");
#else
#endif
        int SandCount = 0;
        Abyss++;
        int NewSandX = 500;
        int NewSandY = 0;
        do
        {

            if (!Walls.Contains((NewSandX, NewSandY + 1)))
            {
                NewSandY++;
                continue;
            }
            else if (!Walls.Contains((NewSandX - 1, NewSandY + 1)))
            {
                NewSandX--;
                NewSandY++;
                continue;
            }
            else if (!Walls.Contains((NewSandX + 1, NewSandY + 1)))
            {
                NewSandX++;
                NewSandY++;
                continue;
            }
            else
            {
                SandCount++;
                Walls.Add((NewSandX, NewSandY));
#if DEBUG
                Console.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
#else
#endif
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (NewSandY <= Abyss);
#if DEBUG
        Console.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
#else
        Console.Write(SandCount);
#endif
    }
    [Benchmark]
    public void Part2()
    {
        HashSet<(int, int)> Walls = new();
        int Floor = 0;
        foreach (string line in InputLines)
        {
            string[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = int.Parse(Wall[i]);
                int Node1Y = int.Parse(Wall[i + 1]);
                int Node2X = int.Parse(Wall[i + 2]);
                int Node2Y = int.Parse(Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X, Node2Y + m));
                        }
                        if (Node1Y > Floor)
                        {
                            Floor = Node1Y;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X, Node1Y + m));
                        }
                        if (Node2Y > Floor)
                        {
                            Floor = Node2Y;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    if (Node2Y > Floor)
                    {
                        Floor = Node1Y;
                    }
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node2X + m, Node1Y));
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Walls.Add((Node1X + m, Node1Y));
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Console.WriteLine("Diagonal walls exist!");
                    break;
                    
                }
            }
        }
#if DEBUG
        Console.WriteLine("Walls are added!");
#else
#endif
        int SandCount = 0;
        Floor+=1; //deepest place sand can be
        int NewSandX = 500;
        int NewSandY = 0;
        do
        {

            if (!Walls.Contains((NewSandX, NewSandY + 1)) && NewSandY < Floor)
            {
                NewSandY++;
                continue;
            }
            else if (!Walls.Contains((NewSandX - 1, NewSandY + 1)) && NewSandY < Floor)
            {
                NewSandX--;
                NewSandY++;
                continue;
            }
            else if (!Walls.Contains((NewSandX + 1, NewSandY + 1)) && NewSandY < Floor)
            {
                NewSandX++;
                NewSandY++;
                continue;
            }
            else
            {
                SandCount++;
                Walls.Add((NewSandX, NewSandY));
#if DEBUG
                Console.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
#else
#endif
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (!Walls.Contains((500,0)));
#if DEBUG
        Console.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
#else
        Console.Write(SandCount);
#endif
    }
}
