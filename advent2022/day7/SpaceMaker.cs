using System.Text.RegularExpressions;

namespace advent2022.day7;

public class SpaceMaker
{
    private readonly Dictionary<string, List<string>> _dirChildren = new();
    private readonly Dictionary<string, int> _dirSizes = new();

    public void Execute()
    {
        var lines = File.ReadLines("./day7/input.txt");
        
        var getSizePattern = new Regex(@"(\d+)");
        var changeDirPattern = new Regex(@"\$ cd (\w+)");
        var dirPattern = new Regex(@"dir (\w+)");
        
        var actualDir = ".";
        _dirSizes[actualDir] = 0;

        foreach (var line in lines)
        {
            if (line == "$ cd ..")
            {
                actualDir = string.Join("/", actualDir.Split("/")[..^1]);
            }
            
            var changeDirMatches = changeDirPattern.Matches(line);
            if (changeDirMatches.Count > 0)
            {
                actualDir += "/"+changeDirMatches.First().Groups[1].ToString();
            }
            else
            {
                var dirMatches = dirPattern.Matches(line);
                if (dirMatches.Count > 0)
                {
                    AddChild(actualDir, actualDir+"/"+dirMatches.First().Groups[1]);
                }
                else
                {
                    var sizeMatches = getSizePattern.Matches(line);
                    if (sizeMatches.Count > 0)
                    {
                        AddSize(actualDir, int.Parse(sizeMatches.First().Groups[1].ToString()));
                    }
                }
            }
        }

        AddSubDirectorySize(".");
        
        var sum = _dirSizes
            .Where(dir => dir.Value <= 100000)
            .Sum(dir => dir.Value);
        
        Console.WriteLine($"first half of the puzzle {sum}");

        var spaceToFree = 30000000 - (70000000 - _dirSizes["."]);
        var dirWithEnoughSpace = _dirSizes
            .Where(dir => dir.Value >= spaceToFree)
            .OrderBy(dir =>dir.Value);
        
        Console.WriteLine($"second half of the puzzle {dirWithEnoughSpace.First().Value}");
    }

    private bool IsLeaf(String dir)
    {
        return !_dirChildren.ContainsKey(dir);
    }

    private void AddChild(String dir, String child)
    {
        AddSize(child, 0);
        if (IsLeaf(dir))
        {
            _dirChildren[dir] = new List<string> {child};
        }
        else
        {
            _dirChildren[dir].Add(child);
        }
    }
    
    private void AddSize(String dir, int size)
    {
        if (!_dirSizes.ContainsKey(dir))
        {
            _dirSizes[dir] = size;
        }
        else
        {
            _dirSizes[dir] += size;
        }
    }

    private int AddSubDirectorySize(string dir)
    {
        if (IsLeaf(dir))
        {
            return _dirSizes[dir];
        }

        foreach (var child in _dirChildren[dir])
        {
            _dirSizes[dir] += AddSubDirectorySize(child);
        }

        return _dirSizes[dir];
    }
}