namespace Data.Models;
using System.ComponentModel.DataAnnotations;
public class BlogPost
{
    public string? Id { get; set; }
    [Required]
    [MinLength(5)]
    public string Title { get; set; } = "";
    [Required]
    public string Text { get; set; } = "";
    public DateTime PublishDate { get; set; }
    public Category? Category { get; set; }
    public List<Tag> Tags { get; set; } = new();
}
