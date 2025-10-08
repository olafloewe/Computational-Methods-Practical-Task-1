using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1 {
    internal class Program {
        public static string Convert(string source, int baseFrom, int baseTo) {

            char[] sourceChars = source.ToCharArray();
            double decimalRepresentation = 0;

            // sign related variables
            bool IsPositive = true;

            // prefix related variables
            string prefix = "";
            string[] prefixes = { "0b", "0x", "0" };

            // digit related variables
            bool DigitsValid = true;
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };



            // SIGN CHECK
            if (sourceChars[0] == '-') IsPositive = false;

            // PREFIX CHECK
            int offset = IsPositive ? 0 : 1;

            // find if any prefix matches substring leading "source"
            foreach (string p in prefixes) {
                if (source.Substring(offset, p.Length) == p) {
                    prefix = p;
                    break;
                }
            }

            if (prefix == "0" && baseFrom != 8) return "Error (source base ambiguous)";
            if (prefix == "0b" && baseFrom != 2) return "Error (source base ambiguous)";
            if (prefix == "0x" && baseFrom != 16) return "Error (source base ambiguous)";

            // DIGIT VALIDITY CHECK

            // get valid digits from all possible digits
            char[] validDigits = new char[baseFrom];
            Array.Copy(digits, validDigits, baseFrom);

            // loop through source checking for validity of digits
            for (int i = (offset + prefix.Length); i < sourceChars.Length; i++) {
                // digit is not valid / not found
                if (!validDigits.Contains(sourceChars[i])) {
                    DigitsValid = false; // TODO case sensitive 
                    return "Error (unrecognized digit)";
                }
                // Console.WriteLine(sourceChars[i]);
            }

            // CONVERSION

            for (int i = 0; i < sourceChars.Length - (offset + prefix.Length); i++ ) {
                Console.WriteLine($"I: {i}");
                decimalRepresentation += double.Parse(Array.IndexOf(digits, sourceChars[sourceChars.Length - i - 1]).ToString()) * Math.Pow((double) baseFrom, (double) i);
            }



            /*
            Console.WriteLine($"Positive: {IsPositive}");
            Console.WriteLine($"Prefix: {prefix}");
            Console.WriteLine($"SLICE {offset + prefix.Length}");
            Console.WriteLine("validDigits: ");
            foreach (char digit in validDigits) {
                Console.Write(digit);
            }
            Console.WriteLine();
            Console.WriteLine($"DigitsValid: {DigitsValid}");
            Console.WriteLine($"Decimal: {decimalRepresentation}");
            */

            return "";
        }

        public static void Main(string[] args) {

            string source = "";
            int baseFrom, baseTo;

            // guard clauses / guard loops for arguments

            do {
                Console.WriteLine("Type in Source string");
                source = Console.ReadLine();
                if (source == "") Console.WriteLine("Error: faulty source string");
            } while (source == "" || source.Length >= 255);

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
            
            // HOLD THE LINE (terminal window) !!!
            Console.ReadLine();
        }
    }
}