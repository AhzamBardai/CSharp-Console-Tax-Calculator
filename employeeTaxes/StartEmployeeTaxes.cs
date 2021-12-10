using System;
using global;

namespace employeeTaxes {

    public class StartEmployeeTaxes : Global {
        public static void Main() {
            try {
                bool empCalcOpen = true;
                do {
                    try {
                        // ask verbose ---- Have not finished this yet
                        //if (!RemoveVerbosePrompt) {
                        //    VerboseVisible = ShowVerbose();
                        //}

                        // get process choice
                        bool choice = WantSortedChoice();
                        if (choice) {
                            bool asc = AscOrDesc();
                            string option = PrintSortOptions();
                            EmployeeList.ProcessAllEmplyees(option, asc);
                        }
                        else EmployeeList.ProcessAllEmplyees();

                        // keep employee calc open
                        KeepGoing("Would you like to keep using the Employee Tax Records?", ref empCalcOpen);
                    }
                    catch (Exception e) {
                        ColorConsoleWriteLine("red", e.Message);
                    }

                } while (empCalcOpen);
            }
            finally {
                Console.WriteLine();
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("dark yellow", "You are exiting the Employee Tax Records");
                Console.WriteLine();
            }
            
        }

        public static bool WantSortedChoice() {
            Console.WriteLine();
            ColorConsoleWriteLine( "yellow" ,"Would you like the Employee Tax Records Sorted? \nEnter yes(Y) or no(N), Default is yes(Y)");
            string input = ColorConsoleReadLine("blue").ToUpper();
            if (input == "NO" || input == "N") return false;
            else if (input == "") {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("blue", "Yes");
                return true;
            }
            else return true;
        }

        public static bool AscOrDesc() {
            Console.WriteLine();
            ColorConsoleWriteLine("yellow", "How would you like them Sorted? Default is Ascending");
            ColorConsoleWrite("magenta", $"==> 1 - ");
            ColorConsoleWriteLine("yellow", "Ascending Order(lowest - highest)");
            ColorConsoleWrite("magenta", $"==> 2 - ");
            ColorConsoleWriteLine("yellow", "Descending Order(highest - lowest)");
            int pick;
            string choice = ColorConsoleReadLine("blue");
            while (!int.TryParse(choice, out pick) && (pick == 1 || pick == 2)) 
                ColorConsoleWriteLine("yellow", "Please Enter the Number of the options above to move forward.");
            if(pick == 2) return false;
            else if (choice == "") {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("blue", "1 - Ascending");
                return true;
            }
            else return true;
        }

        public static string PrintSortOptions() {
            Console.WriteLine();
            ColorConsoleWriteLine("yellow", "Pick one of the sorting options below");
            int c = 0;
            foreach (string option in Enum.GetNames(typeof(SortOptions))) {
                c++;
                ColorConsoleWrite("magenta", $"==> {c} - ");
                ColorConsoleWriteLine("yellow", $"{option}");
            }
            string choice;
            do {
                string input = ColorConsoleReadLine("blue");
                if (int.TryParse(input, out int i)) input = $"{i-1}";
                if (Enum.TryParse(typeof(SortOptions), input, true, out object s)) {
                    choice = s.ToString();
                    break;
                }
                else {
                    ColorConsoleWriteLine("yellow", "Please Enter a number within the limit of options.(1-5) or type in the option exactly as it is in the options.");
                    continue;
                }
            } while (true);
            return choice;

        }
        public enum SortOptions {
            Id,
            Name,
            State,
            Hours,
            Pay,
            TotalPay,
            TaxDue
        }
    }
}