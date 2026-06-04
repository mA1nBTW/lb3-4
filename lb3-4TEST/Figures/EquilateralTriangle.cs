namespace lb3_4TEST.Figures;

//Правильний трикутник задається центром та довжиною сторони.
public class EquilateralTriangle : Triangle
{
    private static readonly double Sqrt3 = Math.Sqrt(3);

    public EquilateralTriangle(double centerX, double centerY, double side)
        : base(
            centerX,            centerY - side * Sqrt3 / 3, 
            centerX - side / 2, centerY + side * Sqrt3 / 6, 
            centerX + side / 2, centerY + side * Sqrt3 / 6)
    { }

    //Конструктор за трьома вершинами (для десеріалізації)
    internal EquilateralTriangle(double x1, double y1, double x2, double y2, double x3, double y3)
        : base(x1, y1, x2, y2, x3, y3) { }

    public double Side => SideA;

    //Формула площі правильного трикутника
    public override double Area => Side * Side * Sqrt3 / 4;

    public override string GetName() => "Правильний трикутник";
}
