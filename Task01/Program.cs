using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо отфильтровать полученные коллекцию, оставив только отрицательные или четные числа.
 * Дважды вывести коллекцию, разделив элементы специальным символом.
 * Остальные указания см. непосредственно в коде!
 * 
 * Пример входных данных:
 * 1 2 3 4 5
 * 
 * Пример выходных:
 * 2:4
 * 2*4
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
 * 
 */

namespace Task01
{ 
    class Program
    {

        static void Main(string[] args)
        {
            RunTesk01();
        }

        public static void RunTesk01()
        {
            int[] arr = null;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = Console.ReadLine().Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                if (arr is null) throw new InvalidOperationException();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
                return;
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                return;
            }

            // использовать синтаксис запросов!
            IEnumerable<int> arrQuery = 
                from x in arr
                where x % 2 == 0 || x < 0
                select x;

            // использовать синтаксис методов!
            IEnumerable<int> arrMethod = arr.Where(x => x % 2 == 0 || x < 0);

            try
            {
                PrintEnumerableCollection<int>(arrQuery, ":");
                PrintEnumerableCollection<int>(arrMethod, "*");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException");
            }
        }

        // Попробуйте осуществить вывод элементов коллекции с учетом разделителя, записав это ОДНИМ ВЫРАЖЕНИЕМ.
        // P.S. Есть два способа, оставьте тот, в котором применяется LINQ...
        public static void PrintEnumerableCollection<T>(IEnumerable<T> collection, string separator)
        {
            if (collection is null) throw new InvalidOperationException();
            Console.WriteLine(collection.Select(x => x.ToString()).Aggregate((x,y) => x + separator + y));
        }
    }
}
