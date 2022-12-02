namespace advent2022.day1;

public class RichestElf
{
    public void Execute()
    {
        var lines = File.ReadLines("./day1/input.txt");

        // long richestElf = 0;
        long summedCalories = 0;
        var richestElves = new long[4];  
            
        foreach (var line in lines)
        {
            if (long.TryParse(line, out var calories))
            {
                summedCalories += calories;
                richestElves[0] = summedCalories;
                Array.Sort(richestElves);
                continue;
            }
            // richestElf = Math.Max(richestElf, summedCalories);
            summedCalories = 0;
        }
        
        Console.WriteLine($"first half of the puzzle {richestElves[3]}");
        // Console.WriteLine($"first half of the puzzle {richestElf}");
        Console.WriteLine($"second half of the puzzle {richestElves[1..].Sum()}");
    }
}