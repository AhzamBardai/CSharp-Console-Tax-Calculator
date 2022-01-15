using System;
using taxCalculator;
using employeeTaxes;
using static global.Global;

namespace finalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string welcome = "Welcome To Woz Tax Calc";
            ColorConsoleWriteLine("cyan", $"{welcome, 72}");
            try {
                bool progOpen = true;
                do {
                    try {

                        // ask verbose ---- Have not finished this yet
                        if (!RemoveVerbosePrompt) {
                            VerboseVisible = ShowVerbose();
                            Console.WriteLine();
                        }

                        // ask for errors
                        if (!RemoveErrorPrompt) {
                            ErrorVisible = ShowErrors();
                            Console.WriteLine();
                        }

                        // evaluate choice
                        int choice = GetChoice();
                        switch (choice) {
                            case 1: StartTaxCalculator.Main(); break;
                            case 2: TaxCalculator.ViewAllRecords(); break;
                            case 3: StartEmployeeTaxes.Main(); break;
                            default: StartTaxCalculator.Main(); break;
                        }


                        // print Errors
                        if (ErrorVisible) {
                            foreach (var err in Errors) {
                                LineBreak(15);
                                err();
                            }
                        }

                        // prompt to try again
                        KeepGoing("Would you like to keep using the program?", ref progOpen);

                    }
                    catch (Exception e) {
                        Errors.Add(() => ColorConsoleWriteLine("red", e.Message));
                    }

                } while (progOpen);
            }

            finally {
                string goodbye = "Goodbye!";
                Console.WriteLine();
                ColorConsoleWriteLine("cyan", $"{goodbye,64}");
            }

        }

        public static int GetChoice() {
            ColorConsoleWrite("magenta", "==> 1 - ");
            ColorConsoleWriteLine("yellow", "Open Tax Calculator");
            ColorConsoleWrite("magenta", "==> 2 - ");
            ColorConsoleWriteLine("yellow", "View All Tax Records");
            ColorConsoleWrite("magenta", "==> 3 - ");
            ColorConsoleWriteLine("yellow", "Process Employee Taxes");
            bool loop = true;
            int choice = 1;

            loop: while (loop) {
                    ColorConsoleWriteLine("yellow", "Please Enter one of the above Choices to move forward.");
                    loop = !int.TryParse(ColorConsoleReadLine("blue"), out choice);
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
