using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Globalization;

namespace ProjectTwo.Entities.Models
{
    public class ClientsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CEP { get; set; }
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
