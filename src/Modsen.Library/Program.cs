using Modsen.Library.Extensions;
using Modsen.Library.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServiceMiddleware();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.InitializeServiceContextProvider();
app.Run();