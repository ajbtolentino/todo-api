namespace todo_api.Service
{
    public interface ITodoService
    {
        IEnumerable<TodoDTO> GetAll();
        TodoDTO Get(int itemId);
        void Add(TodoDTO item);
        void Update(TodoDTO item);
        void Delete(int id);

    }
}

