namespace Api.Models;

public class Journal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public AppUser AppUser { get; set; }
    public string AppUserId { get; set; }
    public ICollection<Trade> Trades { get; set; }
}
