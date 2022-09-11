using Exercise01;
using System.Numerics;

namespace Exercise02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string numberAsWords;
            Console.WriteLine("Enter any number:");
            string userValue = Console.ReadLine() ?? "";

            // mild processing to improve conversion success
            userValue = userValue.Replace(",", "");
            userValue = userValue.Replace(" ", "");

            if (BigInteger.TryParse(userValue, out BigInteger userBigIntegerValue))
                numberAsWords = userBigIntegerValue.Towards();
            else // for outliers
                numberAsWords = "Could not detect number in input.";

            // Display result
            Console.WriteLine(numberAsWords);
            Console.ReadKey();
        }
    }
}