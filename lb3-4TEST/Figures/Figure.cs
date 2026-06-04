namespace lb3_4TEST.Figures;

//Абстрактний базовий клас Фігура
public abstract class Figure
{
    public abstract double Area { get; }
    public abstract double Perimeter { get; }

    public abstract void Move(double dx, double dy);
    public abstract void Scale(double factor);
    public abstract void Draw(char[,] canvas, int offsetX = 0, int offsetY = 0);

    public virtual string GetName() => "Фігура";

    public override string ToString() =>
        $"{GetName()}: Площа = {Area:F2}, Периметр = {Perimeter:F2}";


    //Створити порожній канвас заданого розміру
    public static char[,] CreateCanvas(int width = 60, int height = 25)
    {
        var canvas = new char[height, width];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                canvas[y, x] = ' ';
        return canvas;
    }

    //Вивести канвас у консоль з рамкою
    public static void PrintCanvas(char[,] canvas)
    {
        int h = canvas.GetLength(0), w = canvas.GetLength(1);
        Console.WriteLine("+" + new string('-', w) + "+");
        for (int y = 0; y < h; y++)
        {
            Console.Write('|');
            for (int x = 0; x < w; x++)
                Console.Write(canvas[y, x]);
            Console.WriteLine('|');
        }
        Console.WriteLine("+" + new string('-', w) + "+");
    }

    //Поставити символ на канвасі (з перевіркою меж)
    protected static void Plot(char[,] canvas, int x, int y, char c = '*')
    {
        if (y >= 0 && y < canvas.GetLength(0) && x >= 0 && x < canvas.GetLength(1))
            canvas[y, x] = c;
    }

    //Алгоритм Брезенхема для малювання лінії
    protected static void DrawLine(char[,] canvas, int x0, int y0, int x1, int y1, char c = '*')
    {
        int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = dx + dy;
        while (true)
        {
            Plot(canvas, x0, y0, c);
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 >= dy) { err += dy; x0 += sx; }
            if (e2 <= dx) { err += dx; y0 += sy; }
        }
    }
}
