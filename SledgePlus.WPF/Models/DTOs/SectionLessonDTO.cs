using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Models.DTOs;

public class SectionLessonDTO
{
    public int SectionId { get; set; }
    public int LessonId { get; set; }

    public Section Section { get; set; }
    public Lesson Lesson { get; set; }
}