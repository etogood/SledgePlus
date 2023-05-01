using System.ComponentModel.DataAnnotations.Schema;

namespace SledgePlus.Data.Models;

public class LessonUser
{
    [Key]
    public int LessonUserId { get; set; }

    public int LessonId { get; set; }

    public int UserId { get; set; }


    [ForeignKey("LessonId")]
    public Lesson Lesson { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

}