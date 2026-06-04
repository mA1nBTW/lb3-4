namespace lb3_4TEST.Figures;

public class Point : Figure
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x = 0, double y = 0) { X = x; Y = y; }

    public override double Area => 0;
    public override double Perimeter => 0;

    public double DistanceToOrigin => Math.Sqrt(X * X + Y * Y);

    public override void Move(double dx, double dy) { X += dx; Y += dy; }

    public override void Scale(double factor) { X *= factor; Y *= factor; }

    //Відстань до іншої точки
    public double DistanceTo(Point other) =>
        Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));

    public override string GetName() => "Точка";

    public override string ToString() =>
        $"{GetName()}: ({X:F2}; {Y:F2})";

    public override void Draw(char[,] canvas, int offsetX = 0, int offsetY = 0)
    {
        Plot(canvas, (int)X + offsetX, (int)Y + offsetY, '@');
    }
}
