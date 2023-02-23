// Задача 50. Напишите программу, которая на вход принимает число, возвращает индексы этого элемента в двумерном массиве или же указание, что такого числа нет.
// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// 17 -> такого числа в массиве нет

namespace HW50
{
    class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the array value finder!");
            var arr = new ArrayBuilder(3, 4);
            Console.WriteLine("Here is you array:");
            Console.WriteLine(arr.ToString());
            Console.WriteLine("Insert number to search in the array:");
            var input = Console.ReadLine();
            if (double.TryParse(input, out var key) && arr.TryGetIndexes(key, out var indexes))
            {
                Console.WriteLine(indexes);
            }
            else
            {
                Console.WriteLine("Такого в массиве нет");
            }
        }
    }

    public class ArrayBuilder
    {
        private double[,] _arr;
        private Dictionary<double, string> _values;
        public ArrayBuilder(int row, int col)
        {
            _values = new Dictionary<double, string>();
            _arr = InitializeRadomArray(row, col);
        }

        public bool TryGetIndexes(double key, out string indexes)
        {
            indexes = string.Empty;
            return _values.TryGetValue(key, out indexes);
        }

        public override string ToString()
        {
            return _arr.ToArrString();
        }

        double[,] InitializeRadomArray(int row, int col)
        {
            var result = new double[row, col];
            var rnd = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var signPow = rnd.Next(1, 3);
                    var tenPow = rnd.Next(0, 3);
                    var doubleValue = rnd.NextDouble();
                    var sign = ((double)Math.Pow(-1, signPow));
                    var tens = ((double)Math.Pow(10, tenPow));
                    var roundCount = rnd.Next(0, 3);
                    var arrInput =  Math.Round(doubleValue * sign * tens, roundCount);
                    AddValues(arrInput, i, j);
                    result[i, j] = arrInput;
                }
            }

            return result;
        }

        void AddValues(double key, int row, int col)
        {
            var indexes = $"{row}, {col}";
            if (_values.ContainsKey(key))
            {
                var oldString = _values[key];
                var newString = $"[{oldString}], [{indexes}]";
                _values[key] = newString;
            }
            else
            {
                _values.Add(key, indexes);
            }
        }
    }

    public static class ArrExtension
    {
        public static string ToArrString(this double[,] arr)
        {
            var result = string.Empty;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result += arr[i, j] + "\t";
                }

                result += "\n";
            }

            return result;
        }
    }
}