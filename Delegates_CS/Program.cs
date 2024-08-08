using System;
using System.Linq;

namespace CombinedTasks
{
    public delegate bool NumberPredicate(int number);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a task to execute (1-3):");
            Console.WriteLine("1. Array Operations.");
            Console.WriteLine("2. Various Methods using Delegates.");
            Console.WriteLine("3. Credit Card Class with Events.");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Task1_ArrayOperations();
                    break;
                case 2:
                    Task2_VariousMethods();
                    break;
                case 3:
                    Task3_CreditCardClass();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        static void Task1_ArrayOperations()
        {
            int[] numbers = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };

            NumberPredicate isEven = number => number % 2 == 0;
            NumberPredicate isOdd = number => number % 2 != 0;
            NumberPredicate isPrime = number =>
            {
                if (number <= 1) return false;
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0) return false;
                }
                return true;
            };

            NumberPredicate isFibonacci = number =>
            {
                bool IsPerfectSquare(int n) => Math.Sqrt(n) % 1 == 0;
                return IsPerfectSquare(5 * number * number + 4) || IsPerfectSquare(5 * number * number - 4);
            };

            Console.WriteLine("Choose an array operation (1-4):");
            Console.WriteLine("1. Get all even numbers.");
            Console.WriteLine("2. Get all odd numbers.");
            Console.WriteLine("3. Get all prime numbers.");
            Console.WriteLine("4. Get all Fibonacci numbers.");

            int operationChoice = int.Parse(Console.ReadLine());

            switch (operationChoice)
            {
                case 1:
                    PrintNumbers("Even numbers", numbers.Where(n => isEven(n)).ToArray());
                    break;
                case 2:
                    PrintNumbers("Odd numbers", numbers.Where(n => isOdd(n)).ToArray());
                    break;
                case 3:
                    PrintNumbers("Prime numbers", numbers.Where(n => isPrime(n)).ToArray());
                    break;
                case 4:
                    PrintNumbers("Fibonacci numbers", numbers.Where(n => isFibonacci(n)).ToArray());
                    break;
                default:
                    Console.WriteLine("Invalid operation choice!");
                    break;
            }
        }

        static void Task2_VariousMethods()
        {
            Action showCurrentTime = () => Console.WriteLine("Current Time: " + DateTime.Now.ToShortTimeString());
            Action showCurrentDate = () => Console.WriteLine("Current Date: " + DateTime.Now.ToShortDateString());
            Action showCurrentDayOfWeek = () => Console.WriteLine("Current Day of the Week: " + DateTime.Now.DayOfWeek);

            Func<double, double, double> calculateTriangleArea = (baseLength, height) => 0.5 * baseLength * height;
            Func<double, double, double> calculateRectangleArea = (length, width) => length * width;

            Console.WriteLine("Choose a method to execute (1-5):");
            Console.WriteLine("1. Show current time.");
            Console.WriteLine("2. Show current date.");
            Console.WriteLine("3. Show current day of the week.");
            Console.WriteLine("4. Calculate triangle area.");
            Console.WriteLine("5. Calculate rectangle area.");

            int methodChoice = int.Parse(Console.ReadLine());

            switch (methodChoice)
            {
                case 1:
                    showCurrentTime();
                    break;
                case 2:
                    showCurrentDate();
                    break;
                case 3:
                    showCurrentDayOfWeek();
                    break;
                case 4:
                    Console.WriteLine("Enter base length and height of the triangle:");
                    double baseLength = double.Parse(Console.ReadLine());
                    double height = double.Parse(Console.ReadLine());
                    Console.WriteLine("Triangle Area: " + calculateTriangleArea(baseLength, height));
                    break;
                case 5:
                    Console.WriteLine("Enter length and width of the rectangle:");
                    double length = double.Parse(Console.ReadLine());
                    double width = double.Parse(Console.ReadLine());
                    Console.WriteLine("Rectangle Area: " + calculateRectangleArea(length, width));
                    break;
                default:
                    Console.WriteLine("Invalid method choice!");
                    break;
            }
        }

        static void Task3_CreditCardClass()
        {
            CreditCard card = new CreditCard("1234 5678 9101 1121", "Oleg Zamriy", DateTime.Now.AddYears(3), "1234", 5000);

            card.AccountReplenished += amount => Console.WriteLine($"Account replenished by: {amount} USD");
            card.MoneySpent += amount => Console.WriteLine($"Spent: {amount} USD");
            card.CreditUsed += amount => Console.WriteLine($"Credit used: {amount} USD");
            card.LimitReached += () => Console.WriteLine("Credit limit reached!");
            card.PinChanged += newPin => Console.WriteLine($"PIN changed to: {newPin}");

            Console.WriteLine("Choose an action for your credit card (1-5):");
            Console.WriteLine("1. Replenish account.");
            Console.WriteLine("2. Spend money.");
            Console.WriteLine("3. Use credit.");
            Console.WriteLine("4. Change PIN.");
            Console.WriteLine("5. Check balance.");

            int actionChoice = int.Parse(Console.ReadLine());

            switch (actionChoice)
            {
                case 1:
                    Console.WriteLine("Enter amount to replenish:");
                    double replenishAmount = double.Parse(Console.ReadLine());
                    card.ReplenishAccount(replenishAmount);
                    break;
                case 2:
                    Console.WriteLine("Enter amount to spend:");
                    double spendAmount = double.Parse(Console.ReadLine());
                    card.SpendMoney(spendAmount);
                    break;
                case 3:
                    Console.WriteLine("Enter amount to use from credit:");
                    double creditAmount = double.Parse(Console.ReadLine());
                    card.UseCredit(creditAmount);
                    break;
                case 4:
                    Console.WriteLine("Enter new PIN:");
                    string newPin = Console.ReadLine();
                    card.ChangePin(newPin);
                    break;
                case 5:
                    Console.WriteLine($"Current balance: {card.Balance} USD");
                    Console.WriteLine($"Available credit: {card.AvailableCredit} USD");
                    break;
                default:
                    Console.WriteLine("Invalid action choice!");
                    break;
            }
        }

        static void PrintNumbers(string description, int[] numbers)
        {
            Console.WriteLine($"{description}: {string.Join(", ", numbers)}");
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; }
        public string OwnerName { get; }
        public DateTime ExpirationDate { get; }
        public string Pin { get; private set; }
        public double CreditLimit { get; }
        public double Balance { get; private set; }
        public double AvailableCredit => CreditLimit - Balance;

        public event Action<double> AccountReplenished;
        public event Action<double> MoneySpent;
        public event Action<double> CreditUsed;
        public event Action LimitReached;
        public event Action<string> PinChanged;

        public CreditCard(string cardNumber, string ownerName, DateTime expirationDate, string pin, double creditLimit)
        {
            CardNumber = cardNumber;
            OwnerName = ownerName;
            ExpirationDate = expirationDate;
            Pin = pin;
            CreditLimit = creditLimit;
            Balance = 0;
        }

        public void ReplenishAccount(double amount)
        {
            Balance += amount;
            AccountReplenished?.Invoke(amount);
        }

        public void SpendMoney(double amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds!");
                return;
            }

            Balance -= amount;
            MoneySpent?.Invoke(amount);
        }

        public void UseCredit(double amount)
        {
            if (amount > AvailableCredit)
            {
                Console.WriteLine("Credit limit exceeded!");
                LimitReached?.Invoke();
                return;
            }

            Balance += amount;
            CreditUsed?.Invoke(amount);
        }

        public void ChangePin(string newPin)
        {
            Pin = newPin;
            PinChanged?.Invoke(newPin);
        }
    }
}