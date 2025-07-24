using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace MathGame
{
    public class Program
    {
        public static readonly Dictionary<string, Func<int, int, int>> _operations =
            new Dictionary<string, Func<int, int, int>>()
            {
                { "add", (int x, int y) => x + y },
                { "subtract", (int x, int y) => x - y },
                { "multiply", (int x, int y) => x * y },
                { "divide", (int x, int y) => x % y == 0 ? x / y : 0},
            };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Math Game!");

            Console.Write("Choose an operation {Add, Subtract, Multiply, Divide}: ");
            string operationType = Console.ReadLine().ToLower();

            if (!_operations.ContainsKey(operationType))
            {
                Console.WriteLine("Please choose a valid operation!");
            }
            else
            {
                Console.Write("Enter 1st number (integer): ");
                bool isFirstNumberValid = int.TryParse(Console.ReadLine(), out int firstNumber);
                if (!isFirstNumberValid)
                {
                    Console.WriteLine("Please enter a valid number!");
                }
                else
                {
                    Console.WriteLine("Enter 2nd number: (integer)");
                    bool isSecondNumberValid = int.TryParse(Console.ReadLine(), out int secondNumber);
                    if (!isFirstNumberValid)
                    {
                        Console.WriteLine("Please enter a valid number");
                    }
                    else
                    {
                        var result = _operations[operationType](firstNumber, secondNumber);
                        if (operationType == "divide" && firstNumber != 0 && result == 0)
                        {
                            Console.WriteLine("Division of selected numbers does not produce an integer!");
                        }
                        else
                        {
                            Console.WriteLine($"Result: {result}");
                        }
                    }
                }
            }

        }
    }
}
