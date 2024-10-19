using Amazon.S3;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using LML.NPOManagement.Middeware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Add services to the container.

//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IInvestorService, InvestorService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInventoryService, UserInventoryService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddScoped<INotificationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();
builder.Services.AddScoped<NpomanagementContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom JWT AUTH Middleware
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Authorization")
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials


app.UseAuthorization();

Middlewares(app, app.Environment);

app.MapControllers();

app.Run();

void Middlewares(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.ConfigureExceptionHandler(env);
}
