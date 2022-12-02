namespace advent2022.day1;

public class RichestElf
{
    public void Execute()
    {
        var lines = File.ReadLines("./day1/input.txt");

        long richestElf = 0;
        long summedCalories = 0;

        foreach (var line in lines)
        {
            if (long.TryParse(line, out var calories))
            {
                summedCalories += calories;
                continue;
            }
            richestElf = Math.Max(richestElf, summedCalories);
            summedCalories = 0;
        }

        Console.WriteLine(richestElf);
    }
}