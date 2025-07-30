using Serilog;
using Serilog.Formatting.Json;

namespace HackerNews.HackerNews.Host.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Log.Logger = Configure(new LoggerConfiguration(), builder).CreateBootstrapLogger();

        AppDomain.CurrentDomain.UnhandledException += (_, args) => builder.HandleDomainUnhandledException(args);

        static LoggerConfiguration Configure(
            LoggerConfiguration loggerConfiguration,
            IHostApplicationBuilder builder) => loggerConfiguration
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console(new JsonFormatter());

        builder.Host.UseSerilog((context, loggerConfiguration) => Configure(loggerConfiguration, builder));

        return builder;
    }

    private static void HandleDomainUnhandledException(this WebApplicationBuilder builder, UnhandledExceptionEventArgs args)
    {
        Log.Logger.Fatal((Exception) args.ExceptionObject, "Application terminated");
    }
}