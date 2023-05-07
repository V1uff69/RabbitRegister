using RabbitRegister.EFDbContext;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Model;
using RabbitRegister.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IRabbitService, RabbitService>();
builder.Services.AddDbContext<ItemDbContext>();
builder.Services.AddTransient<DbGenericService<Rabbit>, DbGenericService<Rabbit>>();
builder.Services.AddTransient<DbGenericService<Breeder>, DbGenericService<Breeder>>();
builder.Services.AddTransient<DbGenericService<Order>, DbGenericService<Order>>();
builder.Services.AddTransient<DbGenericService<OrderLine>, DbGenericService<OrderLine>>();
builder.Services.AddTransient<DbGenericService<Product>, DbGenericService<Product>>();
builder.Services.AddTransient<DbGenericService<Trimming>, DbGenericService<Trimming>>();
builder.Services.AddTransient<DbGenericService<Wool>, DbGenericService<Wool>>();
builder.Services.AddTransient<DbGenericService<Yarn>, DbGenericService<Yarn>>();

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
