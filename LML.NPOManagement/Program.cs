using Amazon.S3;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using LML.NPOManagement.Middeware;
using System.Text.Json.Serialization;
using NLog.Web;
using System.Reflection.Metadata;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Info("Application start.");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddControllersWithViews();
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
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();
    builder.Services.AddScoped<NpomanagementContext>();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    // Custom JWT AUTH Middleware
    app.UseMiddleware<JwtMiddleware>();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Authorization")
                    .SetIsOriginAllowed(origin => true) // Allow any origin
                    .AllowCredentials()); // Allow credentials

    app.UseRouting();
    app.UseAuthorization();

    Middlewares(app, app.Environment);

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of an exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

void Middlewares(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.ConfigureExceptionHandler(env);
}