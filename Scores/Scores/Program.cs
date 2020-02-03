using System;

namespace Scores
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter in first name");
            string date = DateTime.Today.ToShortDateString();
            string uName = Console.ReadLine();
            string msg = $"\nWelcome back {uName}. Today is {date}.";
            Console.WriteLine(msg);
            Console.ReadLine();

            string path = @"C:\Users\Student\C-Sharp-Projects\C-Sharp-projects\Scores\Scores\StudentScores.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            double tScore = 0.0;

            Console.WriteLine("\nStudent Scores: \n");
            foreach (string line in lines)
            {
                Console.Write("\n" + line);
                double score = Convert.ToDouble(line);
                tScore += score;
            }

            double average = tScore / lines.Length;
            Console.WriteLine("\n Total of " + lines.Length + " student scores. \tAverage score:" + average); 

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
