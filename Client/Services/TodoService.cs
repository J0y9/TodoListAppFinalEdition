using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using TodoListAppFinalEdition.Shared;

namespace TodoListAppFinalEdition.Client.Services;

public class TodoService : ITodoService
{
    private readonly HttpClient _httpClient;

    public TodoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    
    public async Task<IEnumerable<TodoItem>> GetItems()
    {
        

        var items = await _httpClient.GetFromJsonAsync<IEnumerable<TodoItem>>("api/todo");
        return items!;
    }

    public async Task CreateItem(TodoItem item)
    {
        var result = await _httpClient.PostAsJsonAsync("api/todo", item);
        var response = await result.Content.ReadFromJsonAsync<TodoItem>();
        

    }

    public async Task UpdateItem(TodoItem item)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/todo/{item.Id}", item);
        var response = await result.Content.ReadFromJsonAsync<TodoItem>();
    }

    public async Task DeleteItem(int itemId)
    {
        var result = await _httpClient.DeleteAsync($"api/todo/{itemId}");
        var response = await result.Content.ReadFromJsonAsync<TodoItem>();
    }

    
    
}