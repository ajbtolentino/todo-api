using ASPNetCoreMastersTodoList.Api.AppSettings;
using ASPNetCoreMastersTodoList.Api.Enrichers;
using ASPNetCoreMastersTodoList.Api.Extensions;
using ASPNetCoreMastersTodoList.Api.MIddleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
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

builder.Services.AddSwaggerGen(c =>
{
    /*
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field. NOTE: Include the word 'Bearer'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });*/
});

builder.Services.AddDbContext<DataContext>();

/*
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["jwt:secret"]));

builder.Services.Configure<JwtOptions>(_ => _.SecurityKey = key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
    options.DefaultScheme = "Bearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = key
    };
});*/

builder.AddAutofacDependencies();

var app = builder.Build();

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

/*
app.UseAuthentication();

app.UseAuthorization();
*/

app.MapControllers();

app.Run();
