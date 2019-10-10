using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FortniteLauncher
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        // Fortnite executable and token for both anti-cheats as of 10/9/2019
        private const string EAC_EXECUTABLE = "FortniteClient-Win64-Shipping_EAC.exe";
        private const string EAC_TOKEN = "91e19a79b3c4c7c591d47bd6";
        private const string BE_EXECUTABLE = "FortniteClient-Win64-Shipping_BE.exe";
        private const string BE_TOKEN = "f7b9gah4h5380d10f721dd6a";

        private static Process _antiCheat;

        private static void Main(string[] args)
        {
            // Allocate a new console to our launcher because EpicGamesLauncher hides the original.
            FreeConsole();
            AllocConsole();

            // Join all arguments together into one string w/ spaces.
            string formattedArgs = string.Join(" ", args);

            // Check if the required files exists in the current work path.
            if (!File.Exists("Original.exe") || !File.Exists(EAC_EXECUTABLE) || !File.Exists(BE_EXECUTABLE))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please ensure the working path is the Fortnite Win64 folder!");
                Console.ReadKey();
                return;
            }

            // Setup a process exit event handler.
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            // Anti-cheat selection menu.
            Console.WriteLine("[1]. EasyAntiCheat");
            Console.WriteLine("[2]. BattlEye");
            Console.Write("Option: ");
            string option = Console.ReadLine();

            // Initialize anti-cheat process based on what option is chosen.
            switch (option)
            {
                case "1":
                    _antiCheat = new Process
                    {
                        StartInfo =
                        {
                            FileName        = EAC_EXECUTABLE,
                            Arguments       = $"{formattedArgs} -nobe -fromfl=eac -fltoken={EAC_TOKEN}",
                            CreateNoWindow  = false
                        }
                    };
                    break;
                case "2":
                    _antiCheat = new Process
                    {
                        StartInfo =
                        {
                            FileName        = BE_EXECUTABLE,
                            Arguments       = $"{formattedArgs} -noeac -fromfl=be -fltoken={BE_TOKEN}",
                            CreateNoWindow  = false
                        }
                    };
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    Console.ReadKey();
                    return;
            }

            // Swap our launcher out for the original to avoid detection.
            File.Move(Assembly.GetExecutingAssembly().Location, "Backup.exe");
            File.Move("Original.exe", "FortniteLauncher.exe");

            // Start the Fortnite anti-cheat process.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Launching...");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            _antiCheat.Start();

            // We'll wait for the process to finish, otherwise our launcher will just close instantly.
            _antiCheat.WaitForExit();

            // Swap launchers back.
            File.Move("FortniteLauncher.exe", "Original.exe");
            File.Move("Backup.exe", "FortniteLauncher.exe");
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            // If the launcher is quit midgame the anti-cheat process will be killed.
            if (!_antiCheat.HasExited)
                _antiCheat.Kill();
        }
    }
}
