using System;
using taxCalculator;
using employeeTaxes;
using global;

namespace finalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string welcome = "Welcome To Woz Tax Calc";
            Global.ColorConsoleWriteLine("cyan", $"{welcome, 72}");
            try {
                bool progOpen = true;
                do {
                    try {
                        // evaluate choice
                        int choice = GetChoice();
                        switch (choice) {
                            case 1: StartTaxCalculator.Main(); break;
                            case 2: TaxCalculator.ViewAllRecords(); break;
                            case 3: StartEmployeeTaxes.Main(); break;
                            default: StartTaxCalculator.Main(); break;
                        }

                        // prompt to try again
                        Global.KeepGoing("Would you like to keep using the program?", ref progOpen);

                    }
                    catch (Exception e) {
                        Global.ColorConsoleWriteLine("red", e.Message);
                    }

                } while (progOpen);
            }

            finally {
                string goodbye = "Goodbye!";
                Console.WriteLine();
                Global.ColorConsoleWriteLine("cyan", $"{goodbye,64}");
            }

        }

        public static int GetChoice() {
            Global.ColorConsoleWrite("magenta", "==> 1 - ");
            Global.ColorConsoleWriteLine("yellow", "Open Tax Calculator");
            Global.ColorConsoleWrite("magenta", "==> 2 - ");
            Global.ColorConsoleWriteLine("yellow", "View All Tax Records");
            Global.ColorConsoleWrite("magenta", "==> 3 - ");
            Global.ColorConsoleWriteLine("yellow", "Process Employee Taxes");
            bool loop = true;
            int choice = 1;

            loop: while (loop) {
                    Global.ColorConsoleWriteLine("yellow", "Please Enter one of the above Choices to move forward.");
                    loop = !int.TryParse(Global.ColorConsoleReadLine("blue"), out choice);
                }
            if (choice < 4 && choice > 0) {
                return choice;
            }
            else {
                loop = true;
                goto loop;
            }
        }
        
    }
}
