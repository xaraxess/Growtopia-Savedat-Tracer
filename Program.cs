using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace SaveDatTracer
{//xarax#1337
    class Program
    {
        public static string savedatpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia\save.dat";
        public static string old;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE); //Hide console
            
            int delaytocheck = 1000;
            Timer t = new Timer(TimerCallback, null, 0, delaytocheck);
            old = File.ReadAllText(savedatpath);
            Console.ReadKey();
        }
        private static void TimerCallback(Object o)
        {
            string savedatnew = File.ReadAllText(savedatpath);
            if(String.Compare(old,savedatnew) < 0)
            {
                Console.WriteLine("changed");
                //Send
                Environment.Exit(0); //after sending
            }
        }
    }
}
