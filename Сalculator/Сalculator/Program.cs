using System.Reflection.Emit;

namespace Сalculator
{
    internal class Program
    {
        private const OperandType OperandType = new OperandType();

        static void Main(string[] args)
        {

            Console.WriteLine("Введите математическое выражение:");
            string input = Console.ReadLine();
            var newObject = new Operations();
            var result = newObject.ConvertToPRN(input);
            Console.WriteLine($"{result}");
        }
    }
}
