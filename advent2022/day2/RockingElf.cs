namespace advent2022.day2;

public class RockingElf
{
    /// <summary>
    ///     A for Rock, B for Paper, and C for Scissors
    ///     1 for Rock, 2 for Paper, and 3 for Scissors
    ///     0 if you lost, 3 if the round was a draw, and 6 if you won
    /// </summary>
    public void Execute()
    {
        var splitLines = File.ReadLines("./day2/input.txt")
            .Select(line => line.Split(' '))
            .ToArray();

        // X for Rock, Y for Paper, and Z for Scissors
        var firstScoring = new Dictionary<string, Dictionary<string, long>>
        {
            {"X", new Dictionary<string, long> {{"A", 3}, {"B", 0}, {"C", 6}}},
            {"Y", new Dictionary<string, long> {{"A", 6}, {"B", 3}, {"C", 0}}},
            {"Z", new Dictionary<string, long> {{"A", 0}, {"B", 6}, {"C", 3}}}
        };
        
        var firstTotalScore = splitLines
            .Select(gestures => ShapeScore(gestures[1]) + firstScoring[gestures[1]][gestures[0]])
            .Sum();
        
        Console.WriteLine($"first half of the puzzle {firstTotalScore}");
        
        // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win.
        var newScoring = new Dictionary<string, Dictionary<string, string>>
        {
            {"X", new Dictionary<string, string> {{"A", "Z"}, {"B", "X"}, {"C", "Y"}}},
            {"Y", new Dictionary<string, string> {{"A", "X"}, {"B", "Y"}, {"C", "Z"}}},
            {"Z", new Dictionary<string, string> {{"A", "Y"}, {"B", "Z"}, {"C", "X"}}}
        };

        var secondTotalScore = (from turnStrategy in splitLines
            let myGesture = newScoring[turnStrategy[1]][turnStrategy[0]] 
            select ShapeScore(myGesture) + firstScoring[myGesture][turnStrategy[0]])
            .Sum();
        
        Console.WriteLine($"second half of the puzzle {secondTotalScore}");
    }

    private long ShapeScore(string shape)
    {
        return shape switch
        {
            "X" => 1,
            "Y" => 2,
            "Z" => 3,
            _ => 0
        };
    }
}