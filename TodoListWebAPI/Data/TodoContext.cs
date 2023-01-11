using Microsoft.EntityFrameworkCore;
using TodoListAppFinalEdition.Server;
using TodoListAppFinalEdition.Shared;

namespace TodoListWebAPI.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            
        }
        public DbSet<TodoItem> TodoItems { get; set; }
        //public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TodoItem>()

        //        .HasOne(t => t.User)
        //        .WithMany(u => u.UserTodos)
        //        .HasForeignKey(t => t.UserId);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
