using System;

namespace WordToNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the word to number program!");
            while (true)
            {
                Console.WriteLine();
                Console.Write("Do you want to convert a number in text to number. Enter Y/N: ");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.KeyChar == 'Y')
                {
                    // Convert the text to number
                    ConvertToNumber();
                }
                else
                {
                    MathOperation();
                }
            }
        }

        /// <summary>
        /// The math operation.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static void MathOperation()
        {
            Console.Write("Do you want do math operation. Enter Y/N: ");
            var key1 = Console.ReadKey();
            if (key1.KeyChar == 'Y')
            {
                // Convert the text to number
                Console.WriteLine();
                Console.WriteLine("Enter the first number in text");
                var input = Console.ReadLine();
                var converter = new StringToNumberConverter();
                if (!converter.IsValidNumber(input))
                {
                    Console.WriteLine("Please enter a valid number:");
                    return;
                }

                var firstNumber = converter.ConvertTo(input);
                Console.WriteLine("Enter the math operation: add, minus, multiply, divide");
                var operation = Console.ReadLine();
                if (operation != "add" && operation != "multiply" && operation != "divide" && operation != "minus")
                {
                    Console.WriteLine("Enter the valid math operation: add, minus, multiply, divide");
                    return;
                }

                Console.WriteLine("Enter the second number in text");
                var input2 = Console.ReadLine();

                if (!converter.IsValidNumber(input2))
                {
                    Console.WriteLine("Please enter a valid number:");
                    return;
                }

                var secondNumber = converter.ConvertTo(input2);
                if (operation == "divide" && secondNumber == 0)
                {
                    Console.WriteLine("Cannot divide by zero.");
                    return;
                }

                long result = 0;
                switch (operation)
                {
                    case "add":
                        result = firstNumber + secondNumber;
                        break;
                    case "minus":
                        result = firstNumber - secondNumber;
                        break;
                    case "multiply":
                        result = firstNumber * secondNumber;
                        break;
                    case "divide":
                        result = firstNumber / secondNumber;
                        break;
                }

                Console.WriteLine($"Result of the math operation is {result}");
            }
        }

        /// <summary>
        /// The convert to number.
        /// </summary>
        private static void ConvertToNumber()
        {
            Console.WriteLine("Please enter a valid number in words:");
            var input = Console.ReadLine();
            try
            {
                var converter = new StringToNumberConverter();
                if (!converter.IsValidNumber(input))
                {
                    Console.WriteLine("Please enter a valid number:");
                    return;
                }

                var output = converter.ConvertTo(input);
                Console.WriteLine($"Number format of the number in word is: {output}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Please enter valid text: ");
            }
        }
    }
}
