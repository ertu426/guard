using AuthService.WebAPI.Configure;
using AuthService.WebAPI.Configure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 获取 配置文件中 secretKey 的值
IConfiguration jwtSettings = builder.Configuration.GetSection("JwtSettings");
string secretKey = jwtSettings.GetSection("SecretKey").Value!;
builder.Services.AddJwtAuthentication(secretKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtApplication();

app.MapControllers();

app.Run();