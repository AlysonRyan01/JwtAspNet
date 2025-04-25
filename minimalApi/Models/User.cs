namespace minimalApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
    }
}