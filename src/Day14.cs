using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay14
{
    public readonly static string InputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14Cai.txt";
    public readonly static string SamplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14sample.txt";
    string[] InputLines = File.ReadAllLines(InputPath);
    byte[] Input = File.ReadAllBytes("C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day14Cai.txt");
    public const byte COMMA = 0x2C;
    public const byte NEWLINE = 0x0A;
    public const byte SPACE = 0x20;
    public static ReadOnlySpan<sbyte> MOVE_X => new sbyte[8] { 0, 0, -1, -1, 0, 0, 1, 0 };
    public static ReadOnlySpan<sbyte> MOVE_Y => new sbyte[8] { 1, 1, 1, 1, 1, 1, 1, 0 };
    public (int, int)[,] SandRestingPlaces = new (int, int)[28000,5];

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
               // Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
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
        int Floor = 0;
        byte[][] Cave = new byte[180][];
        for (int i = 0; i < Cave.Length; i++)
        {
            Cave[i] = new byte[1000];
        }

        

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
                        if (Node1Y > Floor)
                        {
                            Floor = Node1Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        if (Node2Y > Floor)
                        {
                            Floor = Node2Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    if (Node1Y > Floor)
                    {
                        Floor = Node1Y;
                    }
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
        Floor += 3;
        Array.Fill<byte>(Cave[Floor - 1], 35);
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
                SandRestingPlaces[SandCount, 1] = (NewSandX, NewSandY);
               // Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
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
        int Floor = 0;
        bool[][] Cave = new bool[180][];
        for (int i = 0; i < Cave.Length; i++)
        {
            Cave[i] = new bool[1000];
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
                        if (Node1Y > Floor)
                        {
                            Floor = Node1Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = true;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        if (Node2Y > Floor)
                        {
                            Floor = Node2Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = true;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    if (Node1Y > Floor)
                    {
                        Floor = Node1Y;
                    }
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
        Floor += 3;
        Debug.Assert(Floor == 172);
        Array.Fill<bool>(Cave[Floor - 1], true);
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
                //Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
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
        int Floor = 0;
        byte[,] Cave = new byte[180, 1000];

        
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
                        if (Node1Y > Floor)
                        {
                            Floor = Node1Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m, Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        if (Node2Y > Floor)
                        {
                            Floor = Node2Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m, Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    if (Node1Y > Floor)
                    {
                        Floor = Node1Y;
                    }
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
        Floor += 3;
        Debug.Assert(Floor == 172);
        for (int i = 0; i < 1000; i++)
        {
            Cave[Floor - 1, i] = 35;
        }
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
               // Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
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
        int Floor = 0;
        byte[][] Cave = new byte[172][];
        for (int i = 0; i < Cave.Length; i++)
        {
            Cave[i] = new byte[1000];
        }

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
                        if (Node1Y > Floor)
                        {
                            Floor = Node1Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node2Y + m][Node1X] = 35;
                        }
                    }
                    else if (Node2Y > Node1Y)
                    {
                        if(Node2Y>Floor)
                        {
                            Floor = Node2Y;
                        }
                        for (int m = 0; m <= Diff; m++)
                        {
                            Cave[Node1Y + m][Node1X] = 35;
                        }
                    }
                }
                else if (Node1Y == Node2Y)
                {
                    int Diff = Math.Abs(Node1X - Node2X);
                    if(Node1Y>Floor)
                    {
                        Floor = Node1Y;
                    }
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
        Floor += 3;
        

        Array.Fill<byte>(Cave[Floor - 1], 35);
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
                SandRestingPlaces[SandCount, 2] = (NewSandX, NewSandY);
                if (SandCount%500==0)
                Debug.WriteLine("Piece of sand has come to rest at " + NewSandX + "," + NewSandY);
                continue;

            }
        } while (Path.Count > 0);
        Debug.WriteLine("We have reached the abyss after " + SandCount + " pieces of sand!");
        Debug.Assert(SandCount == 27551);
        Debug.Write(SandCount);
    }
    
    //This method is broken. Don't use it.
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
    [Benchmark]
    public void Part2CaiMethod()
    {
        
        Point[] WallPoints = new Point[(Input.Length / 9) + 5];
        int WallPointCount = 0;

        int MinX = 500, MaxX = 500;
        int MaxY = 0;

        int Index = 0;
        do
        {
            ushort X = (ushort)(((Input[Index++] & 0xF) * 100) + ((Input[Index++] & 0xF) * 10) + (Input[Index++] & 0xF));
            Debug.Assert(Input[Index] == COMMA, "X coordinate wasn't 3 digits long");
            ++Index;
//#if DEBUG
            // This one is needed for the test input due to small Y coordinates
//            ushort Y = (ushort)(Input[Index++] & 0xF);
 //           if (((Input[Index] >> 4) & 1) == 1) { Y = (ushort)((Y * 10) + (Input[Index++] & 0xF)); }
  //          if (((Input[Index] >> 4) & 1) == 1) { Y = (ushort)((Y * 10) + (Input[Index++] & 0xF)); }
   //         Debug.Assert(Input[Index] == SPACE || Input[Index] == NEWLINE, "Y coordinate wasn't 1, 2 or 3 digits long");

            // This one is faster, and can be used on the full input due to larger Y coordinates
            ushort Y = (ushort)(((Input[Index++] & 0xF) * 10) + (Input[Index++] & 0xF));
            if (((Input[Index] >> 4) & 1) == 1) { Y = (ushort)((Y * 10) + (Input[Index++] & 0xF)); }
            Debug.Assert(Input[Index] == SPACE || Input[Index] == NEWLINE, "Y coordinate wasn't 2 or 3 digits long");
//#endif
            WallPoints[WallPointCount].X = X;
            WallPoints[WallPointCount].Y = Y;
            ++WallPointCount;

            MinX = Math.Min(MinX, X);
            MaxX = Math.Max(MaxX, X);
            MaxY = Math.Max(MaxY, Y);

            if (Input[Index] == NEWLINE)
            {
                ++Index;
                ++WallPointCount; // Leave 1 blank as marker
            }
            else { Index += 4; }
        }
        while (Index < Input.Length-6);

        // Add the floor for part 2
        const ushort FLOOR_X_RANGE = 180; // This may need to be tweaked for different inputs, there's no good way to auto-scale using my implementation.
        MaxX = Math.Max(MaxX, 500 + FLOOR_X_RANGE);
        MinX = Math.Min(MinX, 500 - FLOOR_X_RANGE);
        MaxY += 2;
        WallPoints[WallPointCount].X = 500 - FLOOR_X_RANGE;
        WallPoints[WallPointCount].Y = (ushort)MaxY;
        ++WallPointCount;
        WallPoints[WallPointCount].X = 500 + FLOOR_X_RANGE;
        WallPoints[WallPointCount].Y = (ushort)MaxY;
        ++WallPointCount;

        // Create the cave layout
        int SizeX = MaxX - MinX + 3;
        int SizeY = MaxY + 1;
        byte[] CaveState = new byte[SizeX * SizeY];
        int SandOriginX = 501 - MinX;
        int XOffset = MinX - 1;

        int ToIndex(int x, int y) => (y * SizeX) + x;

        // TODO: Could pull out point X offset operations and vectorize
        for (int i = 1; i < WallPointCount; i++)
        {
            if (WallPoints[i].X == 0) // No more segments in this line
            {
                i++;
                continue;
            }

            if (WallPoints[i].X == WallPoints[i - 1].X) // Moving in Y
            {
                int XLocation = WallPoints[i].X - XOffset;
                int YLocation = WallPoints[i - 1].Y;
                int YTarget = WallPoints[i].Y;
                int YIncrement = ((YTarget - YLocation) >> 31) | 1; // 1 or -1
                do
                {
                    CaveState[ToIndex(XLocation - 1, YLocation)] |= 0b001;
                    CaveState[ToIndex(XLocation, YLocation)] |= 0b010;
                    CaveState[ToIndex(XLocation + 1, YLocation)] |= 0b100;
                    YLocation += YIncrement;
                }
                while (YLocation != YTarget);
                CaveState[ToIndex(XLocation - 1, YLocation)] |= 0b001;
                CaveState[ToIndex(XLocation, YLocation)] |= 0b010;
                CaveState[ToIndex(XLocation + 1, YLocation)] |= 0b100;
            }
            else // Moving in X
            {
                int YLocation = WallPoints[i].Y;
                int XLocation = WallPoints[i - 1].X - XOffset;
                int XTarget = WallPoints[i].X - XOffset;
                int XIncrement = ((XTarget - XLocation) >> 31) | 1; // 1 or -1
                do // TODO: This could be optimized to 1 operation per loop + edges
                {
                    CaveState[ToIndex(XLocation - 1, YLocation)] |= 0b001;
                    CaveState[ToIndex(XLocation, YLocation)] |= 0b010;
                    CaveState[ToIndex(XLocation + 1, YLocation)] |= 0b100;
                    XLocation += XIncrement;
                }
                while (XLocation != XTarget);
                CaveState[ToIndex(XLocation - 1, YLocation)] |= 0b001;
                CaveState[ToIndex(XLocation, YLocation)] |= 0b010;
                CaveState[ToIndex(XLocation + 1, YLocation)] |= 0b100;
            }
        }

#if DEBUG
        byte[] CaveWalls = new byte[CaveState.Length];
        Buffer.BlockCopy(CaveState, 0, CaveWalls, 0, CaveState.Length);
        byte PrevState = CaveState[ToIndex(SandOriginX, 0)];
        CaveState[ToIndex(SandOriginX, 0)] |= 0b010; // Show the sand origin as a wall
        for (int X = 0; X < SizeX; X += 5)
        {
            Console.Write("v{0,-4}", X + XOffset);
        }
        Console.WriteLine();
        for (int Y = 0; Y < SizeY; Y++)
        {
            for (int X = 0; X < SizeX; X++)
            {
                Console.Write((CaveState[ToIndex(X, Y)] & 0b010) == 0 ? '.' : '#');
            }
            Console.WriteLine();
        }
        CaveState[ToIndex(SandOriginX, 0)] = PrevState; // Undo sand origin solidification
#endif

        // Find the initial Y drop distance
        // This is used to skip stepping through the initial vertical drop for every block of sand
        int YDrop = 1;
        while ((CaveState[ToIndex(SandOriginX, YDrop)] & 0b010) == 0) { ++YDrop; }
        --YDrop; // Un-offset

        // Drop the sand
        int SandDropped = 0;
        int SandYLocation;
        do
        {
            SandDropped++;
            int SandXLocation = SandOriginX;
            SandYLocation = YDrop;
            sbyte CurrentMovementY, CurrentMovementX;
            int SandXMoved = 0;
            do
            {
                byte BlocksBelow = CaveState[ToIndex(SandXLocation, SandYLocation + 1)];
                CurrentMovementX = MOVE_X[BlocksBelow];
                CurrentMovementY = MOVE_Y[BlocksBelow];

                SandXMoved += CurrentMovementX & 1;
                SandXLocation += CurrentMovementX;
                SandYLocation += CurrentMovementY;
            }
            while (CurrentMovementY == 1 && SandYLocation != MaxY);

            // Settled
            if (SandXMoved == 0) { --YDrop; }
            CaveState[ToIndex(SandXLocation - 1, SandYLocation)] |= 0b001;
            CaveState[ToIndex(SandXLocation, SandYLocation)] |= 0b010;
            CaveState[ToIndex(SandXLocation + 1, SandYLocation)] |= 0b100;
            /**
#if DEBUG
                        if (SandDropped % 5000 == 0 || SandYLocation == MaxY || SandYLocation == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Sand dropped: {SandDropped}");
                            for (int X = 0; X < SizeX; X += 5)
                            {
                                Console.Write("v{0,-4}", X + XOffset);
                            }
                            Console.WriteLine();
                            for (int Y = 0; Y < SizeY; Y++)
                            {
                                for (int X = 0; X < SizeX; X++)
                                {
                                    char Here = '.';
                                    if ((CaveState[ToIndex(X, Y)] & 0b010) != 0) { Here = 'o'; }
                                    if ((CaveWalls[ToIndex(X, Y)] & 0b010) != 0) { Here = '#'; }
                                    Console.Write(Here);
                                }
                                Console.WriteLine();
                            }
                        }
#endif**/
            SandRestingPlaces[SandDropped, 3] = (SandXLocation+XOffset, SandYLocation);
        }
        while (SandYLocation != MaxY && SandYLocation != 0);

       // Console.Write(SandDropped);
    
        }

}
