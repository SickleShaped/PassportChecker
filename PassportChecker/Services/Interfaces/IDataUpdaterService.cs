using PassportChecker.Models;

namespace PassportChecker.Services.Interfaces;

public interface IDataUpdaterService
{
    public Task GetDataFromSource();
}
