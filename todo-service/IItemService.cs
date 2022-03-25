using System;
using Services.DTO;

namespace todo_service
{
    public interface IItemService
    {
        IEnumerable<ItemDTO> GetAll();
        IEnumerable<ItemDTO> GetAllByFilter(ItemFilterDTO filters);
        ItemDTO Get(int itemId);
        void Add(ItemDTO item);
        void Update(ItemDTO item);
        void Delete(int id);
        //bool CheckUsername(string userName);

    }
}

