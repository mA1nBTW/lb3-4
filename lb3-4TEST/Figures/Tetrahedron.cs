namespace lb3_4TEST.Figures;

public class Tetrahedron : Triangle
{
    public double Height { get; set; }

    public Tetrahedron(double x1, double y1, double x2, double y2,
        double x3, double y3, double height)
        : base(x1, y1, x2, y2, x3, y3)
    {
        Height = height;
    }

    //Площа основи
    public double BaseArea => base.Area;

    //Об'єм
    public double Volume => BaseArea * Height / 3;

    //Повна площа поверхні
    public override double Area
    {
        get
        {
            //Апекс знаходиться над центроїдом
            double cx = (X + X2 + X3) / 3, cy = (Y + Y2 + Y3) / 3;

            //Відстані від апекса до вершин основи
            double d1 = Math.Sqrt(Sq(cx - X)  + Sq(cy - Y)  + Sq(Height));
            double d2 = Math.Sqrt(Sq(cx - X2) + Sq(cy - Y2) + Sq(Height));
            double d3 = Math.Sqrt(Sq(cx - X3) + Sq(cy - Y3) + Sq(Height));

            //Три бічні грані
            double lateral = HeronArea(SideA, d1, d2)
                           + HeronArea(SideB, d2, d3)
                           + HeronArea(SideC, d3, d1);

            return BaseArea + lateral;
        }
    }

    public override double Perimeter => base.Perimeter; //периметр основи

    public override string GetName() => "Тетраедр";

    public override string ToString() =>
        $"{GetName()}: Основа ({X:F1}; {Y:F1})-({X2:F1}; {Y2:F1})-({X3:F1}; {Y3:F1}), " +
        $"Висота = {Height:F2}, Об'єм = {Volume:F2}, Площа поверхні = {Area:F2}";

    //Малювання ізометричної проекції тетраедра
    public override void Draw(char[,] canvas, int offsetX = 0, int offsetY = 0)
    {
        //Основа
        base.Draw(canvas, offsetX, offsetY);

        //Апекс — проекція: зміщений вгору і трохи вправо (ізометрія)
        double cx = (X + X2 + X3) / 3;
        double cy = (Y + Y2 + Y3) / 3;
        int apexX = (int)cx + offsetX + (int)(Height / 4);
        int apexY = (int)cy + offsetY - (int)(Height / 2);

        //Ребра від апекса до вершин основи
        DrawLine(canvas, apexX, apexY, (int)X  + offsetX, (int)Y  + offsetY, '.');
        DrawLine(canvas, apexX, apexY, (int)X2 + offsetX, (int)Y2 + offsetY, '.');
        DrawLine(canvas, apexX, apexY, (int)X3 + offsetX, (int)Y3 + offsetY, '.');

        //Позначити апекс
        Plot(canvas, apexX, apexY, '+');
    }

    private static double Sq(double v) => v * v;

    private static double HeronArea(double a, double b, double c)
    {
        double s = (a + b + c) / 2;
        double val = s * (s - a) * (s - b) * (s - c);
        return val > 0 ? Math.Sqrt(val) : 0;
    }
}
