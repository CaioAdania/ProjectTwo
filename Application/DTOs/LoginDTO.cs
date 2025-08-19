namespace ProjectTwo.Application.DTOs
{
    public class LoginDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public MemberDTO User { get; set; } = new MemberDTO();
    }
    public class MemberDTO
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
    }
}
