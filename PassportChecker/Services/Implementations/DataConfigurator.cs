using PassportChecker.Models.DbModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PassportChecker.Services.Implementations;

public class DataConfigurator
{
    public static List<PassportModel> GetFirstExceptSecond(List<PassportModel> first, List<PassportModel> second)
    {
        var need = new List<PassportModel>(); //только те, которые есть в базе, но уже нет в сурс-файле
        foreach (var passport in first)
        {
            bool needsAdd = true;
            foreach (var passport2 in second)
            {
                if (passport.Equals(passport2))
                {
                    needsAdd = false;
                    break;
                }
            }
            if (needsAdd) need.Add(passport);
        }

        return need;

    }

    public static List<ChangeModel> GetChanges(List<PassportModel> needToDelete, List<PassportModel> needToAdd)
    {
        int date = (DateTime.Now.Year - 2000 - 1) * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day - 1;
        List<ChangeModel> changes = new List<ChangeModel>();

        foreach (var Delete in needToDelete)
        {
            changes.Add(new ChangeModel()
            {
                Series = Delete.Series,
                Number = Delete.Number,
                Id = new Guid(),
                IsActive = true,
                Date = date
            });
        }
        foreach (var Add in needToAdd)
        {
            changes.Add(new ChangeModel()
            {
                Series = Add.Series,
                Number = Add.Number,
                Id = new Guid(),
                IsActive = false,
                Date = date
            });
        }

        return changes;
    }
}
