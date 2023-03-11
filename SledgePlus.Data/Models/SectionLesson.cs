using System.ComponentModel.DataAnnotations.Schema;

namespace SledgePlus.Data.Models;

public class SectionLesson
{
    [Key] 
    public int SectionLessonId { get; set; }

    public int SectionId { get; set; }
    public int LessonId { get; set; }

    [ForeignKey("SectionId")]
    public Section Section { get; set; }
    [ForeignKey("LessonId")]
    public Lesson Lesson { get; set; }
}