using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using TodoListAppFinalEdition.Client.Services;
using TodoListAppFinalEdition.Shared;

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
            await LocalStorage!.RemoveItemAsync("token");
            await AuthStateProvider!.GetAuthenticationStateAsync();
            NavigationManager!.NavigateTo("/", true);
        }

    }
}
