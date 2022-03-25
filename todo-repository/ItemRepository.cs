using todo_domain;

namespace todo_repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext dataContext;

        public ItemRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public void Delete(int id)
        {
            var item = this.dataContext.Items.FirstOrDefault(_ => _.Id == id);
            this.dataContext.Items.Remove(item);

            this.dataContext.SaveChanges();
        }

        public IQueryable<Item> GetAll()
        {
            return this.dataContext.Items.AsQueryable();
        }

        public void Save(Item item)
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

