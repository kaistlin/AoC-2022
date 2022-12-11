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
        public List<int> items;
        
      /**  public Monkey(string name, int[] stats)
        {
            this.name = name;
            this.stats = stats;
        }
        public Monkey() { }**/
    }
    [Benchmark]
    public void part1(){
        
        string[] line;
        Monkey[] Barrel = new Monkey[monkeyInput.Length/7+1];
        //Make-A-Monkey
        for (int i = 0; i < monkeyInput.Length; i += 7) {
            Monkey NewMonkey = new ();
            List<int> MonkeyWrenches = new();
            line = monkeyInput[i + 1].Split(new char[] { ',', ' ' });
            for (int j = 2; j <= line.Length / 2; j++)
            {
                MonkeyWrenches.Add(int.Parse(line[j * 2]));
            }
            line = monkeyInput[i + 2].Split(" ");
            NewMonkey.Operation = char.Parse(line[6]);
            if (!int.TryParse(line[7], out NewMonkey.Operand)) { NewMonkey.Operation = '^'; }
            line = monkeyInput[i + 3].Split(" ");
            NewMonkey.Divisor = int.Parse(line[5]); 
            line = monkeyInput[i + 4].Split(" ");
            NewMonkey.IfTrue = int.Parse(line[9]);
            line = monkeyInput[i + 5].Split(" ");
            NewMonkey.IfFalse = int.Parse(line[9]);
            NewMonkey.items = MonkeyWrenches;
            Barrel[i/7] = NewMonkey;
        }
        for (int Round = 1; Round <= 20; Round++)
        {
            
            for (int j=0; j<Barrel.Length;j++)
            {
                Monkey Monkey = Barrel[j];
                foreach (int item in Monkey.items)
                {
                    Monkey.Inspected++;
                    int newWorry = GetResult(Monkey.Operation, item, Monkey.Operand) / 3;
                    if (newWorry % Monkey.Divisor == 0)
                    {
                        Barrel[Monkey.IfTrue].items.Add(newWorry);
                        
                    }
                    else
                    {
                        Barrel[Monkey.IfFalse].items.Add(newWorry);
                    }
                }
                Monkey.items.Clear();
    //            Console.WriteLine("This round's moves of monkey" + j + " moves was: " + Monkey.Inspected);
            }
        }
        Console.WriteLine(Barrel);
foreach(Monkey Monkey in Barrel)
        {
            Console.WriteLine(Monkey.Inspected +" items");
        }
    }

    private int GetResult(char operation, int item, int operand)
    {
        return operation switch
        {
            '+' => item + operand,
            '*' => item * operand,
            '^' => item * item,
            _ => 0
           
        };
    }

    [Benchmark]
    public void part2(){ 
    
    }
}
