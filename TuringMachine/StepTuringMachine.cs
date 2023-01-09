using TuringMachine.Exceptions;

namespace TuringMachine;

public class StepTuringMachine : TuringMachine
{
    private StepTuringMachine? _prevState;

    public StepTuringMachine()
    {
        StatesAlias = new()
        {
            {States.Q0, "Q0" },
            {States.Q1, "Q1"},
            {States.Q2, "Q2"},
            {States.Q3, "Q3"},
            {States.Q4, "Q4"},
            {States.Q5, "Q5"},
            {States.Qf, "Qf"}
        };
    }

    public string? Solution { get; private set; }
    public bool Solved { get; private set; }
    public Dictionary<States, string>? StatesAlias { get; private set; }
    public string CurrentState { get => StatesAlias![_currentSate]; }

    public void NextStep()
    {
        _prevState = new StepTuringMachine()
        {
            Result = this.Result,
            Solved = this.Solved,
            Solution = this.Solution,
            Tape = this.Tape,
            Transitions = this.Transitions,
            _currentAction = _currentAction,
            _prevState = _prevState,
            _currentSate = _currentSate,
            _headIdx = _headIdx,
        };

        if (_currentAction != Action.STOP || _currentSate != States.Qf)
        {
            if (Tape is null) return;
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
        else
        {
            Solution = string.Join(string.Empty, Tape!).Trim('_');
        }
    }

    public void PrevStep()
    {
        //this = this._prevState;
    }

    public void SetToSolve(string inputString)
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

        Solved = false;
    }
}