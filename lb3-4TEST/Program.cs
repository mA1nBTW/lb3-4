using lb3_4TEST;
using lb3_4TEST.Figures;
using static System.Net.Mime.MediaTypeNames;
using Image = lb3_4TEST.Image;

Console.OutputEncoding = System.Text.Encoding.UTF8;

//Console.WriteLine("Лабораторна робота 3-4");

////Точка
//Console.WriteLine("\n---====== Точка ======---");
//var point = new Point(5, 3);
//Console.WriteLine(point);
//Console.WriteLine($"  Відстань до початку координат: {point.DistanceToOrigin:F2}");

//point.Move(2, 1);
//Console.WriteLine($"  Після Move(2, 1): {point}");

//point.Scale(2);
//Console.WriteLine($"  Після Scale(2):   {point}");

////Трикутник
//Console.WriteLine("\n---====== Трикутник ======---");
//var tri = new Triangle(15, 2, 3, 18, 28, 18);
//Console.WriteLine(tri);
//Console.WriteLine($"  Вершина [0]: ({tri[0].x:F1}; {tri[0].y:F1})");
//Console.WriteLine($"  Вершина [1]: ({tri[1].x:F1}; {tri[1].y:F1})");
//Console.WriteLine($"  Вершина [2]: ({tri[2].x:F1}; {tri[2].y:F1})");

//Console.WriteLine("\n  Малюнок трикутника:");
//var canvas1 = Figure.CreateCanvas(35, 20);
//tri.Draw(canvas1);
//Figure.PrintCanvas(canvas1);

////Заштрихований трикутник
//Console.WriteLine("\n---====== Заштрихований трикутник ======---");
//var hatched = new HatchedTriangle(15, 2, 3, 18, 28, 18, density: 2);
//Console.WriteLine(hatched);

//Console.WriteLine("\n  Малюнок (штрихування):");
//var canvas2 = Figure.CreateCanvas(35, 20);
//hatched.Draw(canvas2);
//Figure.PrintCanvas(canvas2);

////Прямокутний трикутник
//Console.WriteLine("\n---====== Прямокутний трикутник ======---");
//var right = new RightTriangle(2, 2, 20, 15);
//Console.WriteLine(right);

//Console.WriteLine("\n  Малюнок:");
//var canvas3 = Figure.CreateCanvas(28, 20);
//right.Draw(canvas3);
//Figure.PrintCanvas(canvas3);

////Правильний трикутник
//Console.WriteLine("\n---====== Правильний трикутник ======---");
//var equil = new EquilateralTriangle(15, 10, 20);
//Console.WriteLine(equil);

//Console.WriteLine("\n  Малюнок:");
//var canvas4 = Figure.CreateCanvas(35, 20);
//equil.Draw(canvas4);
//Figure.PrintCanvas(canvas4);

////Тетраедр
//Console.WriteLine("\n---====== Тетраедр ======---");
//var tetra = new Tetrahedron(20, 17, 5, 20, 35, 20, height: 14);
//Console.WriteLine(tetra);
//Console.WriteLine($"  Площа основи: {tetra.BaseArea:F2}");
//Console.WriteLine($"  Об'єм:        {tetra.Volume:F2}");

//Console.WriteLine("\n  Малюнок (ізометрична проекція):");
//var canvas5 = Figure.CreateCanvas(45, 22);
//tetra.Draw(canvas5);
//Figure.PrintCanvas(canvas5);

////Поліморфізм - масив Figure[]
//Console.WriteLine("\n---====== Поліморфізм ======---");
//Figure[] figures = [
//    new Point(1, 1),
//    new Triangle(10, 1, 1, 10, 20, 10),
//    new HatchedTriangle(5, 1, 1, 8, 10, 8, 2),
//    new RightTriangle(0, 0, 10, 8),
//    new EquilateralTriangle(10, 8, 12),
//    new Tetrahedron(5, 2, 1, 10, 10, 10, 6)
//];

//foreach (var f in figures)
//{
//    Console.WriteLine($"  {f.GetName(),-30} Площа = {f.Area,10:F2}   Периметр = {f.Perimeter,10:F2}");
//}

////Зображення
//Console.WriteLine("\n---====== Зображення ======---");
//var image = new Image(0, 0, 55, 22);

//image.Add(new Triangle(25, 2, 5, 19, 45, 19));
//image.Add(new RightTriangle(35, 5, 15, 12));
//image.Add(new Point(10, 10));
//image.Add(new Point(50, 3));

//Console.WriteLine(image);
//Console.WriteLine("  Малюнок зображення:");
//image.DrawAll();

////Переміщення всіх фігур
//Console.WriteLine("\n  Після MoveAllFigures(2, -1):");
//image.MoveAllFigures(2, -1);
//Console.WriteLine(image);
//image.DrawAll();

////Масштабування
//Console.WriteLine("\n  Після ScaleImage(1.2):");
//image.ScaleImage(1.2);
//Console.WriteLine(image);
//image.DrawAll();

//Об'єднання двох зображень
Console.WriteLine("\n---====== Об'єднання двох зображень ======---");

var img1 = new Image(0, 0, 55, 20);
img1.Add(new HatchedTriangle(15, 2, 3, 18, 28, 18, density: 2));
img1.Add(new Tetrahedron(20, 17, 5, 20, 35, 20, height: 14));
img1[1].Move(12, -10); 
img1.DrawAll();

var img2 = new Image(0, 0, 65, 30);
img2.Add(new Triangle(15, 2, 3, 10, 20, 12));
img2[0].Move(32, 8);
img2.DrawAll();

img1.Merge(img2);
img1.DrawAll();

Console.WriteLine("\n---====== Перевірка перетину фігур ======---");

var imgCheck = new Image(0, 0, 60, 25);

imgCheck.Add(new Triangle(30, 2, 5, 22, 55, 22));
imgCheck.Add(new RightTriangle(2, 5, 25, 15));
imgCheck.Add(new EquilateralTriangle(50, 8, 8));
imgCheck.Add(new Point(30, 15));
imgCheck.Add(new Point(3, 2));

Console.WriteLine("  Всі фігури зображення:");
imgCheck.DrawAll();

imgCheck.CheckForPeretin(0);
imgCheck.CheckForPeretin(2);
imgCheck.CheckForPeretin(3);