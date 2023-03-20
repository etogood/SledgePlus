using System.ComponentModel.DataAnnotations.Schema;

namespace SledgePlus.Data.Models;

public class Lesson
{
    [Key]
    public int LessonId { get; set; }

    public int SectionId { get; set; }

    public string LessonName { get; set; }
    public string LessonDescription { get; set; }
    public string LessonDocumentName { get; set; }
    public bool IsPractice { get; set; }

    [ForeignKey("SectionId")]
    public Section Section { get; set; }
}