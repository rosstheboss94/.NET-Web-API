#nullable enable

namespace Api.Dtos
{
    public class AppUserDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}