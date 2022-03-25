using Microsoft.Extensions.Logging;
using Services.DTO;
using todo_domain;
using todo_repository;

namespace todo_service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly ILogger logger;

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            this.itemRepository = itemRepository;
            this.logger = logger;
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            var result = this.itemRepository.GetAll().Select(_ => ItemService.ConvertToDTO(_));

            this.logger.LogDebug("{Service} - {Method} - {@Result}", nameof(ItemService), nameof(ItemService.GetAll), result);

            return result;
        }

        public IEnumerable<ItemDTO> GetAllByFilter(ItemFilterDTO filters)
        {
            var result = this.itemRepository.GetAll()
                                      .Where(_ => _.Text.ToLower().Contains(filters.Text.ToLower()))
                                      .Select(_ => ItemService.ConvertToDTO(_));

            this.logger.LogDebug("{Service} - {Method} - {@Result}", nameof(ItemService), nameof(ItemService.GetAllByFilter), result);

            return result;
        }

        public ItemDTO Get(int itemId)
        {
            var item = this.itemRepository.GetAll().FirstOrDefault(_ => _.Id == itemId);

            if (item is not null)
            {
                var result = ItemService.ConvertToDTO(item);

                this.logger.LogInformation("{Service} - {Method} - {@Result}", nameof(ItemService), nameof(ItemService.Get), result);

                return result;
            }

            this.logger.LogDebug("{Service} - {Method} - null", nameof(ItemService), nameof(ItemService.Get));

            return null;
        }

        public void Add(ItemDTO itemDTO)
        {
            this.itemRepository.Save(new Item
            {
                Text = itemDTO.Text,
                DateCreated = itemDTO.DateCreated
            });

            this.logger.LogDebug("{Service} - {Method} - {@Payload}", nameof(ItemService), nameof(ItemService.Add), itemDTO);
        }

        public void Update(ItemDTO itemDTO)
        {
            this.itemRepository.Save(new Item
            {
                Id = itemDTO.Id,
                Text = itemDTO.Text
            });

            this.logger.LogDebug("{Service} - {Method} - {@Payload}", nameof(ItemService), nameof(ItemService.Update), itemDTO);
        }

        public void Delete(int itemId)
        {
            this.itemRepository.Delete(itemId);

            this.logger.LogDebug("{Service} - {Method} - {@itemId}", nameof(ItemService), nameof(ItemService.Delete), itemId);
        }

        private static ItemDTO ConvertToDTO(Item item)
        {
            return new ItemDTO
            {
                DateCreated = item.DateCreated,
                Id = item.Id,
                Text = item.Text
            };
        }
	}
}