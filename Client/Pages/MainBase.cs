using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using TodoListAppFinalEdition.Client.Services;

namespace TodoListAppFinalEdition.Client.Pages
{
    public class MainBase : ComponentBase
    {


        //public TodoBase(IHttpContextAccessor context)
        //{
        //    _context = context;
        //}
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

        protected async Task Logout()
        {
            NavigationManager!.NavigateTo("/", true);
            await LocalStorage!.RemoveItemAsync("token");
            await AuthStateProvider!.GetAuthenticationStateAsync();
        }

    }
}
