using System.Net.Http.Json;
using TodoListAppFinalEdition.Shared;

namespace TodoListAppFinalEdition.Client.Pages
{
    partial class Login : MainBase
    {
        protected UserDto user = new ();
        protected string showError="";
        protected string showSuccess="";

        protected async Task Logini()
        {
            var result = await httpClient!.PostAsJsonAsync($"api/auth/login", user);

            var token = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
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
            if(result.IsSuccessStatusCode)
            {
                showError = "";
                showSuccess = "Account Successfuly Created";
                
            }
            else
            {
                showSuccess = "";
                showError = "username already in use";
            }
        }
    }
}
