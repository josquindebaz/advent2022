namespace advent2022.day3;

public class LoadingElf
{
    public void Execute()
    {
        var lines = File.ReadLines("./day3/input.txt");
        
        var sum = lines
            .Select(line => line[..(line.Length / 2)].Intersect(line[(line.Length / 2)..]))
            .Select(common 
                => (int) common.First())
            .Aggregate<int, long>(0, (current, code) 
                => current + (code > 96 ? code - 96 : code - 38));

        Console.WriteLine($"first half of the puzzle {sum}");

        long secondSum = 0;
        var increment = 0;
        IEnumerable<char> common = new List<char>();
       
        foreach (var line in lines)
        {
            increment += 1;

            if (increment == 1)
            {
                common = line;
                continue;
            }
            
            common = common.Intersect(line);
            
            if (increment == 3)
            {
                increment = 0;
                var code = (int) common.First();
                secondSum += code > 96 ? code - 96 : code - 38;
            }
        }
        Console.WriteLine($"second half of the puzzle {secondSum}");
    }
}