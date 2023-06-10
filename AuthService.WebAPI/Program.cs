using AuthService.WebAPI.Configure;
using AuthService.WebAPI.Configure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AddJwtAuthentication

IConfiguration jwtSettings = configuration.GetSection("JwtSettings");
string secretKey = jwtSettings.GetSection("SecretKey").Value!;
builder.Services.AddJwtAuthentication(secretKey);

#endregion

#region AddCors

builder.Services.AddAllowCors(configuration);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAllCors");
}
else
{
    app.UseCors();
}



app.UseHttpsRedirection();

app.UseApplication();

app.MapControllers();

app.Run();