namespace todo_api.Service
{
	public class TodoDTO
	{
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}

