using PassportChecker.Models;

namespace PassportChecker.Services.Interfaces
{
    public interface IPassportService
    {
        List<Passport> GetInactivePassports();

        List<Change> GetChangesByDate(DateTime date);

        List<Change> GetChangeByPassport(int series, int number);
    }
}
