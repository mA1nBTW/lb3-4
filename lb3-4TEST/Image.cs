using System.Globalization;
using lb3_4TEST.Figures;

namespace lb3_4TEST;

/// <summary>
/// Клас «Зображення» — має положення, розмір та колекцію фігур.
/// Підтримує збереження/завантаження з файлу.
/// </summary>
public class Image
{
    // ── Поля ───────────────────────────────────────────
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }

    private readonly List<Figure> figures = new();

    // ── Конструктор ────────────────────────────────────
    public Image(double x = 0, double y = 0, double width = 60, double height = 25)
    {
        X = x; Y = y; Width = width; Height = height;
    }

    // ── Властивості ────────────────────────────────────
    public int Count => figures.Count;

    // ── Індексатор ─────────────────────────────────────
    public Figure this[int index]
    {
        get => figures[index];
        set => figures[index] = value;
    }

    // ── Методи роботи з колекцією ─────────────────────
    public void Add(Figure f) => figures.Add(f);
    public void Remove(Figure f) => figures.Remove(f);
    public void Clear() => figures.Clear();

    // ── Методи переміщення ─────────────────────────────

    /// <summary>Перемістити всі фігури всередині зображення.</summary>
    public void MoveAllFigures(double dx, double dy)
    {
        foreach (var f in figures) f.Move(dx, dy);
    }

    /// <summary>Перемістити саме зображення.</summary>
    public void MoveImage(double dx, double dy) { X += dx; Y += dy; }

    // ── Масштабування ──────────────────────────────────

    /// <summary>
    /// Масштабувати зображення зі збереженням пропорцій.
    /// Змінює розмір та масштабує всі фігури відносно центру.
    /// </summary>
    public void ScaleImage(double factor)
    {
        double cx = Width / 2, cy = Height / 2;
        Width *= factor;
        Height *= factor;

        foreach (var f in figures)
        {
            // Перемістити відносно центру, масштабувати, повернути
            f.Move(-cx, -cy);
            f.Scale(factor);
            f.Move(cx * factor, cy * factor);
        }
    }

    // ── Об'єднання зображень ──────────────────────────

    /// <summary>Об'єднати з іншим зображенням (додати всі фігури).</summary>
    public void Merge(Image other)
    {
        foreach (var f in other.figures)
            figures.Add(f);

        Width = Math.Max(Width, other.Width);
        Height = Math.Max(Height, other.Height);
    }

    // ── Малювання ──────────────────────────────────────

    /// <summary>Намалювати всі фігури на канвасі та вивести в консоль.</summary>
    public void DrawAll()
    {
        var canvas = Figure.CreateCanvas((int)Width, (int)Height);
        foreach (var f in figures)
            f.Draw(canvas, (int)X, (int)Y);
        Figure.PrintCanvas(canvas);
    }

    // ── Стан об'єкта ───────────────────────────────────

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Зображення: позиція ({X:F1}, {Y:F1}), розмір {Width:F0}x{Height:F0}, фігур: {Count}");
        for (int i = 0; i < figures.Count; i++)
            sb.AppendLine($"  [{i}] {figures[i]}");
        return sb.ToString();
    }

    // ══════════════════════════════════════════════════════
    //  Збереження / Завантаження з файлу
    // ══════════════════════════════════════════════════════

    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    /// <summary>Зберегти зображення у текстовий файл.</summary>
    public void SaveToFile(string path)
    {
        using var sw = new StreamWriter(path);
        sw.WriteLine(string.Join('|', Fmt(X), Fmt(Y), Fmt(Width), Fmt(Height)));
        sw.WriteLine(figures.Count);

        foreach (var f in figures)
        {
            string line = f switch
            {
                Tetrahedron t => $"Tetrahedron|{Fmt(t.X)}|{Fmt(t.Y)}|{Fmt(t.X2)}|{Fmt(t.Y2)}|{Fmt(t.X3)}|{Fmt(t.Y3)}|{Fmt(t.Height)}",
                HatchedTriangle h => $"HatchedTriangle|{Fmt(h.X)}|{Fmt(h.Y)}|{Fmt(h.X2)}|{Fmt(h.Y2)}|{Fmt(h.X3)}|{Fmt(h.Y3)}|{h.HatchDensity}",
                EquilateralTriangle e => $"EquilateralTriangle|{Fmt(e.X)}|{Fmt(e.Y)}|{Fmt(e.X2)}|{Fmt(e.Y2)}|{Fmt(e.X3)}|{Fmt(e.Y3)}",
                RightTriangle r => $"RightTriangle|{Fmt(r.X)}|{Fmt(r.Y)}|{Fmt(r.X2)}|{Fmt(r.Y2)}|{Fmt(r.X3)}|{Fmt(r.Y3)}",
                Triangle tri => $"Triangle|{Fmt(tri.X)}|{Fmt(tri.Y)}|{Fmt(tri.X2)}|{Fmt(tri.Y2)}|{Fmt(tri.X3)}|{Fmt(tri.Y3)}",
                Point p => $"Point|{Fmt(p.X)}|{Fmt(p.Y)}",
                _ => $"Unknown"
            };
            sw.WriteLine(line);
        }
    }

    /// <summary>Завантажити зображення з текстового файлу.</summary>
    public static Image LoadFromFile(string path)
    {
        using var sr = new StreamReader(path);
        var header = sr.ReadLine()!.Split('|');
        var img = new Image(Prs(header[0]), Prs(header[1]), Prs(header[2]), Prs(header[3]));

        int count = int.Parse(sr.ReadLine()!);
        for (int i = 0; i < count; i++)
        {
            var p = sr.ReadLine()!.Split('|');
            Figure f = p[0] switch
            {
                "Point" => new Point(Prs(p[1]), Prs(p[2])),
                "Triangle" => new Triangle(Prs(p[1]), Prs(p[2]), Prs(p[3]), Prs(p[4]), Prs(p[5]), Prs(p[6])),
                "HatchedTriangle" => new HatchedTriangle(Prs(p[1]), Prs(p[2]), Prs(p[3]), Prs(p[4]), Prs(p[5]), Prs(p[6]), int.Parse(p[7])),
                "RightTriangle" => new RightTriangle(Prs(p[1]), Prs(p[2]), Prs(p[3]), Prs(p[4]), Prs(p[5]), Prs(p[6])),
                "EquilateralTriangle" => new EquilateralTriangle(Prs(p[1]), Prs(p[2]), Prs(p[3]), Prs(p[4]), Prs(p[5]), Prs(p[6])),
                "Tetrahedron" => new Tetrahedron(Prs(p[1]), Prs(p[2]), Prs(p[3]), Prs(p[4]), Prs(p[5]), Prs(p[6]), Prs(p[7])),
                _ => throw new FormatException($"Невідомий тип фігури: {p[0]}")
            };
            img.Add(f);
        }
        return img;
    }

    // Скорочення для форматування/парсингу з InvariantCulture
    private static string Fmt(double v) => v.ToString("F4", Inv);
    private static double Prs(string s) => double.Parse(s, Inv);
}
