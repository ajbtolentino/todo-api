using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_domain;

public class Item
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}

