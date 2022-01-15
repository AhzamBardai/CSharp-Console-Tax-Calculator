using System;
using global;

namespace taxCalculator {

    public class TaxRecord : Global {

        // private vars
        private string _stateCode;
        private string _state;
        private int _floor;
        private long _ceiling;
        private decimal _rate;

        // data vars
        public static string FileName => "taxable.csv";
        public static int LineError { get; set; }

        // public properties
        public string StateCode {
            get => _stateCode;
            set => _stateCode = value.Length != 2
                ? BreakError<string>($"Invalid State Code Format. received '{value}'.", LineError, FileName)
                : value;
        }
        public string State {
            get => _state;
            set => _state = value.Length < 0 
                ? BreakError<string>($"Invalid State Name Format, received '{value}'.", LineError, FileName) 
                : value;
        }
        public int Floor {
            get => _floor;
            set => _floor = value < 0
                ? BreakError<int>($"Tax Floor of {State} cannot be less than 0.", LineError, FileName)
                : value;
        }
        public long Ceiling {
            get => _ceiling;
            set => _ceiling = value < 0 || value > 99999999999
                ? BreakError<long>($"Tax Ceiling of {State} cannot be less than 0 or greater than 99999999999.", LineError, FileName)
                : value;

        }
        public decimal Rate {
            get => _rate;
            set => _rate = value < 0M
                ? BreakError<decimal>($"Tax Rate of {State} cannot be less than 0.0.", LineError, FileName)
                : value;
        }

        public TaxRecord( string csvLine, int lineNum ) {
            LineError = lineNum;
            string[] lineParse = csvLine.Split(",");

            StateCode = lineParse[0];
            State = lineParse[1];

            // parse int Floor
            if (int.TryParse(lineParse[2], out int f)) Floor = f;
            else
                BreakError<long>($"Tax Floor format for | State: {State} | not valid. Expected a non-decimal number but received '{lineParse[2]}'.", LineError, FileName);

            // parse long Ceiling
            if (long.TryParse(lineParse[3], out long l)) Ceiling = l;
            else
                BreakError<long>($"Tax Ceiling format for | State: {State} | not valid. Expected a non-decimal number but received '{lineParse[3]}'.", LineError, FileName);

            // parse tax rate
            if (decimal.TryParse(lineParse[4], out decimal d)) Rate = d;
            else
                BreakError<long>($"Tax Rate format for | State: {State} | not valid. Expected a decimal number but received '{lineParse[4]}'.", LineError, FileName);

        }

        public override string ToString() {
            LineBreak(0);
            return $"|Code: {StateCode,5} | State: {State,22} | Floor: {Floor,14} | Ceiling: {Ceiling,20} | Rate: {Rate,10}|";
        }
    }
}