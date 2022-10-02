using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class AppUser : IdentityUser
{
    public ICollection<Journal> Journals { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
