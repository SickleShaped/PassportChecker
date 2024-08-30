using PassportChecker.Models;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations
{
    public class PassportService:IPassportService
    {
        public async List<Passport> GetInactivePassports()
        {

        }

        public async List<Change> GetChangesByDate(DateTime date)
        {

        }

        public async List<Change> GetChangeByPassport(int series, int number)
        {

        }
    }
}
