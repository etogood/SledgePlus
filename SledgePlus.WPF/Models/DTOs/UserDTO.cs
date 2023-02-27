namespace SledgePlus.WPF.Models.DTOs;

public class UserDTO
{
    public int RoleId { get; set; }
    public int GroupId { get; set; }

    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;
}