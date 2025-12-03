using System.Text;
using Microsoft.Extensions.Configuration;

public class Solution
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", false, true)
            .Build();

        string path = config["input-path"]!;

        if (path == null) return;

        string[] arr = File.ReadAllLines(path);

        string joined = string.Join("", arr);
        string[] numbers = joined.Split(',');

        double part1 = 0;
        double part2 = 0;

        foreach (string number in numbers)
        {
            string[] temp = number.Split("-");

            double from = double.Parse(temp[0]);
            double to = double.Parse(temp[1]);

            for (double i = from; i <= to; i++)
            {
                if (IsValidForPart1(i.ToString()))
                    part1 += i;

                if (IsValidForPart2(i.ToString()))
                    part2 += i;
            }
        }

        Console.WriteLine("Part1: " + part1);
        Console.WriteLine("Part2: " + part2);
    }

    private static bool IsValidForPart1(string s)
    {
        double innerCount = 0;

        if (s.Length % 2 != 0)
            return false;

        for (int j = 0; j < s.Length / 2; j++)
        {
            if (s[j] == s[s.Length / 2 + j])
                innerCount++;
        }

        if (innerCount == s.Length / 2)
            return true;


        return false;
    }
    private static bool IsValidForPart2(string s)
    {
        int n = s.Length;

        for (int i = 1; i <= n / 2; i++)
        {
            if (n % i != 0) continue;

            string block = s.Substring(0, i);
            int repeat = n / i;

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < repeat; j++)
                sb.Append(block);

            if (sb.ToString() == s)
                return true;
        }

        return false;
    }
}