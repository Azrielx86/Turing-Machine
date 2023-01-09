namespace TuringMachine.UI;

public partial class MainPage : ContentPage
{
    private StepTuringMachine tm;

    public MainPage()
    {
        InitializeComponent();
        tm = new StepTuringMachine()
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
    }

    private void OnSolveClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(numberA.Text, out int a) || !int.TryParse(numberB.Text, out int b))
        {
            lblSolution.Text = "Cannot solve";
        }
        else
        {
            tm.SetToSolve($"{Convert.ToString(a, 2)}+{Convert.ToString(b, 2)}");
            btnNext.IsEnabled = true;
        }
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        tm.NextStep();
        lblCurrentState.Text = tm.CurrentState;
        lblTapeState.Text = string.Join(string.Empty, tm.Tape);
        lblSolution.Text = tm.Solution is null ? "Solving..." :$"Result: {Convert.ToInt32(tm.Solution, 2)}";
    }

    private void OnPrevClicked(object sender, EventArgs e)
    {

    }
}