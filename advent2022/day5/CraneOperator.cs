namespace advent2022.day5;

public class CraneOperator
{
    public void Execute()
    {
        var lines = File.ReadLines("./day5/input.txt");

        var stacks = new string[9];
        var newStacks = new string[9];
        
        var lineIncrement = 0;
        foreach (var line in lines)
        {
            switch (lineIncrement)
            {
                case < 8:
                {
                    for (var stackIncrement = 0; stackIncrement < 9; stackIncrement++)
                    {
                        var crane = line[stackIncrement * 4 + 1];
                        if (crane == ' ') continue;
                        stacks[stackIncrement] += crane;
                        newStacks[stackIncrement] += line[stackIncrement * 4 + 1];
                    }

                    break;
                }
                case > 9:
                {
                    var move = line.Split(" ");
                    var comingPile = long.Parse(move[3])-1;
                    var goingPile = long.Parse(move[5])-1;
                    var range = int.Parse(move[1]);

                    newStacks[goingPile] = newStacks[comingPile][..range] + newStacks[goingPile];
                    newStacks[comingPile] = newStacks[comingPile][range..];

                    for (var moveIncrement = 0; moveIncrement < range; moveIncrement++)
                    {
                        stacks[goingPile] = stacks[comingPile][0] + stacks[goingPile];
                        stacks[comingPile] = stacks[comingPile][1..];
                    }

                    break;
                }
            }

            lineIncrement++;
        }
        
        var firstMessage = stacks.Aggregate("", (current, stack) => current + stack[0]);
        Console.WriteLine($"first half of the puzzle {firstMessage}");

        var secondMessage = newStacks.Aggregate("", (current, stack) => current + stack[0]);
        Console.WriteLine($"second half of the puzzle {secondMessage}");

    }
}