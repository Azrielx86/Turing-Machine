namespace TuringMachine
{
    public enum Action
    {
        LEFT, RIGHT, STOP
    }

    public enum Result
    {
        ACCEPT, REJECT
    }

    public enum States
    {
        Q0, Q1, Q2, Q3, Q4, Q5, Qf
    }

    public class InvalidSymbolException : Exception
    {
        public override string Message => "Symbol not in Turing Machine language.";
    }

    public class TuringMachine
    {
        private readonly char[] _symbols = { '0', '1', '_' };
        private readonly Dictionary<(States, char), (States, char, Action)> _transitions;
        private Action _currentAction;
        private States _currentSate;
        private int _head;
        private List<char> _tape;
        public TuringMachine()
        {
            _currentSate = States.Q0;
            _transitions = new()
            {
                // Movimiento del cabezal al final del primer número
                {(States.Q0, '0'),(States.Q0, '0', Action.RIGHT)},
                {(States.Q0, '1'),(States.Q0, '1', Action.RIGHT)},
                {(States.Q0, '_'),(States.Q1, '_', Action.RIGHT)},

                // Movimiento del cabezal al final del segundo número
                {(States.Q1, '0'),(States.Q1, '0', Action.RIGHT)},
                {(States.Q1, '1'),(States.Q1, '1', Action.RIGHT)},
                {(States.Q1, '_'),(States.Q2, '_', Action.LEFT)},

                // Substacción de uno en binario
                {(States.Q2, '0'),(States.Q2, '1', Action.LEFT)},
                {(States.Q2, '1'),(States.Q3, '0', Action.LEFT)},
                {(States.Q2, '_'),(States.Q5, '_', Action.RIGHT)},

                // Movimiento a la izquierda del primer bloque
                {(States.Q3, '0'),(States.Q3, '0', Action.LEFT)},
                {(States.Q3, '1'),(States.Q3, '1', Action.LEFT)},
                {(States.Q3, '_'),(States.Q4, '_', Action.LEFT)},

                // Suma de uno en binario
                {(States.Q4, '0'),(States.Q0, '1', Action.RIGHT)},
                {(States.Q4, '1'),(States.Q4, '0', Action.LEFT)},
                {(States.Q4, '_'),(States.Q0, '1', Action.RIGHT)},

                // Limpieza
                {(States.Q5, '1'),(States.Q5, '_', Action.RIGHT)},
                {(States.Q5, '_'),(States.Qf, '_', Action.STOP)},
            };
            _tape = Enumerable.Repeat('_', 10).ToList();
        }

        public string Solve(string inputString)
        {
            _tape = Enumerable.Repeat('_', 10).ToList();
            _tape.AddRange(new List<char>(inputString));
            _tape.AddRange(Enumerable.Repeat('_', 10).ToList());
            _tape.ForEach(i =>
            {
                if (!_symbols.Contains(i))
                    throw new InvalidSymbolException();
            });

            _currentSate = States.Q0;
            _currentAction = Action.RIGHT;

            _head = _tape.IndexOf(_tape.FirstOrDefault(c => c != '_'));
            while (_currentAction != Action.STOP && _currentSate != States.Qf)
            {
                (States, char, Action) transition = _transitions[(_currentSate, _tape[_head])];

                _currentAction = transition.Item3;
                _tape[_head] = transition.Item2;
                _currentSate = transition.Item1;

                switch (_currentAction)
                {
                    case Action.RIGHT:
                        _head++;
                        break;

                    case Action.LEFT:
                        _head--;
                        break;

                    case Action.STOP:
                        break;
                }
            }

            string output = string.Join(string.Empty, _tape).Trim('_');
            return output;
        }
    }
}