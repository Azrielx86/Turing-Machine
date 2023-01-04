using System;

namespace TuringMachine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tm = new TuringMachine();

            Console.Write("Ingresa el número a: ");
            _ = int.TryParse(Console.ReadLine(), out var a);
            Console.Write("Ingresa el número b: ");
            _ = int.TryParse(Console.ReadLine(), out var b);

            var strResult = tm.Solve($"{Convert.ToString(a, 2)}_{Convert.ToString(b, 2)}");
            var result = Convert.ToInt32(strResult, 2);
            Console.WriteLine($"Resultado: {result}");
        }
    }
}