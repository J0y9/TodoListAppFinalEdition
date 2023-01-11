using Microsoft.EntityFrameworkCore;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Data;
using TodoListWebAPI.Dto;
using TodoListWebAPI.Interfaces;

namespace TodoListWebAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            return await _context.TodoItems.OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<TodoItem> GetItem(int itemId)
        {
            return (await _context.TodoItems.FirstOrDefaultAsync(i => i.Id == itemId))!;
        }

        public async Task<TodoItem> CreateItem(TodoItem item)
        {

            var itemCreated = await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return itemCreated.Entity;
        }

        public async Task<TodoItem> UpdateItem(TodoItem item)
        {
            var itemUpdated =  _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
            return itemUpdated.Entity;
        }

        public async Task<TodoItem> DeleteItem(TodoItem item)
        {
            var itemDeleted = _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return itemDeleted.Entity;
        }

        public void DeleteAll()
        {
            _context.TodoItems.RemoveRange();
        }
    }
}
