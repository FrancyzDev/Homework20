using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите задание:");
            Console.WriteLine("1 - Перевод числа из одной системы в другую");
            Console.WriteLine("2 - Преобразование слова в цифру");
            Console.WriteLine("3 - Работа с заграничным паспортом");
            Console.WriteLine("4 - Вычисление логического выражения");
            Console.WriteLine("0 - Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите число: ");
                    string inputNumber = Console.ReadLine();
                    Console.Write("Введите исходную систему (2 или 10): ");
                    int fromBase = int.Parse(Console.ReadLine());
                    Console.Write("Введите целевую систему (2 или 10): ");
                    int toBase = int.Parse(Console.ReadLine());
                    string result = Task1(inputNumber, fromBase, toBase);
                    Console.WriteLine($"Результат: {result}");
                    break;

                case "2":
                    Console.Write("Введите цифру словами: ");
                    string word = Console.ReadLine().ToLower();
                    string digit = Task2(word);
                    Console.WriteLine($"Результат: {digit}");
                    break;

                case "3":
                    try
                    {
                        Console.Write("Введите номер паспорта: ");
                        string passportNumber = Console.ReadLine();
                        Console.Write("Введите ФИО владельца: ");
                        string fullName = Console.ReadLine();
                        Console.Write("Введите дату выдачи: ");
                        DateTime givenDate = DateTime.Parse(Console.ReadLine());
                        Passport passport = new Passport(passportNumber, fullName, givenDate);
                        Console.WriteLine("Паспорт успешно создан");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    break;

                case "4":
                    Console.Write("Введите логическое выражение: ");
                    string expression = Console.ReadLine();

                    bool evalResult = Task4(expression);
                    Console.WriteLine($"Результат: {evalResult}");
                    break;

                case "0":
                    return;
                    
                default:
                    Console.WriteLine("Некорректный выбор");
                    break;
            }
        }
    }

    static string Task1(string input, int fromBase, int toBase)
    {
        if ((fromBase != 2 && fromBase != 10) || (toBase != 2 && toBase != 10))
            throw new ArgumentException("Допустимы только системы 2 и 10");
        int number = Convert.ToInt32(input, fromBase);
        return Convert.ToString(number, toBase);
    }

    static string Task2(string word)
    {
        switch (word)
        {
            case "zero": return "0";
            case "one": return "1";
            case "two": return "2";
            case "three": return "3";
            case "four": return "4";
            case "five": return "5";
            case "six": return "6";
            case "seven": return "7";
            case "eight": return "8";
            case "nine": return "9";
            default: return "Ошибка ввода";
        }
    }

    // TASK3
    class Passport
    {
        public string Number { get; }
        public string FullName { get; }
        public DateTime GivenDate{ get; }

        public Passport(string number, string fullName, DateTime givenDate)
        {
            if (number.Length < 1)
                throw new ArgumentException("Номер паспорта не может быть пустым");

            if (fullName.Length < 1)
                throw new ArgumentException("ФИО не может быть пустым");

            if (givenDate > DateTime.Now)
                throw new ArgumentException("Некорректная дата выдачи");

            Number = number;
            FullName = fullName;
            GivenDate = givenDate;
        }
    }

    static bool Task4(string input)
    {
        try
        {
            string[] operators = { ">=", "<=", "==", "!=", ">", "<" };
            string selectedOperator = null;
            foreach (var op in operators)
            {
                if (input.Contains(op))
                {
                    selectedOperator = op;
                    break;
                }
            }

            if (selectedOperator == null)
                throw new ArgumentException("Некорректное выражение");

            string[] parts = input.Split(selectedOperator);
            if (parts.Length != 2)
                throw new ArgumentException("Ошибка разбора выражения");
            int left = Convert.ToInt32(parts[0]);
            int right = Convert.ToInt32(parts[1]);
            switch (selectedOperator)
            {
                case ">=": return left >= right;
                case "<=": return left <= right;
                case "==": return left == right;
                case "!=": return left != right;
                case ">": return left > right;
                case "<": return left < right;
                default: throw new Exception("Неизвестный оператор");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return false;
        }
    }
}
