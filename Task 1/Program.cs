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

            // sign related variables
            bool IsPositive = true;

            // prefix related variables
            string prefix = "";
            string[] prefixes = { "0b", "0x", "0" };

            // digit related variables
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
                    return "Error (unrecognized digit)";    //TODO case sensitive
                }
            }

            // CONVERSION

            // multiply digit w coresponding power of baseFrom and add as decimal
            for (int i = 0; i < sourceChars.Length - (offset + prefix.Length); i++ ) {
                decimalRepresentation += int.Parse(Array.IndexOf(digits, sourceChars[sourceChars.Length - i - 1]).ToString()) * (int) Math.Pow((double) baseFrom, (double) i);
            }

            if (!IsPositive) decimalRepresentation = -decimalRepresentation;
            if (baseTo == 10) return decimalRepresentation.ToString();

            Stack<Char> baseToStack = new Stack<Char>();
            int rest = decimalRepresentation;
            int counter = 0;

            while (rest != 0) {
                baseToStack.Push(digits[rest % baseTo]);
                rest = rest / baseTo;
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