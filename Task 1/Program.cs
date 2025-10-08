using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1 {
    internal class Program {
        public static string Convert(string source, int baseFrom, int baseTo) {

            // sign related variables
            bool IsPositive = true;

            // prefix related variables
            string prefix = "";
            string[] prefixes = { "0b", "0", "0x" };

            // digit related variables
            bool digitsValid = true;
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };



            // check for sign
            if (source.ToCharArray()[0] == '-') IsPositive = false;
            Console.WriteLine($"Positive: {IsPositive}");

            // check for prefix
            int offset = IsPositive ? 0 : 1;

            // find if any prefix matches substring leading "source"
            foreach (string p in prefixes) {
                if (source.Substring(offset, 2) == p) prefix = p;
            }
            Console.WriteLine($"Prefix: {prefix}");

            // check for valid digits
            char[] validDigits = new char[baseFrom];
            Array.Copy(digits, validDigits, baseFrom);

            // TODO slice prefix and negative off of source
            char[] truncatedValidDigits = validDigits[3..];


            // loop through source and check for digit match
            foreach (char digit in source.ToCharArray()) {
                Console.WriteLine(digit);
                if (!validDigits.Contains(digit)) digitsValid = false; // TODO case sensitive 
            }
            Console.WriteLine($"validDigits: {validDigits}");

            return "";
        }

        public static void Main(string[] args) {

            string source;
            int baseFrom, baseTo;

            // guard clauses / guard loops for arguments

            do {
                Console.WriteLine("Type in Source string");
                source = Console.ReadLine();
                if (source == "") Console.WriteLine("Error: empty source string");
            } while (source == "");

            do {
                Console.WriteLine("Type in Source baseFrom");
                baseFrom = int.Parse(Console.ReadLine());
                if (!(baseFrom <= 16) || !(baseFrom >= 2)) Console.WriteLine("Error: baseFrom out of range");
            } while (!(baseFrom <= 16) || !(baseFrom >= 2));

            do {
                Console.WriteLine("Type in Source baseTo");
                baseTo = int.Parse(Console.ReadLine());
                if (!(baseTo <= 16) || !(baseTo >= 2)) Console.WriteLine("Error: baseTo out of range");
            } while (!(baseTo <= 16) || !(baseTo >= 2));


            Console.WriteLine(Convert(source, baseFrom, baseTo));
        }
    }
}