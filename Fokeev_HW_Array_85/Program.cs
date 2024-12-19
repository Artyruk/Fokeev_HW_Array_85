/*Array85.Дан массив A размера N и целое число K (1 ≤ K ≤ 4, K < N).
Осуществить циклический сдвиг элементов массива вправо на K позиций
(при этом A1 перейдет в AK+1, A2 — в AK+2, . . ., AN — в AK). Допускается
использовать вспомогательный массив из 4 элементов.*/

//Я не совсем понял где можно было бы эффективно применить делегаты
//поэтому использовал её для вызова получения целого числа от пользователя

namespace Fokeev_HW_Array85
{
    using static System.Console;

    class Programm
    {
        delegate int MyDelegate();
        static void Main(string[] args)
        {
            //Использовал здесь делегат для вызова схожих функций
            MyDelegate GetNumber = GetN;
            int n = GetNumber();//Размер массива
            GetNumber = GetK;
            int k = GetNumber();//Число сдвига
            //Работа с массивами
            int[] arr = GetRandomArray(n);
            WriteLine("Массив до сдвига");
            PrintArr(arr);
            Move(ref arr, k);
            WriteLine("Массив после сдвига вариант 1 (пошагово)");
            PrintArr(arr);
            MoveK(ref arr, k);
            WriteLine("Массив после сдвига вариант 2 (сразу на число сдвига)");
            PrintArr(arr);
        }

        static int GetK()//Получение размера сдвига
        {
            WriteLine("Введите значение сдвига от 1 до 4 включитльно");
            int k;
            try
            {
                k = Convert.ToInt32(ReadLine());
                if (!(k >= 1 && k <= 4))
                {
                    throw new Exception("Введено значение вне диапазона");
                }
            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{ex.Message}, \n" +
                    $"Установлено значение по умолчанию k=3");
                ResetColor();
                k = 3;

            }
            return k;
        }
        static int GetN()//Получение размера массива
        {
            WriteLine("Введите размер массива больше 4");
            int n;
            try
            {
                n = Convert.ToInt32(ReadLine());
                if (!(n >= 4))
                {
                    throw new Exception("Введено значение меньше 5");
                }
            }
            catch (Exception ex)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{ex.Message}, \n" +
                    $"Установлено значение по умолчанию n=5");
                ResetColor();
                n = 5;

            }
            return n;
        }
        static int[] GetRandomArray(int size)//Создание и заполнение массива рандомом
        {
            Random r = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = r.Next(0, 100);
            }
            return arr;
        }
        static void PrintArr(int[] arr)//Вывод массива на печать
        {
            foreach (var k in arr)
            {
                Write(k + " ");
            }
            WriteLine();
        }
        static void Move(ref int[] arr, int k)//Функция сдвига на одну позицию "к" раз
        {
            int temp;
            for (int i = 1; i<=k; i++)
            {
                temp = arr[arr.Length - 1];
                for (int j=arr.Length-1; j>0; j--)
                {
                    arr[j] = arr[j - 1];
                }
                arr[0] = temp;
            }
        }
        static void MoveK(ref int[] arr, int k)//Функция сдвига сразу на позицию "к"
        {
            /*в общем решил сделать без доп массива, но, похоже, зря,
            думал решение будет несложное, но в процессе я так завернул,
            что получилась куча циклов*/
            int temp;
            int ostatok = arr.Length % k;
            int range = arr.Length / k;//сколько раз нужно подвинуть целиком
            for (int j = 1; (j <= range)&&(range>1); j++)
            //заходим в цикл только если подвинуть нужно целиком больше одного раза
            {
                for (int i = 0; i < ((j < range) ? k : ostatok); i++)
                    /*Данный цикл хорошо справляется если длина массива
                    кратна количеству сдвигов
                    в принципе, я подумал, что на этом всё решение, но в процессе тестов
                    понял, что ошибся и начал выдумавать последущие циклы*/
                {
                    temp = arr[i + k * j];
                    arr[i + k * j] = arr[i];
                    arr[i] = temp;
                }
            }
            //Если длина массива не кратна сдвигам, то получается
            //что массив не совсем правильно сдвигается
            if ((ostatok >1)&&(range>1))
            {
                //досвдиг если остаток больше одного
                temp = arr[k - 1];
                for (int i = k - 1; i > 0; i--)
                {
                    arr[i] = arr[i - 1];
                }
                arr[0] = temp;
            }
            if ((ostatok == 1) && (range > 1))
            {
                //досдвиг если остаток равен одному
                int i = 0;
                temp = arr[i];
                for (; i < k-1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                arr[i] = temp;
            }
            //если количество сдвигов больше половины длины массива,
            //то свдиг осуществляет нижний цикл
            for (int i = 0, j=0, tail=0; (i < k) && (range<=1); i++)
            {
                tail = i + k;//определяем начало после количества сдвигов
                if (tail >= arr.Length)
                {
                    //как только перешли границу массива двигаем числа в начало
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    j++;
                }
                else
                {
                    //пока не перешли границу массива двигаем числа в конец
                    temp = arr[tail];
                    arr[tail] = arr[i];
                    arr[i] = temp;
                }
            }
        }
    }
}