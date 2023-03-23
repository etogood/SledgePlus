using System.ComponentModel.DataAnnotations.Schema;

namespace SledgePlus.Data.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public int GroupId { get; set; }

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;

    public string Fullname => Surname + ' ' + Name + ' ' + Patronymic;

    [ForeignKey("RoleId")]
    public Role Role { get; set; }
    [ForeignKey("GroupId")]
    public Group Group { get; set; }
}