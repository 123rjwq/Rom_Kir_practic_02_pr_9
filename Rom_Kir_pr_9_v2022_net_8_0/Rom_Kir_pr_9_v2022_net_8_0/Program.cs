using System;

class Program
{
    static void Main(string[] args)
    {
        // Запрос выбора метода ввода координат
        Console.WriteLine("Выберите способ задания координат:");
        Console.WriteLine("1. Ввести координаты вручную");
        Console.WriteLine("2. Случайные координаты");
        string choice = Console.ReadLine();

        char letter1, letter2, letter3;
        int number1, number2, number3;

        // Генератор случайных чисел
        Random random = new Random();

        // Ввод координат
        if (choice == "1")
        {
            // Ввод координат первой и второй фигур
            Console.WriteLine("Введите координаты первой фигуры (буква от a до h, цифра от 1 до 8):");
            string input1 = Console.ReadLine();
            letter1 = input1[0];
            number1 = int.Parse(input1.Substring(1));

            Console.WriteLine("Введите координаты второй фигуры (буква от a до h, цифра от 1 до 8):");
            string input2 = Console.ReadLine();
            letter2 = input2[0];
            number2 = int.Parse(input2.Substring(1));

            // Ввод координат третьего поля
            Console.WriteLine("Введите координаты третьего поля (буква от a до h, цифра от 1 до 8):");
            string input3 = Console.ReadLine();
            letter3 = input3[0];
            number3 = int.Parse(input3.Substring(1));
        }
        else if (choice == "2")
        {
            // Случайные координаты
            letter1 = (char)('a' + random.Next(0, 8)); // Случайная буква от a до h
            number1 = random.Next(1, 9); // Случайное число от 1 до 8

            letter2 = (char)('a' + random.Next(0, 8)); // Случайная буква от a до h
            number2 = random.Next(1, 9); // Случайное число от 1 до 8

            letter3 = (char)('a' + random.Next(0, 8)); // Случайная буква от a до h
            number3 = random.Next(1, 9); // Случайное число от 1 до 8

            Console.WriteLine($"Координаты первой фигуры: {letter1}{number1}");
            Console.WriteLine($"Координаты второй фигуры: {letter2}{number2}");
            Console.WriteLine($"Координаты третьего поля: {letter3}{number3}");
        }
        else
        {
            Console.WriteLine("Некорректный выбор.");
            return;
        }

        // Выбор фигур
        Console.WriteLine("Введите название первой фигуры (ладья, конь, слон, ферзь или король):");
        string figure1 = Console.ReadLine();

        Console.WriteLine("Введите название второй фигуры (ладья, конь, слон, ферзь или король):");
        string figure2 = Console.ReadLine();

        // Проверка, может ли первая фигура дойти до третьего поля, не попав под удар второй фигуры
        string result = CanReach(letter1, number1, letter3, number3, figure1, letter2, number2, figure2);

        Console.WriteLine(result);
    }

