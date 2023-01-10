using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using TodoListAppFinalEdition.Client.Services;
using TodoListAppFinalEdition.Shared;

namespace TodoListAppFinalEdition.Client.Pages
{
    public class TodoBase : ComponentBase
    {
        [Inject]
        public ITodoService? TodoService { get; set; }
        [Inject]
        protected HttpClient? httpClient { get; set; }
        [Inject]
        protected AuthenticationStateProvider? AuthStateProvider { get; set; }
        [Inject]
        protected ILocalStorageService? LocalStorage { get; set; }
        [Inject]
        public  NavigationManager? NavigationManager { get; set; }
        [Inject]
        public IEnumerable<TodoItem>? Items { get; set; }
        [Inject]
        protected IJSRuntime Js { get; set; }
        protected string? additem;
        protected UserDto user = new UserDto();
        protected string showError;
        protected override async Task OnInitializedAsync()
        {
            Items = await TodoService!.GetItems();
        }

        protected void Edit(TodoItem item)
        {
            TodoService!.UpdateItem(item);
        }
        protected void Delete(int id)
        {
            TodoService!.DeleteItem(id);
            Items = Items!.Where(x => x.Id != id);

            //var confirmed =  await Js.InvokeAsync<bool>("confirm", "Are you sure?");
            //if (confirmed)
            //{
            //await TodoService.DeleteItem(id);
            //    Items = Items!.Where(x => x.Id != id);

            //}

        }
        protected void Add()
        {
            TodoService!.CreateItem(new TodoItem() {Title = additem!,IsDone = false});
            additem = "";
            NavigationManager?.NavigateTo("/todo", true);

        }
        protected async Task HandleLogin()
        {
            var result = await httpClient!.PostAsJsonAsync("api/auth/login", user);
           
            var token = await result.Content.ReadAsStringAsync();

            
            if(result.IsSuccessStatusCode)
            {
                await LocalStorage!.SetItemAsync("token", token);
                await AuthStateProvider!.GetAuthenticationStateAsync();
                NavigationManager!.NavigateTo("/todo", true);

            }
            else 
            {
                showError = token;
                
                
            }
        }
        protected async Task req()
        {
            var result = await httpClient!.PostAsJsonAsync("api/auth/register", user);
        }
        protected async Task Logout()
        {
            await LocalStorage!.RemoveItemAsync("token");
            await AuthStateProvider!.GetAuthenticationStateAsync();
            NavigationManager!.NavigateTo("/", true);
        }

    }
}
