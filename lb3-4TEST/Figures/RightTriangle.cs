namespace lb3_4TEST.Figures;

//Прямокутний трикутник задається двома катетами
public class RightTriangle : Triangle
{
    //Основний конструктор: перша вершина + довжини катетів
    public RightTriangle(double x, double y, double legA, double legB)
        : base(x, y, x + legA, y, x, y + legB) { }

    //Конструктор за трьома вершинами (для десеріалізації)
    internal RightTriangle(double x1, double y1, double x2, double y2, double x3, double y3)
        : base(x1, y1, x2, y2, x3, y3) { }

    public double LegA => SideA;
    public double LegB => SideC;
    public double Hypotenuse => SideB;

    //Теорема Піфагора
    public bool IsRight
    {
        get
        {
            double[] sides = [SideA, SideB, SideC];
            Array.Sort(sides);
            return Math.Abs(sides[2] * sides[2] - sides[0] * sides[0] - sides[1] * sides[1]) < 0.01;
        }
    }

    public override string GetName() => "Прямокутний трикутник";

    public override string ToString() =>
        base.ToString() + $", Гіпотенуза = {Hypotenuse:F2}, Прямий кут: {IsRight}";
}
