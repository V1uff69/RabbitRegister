using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RabbitRegister.Services.TrimmingService;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.UserService;
using Microsoft.AspNetCore.Http;
using RabbitRegister.Services.BreederService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ITrimmingService, TrimmingService>();
builder.Services.AddSingleton<IRabbitService, RabbitService>();
builder.Services.AddSingleton<UserService, UserService>();
builder.Services.AddSingleton<IBreederService, BreederService>();
builder.Services.AddDbContext<ItemDbContext>();
builder.Services.AddTransient<DbGenericService<Wool>, DbGenericService<Wool>>();
builder.Services.AddTransient<DbGenericService<Yarn>, DbGenericService<Yarn>>();
builder.Services.AddTransient<UserDbService, UserDbService>();
builder.Services.AddTransient<DbGenericService<Breeder>, DbGenericService<Breeder>>();
builder.Services.AddTransient<DbGenericService<Trimming>, DbGenericService<Trimming>>();
builder.Services.AddTransient<DbGenericService<Rabbit>, DbGenericService<Rabbit>>();
builder.Services.Configure<CookiePolicyOptions>(options => {
	// This lambda determines whether user consent for non-essential cookies is needed for a given request. options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.None;

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
	cookieOptions.LoginPath = "/Main/Login/LogInPage";

});
builder.Services.AddMvc().AddRazorPagesOptions(options => {
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

//app.Use(async (context, next) =>
//{
//    if (context.User.Identity.IsAuthenticated && !context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true"))
//    {
//        context.Response.Redirect("/AccessDenied");
//        return;
//    }
//    await next.Invoke();
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
