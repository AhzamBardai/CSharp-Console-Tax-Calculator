using System;
using System.IO;
using System.Collections.Generic;
using global;
using System.Linq;
using System.ComponentModel;

namespace employeeTaxes {

    public static class EmployeeList {
        static List<EmployeeRecord> employees = new();

        static EmployeeList() {
            try {
                string file = @"Z:\Woz U\Projects\ADN102Final\finalProject\employeeTaxes\employees.csv";
                StreamReader sr = new StreamReader(file);
                int line = 0;
                while (!sr.EndOfStream) {
                    try {
                        line++;
                        EmployeeRecord employee = new(sr.ReadLine(), line);
                        employees.Add(employee);
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

        public static void ProcessAllEmplyees() {
            foreach (EmployeeRecord employee in employees) {
                Global.ColorConsoleWriteLine("green", employee.ToString());
            }
        }

        public static void ProcessAllEmplyees(string sortBy, bool asc = true) {
            Global.LineBreak(0);
            PropertyDescriptor pr = TypeDescriptor.GetProperties(typeof(EmployeeRecord)).Find(sortBy, true);
            IOrderedEnumerable<EmployeeRecord> sorted = 
                asc ? employees.OrderBy(emp => pr.GetValue(emp)) : employees.OrderByDescending(emp => pr.GetValue(emp));

            string order = asc ? "Ascending" : "Descending";
            string heading = $"Sorted Employee Tax Records - {order}";
            string subtitle = $"Sorted By: {pr.Name}";
            Global.ColorConsoleWriteLine("dark cyan", $"{heading, 80}");
            Global.ColorConsoleWriteLine("dark cyan", $"{subtitle, 69}");
            SplitSingleCamelCase(ref sortBy);
            foreach (EmployeeRecord employee in sorted) {
                string[] output = employee.ToString().Split("|", StringSplitOptions.RemoveEmptyEntries);
                Global.ColorConsoleWrite("green", "|");
                foreach (string s in output) {
                    if (s[1..s.IndexOf(":")].ToLower() == sortBy.ToLower()) {
                        Global.ColorConsoleWrite("blue", $"{s}");
                    }
                    else if (s.IndexOf(":") > -1 && s[1..s.IndexOf(":")] == "Tax Due")
                        Global.ColorConsoleWrite("green", s);
                    else
                        Global.ColorConsoleWrite("green", $"{s}");
                    Global.ColorConsoleWrite("green", "|");
                }
            }
        }

        public static void SplitSingleCamelCase( ref string str ) {
            char[] checkUpper = str.ToCharArray();
            int camelCase = 0;
            for (int i = 0; i < checkUpper.Length; i++) {
                if (char.IsUpper(checkUpper[i])) camelCase++;
                if (camelCase >= 2) {
                    str = str[0..i] + " " + str[i..str.Length];
                    break;
                }
            }
        }

    }
}