using System.ComponentModel;

namespace TodoListWebAPI.Dto
{
    public class TodoDto
    {
        public string Title { get; set; } = string.Empty;
        [DefaultValue(false)]
        public bool IsDone { get; set; }
        
        //public int userId { get; set; }

    }
}
