namespace lb3_4TEST.Figures;

public class HatchedTriangle : Triangle
{
    //Щільність штрихування (кожен N-й рядок)
    public int HatchDensity { get; set; }

    public HatchedTriangle(double x1, double y1, double x2, double y2,
        double x3, double y3, int density = 2)
        : base(x1, y1, x2, y2, x3, y3)
    {
        HatchDensity = Math.Max(1, density);
    }

    public override string GetName() => "Заштрихований трикутник";

    public override string ToString() =>
        base.ToString() + $", Штрихування = {HatchDensity}";

    public override void Draw(char[,] canvas, int offsetX = 0, int offsetY = 0)
    {
        int minY = (int)Math.Min(Y, Math.Min(Y2, Y3));
        int maxY = (int)Math.Max(Y, Math.Max(Y2, Y3));
        int minX = (int)Math.Min(X, Math.Min(X2, X3));
        int maxX = (int)Math.Max(X, Math.Max(X2, X3));

        for (int py = minY; py <= maxY; py++)
        {
            if ((py - minY) % HatchDensity != 0) continue;
            for (int px = minX; px <= maxX; px++)
            {
                if (IsPointInside(px, py))
                    Plot(canvas, px + offsetX, py + offsetY, '/');
            }
        }

        base.Draw(canvas, offsetX, offsetY);
    }
}
