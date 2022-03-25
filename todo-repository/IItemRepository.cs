using System;
using todo_domain;

namespace todo_repository
{
    public interface IItemRepository
    {
        IQueryable<Item> GetAll();
        void Save(Item item);
        void Delete(int id);
    }
}

