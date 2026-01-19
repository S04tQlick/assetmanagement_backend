using System.Text;
using Amazon.S3;
using AssetManagement.API.DAL.DatabaseContext;
using AssetManagement.API.DAL.Infrastructure;
using AssetManagement.API.DAL.Services.TokenService;
using AssetManagement.API.Helpers;
using AssetManagement.Entities.DTOs.Auth.Settings;
using AssetManagement.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace AssetManagement.API.Extentions.ProgramExtention;

public static class AddDbContextExtensionCollection
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<InstitutionConnectionInterceptor>();

        //Db
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(ReturnHelpers.Env("ASSET_MGMT_DATABASE_CONNECTION_STRING")));

        //JWT Settings
        services.Configure<JwtSettings>(options =>
        {
            options.Secret = ReturnHelpers.Env("JWT_SECRET");
            options.Issuer = ReturnHelpers.Env("JWT_ISSUER");
            options.Audience = ReturnHelpers.Env("JWT_AUDIENCE");
            options.AccessTokenExpirationMinutes = int.Parse(ReturnHelpers.Env("ACCESS_TOKEN_EXP_MIN"));
            options.RefreshTokenExpirationDays = int.Parse(ReturnHelpers.Env("REFRESH_TOKEN_EXP_DAYS"));
        });

        //Redis (for refresh tokens)
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(ReturnHelpers.Env("ASSET_MGMT_REDIS_CONNECTION_STRING")));
        services.AddSignalR()
            .AddStackExchangeRedis(
                ReturnHelpers.Env("ASSET_MGMT_REDIS_CONNECTION_STRING") ??
                throw new Exception("ASSET_MGMT_REDIS_CONNECTION_STRING Missing"),
                options =>
                    options.Configuration.ChannelPrefix = new RedisChannel("ASMS", RedisChannel.PatternMode.Literal));

        services.AddScoped<ITokenService, TokenService>();


        // EMAIL
        services.Configure<EmailSettingsModel>(options =>
        {
            options.SmtpServer = ReturnHelpers.Env("EMAIL_SMTP_SERVER");
            options.Port = int.Parse(ReturnHelpers.Env("EMAIL_SMTP_PORT"));
            options.SenderName = ReturnHelpers.Env("EMAIL_SENDER_NAME");
            options.SenderEmail = ReturnHelpers.Env("EMAIL_FROM");
            options.Username = ReturnHelpers.Env("EMAIL_USERNAME");
            options.Password = ReturnHelpers.Env("EMAIL_PASSWORD");
            options.UseSsl = bool.Parse(ReturnHelpers.Env("EMAIL_USE_SSL"));
        });

        // Authentication
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ReturnHelpers.Env("JWT_SECRET"))),
                    ValidateIssuer = !string.IsNullOrEmpty(ReturnHelpers.Env("JWT_ISSUER")),
                    ValidIssuer = ReturnHelpers.Env("JWT_ISSUER"),
                    ValidateAudience = !string.IsNullOrEmpty(ReturnHelpers.Env("JWT_AUDIENCE")),
                    ValidAudience = ReturnHelpers.Env("JWT_AUDIENCE"),
                    ClockSkew = TimeSpan.FromSeconds(60)
                };

                // If you want to allow access token to be read from cookie on SSR requests:
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        // Try read from cookie if header absent
                        if (!string.IsNullOrEmpty(ctx.Request.Headers.Authorization)) return Task.CompletedTask;
                        if (ctx.Request.Cookies.TryGetValue("accessToken", out var token))
                        {
                            ctx.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddSingleton(_ =>
        {
            var settings = new AwsSettingsModel
            {
                BucketName = ReturnHelpers.Env("AWS_BUCKET_NAME"),
                Region = ReturnHelpers.Env("AWS_REGION"),
                AccessKey = ReturnHelpers.Env("AWS_ACCESS_KEY_ID"),
                SecretKey = ReturnHelpers.Env("AWS_SECRET_ACCESS_KEY"),
                BucketLogoDirectory = ReturnHelpers.Env("AWS_BUCKET_LOGO_DIRECTORY"),
                BucketDocumentDirectory = ReturnHelpers.Env("AWS_BUCKET_DOCUMENT_DIRECTORY")
            };

            if (string.IsNullOrWhiteSpace(settings.AccessKey) ||
                string.IsNullOrWhiteSpace(settings.SecretKey) ||
                string.IsNullOrWhiteSpace(settings.Region) ||
                string.IsNullOrWhiteSpace(settings.BucketName))
            {
                throw new Exception("AWS environment variables are missing");
            }

            return settings;
        });

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var settings = sp.GetRequiredService<AwsSettingsModel>();
            var region = Amazon.RegionEndpoint.GetBySystemName(settings.Region);
            return new AmazonS3Client(settings.AccessKey, settings.SecretKey, region);
        });

        services.AddAuthorization();
    }
}