using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Register all validators from the current assembly
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes:true);
    });

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();