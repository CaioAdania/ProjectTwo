namespace ProjectTwo.Entities.Models
{
    public class MembersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Profile { get; set; }
        public bool StateCode { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
