
namespace PassportChecker.Models.DbModels;

public class PassportModel
{
    public Guid Id { get; set; }
    public int Series { get; set; }
    public int Number { get;set; }
    public bool IsActive { get; set; }

}
