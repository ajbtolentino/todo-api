using todo_api.Data;

namespace todo_api.Service
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository itemRepository;

        public TodoService(ITodoRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IEnumerable<TodoDTO> GetAll()
        {
            var result = this.itemRepository.GetAll().Select(_ => TodoService.ConvertToDTO(_));

            return result;
        }

        public TodoDTO Get(int itemId)
        {
            var item = this.itemRepository.GetAll().FirstOrDefault(_ => _.Id == itemId);

            if (item is not null)
            {
                var result = TodoService.ConvertToDTO(item);

                return result;
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(TodoDTO itemDTO)
        {
            this.itemRepository.Save(new Todo
            {
                Text = itemDTO.Text,
                DateCreated = itemDTO.DateCreated
            });
        }

        public void Update(TodoDTO itemDTO)
        {
            this.itemRepository.Save(new Todo
            {
                Id = itemDTO.Id,
                Text = itemDTO.Text
            });
        }

        public void Delete(int itemId)
        {
            this.itemRepository.Delete(itemId);
        }

        private static TodoDTO ConvertToDTO(Todo item)
        {
            return new TodoDTO
            {
                DateCreated = item.DateCreated,
                Id = item.Id,
                Text = item.Text
            };
        }
	}
}