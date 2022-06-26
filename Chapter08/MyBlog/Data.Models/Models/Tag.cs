namespace Data.Models;
using System.ComponentModel.DataAnnotations;
public class Tag
{
    public string? Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
}
