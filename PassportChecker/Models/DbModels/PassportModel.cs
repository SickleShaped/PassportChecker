
namespace PassportChecker.Models.DbModels;

public class PassportModel
{
    public int Series { get; set; }
    public int Number { get; set; }

    public override bool Equals(object? obj)
    {

        PassportModel passport1 = obj as PassportModel;
        if (passport1 == null) return false;

        return Number == passport1.Number && Series == passport1.Series;
    }

    public override int GetHashCode()
    {
        return (23 * 31 + Series) * 31 + Number;
    }

}
