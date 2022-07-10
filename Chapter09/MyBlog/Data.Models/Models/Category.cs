namespace Data.Models;
using System.ComponentModel.DataAnnotations;
public class Category
{
    public string? Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
}
