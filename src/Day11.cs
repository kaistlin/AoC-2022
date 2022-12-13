using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src;

[MemoryDiagnoser]
public class AoCDay11
{
    public static readonly string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day11input.txt";
    public static readonly string samplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day11sample.txt";
    public static readonly string[] monkeyInput = File.ReadAllLines(inputPath);
    class Monkey
    {
        public int Inspected;
        public char Operation;
        public int Operand;
        public int Divisor;
        public int IfTrue;
        public int IfFalse;
        public List<ulong> items;

        
      /**  public Monkey(string name, int[] stats)
        {
            this.name = name;
            this.stats = stats;
        }
        public Monkey() { }**/
    }
  /**  [Benchmark]
    public void part1()
    {

        string[] line;
        Monkey[] Barrel = new Monkey[monkeyInput.Length / 7 + 1];
        //Make-A-Monkey
        for (int i = 0; i < monkeyInput.Length; i += 7)
        {
            Monkey NewMonkey = new();
            List<ulong> MonkeyWrenches = new();
            line = monkeyInput[i + 1].Split(new char[] { ',', ' ' });
            for (int j = 2; j <= line.Length / 2; j++)
            {
                MonkeyWrenches.Add(ulong.Parse(line[j * 2]));
            }
            line = monkeyInput[i + 2].Split(" ");
            NewMonkey.Operation = char.Parse(line[6]);
            if (!int.TryParse(line[7], out NewMonkey.Operand)) { NewMonkey.Operation = '^'; NewMonkey.Operand = 0; }
            line = monkeyInput[i + 3].Split(" ");
            NewMonkey.Divisor = int.Parse(line[5]);
            line = monkeyInput[i + 4].Split(" ");
            NewMonkey.IfTrue = int.Parse(line[9]);
            line = monkeyInput[i + 5].Split(" ");
            NewMonkey.IfFalse = int.Parse(line[9]);
            NewMonkey.items = MonkeyWrenches;
            Barrel[i / 7] = NewMonkey;
        }
        for (int Round = 1; Round <= 20; Round++)
        {

            for (int j = 0; j < Barrel.Length; j++)
            {
                Monkey Monkey = Barrel[j];
                foreach (ulong item in Monkey.items)
                {
                    Monkey.Inspected++;
                    
                    ulong newWorry = GetResult(Monkey.Operation, item, Monkey.Operand) / 3;
                    if (newWorry % (ulong)Monkey.Divisor == 0)
                    {
                        Barrel[Monkey.IfTrue].items.Add(newWorry);

                    }
                    else
                    {
                        Barrel[Monkey.IfFalse].items.Add(newWorry);
                    }
                }
                Monkey.items.Clear();
            }
        }
        int LowerMax = 0;
        int CurrMax = 0;
        foreach (Monkey Monkey in Barrel)
        {
            if (Monkey.Inspected > CurrMax && Monkey.Inspected > LowerMax)
            {
                LowerMax = CurrMax;
                CurrMax = Monkey.Inspected;
            }
            else if (Monkey.Inspected > LowerMax) { LowerMax = Monkey.Inspected; }
        }
        ulong answer = ((ulong)((long)LowerMax * (long)CurrMax));
    }

   
    
    [Benchmark]
    public void part2()
    {

        string[] line;
        Monkey[] Barrel = new Monkey[monkeyInput.Length / 7 + 1];
        //Make-A-Monkey
        for (int i = 0; i < monkeyInput.Length; i += 7)
        {
            Monkey NewMonkey = new();
            List<ulong> MonkeyWrenches = new();
            line = monkeyInput[i + 1].Split(new char[] { ',', ' ' });
            for (int j = 2; j <= line.Length / 2; j++)
            {
                MonkeyWrenches.Add(ulong.Parse(line[j * 2]));
            }
            line = monkeyInput[i + 2].Split(" ");
            NewMonkey.Operation = char.Parse(line[6]);
            if (!int.TryParse(line[7], out NewMonkey.Operand)) { NewMonkey.Operation = '^'; NewMonkey.Operand = 0; }
            line = monkeyInput[i + 3].Split(" ");
            NewMonkey.Divisor = int.Parse(line[5]);
            line = monkeyInput[i + 4].Split(" ");
            NewMonkey.IfTrue = int.Parse(line[9]);
            line = monkeyInput[i + 5].Split(" ");
            NewMonkey.IfFalse = int.Parse(line[9]);
            NewMonkey.items = MonkeyWrenches;
            Barrel[i / 7] = NewMonkey;
        }
        ulong LCM = 1;//Least Common Divisor
        for (int i = 0; i < Barrel.Length; i++)
        {
            LCM *= (ulong)Barrel[i].Divisor;
        }

        for (int Round = 1; Round <= 10000; Round++)
        {

            for (int j = 0; j < Barrel.Length; j++)
            {
                Monkey Monkey = Barrel[j];
                foreach (ulong item in Monkey.items)
                {
                    ulong currentWorry = item % LCM;
                    Monkey.Inspected++;
                    ulong newWorry = GetResult(Monkey.Operation, currentWorry, Monkey.Operand);
                    if (newWorry % (ulong)Monkey.Divisor == 0)
                    {
                        Barrel[Monkey.IfTrue].items.Add(newWorry);

                    }
                    else
                    {
                        Barrel[Monkey.IfFalse].items.Add(newWorry);
                    }
                }
                Monkey.items.Clear();
            }
        }
        int LowerMax=0;
        int CurrMax = 0;
        foreach (Monkey Monkey in Barrel)
        {
            if (Monkey.Inspected > CurrMax && Monkey.Inspected>LowerMax)
            {
                LowerMax = CurrMax;
                CurrMax = Monkey.Inspected;
            }
            else if(Monkey.Inspected > LowerMax) { LowerMax = Monkey.Inspected; }
        }
        ulong answer = ((ulong)((long)LowerMax * (long)CurrMax));
        Debug.Assert(answer == 19754471646);
    }**/
    [Benchmark]
    public void Part2AttemptedOptimize()
    {

        string[] line;
        Monkey[] Barrel = new Monkey[monkeyInput.Length / 7 + 1];
        //Make-A-Monkey
        for (int i = 0; i < monkeyInput.Length; i += 7)
        {
            Monkey NewMonkey = new();
            List<ulong> MonkeyWrenches = new();
            line = monkeyInput[i + 1].Split(new char[] { ',', ' ' });
            for (int j = 2; j <= line.Length / 2; j++)
            {
                MonkeyWrenches.Add(uint.Parse(line[j * 2]));
            }
            line = monkeyInput[i + 2].Split(" ");
            NewMonkey.Operation = char.Parse(line[6]);
            if (!int.TryParse(line[7], out NewMonkey.Operand)) { NewMonkey.Operation = '^'; NewMonkey.Operand = 0; }
            line = monkeyInput[i + 3].Split(" ");
            NewMonkey.Divisor = int.Parse(line[5]);
            line = monkeyInput[i + 4].Split(" ");
            NewMonkey.IfTrue = int.Parse(line[9]);
            line = monkeyInput[i + 5].Split(" ");
            NewMonkey.IfFalse = int.Parse(line[9]);
            NewMonkey.items = MonkeyWrenches;
            Barrel[i / 7] = NewMonkey;
        }
        uint LCM = 1;//Least Common Divisor
        for (int i = 0; i < Barrel.Length; i++)
        {
            LCM *= (uint)Barrel[i].Divisor;
        }
        Dictionary<int[],int> memory = new ();
        for (uint Round = 1; Round <= 10000000; Round++)
        {
            int[] inventory = new int[8];
            for(int i = 0; i < inventory.Length; i++)
            {
                inventory[i] = Barrel[i].items.Count();
            }
            for (int j = 0; j < Barrel.Length; j++)
            {
                SortedSet<ulong> items = new SortedSet<ulong>();
                Monkey Monkey = Barrel[j];
                foreach (ulong item in Monkey.items)
                {
                    items.Add(item);
                    ulong currentWorry = item % (ulong)LCM;
                    Monkey.Inspected++;
                    ulong newWorry = GetResult(Monkey.Operation, currentWorry, Monkey.Operand);
                    if (newWorry % (uint)Monkey.Divisor == 0)
                    {
                        Barrel[Monkey.IfTrue].items.Add(newWorry);

                    }
                    else
                    {
                        Barrel[Monkey.IfFalse].items.Add(newWorry);
                    }
                }
                Monkey.items.Clear();
               // inventory[j] = items.Count;
                
            }
            Debug.Assert(inventory.Sum() == 36);
            if (Round % 1000000 == 0)
                Console.WriteLine("Up to " + Round + " rounds");
            if(!memory.TryAdd(inventory,(int)Round))
            {
                Console.WriteLine("Dupe found");
            }
        }
        ulong LowerMax = 0;
        ulong CurrMax = 0;
        foreach (Monkey Monkey in Barrel)
        {
            if ((ulong)Monkey.Inspected > CurrMax && (ulong)Monkey.Inspected > LowerMax)
            {
                LowerMax = CurrMax;
                CurrMax = (ulong)Monkey.Inspected;
            }
            else if ((ulong)Monkey.Inspected > LowerMax) { LowerMax = (ulong)Monkey.Inspected; }
        }
        ulong answer = LowerMax * CurrMax;
        Debug.Assert(answer == 19754471646);
    }
    private ulong GetResult(char operation, ulong item, int operand)
    {
        return operation switch
        {
            '+' => (item + (ulong)operand),
            '*' => (item * (ulong)operand),
            '^' => (item * item),
            _ => 0

        };
    }
    static void findCombinationsUtil(int[] arr, int index,
                                 int num, int reducedNum)
    {
        // Base condition
        if (reducedNum < 0)
            return;

        // If combination is
        // found, print it
        if (reducedNum == 0)
        {
            for (int i = 0; i < index; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
            return;
        }

        // Find the previous number
        // stored in arr[]. It helps
        // in maintaining increasing
        // order
        int prev = (index == 0) ?
                              1 : arr[index - 1];

        // note loop starts from
        // previous number i.e. at
        // array location index - 1
        for (int k = prev; k <= num; k++)
        {
            // next element of
            // array is k
            arr[index] = k;

            // call recursively with
            // reduced number
            findCombinationsUtil(arr, index + 1, num,
                                     reducedNum - k);
        }
    }

    /* Function to find out all
    combinations of positive
    numbers that add upto given
    number. It uses findCombinationsUtil() */
    static void findCombinations(int n)
    {
        // array to store the combinations
        // It can contain max n elements
        int[] arr = new int[n];

        // find all combinations
        findCombinationsUtil(arr, 0, n, n);
    }

}
