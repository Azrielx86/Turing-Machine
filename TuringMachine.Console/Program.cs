namespace TuringMachine;

public class Program
{
    public static void Main(string[] args)
    {
        var tm = new TuringMachine()
        {
            Transitions = new()
            {
                // Movimiento del cabezal al final del primer número
                {(States.Q0, '_'),(States.Q0, '_', Action.RIGHT)},
                {(States.Q0, '0'),(States.Q0, '0', Action.RIGHT)},
                {(States.Q0, '1'),(States.Q0, '1', Action.RIGHT)},
                {(States.Q0, '+'),(States.Q1, '+', Action.RIGHT)},

                // Movimiento del cabezal al final del segundo número
                {(States.Q1, '0'),(States.Q1, '0', Action.RIGHT)},
                {(States.Q1, '1'),(States.Q1, '1', Action.RIGHT)},
                {(States.Q1, '_'),(States.Q2, '_', Action.LEFT)},

                // Substacción de uno en binario
                {(States.Q2, '0'),(States.Q2, '1', Action.LEFT)},
                {(States.Q2, '1'),(States.Q3, '0', Action.LEFT)},
                {(States.Q2, '+'),(States.Q5, '+', Action.RIGHT)},

                // Movimiento a la izquierda del primer bloque
                {(States.Q3, '0'),(States.Q3, '0', Action.LEFT)},
                {(States.Q3, '1'),(States.Q3, '1', Action.LEFT)},
                {(States.Q3, '+'),(States.Q4, '+', Action.LEFT)},

                // Suma de uno en binario
                {(States.Q4, '0'),(States.Q0, '1', Action.RIGHT)},
                {(States.Q4, '1'),(States.Q4, '0', Action.LEFT)},
                {(States.Q4, '_'),(States.Q0, '1', Action.RIGHT)},

                // Limpieza
                {(States.Q5, '1'),(States.Q5, '_', Action.RIGHT)},
                {(States.Q5, '_'),(States.Q5, '_', Action.LEFT)},
                {(States.Q5, '+'),(States.Qf, '_', Action.STOP)},
            }
        };

        Console.Write("Ingresa el número a: ");
        _ = int.TryParse(Console.ReadLine(), out var a);
        Console.Write("Ingresa el número b: ");
        _ = int.TryParse(Console.ReadLine(), out var b);

        var strResult = tm.Solve($"{Convert.ToString(a, 2)}+{Convert.ToString(b, 2)}");
        var result = Convert.ToInt32(strResult, 2);
        Console.WriteLine($"Resultado: {result}");
    }
}