using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddDbContext<ItemDbContext>();
builder.Services.AddTransient<DbGenericService<Wool>, DbGenericService<Wool>>();
builder.Services.Configure<CookiePolicyOptions>(options => {
	// This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true; 
	options.MinimumSameSitePolicy = SameSiteMode.None;

});

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
