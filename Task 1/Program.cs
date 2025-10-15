using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1 {
    internal class Program {
        public static string Convert(string source, int baseFrom, int baseTo) {

            char[] sourceChars = source.ToCharArray();
            int decimalRepresentation = 0;
            String finalNum = "";
            int power = 0;

            // prefix related variables
            string prefix = "";
            string[] prefixes = { "0b", "0x", "0" };

            // digit related variables
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int[] values = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // SIGN CHECK
            bool IsPositive = (sourceChars[0] == '-') ? false : true;

            // PREFIX CHECK
            int offset = (IsPositive) ? 0 : 1;

            // find if any prefix matches substring leading "source"
            foreach (string p in prefixes) {
                if (source.Substring(offset, p.Length) == p) {
                    prefix = p;
                    break;
                }
            }
            
            // Prefix base guard clauses
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
                    return "Error (unrecognized digit)";
                }
            }

            // CONVERSION

            // multiply digit with coresponding power of baseFrom and add as decimal
            for (int i = 0; i < sourceChars.Length - (offset + prefix.Length); i++ ) {
                // memoization of power
                power = (power == 0)? 1 : power * baseFrom;
                int value = 0;

                // find value of a digit by matching its index to index of char in digits array
                for (int j = 0; j < values.Length; j++) {
                    if ($"{sourceChars[sourceChars.Length - i - 1]}" == $"{digits[digits.Length - j - 1]}") {
                        value = values[values.Length - j - 1];
                        break;
                    }
                }
                decimalRepresentation += (value * power);
            }

            // FINALIZATION

            if (!IsPositive) decimalRepresentation = -decimalRepresentation;
            if (baseTo == 10) return decimalRepresentation.ToString();

            Stack<Char> baseToStack = new Stack<Char>();
            int remainder = decimalRepresentation;
            int counter = 0;

            while (remainder != 0){
                baseToStack.Push(digits[remainder % baseTo]);
                remainder = remainder / baseTo;
                counter++;
            }

            while (baseToStack.Count() != 0){
                finalNum = $"{finalNum}{baseToStack.Pop()}";
            }

            return finalNum;
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

/* 
 * can use toString???
 * can use toCharArray
 * 
 * maybe array contains / index of
 * maybe substring
 * 
 * no int.parse
 * no math.pow
 * no indexOf
*/