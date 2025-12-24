using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.DAL.Repositories.AddressesRepository;
using AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;
using AssetManagement.API.DAL.Repositories.AssetsRepository;
using AssetManagement.API.DAL.Repositories.AssetTypesRepository;
using AssetManagement.API.DAL.Repositories.BranchesRepository;
using AssetManagement.API.DAL.Repositories.InstitutionsRepository; 
using AssetManagement.API.DAL.Repositories.RolesRepository; 
using AssetManagement.API.DAL.Repositories.UserRolesRepository;
using AssetManagement.API.DAL.Repositories.UsersRepository;
using AssetManagement.API.DAL.Repositories.VendorsRepository; 
using AssetManagement.API.DAL.SanityImageDirectory.BackgroundServices;
using AssetManagement.API.DAL.SanityImageDirectory.Repositories;
using AssetManagement.API.DAL.SanityImageDirectory.Services;
using AssetManagement.API.DAL.Services.AddressesService;
using AssetManagement.API.DAL.Services.AssetCategoryService;
using AssetManagement.API.DAL.Services.AssetService;
using AssetManagement.API.DAL.Services.AssetTypeService;
using AssetManagement.API.DAL.Services.BackgroundServices;
using AssetManagement.API.DAL.Services.BranchesService;
using AssetManagement.API.DAL.Services.DepreciationService;
using AssetManagement.API.DAL.Services.EmailService;
using AssetManagement.API.DAL.Services.InstitutionContextService;
using AssetManagement.API.DAL.Services.InstitutionService; 
using AssetManagement.API.DAL.Services.NotificationService;
using AssetManagement.API.DAL.Services.RedisPublisher;
using AssetManagement.API.DAL.Services.ReportExportService;
using AssetManagement.API.DAL.Services.RolesService;
using AssetManagement.API.DAL.Services.SubscriptionService;
using AssetManagement.API.DAL.Services.UserRolesService;
using AssetManagement.API.DAL.Services.UsersService;
using AssetManagement.API.DAL.Services.VendorsService;
using AssetManagement.API.Helpers;
using AssetManagement.API.Middleware; 
using Microsoft.OpenApi.Models;
using Serilog;

namespace AssetManagement.API.Extentions.ProgramExtention;

public static class AddDependencyInjectionExtentionCollection
{
    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        // 🔹 Configure Serilog first
        Log.Logger = CustomLogger.CreateLogger();

        builder.Host.UseSerilog();
        
        
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
        
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext();
        
        builder.Services.AddControllers();
        builder.Services.AddCustomValidationResponses();

        builder.Services.AddHttpContextAccessor();
        
        // Register AutoMapper
        //builder.Services.AddAutoMapper(typeof(Program)); 
        
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        

        builder.Services.AddScoped(typeof(IRepositoryQueryHandler<>), typeof(RepositoryQueryHandler<>));
        builder.Services.AddScoped<IInstitutionContextService, InstitutionContextService>();
        

        
        
        //builder.Services.AddAutoMapper(cfg => { }, typeof(InstitutionProfile).Assembly);
        
        // builder.Services.AddAutoMapper(typeof(InstitutionProfile).Assembly);
        // builder.Services.AddAutoMapper(cfg => { }, typeof(InstitutionProfile).Assembly);
        //builder.Services.AddAutoMapper(typeof(InstitutionProfile).Assembly, typeof(AssetProfile).Assembly);


        
        builder.Services.AddScoped<IAssetTypeRepository, AssetTypeRepository>();
        builder.Services.AddScoped<IAssetTypeService, AssetTypeService>();
        
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IAddressesService, AddressesService>();

        builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
        builder.Services.AddScoped<IInstitutionService, InstitutionService>();
        
        builder.Services.AddScoped<IAssetRepository, AssetRepository>();
        builder.Services.AddScoped<IAssetService, AssetService>();

        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        
        builder.Services.AddScoped<IAssetCategoryRepository, AssetCategoryRepository>();
        builder.Services.AddScoped<IAssetCategoriesService, AssetCategoriesService>();
        
        builder.Services.AddScoped<IBranchRepository, BranchRepository>();
        builder.Services.AddScoped<IBranchesService, BranchesService>();
        
        builder.Services.AddScoped<IVendorRepository, VendorRepository>();
        builder.Services.AddScoped<IVendorsService, VendorsService>();
        
        builder.Services.AddScoped<IAssetRepository, AssetRepository>();
        builder.Services.AddScoped<IAssetService, AssetService>();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUsersService, UsersService>(); 
        
        builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        builder.Services.AddScoped<IUserRolesService, UserRolesService>();
        
        
        // //Sanity Image repository and services
        // builder.Services.AddScoped<ISanityService, SanityService>();
        // builder.Services.AddScoped<ISanityRepository, SanityRepository>();
        // builder.Services.AddHttpClient();
        
        
        
        builder.Services.AddScoped<ISanityImageRepository, SanityImageRepository>();
        builder.Services.AddScoped<ISanityImageService, SanityImageService>();
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ReplaceSanityImageHandler).Assembly);
        });
        builder.Services.AddHostedService<SanityAssetCleanupWorker>();
        
        
        
        


        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IRedisPublisher, RedisPublisher>();
        builder.Services.AddScoped<INotificationService, NotificationService>();

        builder.Services.AddScoped<IDepreciationService, DepreciationService>();
        builder.Services.AddScoped<IReportExportService, ReportExportService>();

        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        
        builder.Services.AddHostedService<SubscriptionBackgroundService>();
        builder.Services.AddHostedService<SubscriptionMaintenanceService>();
        
        
        
        builder.Services.AddHttpClient();

    }
}