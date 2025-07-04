using MassTransit;
using Quartz;
using Scalar.AspNetCore;
using System.Reflection;
using XAuth.BackgroundJobs.Jobs;

namespace XAuth.API;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddOpenApi();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
            cfg.Lifetime = ServiceLifetime.Scoped;
        });
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.MapScalarApiReference(options =>
        {
            options
                .WithTheme(ScalarTheme.Kepler)
                .WithDarkModeToggle(true)
                .WithClientButton(true);
        });

        #region other services
        //app.MapScalarApiReference(options => options
        //    .AddPreferredSecuritySchemes(["OAuth2"])
        //    .AddOAuth2Authentication("OAuth2", scheme =>
        //    {
        //        scheme.Flows = new ScalarFlows
        //        {
        //            AuthorizationCode = new AuthorizationCodeFlow
        //            {
        //                ClientId = "your-client-id",
        //                RedirectUri = "https://your-app.com/callback"
        //            },
        //            ClientCredentials = new ClientCredentialsFlow
        //            {
        //                ClientId = "your-client-id",
        //                ClientSecret = "your-client-secret"
        //            }
        //        };

        //        scheme.DefaultScopes = ["profile", "email"];
        //    })
        //    .WithTheme(ScalarTheme.Kepler)
        //    .WithDarkModeToggle(true)
        //    .WithClientButton(true)
        //);
        #endregion

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        /*
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost");

                cfg.ReceiveEndpoint("user-events", e =>
                {
                    e.Consumer<UserRegisteredEventConsumer>();
                });
            });

            x.AddQuartzConsumers();
        });
        
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        */
        
        builder.Services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var cleanupJobKey = new JobKey("cleanup-unconfirmed-users");
            q.AddJob<CleanupUnconfirmedUsersJob>(opts => opts.WithIdentity(cleanupJobKey));

            q.AddTrigger(opts => opts
                .ForJob(cleanupJobKey)
                .WithIdentity("cleanup-unconfirmed-users-trigger")
                .WithCronSchedule("0 0 3 * * ?")); // Every day at 3 AM
        });

      
    }
}