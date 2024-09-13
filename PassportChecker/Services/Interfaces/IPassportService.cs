using PassportChecker.Models;

namespace PassportChecker.Services.Interfaces;

public interface IPassportService
{
    Task<Passport> GetInactivePassports(int series, int number);

    Task<List<Change>> GetChangesByDate(int date);

    Task<List<Change>> GetChangeByPassport(int series, int number);
}
