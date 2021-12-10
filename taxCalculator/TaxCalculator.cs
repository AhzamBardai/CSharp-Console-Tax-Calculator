using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using global;
using System.ComponentModel;

namespace taxCalculator {

    public static class TaxCalculator {

        public static Dictionary<string, List<TaxRecord>> record = new Dictionary<string, List<TaxRecord>>();

        static TaxCalculator() {
            try {
                string file = @"Z:\Woz U\Projects\ADN102Final\finalProject\taxCalculator\taxtable.csv";
                StreamReader sr = new StreamReader(file);
                int lineCount = 0;
                while (!sr.EndOfStream) {
                    try {
                        lineCount++;
                        TaxRecord tr = new(sr.ReadLine(), lineCount);
                        if (record.ContainsKey(tr.StateCode)) {
                            record[tr.StateCode].Add(tr);
                        }
                        else {
                            record[tr.StateCode] = new List<TaxRecord>();
                            record[tr.StateCode].Add(tr);
                        }
                    }
                    catch (Exception e) {
                        Global.ColorConsoleWriteLine("red", e.Message);
                    }

                }
                sr.Close();
            }
            catch (Exception e) {
                Global.ColorConsoleWriteLine("red", e.Message);
            }
        }

        public static decimal ComputeTaxFor( string state, long income ) {
            decimal tax = 0M;
            try {
                if (!record.ContainsKey(state)) 
                    Global.BreakError<string>(15, $"State value provided is not available in the current records. Please try again. Received value {state}");
                else {
                    foreach (TaxRecord tr in record[state]) {
                        if (income > tr.Ceiling) {
                            tax = (tr.Ceiling - tr.Floor) * tr.Rate;
                        }
                        else if (income > tr.Floor && income < tr.Ceiling) {
                            tax += (income - tr.Floor) * tr.Rate;
                            break;
                        }
                    }
                }
            }
            catch (Exception e) {
                Global.ColorConsoleWriteLine("red", e.Message);
            }
            return tax;
        }

        public static decimal ComputeTaxForFullStateName(string state, long income) =>
            ComputeTaxFor(GetStateCode(state), income);

        public static void ViewAllRecords() {
            foreach (KeyValuePair<string, List<TaxRecord>> rec in record) {
                foreach (TaxRecord t in rec.Value) {
                    Global.ColorConsoleWriteLine("green", t.ToString());
                }
            }
        }

        public static string GetStateName( string code ) => record[code][0].State;

        public static string GetBrackets(string code) {
            string brackets = "", c = code.ToUpper();
            if(code.Length > 2) c = GetStateCode(code);
            foreach (TaxRecord tr in record[c])
                brackets += $"[ Floor: ${tr.Floor, 6} | Ceiling: ${tr.Ceiling, 11} | Rate: {tr.Rate, 6} ]\n";
            return brackets;
        }

        public static string GetStateCode(string name ) {
            string code = "";
            foreach (List<TaxRecord> tr in record.Values) 
                if (tr[0].State.ToLower() == name.ToLower()) code = tr[0].StateCode;
            if (code == "") 
                Global.BreakError<string>(15, $"State value provided is not available in the current records. Please try again. Received value {name}" );
            return code;

        }

        // test sort
        public static void Test(string prop, string key) {
            PropertyDescriptor pr = TypeDescriptor.GetProperties(record[key]).Find(prop, false);
            IOrderedEnumerable<TaxRecord> sort = record[key].OrderBy(x => pr.GetValue(x));
            foreach (TaxRecord tr in sort) {
                Console.WriteLine(tr.ToString());   
            }
        }

    }
}