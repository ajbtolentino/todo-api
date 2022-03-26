namespace todo_api.Data
{
    public interface ITodoRepository
    {
        IQueryable<Todo> GetAll();
        void Save(Todo item);
        void Delete(int id);
    }
}

