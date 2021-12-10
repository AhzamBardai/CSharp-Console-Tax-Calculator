using System;
using global;

namespace taxCalculator
{
    public class StartTaxCalculator : Global
    {
        public static void Main()
        {
            try {
                bool calcOpen = true;
                do {
                    try {
                        // ask verbose ---- Have not finished this yet
                        //if (!RemoveVerbosePrompt) {
                        //    VerboseVisible = ShowVerbose();
                        //}

                        // get input
                        string state;
                        long income;
                        getData(out state, out income);

                        // print tax
                        PrintTaxEvaluation(state, income);

                        // prompt to keep going
                        KeepGoing("Would you like to to keep using the Tax Calculator?", ref calcOpen);
                    }
                    catch (Exception e) {
                        ColorConsoleWriteLine("red", e.Message);
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
                try {
                    // get state
                    ColorConsoleWriteLine("yellow", "Enter State: ");
                    string input = ColorConsoleReadLine("blue");
                    string s = input.Length != 2
                        ? BreakError<string>(15, $"State input is not valid. State value recieved '{input}'")
                        : input;

                    // get income
                    ColorConsoleWriteLine("yellow", "Enter Income: $ ");
                    input = ColorConsoleReadLine("blue");
                    long inc = long.TryParse(input, out long i)
                            ? i
                            : BreakError<long>(15, $"Income recieved is not a valid Income number. Income recieved '{input}'");


                    if (s != "" && inc > 0) {
                        state = s;
                        income = inc;
                        break;
                    }
                    else continue;
                }
                catch (Exception e) {
                    ColorConsoleWriteLine("red", e.Message);
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
                : BreakError<decimal>(15, "State is required and needs to be in a Code(XX) format or the full state name. Anything below 2 Characters will be denied, 2 characters is a state code and above 2 characters is a full state name.");
            decimal avgTax = tax == 0 ? 0 : income/tax;

            // prints
            LineBreak(15);
            ColorConsoleWrite("green", $"Calculated Tax: ");
            ColorConsoleWriteLine("dark cyan", $"${tax:N2}");
            ColorConsoleWriteLine("green", $"State Name: {Titlecase(stateName)} \nAvg Tax Rate: {avgTax:N2}");
            ColorConsoleWriteLine("green", $"Brackets for {Titlecase(stateName)}:");
            ColorConsoleWriteLine("green", TaxCalculator.GetBrackets(state));
        }
    }
}
