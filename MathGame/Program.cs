using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System;

namespace MathGame
{
    public class Program
    {
        public static readonly Dictionary<string, Func<int, int, int>> Operations =
            new Dictionary<string, Func<int, int, int>>()
            {
                { "add", (int x, int y) => x + y },
                { "subtract", (int x, int y) => x - y },
                { "multiply", (int x, int y) => x * y },
                { "divide", (int x, int y) => x % y == 0 ? x / y : 0 },
            };

        public static readonly List<string> Results = new List<string>();
        private static string InvalidDivisionMessage = "Division of entered numbers does not produce an integer!";
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to my Math Game!");
            bool playAgain = true;
            bool showHistory = false;
            var totalTimeStopwatch = Stopwatch.GetTimestamp();
            while (playAgain)
            {
                var stopwatch = Stopwatch.GetTimestamp();
                Console.Write("Choose an operation {Add, Subtract, Multiply, Divide}: ");
                string operationType = Console.ReadLine().ToLower();

                if (!Operations.TryGetValue(operationType, out Func<int, int, int> operation))
                {
                    Console.WriteLine("Please choose a valid operation!");
                }
                else
                {
                    Console.Write("Enter 1st number (integer): ");
                    bool isFirstNumberValid;
                    int firstNumber;
                    while ((isFirstNumberValid = int.TryParse(Console.ReadLine(), out firstNumber)) == false)
                    {
                        Console.WriteLine("Please enter a valid integer!");
                        Console.Write("Enter 1st number (integer): ");
                    }

                    Console.Write("Enter 2nd number (integer): ");
                    bool isSecondNumberValid;
                    int secondNumber;
                    while ((isFirstNumberValid = int.TryParse(Console.ReadLine(), out secondNumber)) == false)
                    {
                        Console.WriteLine("Please enter a valid integer!");
                        Console.Write("Enter 2nd number (integer): ");
                    }

                    var result = operation(firstNumber, secondNumber);
                    string resolution = string.Empty;

                    if (operationType == "divide" && firstNumber != 0 && result == 0)
                    {
                        Console.WriteLine(InvalidDivisionMessage);
                        resolution = InvalidDivisionMessage;
                    }
                    else
                    {
                        Console.WriteLine($"Result: {result}");
                        resolution = result.ToString();
                    }

                    var elapsedTime = Math.Round(Stopwatch.GetElapsedTime(stopwatch).TotalSeconds, 2);

                    Console.WriteLine($"It took you ~{elapsedTime} seconds to finish this game.");
                    Results.Add(resolution);
                    Console.Write("Would you like to play again? (Yes/No): ");
                    playAgain = Console.ReadLine().ToLower() == "yes";

                    if (Results.Count > 1)
                    {
                        Console.Write("Would you like to see game history? (Yes/No): ");
                        showHistory = Console.ReadLine().ToLower() == "yes";
                        if (showHistory)
                        {
                            Console.WriteLine(string.Join(Environment.NewLine, Results));
                            if (!playAgain)
                            {
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
            }
            var totalElapsedTimeMinutes = Math.Round(Stopwatch.GetElapsedTime(totalTimeStopwatch).TotalMinutes - 0.017, 2);
            Console.WriteLine($"This game session took you ~{(totalElapsedTimeMinutes < 1 ? Math.Round(Stopwatch.GetElapsedTime(totalTimeStopwatch).TotalSeconds - 1, 2) + " seconds" : totalElapsedTimeMinutes) + " minutes"}.");

        }
    }
}


