using TodoListAppFinalEdition.Server;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Dto;

namespace TodoListWebAPI.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetItems();
        Task<TodoItem> GetItem(int itemId);
        Task<TodoItem> CreateItem(TodoItem item);
        Task<TodoItem> UpdateItem(TodoItem item);
        Task<TodoItem> DeleteItem(TodoItem item);
        void DeleteAll();


    }
}
