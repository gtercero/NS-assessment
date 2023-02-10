using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using NS_App.Services.Interfaces;
using NS_App.Services;
using NS_App.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));


var CosmosDBSettings = builder.Configuration.GetSection("CosmosDBSettings").Get<CosmosDBSettings>();
builder.Services.AddSingleton<ICosmosDBService>(ServiceProvider =>
{
    var client = new Microsoft.Azure.Cosmos.CosmosClient(CosmosDBSettings.Account, CosmosDBSettings.Key);
    return new CosmosDbService(client, CosmosDBSettings.DatabaseName, CosmosDBSettings.ContainerName);
});

var BlobStorageSettings = builder.Configuration.GetSection("BlobStorageSettings").Get<BlobStorageSettings>();
builder.Services.AddSingleton<IBlobStorageService>(ServiceProvider =>
{
    return new BlobStorageService(BlobStorageSettings.ConnectionString, BlobStorageSettings.ContainerName);
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=File}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
