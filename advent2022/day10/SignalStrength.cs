using System.Collections;

namespace advent2022.day10;

public class SignalStrength
{
    public void Execute()
    {
        var lines = File.ReadLines("./day10/test.txt");
        // lines = File.ReadLines("./day10/test0.txt");
        lines = File.ReadLines("./day10/input.txt");

        var instructions = new Queue();
        foreach (var line in lines)
            instructions.Enqueue(line);
        
        var register = 1;
        var signalSum = 0;
        var cycle = 1;
        var display = "";

        while (instructions.Count > 0)
        {
            var instruction = instructions.Dequeue()!.ToString();

            int value;
            int steps;
            if (instruction != "noop")
            {
                value = int.Parse(instruction!.Split(" ")[1]);
                steps = 2;
            }
            else
            {
                value = 0;
                steps = 1;
            }

            for (var i = 1; i < 1 + steps; i++)
            {
                if ((cycle - 20) % 40 == 0)
                {
                    var strength = register * cycle;
                    signalSum += strength;
                }

                var crt = (cycle - 1) % 40;
                
                display += crt >= register - 1 && crt <= register + 1 ? "#" : " ";
                
                if (crt == 39)
                {
                    Console.WriteLine(display);
                    display = "";
                }
                
                cycle++;
            }

            register += value;
        }
        Console.WriteLine($"first half of the puzzle {signalSum}");
    }
}