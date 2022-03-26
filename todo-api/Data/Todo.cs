using System.ComponentModel.DataAnnotations;

namespace todo_api.Data;

public class Todo
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}

