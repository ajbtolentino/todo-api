namespace todo_api.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext dataContext;

        public TodoRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public void Delete(int id)
        {
            var item = this.dataContext.Items.FirstOrDefault(_ => _.Id == id);

            if (item is not null)
            {
                this.dataContext.Items.Remove(item);

                this.dataContext.SaveChanges();
            }
        }

        public IQueryable<Todo> GetAll()
        {
            return this.dataContext.Items.AsQueryable();
        }

        public void Save(Todo item)
        {
            var result = this.dataContext.Items.FirstOrDefault(_ => _.Id == item.Id);

            if(result is null)
            {
                this.dataContext.Items.Add(item);
            }
            else
            {
                result.Text = item.Text;
            }

            this.dataContext.SaveChanges();
        }
    }
}

