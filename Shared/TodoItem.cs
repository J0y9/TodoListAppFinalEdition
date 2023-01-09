using System.ComponentModel;

namespace TodoListAppFinalEdition.Shared
{
    public class TodoItem
    {
        public string Title { get; set; } = "";
        [DefaultValue(false)]
        public int Id { get; set; }
        public bool IsDone { get; set; }
    
    }
}
