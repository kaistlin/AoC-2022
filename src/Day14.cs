using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay14
{
    public readonly static string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14input.txt";
    public readonly static string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14sample.txt";
    string[] InputLines = File.ReadAllLines(SamplePath);
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
        Debug.WriteLine("Walls are added!");
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
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (NewSandY <= Abyss);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
    }
    [Benchmark]
    public void Part2()
    {
        HashSet<(int, int)> Walls = new();
        int Floor = 0;
        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
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
        Floor += 1; //deepest place sand can be
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
        } while (!Walls.Contains((500, 0)));
#if DEBUG
        Console.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);

#else
       // Console.Write(SandCount);
        
#endif
    }
    [Benchmark]
    public void Part2ArrayOfByteArrays()
    {
        int Floor = 172;
        byte[][] Cave = new byte[Floor][];
        for (int i = 0; i < Floor; i++)
        {
            Cave[i] = new byte[1000];
        }

        Array.Fill<byte>(Cave[Floor - 1], 35);

        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node2X + m] = 35;
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node1X + m] = 35;
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Debug.WriteLine("Diagonal walls exist!");
                    break;

                }
            }
        }
        Debug.WriteLine("Walls are added!");
        int SandCount = 0;
        int NewSandX = 500;
        int NewSandY = 0;
        do
        {

            if (Cave[NewSandY + 1][NewSandX] == 0)
            {
                NewSandY++;
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX - 1] == 0)
            {
                NewSandX--;
                NewSandY++;
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX + 1] == 0)
            {
                NewSandX++;
                NewSandY++;
                continue;
            }
            else
            {
                SandCount++;
                Cave[NewSandY][NewSandX] = 111;
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (Cave[0][500] == 0);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
    [Benchmark]
    public void Part2ArrayOfBoolArrays()
    {
        int Floor = 172;
        bool[][] Cave = new bool[Floor][];
        for (int i = 0; i < Floor; i++)
        {
            Cave[i] = new bool[1000];
        }

        Array.Fill<bool>(Cave[Floor - 1], true);

        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = true;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = true;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node2X + m] = true;
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node1X + m] = true;
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Debug.WriteLine("Diagonal walls exist!");
                    break;

                }
            }
        }
        Debug.WriteLine("Walls are added!");
        int SandCount = 0;
        int NewSandX = 500;
        int NewSandY = 0;
        do
        {

            if (!Cave[NewSandY + 1][NewSandX])
            {
                NewSandY++;
                continue;
            }
            else if (!Cave[NewSandY + 1][NewSandX - 1])
            {
                NewSandX--;
                NewSandY++;
                continue;
            }
            else if (!Cave[NewSandY + 1][NewSandX + 1])
            {
                NewSandX++;
                NewSandY++;
                continue;
            }
            else
            {
                SandCount++;
                Cave[NewSandY][NewSandX] = true;
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (!Cave[0][500]);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
    [Benchmark]
    public void Part2ByteArray2D()
    {
        int Floor = 172;
        byte[,] Cave = new byte[Floor, 1000];

        for (int i = 0; i < 1000; i++)
        {
            Cave[Floor - 1, i] = 35;
        }
        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m, Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m, Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y, Node2X + m] = 35;
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y, Node1X + m] = 35;
                        }
                    }
                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Debug.WriteLine("Diagonal walls exist!");
                    break;
                }
            }
        }
        Debug.WriteLine("Walls are added!");
        int SandCount = 0;
        int NewSandX = 500;
        int NewSandY = 0;
        do
        {

            if (Cave[NewSandY + 1, NewSandX] == 0)
            {
                NewSandY++;
                continue;
            }
            else if (Cave[NewSandY + 1, NewSandX - 1] == 0)
            {
                NewSandX--;
                NewSandY++;
                continue;
            }
            else if (Cave[NewSandY + 1, NewSandX + 1] == 0)
            {
                NewSandX++;
                NewSandY++;
                continue;
            }
            else
            {
                SandCount++;
                Cave[NewSandY, NewSandX] = 111;
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                NewSandX = 500;
                NewSandY = 0;
            }
        } while (Cave[0, 500] == 0);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
    [Benchmark]
    public void Part2ByteArraysWithStack()
    {
        int Floor = 172;
        byte[][] Cave = new byte[Floor][];
        for (int i = 0; i < Floor; i++)
        {
            Cave[i] = new byte[1000];
        }

        Array.Fill<byte>(Cave[Floor - 1], 35);

        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node2X + m] = 35;
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node1X + m] = 35;
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Debug.WriteLine("Diagonal walls exist!");
                    break;

                }
            }
        }
        Debug.WriteLine("Walls are added!");
        int SandCount = 0;
        int NewSandX = 500;
        int NewSandY = 0;
        (int, int) NewSand = (0, 500);
        Stack<(int, int)> Path = new Stack<(int, int)>();
        Path.Push((NewSand));
        do
        {
            NewSand = Path.Pop();
            NewSandY = NewSand.Item1;
            NewSandX = NewSand.Item2;

            if (Cave[NewSandY + 1][NewSandX] == 0)
            {
                Path.Push((NewSand));
                NewSand.Item1++;
                Path.Push((NewSand));
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX - 1] == 0)
            {
                Path.Push((NewSand));
                NewSand.Item1++;
                NewSand.Item2--;
                Path.Push((NewSand));
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX + 1] == 0)
            {
                Path.Push((NewSand));
                NewSand.Item1++;
                NewSand.Item2++;
                Path.Push((NewSand));
                continue;
            }
            else
            {
                SandCount++;
                Cave[NewSandY][NewSandX] = 111;
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                continue;

            }
        } while (Path.Count > 0);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
    public void Part2ByteArraysReversal()
    {
        int Floor = 12;
        byte[][] Cave = new byte[Floor][];
        for (int i = 0; i < Floor; i++)
        {
            Cave[i] = new byte[1000];
        }

        Array.Fill<byte>(Cave[Floor - 1], 35);

        foreach (string line in InputLines)
        {
            int[] Wall = line.Split(new char[] { ' ', '-', ',', '>' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            for (int i = 0; i < Wall.Length - 2; i += 2)
            {
                int Node1X = (Wall[i]);
                int Node1Y = (Wall[i + 1]);
                int Node2X = (Wall[i + 2]);
                int Node2Y = (Wall[i + 3]);
                if (Node1X == Node2X)
                {

                    int Diff = Math.Abs(Node1Y - Node2Y);
                    if (Node1Y > Node2Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if (Node1X > Node2X)
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node2X + m] = 35;
                        }
                    }
                    else
                    {
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y][Node1X + m] = 35;
                        }
                    }

                }
                else if (Wall[i] != Wall[i + 1] && Wall[i + 2] != Wall[i + 3])
                {
                    Debug.WriteLine("Diagonal walls exist!");
                    break;

                }
            }
        }
        Debug.WriteLine("Walls are added!");
        int SandCount = 0;
        int NewSandX = 500;
        int NewSandY = 0;
        (int, int) NewSand = (0, 500);
        (int, int) OldSand;
        //Stack<(int, int)> Path = new Stack<(int, int)>();
        do
        {
            NewSandY = NewSand.Item1;
            NewSandX = NewSand.Item2;

            if (Cave[NewSandY + 1][NewSandX] == 0)
            {
                OldSand = NewSand;
                NewSand.Item1++;
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX - 1] == 0)
            {
                OldSand = NewSand;
                NewSand.Item1++;
                NewSand.Item2--;
                continue;
            }
            else if (Cave[NewSandY + 1][NewSandX + 1] == 0)
            {
                OldSand = NewSand;
                NewSand.Item1++;
                NewSand.Item2++;
                continue;
            }
            else
            {
                if (Cave[NewSandY][NewSandX] == 111)
                {
                    NewSand.Item1--;
                    if (Cave[NewSandY][NewSandX+1] == 111)
                    {
                        NewSand.Item2++;
                    }
                    else if (Cave[NewSandY][NewSandX-1] == 111)
                    {
                        NewSand.Item2--;
                    }

                    continue;
                }
                //else { NewSand = OldSand; }
                SandCount++;
                Cave[NewSandY][NewSandX] = 111;
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                continue;

            }
        } while (Cave[0][500]==0);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
}
