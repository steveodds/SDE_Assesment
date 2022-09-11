using System.Numerics;

namespace Exercise01
{
    public static class NumberExtensions
    {
        private static readonly string[] singleNumbers = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] tens = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        // handle various numeric types
        public static string Towards(this int userInt)
        {
            var bigInteger = new BigInteger(userInt);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        public static string Towards(this uint userUInt)
        {
            var bigInteger = new BigInteger(userUInt);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        public static string Towards(this long userLong)
        {
            var bigInteger = new BigInteger(userLong);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        public static string Towards(this ulong userULong)
        {
            var bigInteger = new BigInteger(userULong);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        public static string Towards(this float userFloat)
        {
            var bigInteger = new BigInteger(userFloat);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }
        public static string Towards(this double userDouble)
        {
            var bigInteger = new BigInteger(userDouble);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        public static string Towards(this decimal userDecimal)
        {
            var bigInteger = new BigInteger(userDecimal);
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }
        public static string Towards(this BigInteger bigInteger)
        {
            return NumberAsWords(bigInteger).TrimEnd(',', ' ');
        }

        // Algo for converting to words
        private static string NumberAsWords(BigInteger number)
        {
            // handle negatives
            if (number < 0)
                return "negative " + NumberAsWords(BigInteger.Abs(number));

            string words = "";

            // For number groups / classes e.g. million
            // Loses precision at septillion

            if ((number / new BigInteger(1e21)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1e21)) + " sextillion, ";
                number %= new BigInteger(1e21);
            }

            if ((number / new BigInteger(1e18)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1e18)) + " quintillion, ";
                number %= new BigInteger(1e18);
            }

            if ((number / new BigInteger(1e15)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1e15)) + " quadrillion, ";
                number %= new BigInteger(1e15);
            }

            if ((number / new BigInteger(1e12)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1e12)) + " trillion, ";
                number %= new BigInteger(1e12);
            }

            if ((number / new BigInteger(1000000000)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1000000000)) + " billion, ";
                number %= new BigInteger(1000000000);
            }

            if ((number / new BigInteger(1000000)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1000000)) + " million, ";
                number %= new BigInteger(1000000);
            }

            if ((number / new BigInteger(1000)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(1000)) + " thousand, ";
                number %= new BigInteger(1000);
            }

            if ((number / new BigInteger(100)) > 0)
            {
                words += NumberAsWords(number / new BigInteger(100)) + " hundred ";
                number %= 100;
            }

            // For individualistic numbers e.g. twenty
            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += singleNumbers[(int)number];
                else
                {
                    words += tens[(int)number / 10];
                    if ((number % 10) > 0)
                        words += "-" + singleNumbers[(int)number % 10];
                }
            }

            return words;
        }
    }
}