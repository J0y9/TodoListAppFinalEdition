using AutoMapper;
using TodoListAppFinalEdition.Shared;

namespace TodoListWebAPI.Dto
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<TodoDto,TodoItem>();
        }
    }
}
