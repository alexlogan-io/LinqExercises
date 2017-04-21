using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var digits = Enumerable.Range(1000,9000)
                .Select(d => d * 6)
                .Select(d => d.ToString())
                .Where(d => d.Length == 4)
                .Where(d => d.FirstAndFinalAreEqual())
                .Where(d => d.SecondAndThirdEqualFirst())
                .Select(d => d.ToInt() / 6)
                .Where(d => d.IsConsecutive())
                .ToList();

            digits.ForEach(d => Console.WriteLine(d));

            Console.ReadLine();
        }
    }

    public static class ExtensionMethods{
        public static bool FirstAndFinalAreEqual(this string value) =>
            value[0] == value[3];
        
        public static bool SecondAndThirdEqualFirst(this string value) =>
            value[1].CharToInt() + value[2].CharToInt() == value[0].CharToInt();
        
        public static bool IsConsecutive(this int value){
            return value.OrderInt()
                        .Aggregate((current, item) => 
                                    (item.ToInt() - current.ToInt() == -1) ? item : ""
                                    )
                        .Any();
        }

        public static IEnumerable<string> OrderInt(this int value){
            return value.ToString()
                        .ToCharArray()
                        .OrderByDescending(c => c.CharToInt())
                        .Select(c => c.ToString());
        }

        public static int ToInt(this string value) =>
            string.IsNullOrEmpty(value) ? 0 : int.Parse(value);

        public static int CharToInt(this char value){
            try{
                return int.Parse(value.ToString());
            }catch(Exception){
                return 0;
            }
        }
    }
}
