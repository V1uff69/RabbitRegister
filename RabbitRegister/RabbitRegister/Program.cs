using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RabbitRegister.Services.TrimmingService;
using RabbitRegister.Services.RabbitService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ITrimmingService, TrimmingService>();
builder.Services.AddSingleton<IRabbitService, RabbitService>();
builder.Services.AddDbContext<ItemDbContext>();
builder.Services.AddTransient<DbGenericService<Wool>, DbGenericService<Wool>>();
builder.Services.AddTransient<DbGenericService<Yarn>, DbGenericService<Yarn>>();
builder.Services.AddTransient<DbGenericService<Trimming>, DbGenericService<Trimming>>();
builder.Services.AddTransient<DbGenericService<Rabbit>, DbGenericService<Rabbit>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
