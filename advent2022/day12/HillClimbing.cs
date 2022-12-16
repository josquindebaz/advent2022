namespace advent2022.day12;

public class HillClimbing
{
    private readonly List<List<int>> _map = new();
    private readonly Dictionary<string, List<string>> _ways = new();
    private string _destination = "";

    public void Execute()
    {
        var lines = File.ReadLines("./day12/test.txt").ToList();
        lines = File.ReadLines("./day12/input.txt").ToList();

        var position = "";
        List<string> startingPositions = new List<string>();

        for (var y = 0; y < lines.Count; y++)
        {
            var line = lines[y];
            var lineMap = new List<int>();

            for (var x = 0; x < line.Length; x++)
            {
                var element = line[x];
                switch (element)
                {
                    case 'S':
                        lineMap.Add('a');
                        position = $"{x},{y}";
                        startingPositions.Add(position);
                        break;
                    case 'E':
                        lineMap.Add('z');
                        _destination = $"{x},{y}";
                        break;
                    default:
                        lineMap.Add(element);
                        if(element == 'a') startingPositions.Add($"{x},{y}");
                        break;
                }
            }

            _map.Add(lineMap);
        }

        for (var y = 0; y < _map.Count; y++)
        for (var x = 0; x < _map[0].Count; x++)
        {
            var ways = GetWays(x, y);
            _ways[$"{x},{y}"] = ways;
        }

        Console.WriteLine($"first half of the puzzle {ComputeShortestPath(position)}");

        var trajectories = new List<int>();
        foreach (var start in startingPositions)
        {
            var test = ComputeShortestPath(start);
            if (test > 0)
                trajectories.Add(test);
        }
        Console.WriteLine($"second half of the puzzle {trajectories.Min()}");
    }

    private int ComputeShortestPath(string start)
    {
        // Console.WriteLine(start);
        var trajectories = new Queue<string>();
        trajectories.Enqueue(start);
        var distances = new Dictionary<string, int> { {start, 0} };
        List<string> visited = new();

        int step = 0;
         
        while (trajectories.Count > 0)
        {
            var trajectory = trajectories.Dequeue();
            var candidates = _ways[trajectory]
                .Where(way => !visited.Contains(way) || distances[way] > distances[trajectory] + 1);

            foreach (var way in candidates)
            {
                if (!distances.ContainsKey(way)) distances[way] = distances[trajectory] + 1;
                if (distances[way] < distances[trajectory] + 1) continue;
                visited.Add(way);
                trajectories.Enqueue(way);

                if (way == _destination)
                    step = distances[trajectory] + 1;
            }
        }
        return step;
    }
    private List<string> GetWays(int x, int y)
    {
        var valid = new List<string>();
        var actual = _map[y][x];

        for (var i = -1; i < 2; i += 2)
        {
            var neighbour = x + i;
            if (neighbour < 0 || neighbour >= _map[0].Count) continue;
            var value = _map[y][neighbour];
            if (actual >= value - 1) valid.Add($"{neighbour},{y}");
        }

        for (var j = -1; j < 2; j += 2)
        {
            var neighbour = y + j;
            if (neighbour < 0 || neighbour >= _map.Count) continue;
            var value = _map[neighbour][x];
            if (actual >= value - 1) valid.Add($"{x},{neighbour}");
        }

        return valid;
    }
}