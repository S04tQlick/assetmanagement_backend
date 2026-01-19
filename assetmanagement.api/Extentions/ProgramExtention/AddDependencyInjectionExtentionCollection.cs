using AssetManagement.API.DAL.QueryHandlers.RepositoryQueryHandler;
using AssetManagement.API.DAL.Repositories.AddressesRepository;
using AssetManagement.API.DAL.Repositories.AssetCategoriesRepository;
using AssetManagement.API.DAL.Repositories.AssetsRepository;
using AssetManagement.API.DAL.Repositories.AssetTypesRepository;
using AssetManagement.API.DAL.Repositories.AwsRepository;
using AssetManagement.API.DAL.Repositories.BranchesRepository;
using AssetManagement.API.DAL.Repositories.FileUploadsRepository;
using AssetManagement.API.DAL.Repositories.InstitutionsRepository; 
using AssetManagement.API.DAL.Repositories.RolesRepository; 
using AssetManagement.API.DAL.Repositories.UserRolesRepository;
using AssetManagement.API.DAL.Repositories.UsersRepository;
using AssetManagement.API.DAL.Repositories.VendorsRepository; 
using AssetManagement.API.DAL.Services.AddressesService;
using AssetManagement.API.DAL.Services.AssetCategoryService;
using AssetManagement.API.DAL.Services.AssetService;
using AssetManagement.API.DAL.Services.AssetTypeService;
using AssetManagement.API.DAL.Services.AwsService;
using AssetManagement.API.DAL.Services.BackgroundServices;
using AssetManagement.API.DAL.Services.BranchesService;
using AssetManagement.API.DAL.Services.DepreciationService;
using AssetManagement.API.DAL.Services.EmailService;
using AssetManagement.API.DAL.Services.FileUploadService;
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
using VendorService = AssetManagement.API.DAL.Services.VendorsService.VendorService;

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
        
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped(typeof(IRepositoryQueryHandler<>), typeof(RepositoryQueryHandler<>));
        //builder.Services.AddScoped(typeof(IServiceQueryHandler<>), typeof(ServiceQueryHandler<>));
        builder.Services.AddScoped<IInstitutionContextService, InstitutionContextService>();
        
        
        builder.Services.AddScoped<IAssetTypeRepository, AssetTypeRepository>();
        builder.Services.AddScoped<IAssetTypeService, AssetTypeService>();
        
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IAddressService, AddressService>();

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
        // builder.Services.AddScoped<IVendorsService, VendorsService>();
        
        
        //builder.Services.AddScoped<IRepositoryQueryHandler<VendorsModel>, RepositoryQueryHandler<VendorsModel>>();
        builder.Services.AddScoped<IVendorService, VendorService>();
        
        builder.Services.AddScoped<IAssetRepository, AssetRepository>();
        builder.Services.AddScoped<IAssetService, AssetService>();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUsersService, UsersService>(); 
        
        builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        builder.Services.AddScoped<IUserRoleService, UserRoleService>();
        
        
        // builder.Services.AddScoped<ISanityImageRepository, SanityImageRepository>();
        // builder.Services.AddScoped<ISanityImageService, SanityImageService>();
        // builder.Services.AddMediatR(cfg =>
        // {
        //     cfg.RegisterServicesFromAssembly(typeof(ReplaceSanityImageHandler).Assembly);
        // });
        // builder.Services.AddHostedService<SanityAssetCleanupWorker>();
        
        
        
        
        
        
        
        
        builder.Services.AddScoped<IS3Service,S3Service>();
        builder.Services.AddScoped<IS3Repository, S3Repository>();

        builder.Services.AddScoped<IFileUploadService, FileUploadService>();
        builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
        
        
        
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