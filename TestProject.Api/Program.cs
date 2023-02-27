using TestProject.Api.Middleware;
using TestProject.Application;
using TestProject.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("TestPolicy", conf =>
    {
        conf.AllowAnyHeader();
        conf.AllowAnyMethod();
        conf.AllowAnyOrigin();
    });
    //Not recommended for production only for testing purposes
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("TestPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
