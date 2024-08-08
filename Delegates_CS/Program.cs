using System;

namespace CombinedApp
{
    public delegate void MessageDelegate(string message);
    public delegate double ArithmeticOperation(double x, double y);
    public static class NumberPredicates
    {
        public static Predicate<int> IsEven = num => num % 2 == 0;
        public static Predicate<int> IsOdd = num => num % 2 != 0;
        public static Predicate<int> IsPrime = num =>
        {
            if (num <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0) return false;
            }
            return true;
        };
        public static Predicate<int> IsFibonacci = num =>
        {
            bool IsPerfectSquare(int n) => Math.Sqrt(n) % 1 == 0;
            return IsPerfectSquare(5 * num * num + 4) || IsPerfectSquare(5 * num * num - 4);
        };
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a task to execute (1-4):");
            Console.WriteLine("1. Display a message using delegates.");
            Console.WriteLine("2. Perform arithmetic operations using delegates.");
            Console.WriteLine("3. Check number properties using predicates.");
            Console.WriteLine("4. Perform arithmetic operations using Invoke.");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Task1_DisplayMessage();
                    break;
                case 2:
                    Task2_ArithmeticOperations();
                    break;
                case 3:
                    Task3_CheckNumberProperties();
                    break;
                case 4:
                    Task4_ArithmeticOperationsInvoke();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        static void Task1_DisplayMessage()
        {
            MessageDelegate showMessage = DisplayMessage;
            showMessage += DisplayUpperCaseMessage;
            showMessage += DisplayLowerCaseMessage;

            showMessage("Hello, Delegates!");
        }

        static void Task2_ArithmeticOperations()
        {
            ArithmeticOperation add = Add;
            ArithmeticOperation subtract = Subtract;
            ArithmeticOperation multiply = Multiply;

            double a = 10, b = 5;

            Console.WriteLine($"Addition: {add(a, b)}");
            Console.WriteLine($"Subtraction: {subtract(a, b)}");
            Console.WriteLine($"Multiplication: {multiply(a, b)}");
        }

        static void Task3_CheckNumberProperties()
        {
            int number = 21;

            Console.WriteLine($"Is {number} even? {NumberPredicates.IsEven(number)}");
            Console.WriteLine($"Is {number} odd? {NumberPredicates.IsOdd(number)}");
            Console.WriteLine($"Is {number} prime? {NumberPredicates.IsPrime(number)}");
            Console.WriteLine($"Is {number} Fibonacci? {NumberPredicates.IsFibonacci(number)}");
        }

        static void Task4_ArithmeticOperationsInvoke()
        {
            ArithmeticOperation add = Add;
            ArithmeticOperation subtract = Subtract;
            ArithmeticOperation multiply = Multiply;

            double a = 15, b = 3;

            Console.WriteLine($"Addition: {add.Invoke(a, b)}");
            Console.WriteLine($"Subtraction: {subtract.Invoke(a, b)}");
            Console.WriteLine($"Multiplication: {multiply.Invoke(a, b)}");
        }

        static void DisplayMessage(string message)
        {
            Console.WriteLine("Message: " + message);
        }

        static void DisplayUpperCaseMessage(string message)
        {
            Console.WriteLine("Uppercase Message: " + message.ToUpper());
        }

        static void DisplayLowerCaseMessage(string message)
        {
            Console.WriteLine("Lowercase Message: " + message.ToLower());
        }

        static double Add(double x, double y) => x + y;

        static double Subtract(double x, double y) => x - y;

        static double Multiply(double x, double y) => x * y;
    }
}