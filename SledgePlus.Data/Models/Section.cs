namespace SledgePlus.Data.Models;

public class Section
{
    [Key]
    public int SectionId { get; set; }

    public string SectionHeader { get; set; }
}