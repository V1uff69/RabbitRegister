using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RabbitRegister.Services.TrimmingService;
using RabbitRegister.Services.RabbitService;
using Microsoft.AspNetCore.Http;
using RabbitRegister.Services.BreederService;
using Microsoft.AspNetCore.Authorization;
using RabbitRegister.Services.Store;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ItemDbContext>();
builder.Services.AddSingleton<IRabbitService, RabbitService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IBreederService, BreederService>();
builder.Services.AddSingleton<ITrimmingService, TrimmingService>();
//We are using Singleton because Transient creates a new instance of our lists
//when navigationg to other pages in the same session, and it need to stay
//saved from the moment something is added on one page, and then transfers to other pages
//of the same session
builder.Services.AddSingleton<IStoreService, StoreService>();
builder.Services.AddTransient<DbGenericService<Wool>, DbGenericService<Wool>>();
builder.Services.AddTransient<DbGenericService<Yarn>, DbGenericService<Yarn>>();
builder.Services.AddTransient<DbGenericService<Order>, DbGenericService<Order>>();
builder.Services.AddTransient<DbGenericService<Rabbit>, DbGenericService<Rabbit>>();
builder.Services.AddTransient<DbGenericService<Breeder>, DbGenericService<Breeder>>();
builder.Services.AddTransient<DbGenericService<Trimming>, DbGenericService<Trimming>>();
builder.Services.AddTransient<DbGenericService<OrderLine>, DbGenericService<OrderLine>>();
/// This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions =>
{
    cookieOptions.LoginPath = "/Main/Login/LogInPage";

});
///This initiates custom authorization policies
builder.Services.AddAuthorization(options =>
{
    /// Creates an authorization policy named "AdminOnly".
    /// This policy allows access only to users with the "Admin" role.
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
    /// Creates an authorization policy named "BreederOnly".
    /// This policy allows access only to users with the "Breeder" role.
    options.AddPolicy("BreederOnly", policy =>
        policy.RequireRole("Breeder"));
});

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeFolder("/Main/Product/Yarn");
    options.Conventions.AuthorizeFolder("/Main/Product/Wool");


}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();