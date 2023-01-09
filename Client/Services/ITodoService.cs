
using System.Collections;
using TodoListAppFinalEdition.Shared;

namespace TodoListAppFinalEdition.Client.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetItems();
        Task CreateItem(TodoItem item);
        Task DeleteItem(int itemId);
        Task UpdateItem(TodoItem item);
    }
}
