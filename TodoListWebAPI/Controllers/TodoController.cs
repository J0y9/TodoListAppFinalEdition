using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListAppFinalEdition.Server;
using TodoListAppFinalEdition.Shared;
using TodoListWebAPI.Dto;
using TodoListWebAPI.Interfaces;

namespace TodoListWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
        {
            var items = await _repository.GetItems();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateItem(TodoDto item)
        {
            if (item == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest();

            var mapItem = _mapper.Map<TodoItem>(item);
            var itemCreate = await _repository.CreateItem(mapItem);
            return Ok(itemCreate);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult<TodoItem>> UpdateItem(int itemId,TodoItem item)

        {
            if (item == null)
                return BadRequest(ModelState);
            
            if (!ModelState.IsValid)
                return BadRequest();
            var itemUpdate = await _repository.UpdateItem(item);
            return Ok(itemUpdate);

        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<TodoItem>> DeleteItem(int itemId)
        {
            var itemToDelete = await _repository.GetItem(itemId);
            if (itemToDelete == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var itemDelete = await _repository.DeleteItem(itemToDelete);
            return Ok(itemDelete);
        }

        
    }
}
