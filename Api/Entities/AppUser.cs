using Microsoft.AspNetCore.Identity;

namespace Api.Entities
{
    public class AppUser : IdentityUser
    {
        //public string Password { get; set; }
        public ICollection<Journal> Journals { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}