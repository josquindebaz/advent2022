namespace advent2022.day15;

public class Sensors
{
    public void Execute()
    {
        // var sensors = File.ReadLines("./day15/test.txt")
        var sensors = File.ReadLines("./day15/input.txt")
            .Select(Sensor.Parse);
        
        // const int row = 10;
        const int row = 2000000;

        var unavailable = from sensor in sensors
            let position = sensor.BeaconPosition
            where position.Y == row
            select position.X;

        var result = sensors.SelectMany(sensor => sensor.MakeRange(row).Values)
            .Except(unavailable);
        
        Console.WriteLine($"first half of the puzzle {result.Count()}");
        }

    
    public readonly record struct Coordinates(int X, int Y)
    {
        public int Manhattan(Coordinates right)
        {
            return Math.Abs(X - right.X) + Math.Abs(Y - right.Y);
        }
    }

    private record struct Range(int Begin, int End)
    {
        public static readonly Range Empty = new(0, -1);

        public bool IsEmpty => Begin > End;

        public IEnumerable<int> Values => IsEmpty ? Enumerable.Empty<int>() : Enumerable.Range(Begin, End - Begin + 1);

    }
    
    private record Sensor(Coordinates SensorPosition, Coordinates BeaconPosition)
    {
        public int Distance { get; } = SensorPosition.Manhattan(BeaconPosition);

        public Range MakeRange(int row)
        {
            var y = Math.Abs(row - SensorPosition.Y);
            if (y > Distance)
                return Range.Empty;

            var x = Distance - y;
            return new Range(SensorPosition.X - x, SensorPosition.X + x);
        }

        public static Sensor Parse(string line)
        {
            var sp = line.Split("=");
            
            return new Sensor(
                new Coordinates(
                    int.Parse(sp[1].Split(",")[0]),
                    int.Parse(sp[2].Split(":")[0])), 
                new Coordinates(
                    int.Parse(sp[3].Split(",")[0]),
                    int.Parse(sp[4])
                    )
                );
        }
    }
}