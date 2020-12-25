using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstThread = new Thread(StringThread);
            var secondThread = new Thread(SecondThread);
            firstThread.Start();
            secondThread.Start();
        }


        public static void StringThread()
        {
            var sr = new StreamReader("asd.txt");
            string Originalline, formatedLine;
            for (int i = 0; i < 10; i++)
            {
                while (!sr.EndOfStream)
                {
                    Originalline = sr.ReadLine();
                    formatedLine = Originalline.Replace('e', 'Y');
                    Console.WriteLine(formatedLine);
                    Thread.Sleep(300);
                }
            }

            sr.Close();
        }


        public static void SecondThread()
        {
            int min = 1,
                max = 3000,
                value = min;

            for (var i = min; i < max; i++)
            {
                if (!isDevided(i))
                    continue;
                Thread.Sleep(500);
                Console.WriteLine("Поток 2: делится на 3 число " + i + "\n");
            }
        }


        private static bool isDevided(int N)
        {
            var tf = true;
            if (N % 3 == 0) 
                return tf;
            tf = false;
            return tf;

        }
    }
}