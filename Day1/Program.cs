using Microsoft.Extensions.Configuration;
public class Solution
{
    static void Main()
    {
        //string[] sampleArr = { "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82" };
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", false, true)
            .Build();

        string path = config["input-path"];
        
        if (path == null) return;

        string[] arr = File.ReadAllLines(path);

        int part1 = CalculateZerosPart1(arr, 50);
        int part2 = CalculateZerosPart2(arr, 50);

        Console.WriteLine("Part1: " + part1);
        Console.WriteLine("Part2: " + part2);
    }

    private static int CalculateZerosPart1(string[] array, int pos)
    {
        int count = 0;
        foreach (var arr in array)
        {
            int k = int.Parse(arr.Substring(1));

            if (arr.Contains('R'))
            {
                pos = (pos + k) % 100;
            }
            else if (arr.Contains('L'))
            {
                pos = (pos - k + 100) % 100;

            }
            if (pos == 0) count++;
        }

        return count;
    }

    private static int CalculateZerosPart2(string[] array, int pos)
    {
        int count = 0;

        foreach (var cmd in array)
        {
            int steps = int.Parse(cmd.Substring(1));
            bool isRight = cmd[0] == 'R';

            for (int i = 0; i < steps; i++)
            {
                pos = isRight
                    ? (pos + 1) % 100
                    : (pos + 99) % 100;

                if (pos == 0)
                    count++;
            }
        }

        return count;
    }
}