namespace demo_api.Models
{
    public class UserToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public TimeSpan Validaty { get; set; }
        public DateTime ExpiredTime { get; set; }

        // Navigation

        public User? User { get; set; }

    }
}