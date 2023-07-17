using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter a numeric value:");

        try
        {
            string input = Console.ReadLine();
            int number = Convert.ToInt32(input);
            Console.WriteLine($"You entered: {number}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input! Please enter a valid numeric value.");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

