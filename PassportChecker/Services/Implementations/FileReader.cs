using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models.DbModels;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations;

public class FileReader
{
    public static List<PassportModel> GetPassports(string path)
    {
        List<PassportModel> passports = new List<PassportModel>();
        using (TextFieldParser tfp = new TextFieldParser("Data.csv"))
        {
            tfp.TextFieldType = FieldType.Delimited;
            tfp.SetDelimiters(",");
            tfp.ReadFields();
            while (!tfp.EndOfData && passports.Count<1500000)
            {
                PassportModel passport = new PassportModel();
                string[] fields = tfp.ReadFields();

                bool seriesSuccess = Int32.TryParse(fields[0], out int series);
                bool numberSuccess = Int32.TryParse(fields[1], out int number);
                bool formatSuccess = seriesSuccess && numberSuccess && series >= 1000 && fields[0].Length == 4 && fields[1].Length == 6;

                if (formatSuccess)
                {
                    passport.Series = series;
                    passport.Number = number;
                    passports.Add(passport);
                }
            }
        }
        return passports;
    }
}
