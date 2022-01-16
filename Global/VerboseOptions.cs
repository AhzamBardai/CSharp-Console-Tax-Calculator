
namespace global {
    public static class VerboseOptions {
        public const string Intro = 
            "Welcome to C# Tax Calculator. This calculator uses a CSV files to parse tax brackets for all states and calculate taxes " +
            "\nfor a list of employees. To use this console app you have 3 options. All errors are shown by default but you have the " +
            "\noption to suppress all errors. Toggling the Verbose mode and Errors mode is available everytime you go back to the " +
            "\nmain app. If you select Tax Calculator or Employee Taxes you will be taken into those part of the app and will be " +
            "\nasked if you would like to leave back to the main program after each successful use." +
            "\n\nI hope you enjoy this App! If you think I can anything different to improve this leave comments here" +
            "\n https://github.com/AhzamBardai/CSharp-Console-Tax-Calculator \n";

        public const string CalcIntro = 
            "\n1) Tax Calculator - this options provides a calculator which takes in a state code or it's full name and an income " +
            "\namount. It then provides a tax estimate after spliting the income between tax brackets. This options will also display " +
            "\ntax brackets that it used to calculate said tax.";

        public const string RecordsIntro =
            "\n2) Tax Records - This options provides all tax records for all states that is in the tax calulator registry. It splits " +
            "\neach record in taxable.csv and prints all records in a tabular format. ";

        public const string EmpIntro = 
            "\n3) Employee Taxes - This option will use the tax calculator and calculate all taxes for each employee in employees.csv " +
            "\nfile. It will then return that data in a tabular format.";
    }
}