    // Метод для проверки, может ли белая фигура дойти до третьего поля, не попав под удар черной фигуры
    static string CanReach(char startLetter, int startNumber, char endLetter, int endNumber, string startFigure, char threatLetter, int threatNumber, string threatFigure)
    {
        // Проверка совпадения координат третьего поля с черной фигурой
        if (startLetter == threatLetter && startNumber == threatNumber)
        {
            return "Белая фигура не может дойти до третьего поля, так как оно занято черной фигурой.";
        }

        // Например, для ладьи и ферзя можно проверить, что координаты не совпадают
        if (startFigure == "ладья" || startFigure == "ферзь")

        {
            if (startLetter == endLetter && startNumber == endNumber)
            {
                return "Белая фигура не может делать ход на то же поле, на котором она стоит.";
            }

            // Проверка на наличие угрозы со стороны второй фигуры
            if (startLetter == threatLetter || startNumber == threatNumber)
            {
                return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
            }

            // Проверка на угрозу срубить белую фигуру со стороны второй фигуры
            if (threatLetter == endLetter && threatNumber == endNumber)
            {
                return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
            }

            return "Белая фигура может дойти до третьего поля.";
        }

        // Для слона проверяем, находится ли третье поле на его диагонали
        if (startFigure == "слон")
        {
            if (Math.Abs(startLetter - endLetter) == Math.Abs(startNumber - endNumber))
            {
                // Проверка на наличие угрозы со стороны второй фигуры
                if (Math.Abs(startLetter - threatLetter) == Math.Abs(startNumber - threatNumber))
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }

                // Проверка на угрозу срубить белую фигуру со стороны второй фигуры
                if (Math.Abs(threatLetter - endLetter) == Math.Abs(threatNumber - endNumber))
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }

                return "Белая фигура может дойти до третьего поля.";
            }
            else
            {
                return "Белая фигура не может дойти до третьего поля, так как поле находится на другой диагонали.";
            }
        }

        // Для коня проверяем возможность сделать ход "г-образным" образом
        if (startFigure == "конь")
        {
            int deltaLetter = Math.Abs(startLetter - endLetter);
            int deltaNumber = Math.Abs(startNumber - endNumber);

            if (deltaLetter == 1 && deltaNumber == 2 || deltaLetter == 2 && deltaNumber == 1)
            {
                // Проверка на наличие угрозы со стороны второй фигуры
                if (startLetter + 1 == threatLetter && startNumber + 2 == threatNumber) // Угроза справа сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter + 1 == threatLetter && startNumber - 2 == threatNumber) // Угроза справа снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter - 1 == threatLetter && startNumber + 2 == threatNumber) // Угроза слева сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter - 1 == threatLetter && startNumber - 2 == threatNumber) // Угроза слева снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }

                if (startLetter + 2 == threatLetter && startNumber + 1 == threatNumber) // Угроза справа сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter + 2 == threatLetter && startNumber - 1 == threatNumber) // Угроза справа снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter - 2 == threatLetter && startNumber + 1 == threatNumber) // Угроза слева сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (startLetter - 2 == threatLetter && startNumber - 1 == threatNumber) // Угроза слева снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }

                // Проверка на угрозу срубить белую фигуру со стороны второй фигуры
                if (endLetter + 1 == threatLetter && endNumber + 2 == threatNumber) // Угроза справа сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter + 1 == threatLetter && endNumber - 2 == threatNumber) // Угроза справа снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter - 1 == threatLetter && endNumber + 2 == threatNumber) // Угроза слева сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter - 1 == threatLetter && endNumber - 2 == threatNumber) // Угроза слева снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter + 2 == threatLetter && endNumber + 1 == threatNumber) // Угроза справа сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter + 2 == threatLetter && endNumber - 1 == threatNumber) // Угроза справа снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (endLetter - 2 == threatLetter && endNumber + 1 == threatNumber) // Угроза слева сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";


                }
                if (endLetter - 2 == threatLetter && endNumber - 1 == threatNumber) // Угроза слева снизу
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }

                return "Белая фигура может дойти до третьего поля.";
            }
            else
            {

                return "Белая фигура не может дойти до третьего поля, так как требуется сделать ход 'г-образным' образом.";
            }
        }

        // Для короля проверяем, что разница между буквами и числами не больше 1
        if (startFigure == "король")
        {
            int deltaLetter = Math.Abs(startLetter - endLetter);
            int deltaNumber = Math.Abs(startNumber - endNumber);

            if (deltaLetter <= 1 && deltaNumber <= 1)
            {
                // Проверка на наличие угрозы со стороны второй фигуры
                if (deltaLetter == 0 && Math.Abs(startNumber - threatNumber) == 1) // Угроза снизу или сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (deltaNumber == 0 && Math.Abs(startLetter - threatLetter) == 1) // Угроза слева или справа
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }
                if (Math.Abs(startLetter - threatLetter) == 1 && Math.Abs(startNumber - threatNumber) == 1) // Угроза по диагонали
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой второй фигуры.";
                }

                // Проверка на угрозу срубить белую фигуру со стороны второй фигуры
                if (deltaLetter == 0 && Math.Abs(endNumber - threatNumber) == 1) // Угроза снизу или сверху
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (deltaNumber == 0 && Math.Abs(endLetter - threatLetter) == 1) // Угроза слева или справа
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }
                if (Math.Abs(endLetter - threatLetter) == 1 && Math.Abs(endNumber - threatNumber) == 1) // Угроза по диагонали
                {
                    return "Белая фигура не может дойти до третьего поля, так как находится под угрозой быть срубленной второй фигурой.";
                }

                return "Белая фигура может дойти до третьего поля.";
            }
            else
            {
                return "Белая фигура не может дойти до третьего поля, так как разница между координатами больше 1.";
            }
        }

        return "Некорректный выбор фигур.";
    }
}



