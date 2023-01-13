using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Data;
using TodoListWebAPI.Dto;
using TodoListWebAPI.Interfaces;

namespace TodoListWebAPI.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoRepository(TodoContext context,IHttpContextAccessor htppContextAcessor)
        {
            _context = context;
            _httpContextAccessor = htppContextAcessor;
        }
        
        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            var userId = GetUserId();
            return await _context.TodoItems.Where(t=> t.UserId == userId).ToListAsync();
        }

        public async Task<TodoItem> GetItem(int itemId)
        {
            return (await _context.TodoItems.FirstOrDefaultAsync(i => i.Id == itemId))!;
        }

        public async Task<TodoItem> CreateItem(TodoItem item)
        {
            item.UserId = GetUserId();
            var itemCreated = await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return itemCreated.Entity;
        }


        public async Task<TodoItem> UpdateItem(TodoItem item)
        {
            item.UserId = GetUserId();
            var itemUpdated =  _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
            return itemUpdated.Entity;
        }

        public async Task<TodoItem> DeleteItem(TodoItem item)
        {
            item.UserId = GetUserId();
            var itemDeleted = _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return itemDeleted.Entity;
        }

        public void DeleteAll()
        {
            _context.TodoItems.RemoveRange();
        }

        public  int GetUserId()
        {
            var userId = Convert.ToInt32(_httpContextAccessor?.HttpContext?.User.FindFirstValue("userId"));

            return userId;
        }
    }
}
