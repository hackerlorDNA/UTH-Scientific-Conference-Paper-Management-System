using Microsoft.EntityFrameworkCore;
using UTHConfMS.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// CẤU HÌNH SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CẤU HÌNH DATABASE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Tự động tạo Database khi chạy (Migration)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        Console.WriteLine("--> Database Migration Successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Database Migration Failed: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();