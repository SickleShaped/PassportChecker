using Microsoft.VisualBasic.FileIO;
using PassportChecker.Models;
using PassportChecker.Services.Interfaces;
using System.ComponentModel;

namespace PassportChecker.Services.Implementations;

public class ReaderService:IReaderService
{
    public List<Passport> GetDataFromSource()
    {
        var x = GetPassports();



        return x;
    }

    private List<Passport> GetPassports()
    {
        List<Passport> passports = new List<Passport>();
        using (TextFieldParser tfp = new TextFieldParser(@"C:\\PassportChecker\Data.csv"))
        {

            tfp.TextFieldType = FieldType.Delimited;
            tfp.SetDelimiters(",");
            tfp.ReadFields();
            while (!tfp.EndOfData)
            {
                Passport passport = new Passport();
                string[] fields = tfp.ReadFields();

                bool seriesSuccess = Int32.TryParse(fields[0], out int series);
                bool numberSuccess = Int32.TryParse(fields[1], out int number);

                if(seriesSuccess && numberSuccess)
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
