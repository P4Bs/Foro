namespace ForoWebApp.Models
{
    public record class UserLoginModel
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
