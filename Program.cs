using Microsoft.EntityFrameworkCore;
using YatraBackend.Common.Configs;
using YatraBackend.Database;
using YatraBackend.Services;
using YatraBackend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    {
        options.AddPolicy("allow-all-policy", policy =>
        {
            policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
        });
    });

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("YatraDbConnection"));
});


//Register interfaces here
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();


//Register IOptions here
builder.Services.Configure<JwtConfigs>(builder.Configuration.GetSection(JwtConfigs.SectionName));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if ((await db.Database.GetPendingMigrationsAsync()).Any())
{
    await db.Database.MigrateAsync();
    Console.WriteLine("Pending migrations applied.");
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseCors("allow-all-policy");
app.MapControllers();

app.Run();