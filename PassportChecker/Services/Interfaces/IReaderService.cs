using PassportChecker.Models;

namespace PassportChecker.Services.Interfaces;

public interface IReaderService
{
    public Task GetDataFromSource();
}
