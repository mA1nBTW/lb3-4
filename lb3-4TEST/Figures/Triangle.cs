namespace lb3_4TEST.Figures;

public class Triangle : Point
{
    public double X2 { get; set; }
    public double Y2 { get; set; }
    public double X3 { get; set; }
    public double Y3 { get; set; }

    public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        : base(x1, y1)
    {
        X2 = x2; Y2 = y2;
        X3 = x3; Y3 = y3;
    }

    public double SideA => Math.Sqrt(Math.Pow(X2 - X, 2) + Math.Pow(Y2 - Y, 2));
    public double SideB => Math.Sqrt(Math.Pow(X3 - X2, 2) + Math.Pow(Y3 - Y2, 2));
    public double SideC => Math.Sqrt(Math.Pow(X - X3, 2) + Math.Pow(Y - Y3, 2));

    public override double Perimeter => SideA + SideB + SideC;

    public override double Area
    {
        get
        {
            double s = Perimeter / 2;
            double val = s * (s - SideA) * (s - SideB) * (s - SideC);
            return val > 0 ? Math.Sqrt(val) : 0;
        }
    }

    //Індексатор
    public (double x, double y) this[int index]
    {
        get => index switch
        {
            0 => (X, Y),
            1 => (X2, Y2),
            2 => (X3, Y3),
            _ => throw new IndexOutOfRangeException("Індекс вершини: 0, 1 або 2")
        };
        set
        {
            switch (index)
            {
                case 0: X = value.x; Y = value.y; break;
                case 1: X2 = value.x; Y2 = value.y; break;
                case 2: X3 = value.x; Y3 = value.y; break;
                default: throw new IndexOutOfRangeException("Індекс вершини: 0, 1 або 2");
            }
        }
    }

    public override void Move(double dx, double dy)
    {
        base.Move(dx, dy);
        X2 += dx; Y2 += dy;
        X3 += dx; Y3 += dy;
    }

    public override void Scale(double factor)
    {
        double cx = (X + X2 + X3) / 3, cy = (Y + Y2 + Y3) / 3;
        X  = cx + (X  - cx) * factor; Y  = cy + (Y  - cy) * factor;
        X2 = cx + (X2 - cx) * factor; Y2 = cy + (Y2 - cy) * factor;
        X3 = cx + (X3 - cx) * factor; Y3 = cy + (Y3 - cy) * factor;
    }

    public void MoveTo(double newX, double newY)
    {
        double cx = (X + X2 + X3) / 3, cy = (Y + Y2 + Y3) / 3;
        Move(newX - cx, newY - cy);
    }

    public override string GetName() => "Трикутник";

    public override string ToString() =>
        $"{GetName()}: ({X:F1}; {Y:F1})-({X2:F1}; {Y2:F1})-({X3:F1}; {Y3:F1}), " +
        $"Сторони: {SideA:F2}, {SideB:F2}, {SideC:F2}, " +
        $"Площа = {Area:F2}, Периметр = {Perimeter:F2}";

    public override void Draw(char[,] canvas, int offsetX = 0, int offsetY = 0)
    {
        int px1 = (int)X + offsetX,  py1 = (int)Y + offsetY;
        int px2 = (int)X2 + offsetX, py2 = (int)Y2 + offsetY;
        int px3 = (int)X3 + offsetX, py3 = (int)Y3 + offsetY;

        DrawLine(canvas, px1, py1, px2, py2, '*');
        DrawLine(canvas, px2, py2, px3, py3, '*');
        DrawLine(canvas, px3, py3, px1, py1, '*');
    }

    //Перевірка точки всередині трикутника
    protected bool IsPointInside(double px, double py)
    {
        double d1 = CrossSign(px, py, X, Y, X2, Y2);
        double d2 = CrossSign(px, py, X2, Y2, X3, Y3);
        double d3 = CrossSign(px, py, X3, Y3, X, Y);
        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);
        return !(hasNeg && hasPos);
    }

    private static double CrossSign(double px, double py,
        double x1, double y1, double x2, double y2) =>
        (px - x2) * (y1 - y2) - (x1 - x2) * (py - y2);
}
