using System;
using System.Collections.Generic;
using System.Text;

namespace global {

    public class Global {

        static readonly Dictionary<string, int> colorOptions = new() {
            { "red", 12 }, { "green", 10 }, { "white", 15 }, { "yellow", 14 }, { "blue", 9 },
            { "cyan", 11 }, { "gray", 7 }, { "dark yellow", 6 }, { "magenta", 13 }, { "dark cyan", 3 }
        };
        public static bool RemoveVerbosePrompt { get; set; }
        public static bool RemoveErrorPrompt { get; set; }
        public static bool VerboseVisible { get; set; }
        public static bool ErrorVisible { get; set; }
        public static List<Action> Errors = new();

        public static void LineBreak( int limiter = 20 ) => 
            Console.WriteLine(new StringBuilder(Console.WindowWidth - limiter).Insert(0, "-", Console.WindowWidth - limiter).ToString());

        public static void ColorConsoleWrite( int color, string text ) {
            if (!colorOptions.ContainsValue(color)) {
                Console.Write(text);
            }
            else {
                Console.ForegroundColor = (ConsoleColor)color;
                Console.Write(text);
                Console.ResetColor();
            }

        }

        public static void ColorConsoleWriteLine( int color, string text ) {
            if (!colorOptions.ContainsValue(color)) {
                Console.WriteLine(text);
            }
            else {
                Console.ForegroundColor = (ConsoleColor)color;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }

        public static void ColorConsoleWrite( string color, string text ) {
            if (colorOptions.ContainsKey(color.Trim().ToLower())) {
                Console.ForegroundColor = (ConsoleColor)colorOptions[color.Trim().ToLower()];
                Console.Write(text);
                Console.ResetColor();
            }
            else {
                Console.Write(text);
            }

        }

        public static void ColorConsoleWriteLine( string color, string text ) {
            if (colorOptions.ContainsKey(color.Trim().ToLower())) {
                Console.ForegroundColor = (ConsoleColor)colorOptions[color.Trim().ToLower()];
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else {
                Console.WriteLine(text);
            }
        }

        public static string ColorConsoleReadLine( string color ) {
            string text;
            ColorConsoleWrite("magenta", "==> ");
            if (colorOptions.ContainsKey(color.Trim().ToLower())) {
                Console.ForegroundColor = (ConsoleColor)colorOptions[color.Trim().ToLower()];
                text = Console.ReadLine();
                Console.ResetColor();
            }
            else {
                text = Console.ReadLine();
            }
            return text;
        }

        public static bool ShowErrors() {
            ColorConsoleWriteLine("yellow", "Would you like for further errors to be displayed? \nEnter yes(Y), no(N) or to remove prompt(X) Default is yes(Y) => ");
            string choice = ColorConsoleReadLine("blue").ToUpper();

            if (choice == "YES" || choice == "Y") {
                return true;
            }
            else if (choice == "NO" || choice == "N") {
                return false;
            }
            else if (choice == "X") {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("dark yellow", "This prompt will not be shown again but the default choice will still remain. Default is yes(Y)");
                RemoveErrorPrompt = true;
            }
            else {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("blue", "Yes");
                return true;
            }
            return true;
        }

        public static bool ShowVerbose() {
            ColorConsoleWriteLine("yellow", "Would you like the verbose version? \nEnter yes(Y), no(N) or to remove prompt(X) Default is no(N)");
            string choice = ColorConsoleReadLine("blue").ToUpper();

            if (choice == "YES" || choice == "Y") {
                return true;
            }
            else if (choice == "NO" || choice == "N") {
                return false;
            }
            else if (choice == "X") {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("dark yellow", "This prompt will not be shown again but the default choice will still remain. Default is no(N)");
                RemoveVerbosePrompt = true;
            }
            else {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("blue", "No");
                return false;
            }
            return false;
        }

        public static void KeepGoing( string text, ref bool toggle ) {
            LineBreak(0);
            ColorConsoleWriteLine("dark yellow", $"{text} \nPress Any Key to keep going or type 'no' or 'n' to Exit");
            string choice = ColorConsoleReadLine("blue").ToUpper();
            if (choice == "N" || choice == "NO") toggle = false;
            else if (choice == "") {
                ColorConsoleWrite("magenta", "==> ");
                ColorConsoleWriteLine("blue", "Yes");
                toggle = true;
            }
            else toggle = true;
        }

        public static T BreakError<T>(string text, int line = 0, string file = "") {
            string lineError = line == 0 || file == "" ? "" : $"\n=> Error occured on line {line} of {file}.";
            throw new Exception(text + lineError);
        }

        public static string Titlecase(string text) => text[..1].ToUpper() + text[1..];

        public static string SeperateToTitleCase( string str ) {
            StringBuilder retStr = new();

            retStr.Append(char.ToUpper(str[0]));
            for (int i = 1; i < str.Length; i++) {
                if (char.IsUpper(str[i]))
                    retStr.Append(" ");
                retStr.Append(str[i]);
            }

            return retStr.ToString();
        }

    }
}