using System;
using System.Collections.Generic;
using System.Linq;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке убывания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            int N = 0;
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();
            try
            {
                N = int.Parse(Console.ReadLine());
                for (int i = 0; i < N; i++)
                {
                    string[] str = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.Parse(str[2]) < 0 || int.Parse(str[2]) > 3) throw new ArgumentException();
                    if (int.Parse(str[1]) < 1970 || int.Parse(str[1]) > 2020) throw new ArgumentException();
                    computerInfoList.Add(new ComputerInfo { Owner = str[0], ComputerManufacturer = new Manufacturer(str[2], str[1]) });
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
                return;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
                return;
            }


            // выполните сортировку одним выражением
            var computerInfoQuery = from ci in computerInfoList
                                    orderby ci.Owner, ci.ComputerManufacturer.name, ci.ComputerManufacturer.date descending
                                    select ci;

            PrintCollectionInOneLine(computerInfoQuery);

            Console.WriteLine();

            // выполните сортировку одним выражением
            var computerInfoMethods = computerInfoList.OrderByDescending(x => x.Owner).ThenBy(x => x.ComputerManufacturer.name).ThenByDescending(x => x.ComputerManufacturer.date);

            PrintCollectionInOneLine(computerInfoMethods);
            
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection)
        {
            Console.WriteLine(collection.Select(x => x.ToString()).Aggregate((x, y) => x + "\n" + y));
        }
    }

    class Manufacturer
    {
        public string name { get; set; }
        public string date { get; set; }

        public Manufacturer(string name, string date)
        {
            this.name = name == "0" ? "Dell" : name == "1" ? "Asus" : name == "2" ? "Apple" : "Microsoft";
            this.date = "[" + date + "]";
        }
    }

    class ComputerInfo
    {
        public string Owner { get; set; }
        public Manufacturer ComputerManufacturer { get; set; }

        public override string ToString()
        {
            return Owner + ": " + ComputerManufacturer.name + " " + ComputerManufacturer.date;
        }
    }
}
