using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the number of cubes:");
        if (int.TryParse(Console.ReadLine(), out int cubes) && cubes >= 0)
        {
            Console.WriteLine($"Number of steps: {CountSteps(cubes)}");
        }
        else
        {
            Console.WriteLine("Error: Enter a non-negative integer");
            Main();
        }
    }

    static int CountSteps(int cubes, int step = 1, int count = 0)
    {
        return cubes >= step ? CountSteps(cubes - step, step + 1, count + 1) : count;
    }
}
