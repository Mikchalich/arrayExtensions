using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace ConsoleApp40
{
    public static class ArrayExtensions
    {
        enum OutputType
        {
            WRITELINE,
            WRITE,
            SPACEWRITE
        }
        //Метод для объектов, невозможно использовать с примитивными типами
        public static IEnumerable<TOut> ChangeAllObjects<Tin, TOut>(this ICollection<Tin> list, Func<Tin, TOut> f)
            where Tin : class
        {
            List<TOut> outs = new List<TOut>();

            foreach (var item in list)
            {
                outs.Add(f(item));
            }
            return outs;
        }
        public static IEnumerable<string> ChangeAllObjects(this ICollection<string> list, Func<string, string> f)
        {
            List<string> outs = new List<string>();

            foreach (var item in list)
            {
                outs.Add(f(item));
            }
            list.Clear();
            foreach (var item in outs)
            {
                list.Add(item);
            }
            return list;
        }
        public static void WriteAll<T, TResult>(this ICollection<T> list, Func<T, TResult> f)
        {
            foreach (var item in list)
            {
                Console.WriteLine(f(item));
            }
        }
        public static void WriteAll<T>(this ICollection<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        //Метод для примитивных типов(int, bool, float, double)
        public static IEnumerable<T> ChangeAll<T>(this ICollection<T> list, Func<T, T> f)
        {
            List<T> outs = new List<T>();

            foreach (var item in list)
            {
                outs.Add(f(item));
            }
            list.Clear();
            foreach (var item in outs)
            {
                list.Add(item);
            }
            return list;
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            //Создаём список из объектов класса Doremi
            List<Doremi> doremis = new List<Doremi>() { new Doremi(), new Doremi(), new Doremi(), new Doremi(), new Doremi() };
            //Создаём список из примитивного типа int
            List<int> integers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Создаём список строк
            List<string> strings = new List<string>() { "One", "tWo", "tHree", "fOur" };
            //Пользуемся методом для примитивов умножая наши числа на 2, метод для объектов просто недоступен для нашего списка
            integers.ChangeAll(x => x * 2);
            //Пользуемся методом для объектов декрементируя свойство Count класса Doremi,
            doremis.ChangeAllObjects(p => p.Count--).ToList();
            //Для типа string доступны оба метода, из-за его структуры мне пришлось перегрузить метод ChangeAllObjects(); и захардкодить его
            //Чтобы его можно было использовать и как объект, и как примитив
            //В данном примере мы переписываем все строки в нижний регистр по примеру "ЯбЛоКо" => "яблоко"
            strings.ChangeAllObjects(x => x.ToLower());
            //А в этом мы просто складываем строки
            strings.ChangeAll(x => x + "!");
            //Используем метод вывода и выводим декрементированные значения Count из объектов класса в нашем списке doremi
            Console.WriteLine("Doremis count:");
            doremis.WriteAll(x => x.Count);
            //Для вывода примитивов можно воспользоваться простым вызовом WriteAll(); без лямбда-функции
            Console.WriteLine("Integers:");
            integers.WriteAll();
            Console.WriteLine("Strings:");
            strings.WriteAll();
            //Я создал данное расширение ради облегчения использования простых действий, которые вам приходится прописывать в лишний раз создаваемым foreach.
            //Лямбда функции могут облегчить эту задачу 
        }
    }
}
