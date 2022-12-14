using System.Data;
using System.Text.RegularExpressions;

namespace advent2022.day11;

public class MonkeyBusiness
{
    public void Execute()
    {
        var content = File.ReadAllText("./day11/test.txt");
        content = File.ReadAllText("./day11/input.txt");

        var monkeyList = new List<Monkey>();

        foreach (var monkey in content.Split("Monkey ")[1..]) monkeyList.Add(CreateMonkey(monkey));

        for (var round = 1; round < 21; round++)
        {
            Console.WriteLine($"round {round}");
            foreach (var monkey in monkeyList)
                while (monkey.Items.Count > 0)
                {
                    monkey.Inspections += 1;
                    var item = monkey.Items[0];
                    var interest = monkey.Operate(item) / 3 ;
                    
                    if (interest % monkey.Test == 0)
                        monkeyList[monkey.TrueMove].AddItem(interest);
                    else
                        monkeyList[monkey.FalseMove].AddItem(interest);
                    
                    monkey.Items.RemoveAt(0);
                }

            foreach (var monkey in monkeyList)
            {
                Console.WriteLine(string.Join(" ", monkey.Items));
            }
        }

        foreach (var monkey in monkeyList) Console.WriteLine(monkey.Inspections);

        var result = monkeyList
            .OrderByDescending(m => m.Inspections)
            .Take(2)
            .Select(m => m.Inspections)
            .ToList();
        
        Console.WriteLine($"first half of the puzzle {result[0] * result[1]}");
    }

    private Monkey CreateMonkey(string description)
    {
        var monkey = new Monkey();

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

public class Monkey
{
    public readonly List<int> Items = new();
    public int FalseMove;
    public int Inspections;
    public string Operation = "";
    public int Test;
    public int TrueMove;

    public void AddItem(int item)
    {
        Items.Add(item);
    }
    public int Operate(int item)
    {
        var result = Operation.Replace("old", item.ToString());
        return (int) new DataTable().Compute(result, string.Empty);
    }
}