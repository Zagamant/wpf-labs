
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread firstThread = new Thread(StringThread);
            Thread secondThread = new Thread(SecondThread);
            firstThread.Start();
            secondThread.Start();
        }


        public static void StringThread()
        {
            StreamReader sr = new StreamReader("harry.txt"); 
            string Originalline, formatedLine;
            while (!sr.EndOfStream) {
                Originalline = sr.ReadLine();
                formatedLine = Originalline.Replace('e', 'Y');
                Console.WriteLine(formatedLine);
                Thread.Sleep(300);
            }
            sr.Close();
        }

        

        public static void SecondThread() {
            int min = 1,
                max = 3000,
                value = min;

            for (int i = min; i < max; i++) {
                if (isDevided(i)) {
                    Thread.Sleep(500);
                    Console.WriteLine("Поток 2: делится на 3 число " + i + "\n");
                }
            }
        }


        private static bool isDevided(int N) {
            bool tf = true;
                if (N % 3 != 0){
                    tf = false;
                    return tf;
                }
            return tf;
        }
    }


}

