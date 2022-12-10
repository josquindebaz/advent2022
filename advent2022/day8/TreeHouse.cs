namespace advent2022.day8;

public class TreeHouse
{
    int[][] _forest = null!;

    public void Execute()
    {
        // var lines = File.ReadLines("./day8/test.txt");
        var lines = File.ReadLines("./day8/input.txt");

        _forest = lines.Select(line => line.Select(t => int.Parse(new string(t, 1))).ToArray()).ToArray();
        
        int sum = 0;
        int max = 0;
        for (int line = 0; line < _forest.Length; line++)
        {
            for (int row = 0; row < _forest[line].Length; row++)
            {
                // Console.WriteLine($"line:{line} row:{row} tree:{_forest[line][row]} ");
                sum += IsVisible(line, row) ? 1 : 0;
                max = new int[]{max, ScoreScene(line, row)}.Max();
            }
        }
        
        Console.WriteLine($"first half of the puzzle {sum}");
        Console.WriteLine($"second half of the puzzle {max}");
    }

    private bool IsVisible(int line, int row)
    {
        var tree = _forest[line][row];

        var before = from t in _forest[line][..row] where tree <= t select t;
        if (!before.Any())  return true;

        var after = from t in _forest[line][(row+1)..] where tree <= t select t;
        if (!after.Any()) return true;

        var above = 0;
        for (int l = 0; l < line; l++)
        {
            above += tree <= _forest[l][row] ? 1:0;
        }
        if (above == 0) return true;
        
        var below = 0;
        for (int l = line+1; l < _forest.Length; l++)
        {
            below += tree <= _forest[l][row] ? 1:0;
        }
        if (below == 0) return true;
        
        return false;
    }

    private int ScoreScene(int line, int row)
    {

        if (line == 0 || row == 0 || line == _forest.Length - 1 || row == _forest[0].Length-1) return 0;

        int tree = _forest[line][row];

        int before = 1;
        for (int r = row-1; r > 0; r--)
        {
            if (tree <= _forest[line][r])
            {
                break;
            }
            before += 1;
        }

        int after = 1;
        for (int r = row+1; r < _forest[line].Length-1; r++)
        {
            if (tree <= _forest[line][r])
            {
                break;
            }
            after += 1;
        }

        int above = 1;
        for (int l = line-1; l > 0; l--)
        {
            if (tree <= _forest[l][row])
            {
                break;
            }

            above += 1;
        }

        int below = 1;
        for (int l = line+1; l < _forest.Length-1; l++)
        {
            if (tree <= _forest[l][row])
            {
                break;
            }
            below += 1;
        }
        
        return below * after * before * above;
    }

}