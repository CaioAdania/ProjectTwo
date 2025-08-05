using System.Globalization;

namespace ProjectTwo.Entities.Models
{
    public class ItensModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Amout { get; set; }
        public bool IsDeleted { get; set; }
        public bool StateCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
