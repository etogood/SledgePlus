namespace SledgePlus.Data.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}