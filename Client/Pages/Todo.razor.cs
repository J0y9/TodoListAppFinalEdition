using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using TodoListAppFinalEdition.Shared;

namespace TodoListAppFinalEdition.Client.Pages
{
    partial class  Todo : MainBase
    {
        [Inject]
        public IEnumerable<TodoItem>? Items { get; set; }
        [Inject]
        protected IJSRuntime Js { get; set; }
        protected string? additem;



        protected override async Task OnInitializedAsync()
        {
            Items = await TodoService!.GetItems();
        }
        protected void Add()
        {
            TodoService!.CreateItem(new TodoItem() { Title = additem!, IsDone = false });
            additem = "";
            NavigationManager?.NavigateTo("/todo", true);

        }
        protected void Edit(TodoItem item)
        {
            TodoService!.UpdateItem(item);
        }
        protected async Task Delete(int id)
        {


            var confirmed = await Js.InvokeAsync<bool>("confirm", "Are you sure?");
            if (confirmed)
            {
                await TodoService.DeleteItem(id);
                Items = Items!.Where(x => x.Id != id);

            }

        }
        
        
    }
}
