using ASPNetCoreMastersTodoList.Api.Enrichers;
using ASPNetCoreMastersTodoList.Api.Extensions;
using ASPNetCoreMastersTodoList.Api.MIddleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using todo_repository;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.Enrich.With<UserEnricher>();
});

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

builder.AddAutofacDependencies();

var app = builder.Build();

var context = app.Services.GetRequiredService< DataContext>();

context.Database.Migrate();

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}


app.UsePerformaceLogger();
app.UseCustomErrorMessage();
app.UseSerilogRequestLogging();

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
