using Infrastructures;
using WebAPI.Middlewares;
using WebAPI;
using Domain;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddInfrastructuresService(configuration!.ConnectionStrings.DatabaseConnection);
builder.Services.AddWebAPIService(configuration.Jwt.Key,
                                  configuration.Jwt.Issuer,
                                  configuration.Jwt.Audience,
                                  configuration.BaseUrl.Outlook);
builder.Services.AddSingleton(configuration);

/*
    register with singleton life time
    now we can use dependency injection for AppConfiguration
*/
builder.Services.AddSingleton(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.MapHealthChecks("/healthchecks");
app.UseHttpsRedirection();
// todo authentication
app.UseAuthorization();

app.MapControllers();

app.Run();

// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }