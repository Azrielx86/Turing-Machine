using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace TuringMachine.UI;

public partial class MainPage : ContentPage
{
    private StepTuringMachine tm;
#if ANDROID
    private float _cellSize = 140;
#else
    private float _cellSize = 60;
#endif

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
            lblCurrentState.Text = $"Estado actual: {tm.CurrentState}";
            lblSolution.Text = tm.Solution is null || !tm.Solved ? "Resolviendo..." : $"Resultado: {tm.Solution} (binario) = {Convert.ToInt32(tm.Solution, 2)}";
            canvasView.InvalidateSurface();
        }
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        tm.NextStep();
        lblCurrentState.Text = $"Estado actual: {tm.CurrentState}";
        lblSolution.Text = tm.Solution is null || !tm.Solved ? "Resolviendo..." : $"Resultado: {tm.Solution} (binario) = {Convert.ToInt32(tm.Solution, 2)}";
        canvasView.InvalidateSurface();
    }

    private void OnPrevClicked(object sender, EventArgs e)
    {
    }

    private void OnCanvasPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        SKColor color = SKColor.Parse("#1a1a1a");
        e.Surface.Canvas.Clear(color);

        if (tm.Tape is null) return;

        for (int i = 0; i < tm.Tape.Count; i++)
        {
            SKPaint skPaint = new();
            skPaint.Style = i == tm.CurrentPosition ? SKPaintStyle.Fill : SKPaintStyle.Stroke;
            skPaint.Color = SKColor.Parse("#26a69a");
            skPaint.StrokeWidth = 2;

            var drawPoint = new SKPoint(i * _cellSize - (tm.CurrentPosition * _cellSize) + (float)(canvasView.Width) + (_cellSize / 2), (float)(canvasView.HeightRequest / 2));

            SKRect rectangle = new()
            {
                Size = new SKSize(_cellSize, _cellSize),
                Location = drawPoint
            };

            float textMove = DeviceInfo.Current.Platform == DevicePlatform.WinUI ? 50 : 100;

            e.Surface.Canvas.DrawRect(rectangle, skPaint);
            e.Surface.Canvas.DrawText(tm.Tape[i].ToString(),
                new SKPoint((i * _cellSize) - (tm.CurrentPosition * _cellSize) + (float)(canvasView.Width) + textMove + (_cellSize / 2) - 3, (float)(canvasView.HeightRequest / 2) + textMove),
                new SKPaint() { Color = SKColor.Parse("#ffffff"), TextSize = DeviceInfo.Current.Platform == DevicePlatform.WinUI ? 40 : 80, TextAlign = SKTextAlign.Center });
        }
    }
}