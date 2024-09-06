namespace PassportChecker.Models.DbModels;

public class ChangeModel
{
    public Guid Id { get; set; }
    public int Series {  get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
}
