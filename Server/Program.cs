using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddAuthentication("cookie").AddCookie("cookie");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseAuthentication();

//app.MapGet("/username", (HttpContext ctx) =>
//{
//    return ctx.User.FindFirst("usr").Value;
//}

//);
//app.MapGet("/login", async (HttpContext ctx) =>
//{
//    var claims = new List<Claim>();
//    claims.Add(new Claim("usr","Asd"));
//    var identity = new ClaimsIdentity(claims, "cookie");
//    var user = new ClaimsPrincipal(identity);

//    await ctx.SignInAsync("cookie", user);
//     ctx.Response.Redirect("/");

//});
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
