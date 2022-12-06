namespace advent2022.day6;

public class StreamerDecode
{
    public void Execute()
    {
        var lines = File.ReadLines("./day6/input.txt");
        var stream = lines.First();
        
        for (int markerIncrement = 0; markerIncrement < stream.Length-3; markerIncrement++)
        {
            int range = markerIncrement + 4;
            if (stream[markerIncrement..range].Distinct().Count() == 4)
            {
                Console.WriteLine($"first half of the puzzle {markerIncrement+4} ({stream[markerIncrement..range]})");
                break;
            }
        }

        for (int markerIncrement = 0; markerIncrement < stream.Length-13; markerIncrement++)
        {
            int range = markerIncrement + 14;
            if (stream[markerIncrement..range].Distinct().Count() == 14)
            {
                Console.WriteLine($"second half of the puzzle {markerIncrement+14} ({stream[markerIncrement..range]})");
                break;
            }
        }
    }
}