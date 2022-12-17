namespace advent2022.day14;

public class SandBox
{
    public void Execute()
    {
        var map = new Dictionary<string, int>();
        
        var lines = File.ReadLines("./day14/test.txt");
        // lines = File.ReadLines("./day14/input.txt").ToList();
        
        int xmax = 0;
        int xmin = int.MaxValue;
        int ymax = 0;
        
        
        foreach (var line in lines)
        {
            var rocks = line.Split(" -> ");
            
            
            for (int r = 0; r < rocks.Length-1; r++)
            {
                int begin;
                int end;
 
                var first = rocks[r].Split(",").Select(int.Parse).ToArray();
                var second = rocks[r+1].Split(",").Select(int.Parse).ToArray();

                xmax = new List<int> {xmax, first[0], second[0]}.Max();
                xmin = new List<int> {xmin, first[0], second[0]}.Min();
                ymax = new List<int> {ymax, first[1], second[1]}.Max();
                
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
        
        for (int y = 0; y < ymax+1; y++)
        {
            for (int x = xmin; x < xmax+1; x++)
            {
                if(!map.ContainsKey($"{x},{y}"))
                    map[$"{x},{y}"] = 0;
            }
        }

        var result = "";
        var incr = 0;
        while (result == "")
        {
            incr++;
            result = PourSand(map);
        }

        ShowMap(map, xmin, xmax, ymax);
        Console.WriteLine($"first half of the puzzle {incr-1}");
        
    }

    private string PourSand(Dictionary<string, int> map)
    {

        var canMove = true;
        int x = 500;
        int y = 0;
        
        while (canMove)
        {
            if (!map.ContainsKey($"{x},{y+1}") 
                || !map.ContainsKey($"{x - 1},{y + 1}") 
                || !map.ContainsKey($"{x + 1},{y + 1}"))
            {
                return "done";
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

        return "";
    }
    
    private void ShowMap(Dictionary<string, int> map, int xmin, int xmax, int ymax)
    {
        for (int y = 0; y < ymax+1; y++)
        {
            for (int x = xmin; x < xmax+1; x++)
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