namespace ProjectTwo.Entities.Models
{
    public class ProfileTypeMemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public ICollection<MembersModel> Members { get; set; }
    }
}
