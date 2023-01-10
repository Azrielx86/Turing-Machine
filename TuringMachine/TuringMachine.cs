using TuringMachine.Exceptions;

namespace TuringMachine;

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

public class TuringMachine
{
    protected readonly System.Timers.Timer _internalTimer;
    protected readonly char[] _symbols = { '0', '1', '+', '_' };
    protected Action _currentAction;
    protected States _currentSate;
    protected int _headIdx;
    public TuringMachine()
    {
        _currentSate = States.Q0;
        _internalTimer = new(10000)
        {
            AutoReset = false
        };
        _internalTimer.Elapsed += (s, e) =>
        {
            throw new TimeExceededException();
        };
        Result = Result.ACCEPT;
    }

    public Result Result { get; protected set; }
    public List<char>? Tape { get; protected set; }
    public Dictionary<(States, char), (States, char, Action)>? Transitions { get; set; }
    public int CurrentPosition { get => _headIdx; }

    public string Solve(string inputString)
    {
        Tape = Enumerable.Repeat('_', 10).ToList();
        Tape.AddRange(new List<char>(inputString));
        Tape.AddRange(Enumerable.Repeat('_', 10).ToList());
        Tape.ForEach(i =>
        {
            if (!_symbols.Contains(i))
                throw new InvalidSymbolException();
        });

        _currentSate = States.Q0;
        _currentAction = Action.RIGHT;

        _headIdx = Tape.IndexOf(Tape.FirstOrDefault(c => c != '_'));
        try
        {
            _internalTimer.Start();

            while (_currentAction != Action.STOP || _currentSate != States.Qf)
            {
                (States, char, Action) transition = Transitions![(_currentSate, Tape[_headIdx])];

                _currentAction = transition.Item3;
                Tape[_headIdx] = transition.Item2;
                _currentSate = transition.Item1;

                switch (_currentAction)
                {
                    case Action.RIGHT:
                        _headIdx++;
                        break;

                    case Action.LEFT:
                        _headIdx--;
                        break;

                    case Action.STOP:
                        break;
                }
            }

            _internalTimer.Stop();
        }
        catch (TimeExceededException)
        {
            Result = Result.REJECT;
            return "0";
        }
        catch(Exception)
        {
            return "0";
        }

        return string.Join(string.Empty, Tape).Trim('_');
    }
}