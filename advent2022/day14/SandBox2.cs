namespace advent2022.day14;

public class SandBox2
{
    int _ymax = 0;
    int _xmin = int.MaxValue;
    int _xmax = 0;

    public void Execute()
    {
        var map = new Dictionary<string, int>();
        
        var lines = File.ReadLines("./day14/test.txt");
        lines = File.ReadLines("./day14/input.txt").ToList();
        
        
        foreach (var line in lines)
        {
            var rocks = line.Split(" -> ");
            
            
            for (int r = 0; r < rocks.Length-1; r++)
            {
                int begin;
                int end;
 
                var first = rocks[r].Split(",").Select(int.Parse).ToArray();
                var second = rocks[r+1].Split(",").Select(int.Parse).ToArray();

                _xmax = new List<int> {_xmax, first[0], second[0]}.Max();
                _xmin = new List<int> {_xmin, first[0], second[0]}.Min();
                _ymax = new List<int> {_ymax, first[1], second[1]}.Max();
                
                if (first[0] == second[0])
                {
                    if (first[1] < second[1])
                    {
                        begin = first[1];
                        end = second[1];
                    }
                    else
                    {
                        begin = second[1];
                        end = first[1];
                    }
                    
                    for (int y = begin; y <= end; y++)
                    {
                        map[$"{first[0]},{y}"] = 2;
                    }
                }
                else
                {
                    if (first[0] < second[0])
                    {
                        begin = first[0];
                        end = second[0];
                    }
                    else
                    {
                        begin = second[0];
                        end = first[0];
                    }
                    for (int x = begin; x <= end; x++)
                    {
                        map[$"{x},{first[1]}"] = 2;
                    }
                }
            }
        }
        
        for (int y = 0; y < _ymax+3; y++)
        {
            for (int x = _xmin-2; x < _xmax+4; x++)
            {
                if(!map.ContainsKey($"{x},{y}"))
                    map[$"{x},{y}"] = 0;
                if (y == _ymax+2) map[$"{x},{y}"] = 2; 
            }
        }
        
        var result = "";
        var incr = 0;
        while (result == "")
        {
            incr++;
            result = PourSand(map);
        }
        
        ShowMap(map);
        Console.WriteLine($"second half of the puzzle {incr}");
    }

    private string PourSand(Dictionary<string, int> map)
    {

        var canMove = true;
        int x = 500;
        int y = 0;
        
        while (canMove)
        {
           
            if (x < _xmin +1)
            {
                _xmin--;
                for (int j = 0; j < _ymax+3; j++)
                {
                    map[$"{_xmin-1},{j}"] = 0;
                    if (j == _ymax+2) map[$"{_xmin-1},{j}"] = 2;
                }
            }
            
            if (x > _xmax-1)
            {
                _xmax++;
                for (int j = 0; j < _ymax+3; j++)
                {
                    map[$"{_xmax+1},{j}"] = 0;
                    if (j == _ymax+2) map[$"{_xmax+1},{j}"] = 2;
                }
            }
            
            if (map[$"{x},{y + 1}"] == 0)
            {
                y++;
                continue;
            }

            if (map[$"{x - 1},{y + 1}"] == 0)
            {
                x--;
                y++;
                continue;
            }

            if (map[$"{x + 1},{y + 1}"] == 0)
            {
                x++;
                y++;
                continue;
            }
            
            canMove = false;
        }

        if (map.ContainsKey($"{x},{y}"))
        {
            map[$"{x},{y}"] = 1;
        }
        
        if (x == 500 && y == 0) return "done";

        return "";
    }
    
    private void ShowMap(Dictionary<string, int> map)
    {
        for (int y = 0; y < _ymax+3; y++)
        {
            for (int x = _xmin-1; x < _xmax+2; x++)
            {
                switch (map[$"{x},{y}"])
                {
                    case 1:
                        Console.Write("o");
                        break;
                    case 2:
                        Console.Write("#");
                        break;
                    default:
                        Console.Write("."); 
                        break;
                }
            }
            Console.WriteLine("");
        }
    }
    
}