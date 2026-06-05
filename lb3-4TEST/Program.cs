using lb3_4TEST;
using lb3_4TEST.Figures;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторна робота 3-4");

//Точка
Console.WriteLine("\n---====== Точка ======---");
var point = new Point(5, 3);
Console.WriteLine(point);
Console.WriteLine($"  Відстань до початку координат: {point.DistanceToOrigin:F2}");

point.Move(2, 1);
Console.WriteLine($"  Після Move(2, 1): {point}");

point.Scale(2);
Console.WriteLine($"  Після Scale(2):   {point}");

//Трикутник
Console.WriteLine("\n---====== Трикутник ======---");
var tri = new Triangle(15, 2, 3, 18, 28, 18);
Console.WriteLine(tri);
Console.WriteLine($"  Вершина [0]: ({tri[0].x:F1}; {tri[0].y:F1})");
Console.WriteLine($"  Вершина [1]: ({tri[1].x:F1}; {tri[1].y:F1})");
Console.WriteLine($"  Вершина [2]: ({tri[2].x:F1}; {tri[2].y:F1})");

Console.WriteLine("\n  Малюнок трикутника:");
var canvas1 = Figure.CreateCanvas(35, 20);
tri.Draw(canvas1);
Figure.PrintCanvas(canvas1);

//Заштрихований трикутник
Console.WriteLine("\n---====== Заштрихований трикутник ======---");
var hatched = new HatchedTriangle(15, 2, 3, 18, 28, 18, density: 2);
Console.WriteLine(hatched);

Console.WriteLine("\n  Малюнок (штрихування):");
var canvas2 = Figure.CreateCanvas(35, 20);
hatched.Draw(canvas2);
Figure.PrintCanvas(canvas2);

//Прямокутний трикутник
Console.WriteLine("\n---====== Прямокутний трикутник ======---");
var right = new RightTriangle(2, 2, 20, 15);
Console.WriteLine(right);

Console.WriteLine("\n  Малюнок:");
var canvas3 = Figure.CreateCanvas(28, 20);
right.Draw(canvas3);
Figure.PrintCanvas(canvas3);

//Правильний трикутник
Console.WriteLine("\n---====== Правильний трикутник ======---");
var equil = new EquilateralTriangle(15, 10, 20);
Console.WriteLine(equil);

Console.WriteLine("\n  Малюнок:");
var canvas4 = Figure.CreateCanvas(35, 20);
equil.Draw(canvas4);
Figure.PrintCanvas(canvas4);

//Тетраедр
Console.WriteLine("\n---====== Тетраедр ======---");
var tetra = new Tetrahedron(20, 17, 5, 20, 35, 20, height: 14);
Console.WriteLine(tetra);
Console.WriteLine($"  Площа основи: {tetra.BaseArea:F2}");
Console.WriteLine($"  Об'єм:        {tetra.Volume:F2}");

Console.WriteLine("\n  Малюнок (ізометрична проекція):");
var canvas5 = Figure.CreateCanvas(45, 22);
tetra.Draw(canvas5);
Figure.PrintCanvas(canvas5);

//Поліморфізм - масив Figure[]
Console.WriteLine("\n---====== Поліморфізм ======---");
Figure[] figures = [
    new Point(1, 1),
    new Triangle(10, 1, 1, 10, 20, 10),
    new HatchedTriangle(5, 1, 1, 8, 10, 8, 2),
    new RightTriangle(0, 0, 10, 8),
    new EquilateralTriangle(10, 8, 12),
    new Tetrahedron(5, 2, 1, 10, 10, 10, 6)
];

foreach (var f in figures)
{
    Console.WriteLine($"  {f.GetName(),-30} Площа = {f.Area,10:F2}   Периметр = {f.Perimeter,10:F2}");
}

//Зображення
Console.WriteLine("\n---====== Зображення ======---");
var image = new Image(0, 0, 55, 22);

image.Add(new Triangle(25, 2, 5, 19, 45, 19));
image.Add(new RightTriangle(35, 5, 15, 12));
image.Add(new Point(10, 10));
image.Add(new Point(50, 3));

Console.WriteLine(image);
Console.WriteLine("  Малюнок зображення:");
image.DrawAll();

//Переміщення всіх фігур
Console.WriteLine("\n  Після MoveAllFigures(2, -1):");
image.MoveAllFigures(2, -1);
Console.WriteLine(image);
image.DrawAll();

//Масштабування
Console.WriteLine("\n  Після ScaleImage(1.2):");
image.ScaleImage(1.2);
Console.WriteLine(image);
image.DrawAll();

//Об'єднання зображень
Console.WriteLine("\n---====== Об'єднання зображень ======---");
var image2 = new Image(0, 0, 40, 15);
image2.Add(new EquilateralTriangle(20, 8, 14));

Console.WriteLine("  Друге зображення:");
Console.WriteLine(image2);

image.Merge(image2);
Console.WriteLine("  Після об'єднання:");
Console.WriteLine(image);
image.DrawAll();

//Збереження / завантаження з файлу
Console.WriteLine("\n---====== Збереження та завантаження ======---");
string filePath = "image_data.txt";
image.SaveToFile(filePath);
Console.WriteLine($"  Зображення збережено у файл: {filePath}");

var loaded = Image.LoadFromFile(filePath);
Console.WriteLine($"  Зображення завантажено з файлу:");
Console.WriteLine(loaded);

Console.WriteLine("  Малюнок завантаженого зображення:");
loaded.DrawAll();

//Індексатор Image
Console.WriteLine("\n---====== Індексатор зображення ======---");
Console.WriteLine($"  Фігура [0]: {loaded[0]}");
Console.WriteLine($"  Фігура [1]: {loaded[1]}");

Console.WriteLine("\n---====== Кінець програми ======---");
