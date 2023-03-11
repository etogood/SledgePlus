namespace SledgePlus.Data.Models;

public class Lesson
{
    [Key]
    public int LessonId { get; set; }

    public string LessonName { get; set; }
    public string LessonDescription { get; set; }
    public bool IsPractice { get; set; }
}