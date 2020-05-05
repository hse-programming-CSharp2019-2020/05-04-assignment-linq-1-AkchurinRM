using System;
using System.Linq;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 */
namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTesk02();
        }

        public static void RunTesk02()
        {
            int[] arr = null;
            string[] arr_str = null;
            try
            {
                arr_str = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).TakeWhile(x => x != "0").ToArray();
                arr = arr_str.Select(x => int.Parse(x)).ToArray();
                // был вариант с
                // bool flag = true;
                // arr = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => (int)Math.Pow(int.Parse(x), 2)).Where(x => { flag = x != 0; return flag; }).ToArray();
                if (arr is null) throw new InvalidOperationException();

            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
                return;
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
                return;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                return;
            }

            try
            {
                var filteredCollection = arr.Select(x => (int)Math.Pow(x, 2)).ToArray();

                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = Enumerable.Average(filteredCollection);
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = filteredCollection.Average();


                // вывести элементы коллекции в одну строку
                Console.WriteLine(averageUsingStaticForm.ToString("N3"));
                Console.WriteLine(averageUsingInstanceForm.ToString("N3"));

                Console.WriteLine(arr.Select(x => (x).ToString()).Aggregate((x,y) => x + " " + y));
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }
          
        }
        
    }
}