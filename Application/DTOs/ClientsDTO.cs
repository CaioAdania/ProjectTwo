using System.ComponentModel.DataAnnotations;

namespace ProjectTwo.Application.DTOs
{
    public class ClientsDTO
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? City { get; set; } = null;
        public int? Number { get; set; } = null;
    }
}
