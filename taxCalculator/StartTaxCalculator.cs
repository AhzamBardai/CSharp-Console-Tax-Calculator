using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using global;

namespace taxCalculator
{
    public class StartTaxCalculator : Global
    {
        public static void Main()
        {
            try {
                bool calcOpen = true;
                ColorConsoleWriteLine("dark cyan", $"{"Tax Calculator",65}");
                do {
                    try {

                        // print verbose
                        if (VerboseVisible) {
                            ColorConsoleWriteLine("dark yellow", VerboseOptions.CalcIntro);
                        }

                        // run tax calc static ctor
                        RuntimeHelpers.RunClassConstructor(typeof(TaxCalculator).TypeHandle);

                        // print Errors
                        if (ErrorVisible) {
                            foreach (var err in Errors) {
                                LineBreak(15);
                                err();
                            }
                            Errors.Clear();
                        }

                        // get input
                        getData(out string state, out long income);

                        // print tax
                        PrintTaxEvaluation(state, income);

                        // prompt to keep going
                        KeepGoing("Would you like to to keep using the Tax Calculator?", ref calcOpen, "Tax Calculator");
                    }
                    catch (Exception e) {
                        Errors.Add(() => ColorConsoleWriteLine("red", e.Message));
                    }
                    
                } while (calcOpen); 
            }
            
            finally {
                Console.WriteLine();
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("dark yellow", "You have exited the Tax Calculator");
                Console.WriteLine();
            }
            
        }

        public static void getData( out string state, out long income ) {
            LineBreak(0);
            do {
                bool valid = true;

                // get state
                ColorConsoleWriteLine("yellow", "Enter State: ");
                string inputS = ColorConsoleReadLine("blue");
                if (!(inputS.Length >= 2 && new Regex("^[a-zA-Z]+$").IsMatch(inputS))) {
                    valid = false;
                    ColorConsoleWriteLine("dark yellow", $"State input is not valid. State value recieved '{inputS}'");
                    Console.WriteLine();
                    continue;
                } else Console.WriteLine();

                // get income
                ColorConsoleWriteLine("yellow", "Enter Income: $ ");
                string inputL = ColorConsoleReadLine("blue");
                if(!long.TryParse(inputL, out long inc)){
                    valid = false;
                    ColorConsoleWriteLine("dark yellow", $"Income recieved is not a valid Income number. Income recieved '{inputL}'");
                    Console.WriteLine();
                    continue;
                } else Console.WriteLine();

                if (valid) {
                    state = inputS;
                    income = inc;
                    break;
                }
                
            } while (true);
            
        }

        public static void PrintTaxEvaluation(string state, long income) {
            // data
            string upperCase = state.Length == 2 ? state.ToUpper() : state;
            string stateName = state.Length > 2 ? state : TaxCalculator.GetStateName(state.ToUpper());
            decimal tax = state.Length == 2
                ? TaxCalculator.ComputeTaxFor(upperCase, income)
                : state.Length > 2
                ? TaxCalculator.ComputeTaxForFullStateName(upperCase.ToLower(), income)
                : BreakError<decimal>("State is required and needs to be in a Code(XX) format or the full state name. Anything below 2 Characters will be denied, 2 characters is a state code and above 2 characters is a full state name.");
            decimal avgTax = tax == 0 ? 0 : income / tax;

            // tax
            ColorConsoleWrite("green", $"Calculated Tax: ");
            ColorConsoleWriteLine("dark cyan", $"${tax:N2}");

            // state
            ColorConsoleWrite("green", $"State Name: ");
            ColorConsoleWriteLine("dark cyan", $"{Titlecase(stateName)}");

            // avg rate
            ColorConsoleWrite("green", $"Avg Tax Rate: ");
            ColorConsoleWriteLine("dark cyan", $"{avgTax:N2}%");

            // brackets
            ColorConsoleWrite("green", $"Brackets for State: ");
            ColorConsoleWriteLine("dark cyan", $"{Titlecase(stateName)}");
            ColorConsoleWriteLine("green", TaxCalculator.GetBrackets(state));
        }

        public static void GetRecords() {

            // run tax calc static ctor
            RuntimeHelpers.RunClassConstructor(typeof(TaxCalculator).TypeHandle);

            // print verbose
            if (VerboseVisible) {
                ColorConsoleWriteLine("dark yellow", VerboseOptions.RecordsIntro);
            }

            // print Errors
            if (ErrorVisible) {
                foreach (var err in Errors) {
                    LineBreak(15);
                    err();
                }
                LineBreak(15);
                Errors.Clear();
            }

            TaxCalculator.ViewAllRecords();
        }
    }
}
