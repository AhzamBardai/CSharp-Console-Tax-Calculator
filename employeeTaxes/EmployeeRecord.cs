using System;
using System.Text.RegularExpressions;
using global;
using taxCalculator;

namespace employeeTaxes {

    public class EmployeeRecord : Global {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int Hours { get; set; }
        public decimal Pay { get; set; }
        public int LineError { get; set; }
        public static string FileName => "employees.csv";
        public EmployeeRecord( string csvLine, int lineErr ) {
            LineError = lineErr;
            string[] lineParse = csvLine.Split(",");

            // parse id
            if (int.TryParse(lineParse[0], out int id)) Id = id;
            else
                BreakError<string>($"Employee id format invalid. Expected a non-decimal number, received '{lineParse[0]}'.", LineError, FileName);

            // parse name
            if (!new Regex("^[a-zA-Z]+$").IsMatch(lineParse[1]))
                BreakError<string>($"Employee name can only have alpha characters, received '{lineParse[1]}'.", LineError, FileName);
            else Name = lineParse[1];

            // parse state
            if (!new Regex("^[a-zA-Z]+$").IsMatch(lineParse[2]) && lineParse[2].Length != 2)
                BreakError<string>($"State can only be 2 alpha characters, received '{lineParse[2]}'.", LineError, FileName);
            else if (!TaxCalculator.record.ContainsKey(lineParse[2]))
                BreakError<string>($"Employee's State is not available in the current registery, received '{lineParse[2]}'", LineError, FileName);
            else State = lineParse[2];

            // parse hours
            if (int.TryParse(lineParse[3], out int hour)) Hours = hour;
            else 
                BreakError<string>($"Employee Hours format invalid. Expected a non-decimal number, received '{lineParse[3]}'.", LineError, FileName);

            // parse pay
            if (decimal.TryParse(lineParse[4], out decimal pay)) Pay = pay;
            else 
                BreakError<string>($"Employee Hourly Pay format invalid. Expected a decimal number, received '{lineParse[4]}'.", LineError, FileName);
        }
        public int TotalPay => (int)(Hours * Pay);
        public decimal TaxDue => State != null && TotalPay > 0
            ? TaxCalculator.ComputeTaxFor(State, TotalPay)
            : BreakError<decimal>("There was an error calculating tax.", LineError, FileName);

        public override string ToString() {
            LineBreak(0);
            return $"| ID: {Id, 4} | Name: {Name, 7} | State: {State, 4} | Hours: {Hours, 6} | Pay: {$"{Pay:N2}", 8} | Total Pay: {$"{TotalPay:N2}", 10} | Tax Due: {$"{TaxDue:N2}", 10} |";
        }
    }
}