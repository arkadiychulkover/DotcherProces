using System.Runtime.InteropServices;
using System.Diagnostics;
using System;

namespace DotcherProces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Tasks.FirstTask();
            //Tasks.SecondTask();
            //Tasks.ThirdTask();
            Tasks.FourthTask();
        }

        internal static class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            #region
            public const uint B_YESNO = 0x00000004;
            public const uint B_YESNOCANCEL = 0x00000003;
            public const uint B_OK = 0x00000000;

            public const uint B_ICONQUESTION = 0x00000020;
            public const uint B_ICONINFO = 0x00000040;
            public const uint B_ICONVOSKL = 0x00000030;

            public const int YES = 6;
            public const int NO = 7;
            public const int CANCEL = 2;

            public const uint W_CLOSE = 0x0010;
            public const uint W_SETTEXT = 0x000C;
            #endregion
        }
        public static class Tasks
        {
            public static void FirstTask()
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "Notepad.exe";
                    process.Start();
                    Console.WriteLine($"Process with ID {process.Id} has started");

                    process.WaitForExit();
                    if (process.HasExited)
                    {
                        Console.WriteLine($"Process has exited with ID {process.ExitCode}");
                        process.Close();
                    }
                }
            }
            static void SecondMenu() 
            {
                Console.WriteLine("1. Wait for exit and show code");
                Console.WriteLine("2. Close process");
            }
            public static void SecondTask()
            {
                using (Process process = new Process())
                {
                    SecondMenu();

                    process.StartInfo.FileName = "Notepad.exe";
                    process.Start();
                    Console.WriteLine($"Process with ID {process.Id} has started");
                    int choise = Convert.ToInt32(Console.ReadLine());
                    switch (choise)
                    {
                        case 1:
                            process.WaitForExit();
                            if (process.HasExited)
                            {
                                Console.WriteLine($"Process has exited with ID {process.ExitCode}");
                                process.Close();
                            }
                            break;
                        case 2:
                            process.Kill();
                            Console.WriteLine($"Process with ID {process.Id} has been killed");
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
            public static void ThirdTask()
            {
                Console.WriteLine("Enter first number:");
                int num1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter second number:");
                int num2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter operation (+, -, *, /):");
                string op = Console.ReadLine();

                string arguments = $"{num1} {num2} {op}";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ChildApp.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine($"Result: {result}");
                } 
            }
            public static void FourthTask() 
            {
                Console.WriteLine("Enter path:");
                string path = Console.ReadLine();
                Console.WriteLine("Enter word:");
                string word = Console.ReadLine();
                string arguments = $"{path} {word}";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ChildApp.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    string result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine($"Result: {result}");
                }
            }
        }
    }
}
