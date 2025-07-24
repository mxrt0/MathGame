using System.Numerics;
using System.Security.Cryptography.X509Certificates;

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
        private static string invalidDivisionMessage = "Division of entered numbers does not produce an integer!";
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to my Math Game!");
            bool playAgain = true;
            bool showHistory = false;

            while (playAgain)
            {
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
                        Console.WriteLine(invalidDivisionMessage);
                        resolution = invalidDivisionMessage;
                    }
                    else
                    {
                        Console.WriteLine($"Result: {result}");
                        resolution = result.ToString();
                    }

                    Results.Add(resolution);
                    Console.Write("Would you like to play again (Yes/No): ");
                    playAgain = Console.ReadLine().ToLower() == "yes";

                    if (Results.Count > 1)
                    {
                        Console.Write("Would you like to see game history? (Yes/No): ");
                        showHistory = Console.ReadLine().ToLower() == "yes";
                        if (showHistory)
                        {
                            Console.WriteLine(string.Join(Environment.NewLine, Results));
                        }
                    }
                }
            }
        }
    }
}


