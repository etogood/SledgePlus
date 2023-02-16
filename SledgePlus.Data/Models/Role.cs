namespace SledgePlus.Data.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    
    public string RoleName { get; set; }
    public string RolePreferences { get; set; }
}