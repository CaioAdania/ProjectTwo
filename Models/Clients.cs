using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Globalization;

namespace ProjectTwo.Models
{
    public class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public int Number { get; set; }
        public string? Country { get; set; }
        public bool IsDeleted { get; set; }
        public bool StateCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
