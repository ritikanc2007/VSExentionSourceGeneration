using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.OutputCaching;
using Restarted.System.Api.Administration;
//using Restarted.System.Api.Administration.SignalRHub;
using Restarted.System.Application;
using Restarted.System.Infrastructure;


var CORS_FeedClientApp = "FeedClientApp";

var builder = WebApplication.CreateBuilder(args);
{
    //SIGNALR- Register singalR service. 
    //builder.Services.AddSignalR();
    //builder.Services.AddHostedService<DashboardFeedService>();
    //health check 
    builder.Services.AddHealthChecks();

    builder.Services
           .AddPresentation()
           .AddApplication()
           .AddInfrastructure(builder.Configuration);



    builder.Services.AddCors((options) =>
    {
        options.AddPolicy(CORS_FeedClientApp,
            new CorsPolicyBuilder()
            .WithOrigins("http://localhost:4200", "http://localhost:51778")
            //.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build());
    });


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => {
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        c.IgnoreObsoleteActions();
        c.IgnoreObsoleteProperties();
        c.CustomSchemaIds(type => type.FullName);
    });

    /*In combination with UseDeveloperExceptionPage, this captures database-related exceptions that can be 
     * resolved by using Entity Framework migrations. When these exceptions occur, an HTML response with details
     * about possible actions to resolve the issue is generated.
     */
    //builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    builder.Services.AddOutputCache(options => options.AddPolicy(
        "Organization",
        builder => builder.Expire(TimeSpan.FromMinutes(60)) // caching branch lookup for 5 mins
        .Tag("OrgaganizationsLookupPolicyTag")
        ));

    //  WORKING- need to exxplore
    //builder.Services.AddTransient<IOutputCacheStore, CustomOutputCacheStore>();

}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


   // app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors(CORS_FeedClientApp);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.UseOutputCache();

    //SIGNALR-Creating minimal api endpoint to return groups, hardcoded data
    //app.MapGet("/api/groups", () => Data.Groups).WithName("GetAllGroups");
    //SIGNALR- user singalR hub, map endpoint
   // app.MapHub<DashboardHub>("/feed");

    app.MapHealthChecks("/healthz");

    app.Run();
}