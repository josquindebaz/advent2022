namespace advent2022.day9;

public class Plancker
{
    public void Execute()
    {
        var lines = File.ReadLines("./day9/test.txt");
        lines = File.ReadLines("./day9/test2.txt");
        lines = File.ReadLines("./day9/input.txt");

        var headPosition = new List<int> {0, 0};
        var tailPosition = new List<int> {0, 0};
        var tailTrajectory = new List<string> {string.Join(",", tailPosition)};

        var longTailPositions = new List<List<int>>
        {
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
            new() {0, 0},
        };

        var longTailTrajectory = new List<string> {string.Join(",", longTailPositions.Last())};

        var directions =
            new Dictionary<char, int[]>
            {
                {'R', new[] {0, 1}},
                {'U', new[] {1, 1}},
                {'L', new[] {0, -1}},
                {'D', new[] {1, -1}}
            };

        foreach (var line in lines)
        {
            var s = line.Split(" ");
            var move = Convert.ToChar(s[0]);
            var steps = int.Parse(s[1]);
            var axe = directions[move][0];

            for (var i = 0; i < steps; i++)
            {
                headPosition[axe] += directions[move][1];
                tailPosition = CalculatePosition(headPosition, tailPosition);
                tailTrajectory.Add(string.Join(",", tailPosition));

                for (var element = 0; element < longTailPositions.Count; element++)
                {
                    var predecessorPosition = (element == 0) 
                        ? headPosition
                        : longTailPositions[element-1];
                    
                    var newPosition = CalculatePosition(predecessorPosition, longTailPositions[element]);
                    
                    longTailPositions[element] = newPosition;
                }

                longTailTrajectory.Add(string.Join(",", longTailPositions.Last()));
            }
        }

        Console.WriteLine($"first half of the puzzle {tailTrajectory.Distinct().Count()}");
        Console.WriteLine($"second half of the puzzle {longTailTrajectory.Distinct().Count()}");
    }

    private List<int> CalculatePosition(List<int> aheadPosition, List<int> followerPosition)
    {
        var delta = new int[2];
        delta[0] = aheadPosition[0] - followerPosition[0];
        delta[1] = aheadPosition[1] - followerPosition[1];

        switch (delta[0])
        {
            case > 1:
                followerPosition[0] += 1;
                switch (delta[1])
                {
                    case > 0:
                        followerPosition[1] += 1;
                        return followerPosition;
                    case < 0:
                        followerPosition[1] -= 1;
                        return followerPosition;
                }
                break;
            case < -1:
                followerPosition[0] -= 1;
                switch (delta[1])
                {
                    case > 0:
                        followerPosition[1] += 1;
                        return followerPosition;
                    case < 0:
                        followerPosition[1] -= 1;
                        return followerPosition;
                }
                break;
        }

        switch (delta[1])
        {
            case > 1:
            {
                followerPosition[1] += 1;
                switch (delta[0])
                {
                    case > 0:
                        followerPosition[0] += 1;
                        return followerPosition;
                    case < 0:
                        followerPosition[0] -= 1;
                        return followerPosition;
                }
                break;
            }
            case < -1:
            {
                followerPosition[1] -= 1;
                switch (delta[0])
                {
                    case > 0:
                        followerPosition[0] += 1;
                        return followerPosition;
                    case < 0:
                        followerPosition[0] -= 1;
                        return followerPosition;
                }
                break;
            }
        }

        return followerPosition;
    }
}