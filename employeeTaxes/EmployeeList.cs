using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using static global.Global;

namespace employeeTaxes {

    public static class EmployeeList {
        static List<EmployeeRecord> employees = new();
        
        static EmployeeList() {
            // to catch errors in static class since this runs before everything
            List<Action> EmpErrors = new();
            try {
                string file = @"Z:\Woz U\Projects\ADN102Final\finalProject\employeeTaxes\employees.csv";
                using StreamReader sr = new StreamReader(file);
                int line = 0;
                while (!sr.EndOfStream) {
                    try {
                        line++;
                        EmployeeRecord employee = new(sr.ReadLine(), line);
                        employees.Add(employee);
                    }
                    catch (Exception e) {
                        EmpErrors.Add( () => ColorConsoleWriteLine("red", e.Message));
                    }   
                }
                sr.Close();
            }
            catch (Exception e) {
                EmpErrors.Add( () => ColorConsoleWriteLine("red", e.Message));
            }
            finally {
                Errors = Errors.Concat(EmpErrors).ToList();
            }
        }

        public static void ProcessAllEmplyees() {
            Console.ResetColor();
            foreach (EmployeeRecord employee in employees) {
                ColorConsoleWriteLine("green", employee.ToString());
            }
        }

        public static void ProcessAllEmplyees(string sortBy, bool asc = true) {
            LineBreak(0);
            PropertyDescriptor pr = TypeDescriptor.GetProperties(typeof(EmployeeRecord)).Find(sortBy, true);
            IOrderedEnumerable<EmployeeRecord> sorted = 
                asc ? employees.OrderBy(emp => pr.GetValue(emp)) : employees.OrderByDescending(emp => pr.GetValue(emp));

            sortBy = SeperateToTitleCase(sortBy);
            string order = asc ? "Ascending" : "Descending";
            string heading = $"Sorted Employee Tax Records - {order}";
            string subtitle = $"Sorted By: {sortBy}";
            ColorConsoleWriteLine("dark cyan", $"{heading, 80}");
            ColorConsoleWriteLine("dark cyan", $"{subtitle, 69}");
            foreach (EmployeeRecord employee in sorted) {
                string[] output = employee.ToString().Split("|", StringSplitOptions.RemoveEmptyEntries);
                ColorConsoleWrite("green", "|");
                foreach (string s in output) {
                    if (s[1..s.IndexOf(":")].Replace(" ", "").ToLower() == sortBy.Replace(" ", "").ToLower()) {
                        ColorConsoleWrite("blue", $"{s}");
                    }
                    else {
                        ColorConsoleWrite("green", $"{s}");
                    }
                    ColorConsoleWrite("green", "|");
                }
            }
            LineBreak(0);
        }

    }
}