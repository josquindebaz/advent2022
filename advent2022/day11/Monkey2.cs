using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.RegularExpressions;

namespace advent2022.day11;

public class Monkey2
{
    public void Execute()
    {
        var content = File.ReadAllText("./day11/test.txt");
        content = File.ReadAllText("./day11/input.txt");

        
        var monkeyList = new List<HeavyMonkey>();

        foreach (var monkey in content.Split("Monkey ")[1..])
            monkeyList.Add(CreateMonkey(monkey));

        BigInteger reduce = 1;
        foreach (var value in monkeyList.Select(m => m.Test))
        {
            reduce *= value;
        }
        
        for (var round = 1; round < 10001; round++)
        {
            foreach (var monkey in monkeyList)
                while (monkey.Items.Count > 0)
                {
                    monkey.Inspections += 1;
                    var item = monkey.Items[0];
                    var interest = monkey.Operate(item) % reduce;
                    
                    if (interest % monkey.Test == 0)
                        monkeyList[monkey.TrueMove].AddItem(interest);
                    else
                        monkeyList[monkey.FalseMove].AddItem(interest);
                    
                    monkey.Items.RemoveAt(0);
                }
        }
        
        var result = monkeyList
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Select(m => m.Inspections)
            .ToList();
        
        Console.WriteLine($"second half of the puzzle {result[0] * result[1]}");
        
    }
    
    private HeavyMonkey CreateMonkey(string description)
    {
        var monkey = new HeavyMonkey();

        var itemsPattern = new Regex(@"Starting items: (.*)");
        var items = itemsPattern.Match(description).Groups[1].ToString().Split(", ");

        foreach (var item in items) monkey.AddItem(int.Parse(item));

        var operationPattern = new Regex(@"Operation: new = (.*)");
        monkey.Operation = operationPattern.Match(description).Groups[1].ToString();

        var testPattern = new Regex(@"Test: divisible by (\d+)");
        monkey.Test = int.Parse(testPattern.Match(description).Groups[1].ToString());

        var truePattern = new Regex(@"If true: throw to monkey (\d+)");
        monkey.TrueMove = int.Parse(truePattern.Match(description).Groups[1].ToString());

        var falsePattern = new Regex(@"If false: throw to monkey (\d+)");
        monkey.FalseMove = int.Parse(falsePattern.Match(description).Groups[1].ToString());

        return monkey;
    }
}

public class HeavyMonkey
{
    public readonly List<BigInteger> Items = new();
    public int FalseMove;
    public BigInteger Inspections;
    public string Operation = "";
    public int Test;
    public int TrueMove;

    public void AddItem(BigInteger item)
    {
        Items.Add(item);
    }

    public BigInteger Operate(BigInteger item)
    {
        var plus = new Regex(@"old \+ (\d+)");
        if (plus.Match(Operation).Length > 0)
            return item + int.Parse(plus.Match(Operation).Groups[1].ToString());
 
        var multi = new Regex(@"old \* (\d+)");
        if (multi.Match(Operation).Length > 0)
            return item * int.Parse(multi.Match(Operation).Groups[1].ToString());

        return item * item;
    }
}