namespace Database.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FavoriteColor { get; set; }
        public string Role { get; set; }

    }
}
