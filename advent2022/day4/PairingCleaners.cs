using System.Text.RegularExpressions;

namespace advent2022.day4;

public class PairingCleaners
{
    public void Execute()
    {
        var lines = File.ReadLines("./day4/input.txt");

        var containingPairs = 0;
        var overlappingPairs = 0;
        var regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");

        foreach (var line in lines)
        {
            var limits = regex.Matches(line)
                .First()
                .Groups;

            var e1L1 = long.Parse(limits[1].ToString());
            var e1L2 = long.Parse(limits[2].ToString());
            var e2L1 = long.Parse(limits[3].ToString());
            var e2L2 = long.Parse(limits[4].ToString());

            if (e1L1 <= e2L1 && e1L2 >= e2L2) containingPairs++;
            if (e1L1 >= e2L1 && e1L2 <= e2L2) containingPairs++;
            if (e1L1 == e2L1 && e1L2 == e2L2) containingPairs--;
            
            if (e1L1 >= e2L1 && e1L2 <= e2L1) overlappingPairs++;
            else if (e1L2 >= e2L1 && e1L2 <= e2L2) overlappingPairs++;
            else if (e1L1 <= e2L1 && e1L2 >= e2L2) overlappingPairs++;
            else if (e1L1 <= e2L2 && e1L2 >= e2L2) overlappingPairs++;
        }

        Console.WriteLine($"first half of the puzzle {containingPairs}");
        Console.WriteLine($"second half of the puzzle {overlappingPairs}");
    }
}