namespace TuringMachine.UI.Drawables;

internal class TapeCell : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Color.Parse("#dc143c");
        canvas.StrokeSize = 2;
        canvas.DrawRectangle(10, 10, 10, 10);
    }
}