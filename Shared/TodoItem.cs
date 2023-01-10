using System.ComponentModel;

namespace TodoListAppFinalEdition.Shared
{
    public class TodoItem
    {
        
        public int Id { get; set; }
        public string Title { get; set; } = "";
        [DefaultValue(false)]
        public bool IsDone { get; set; }
        //public virtual User User { get; set; }
        //public int UserId { get; set; }
        

    }
}
