using PassportChecker.Models;

namespace PassportChecker.Services.Interfaces;

public interface IPassportService
{
    Task<List<Passport>> GetInactivePassports();

    Task<List<Change>> GetChangesByDate(DateTime date);

    Task<List<Change>> GetChangeByPassport(int series, int number);
}
