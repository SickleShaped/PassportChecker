using PassportChecker.Models.Enums;

namespace PassportChecker.Models
{
    public class Change
    {
        public int Series { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        
    }
}
