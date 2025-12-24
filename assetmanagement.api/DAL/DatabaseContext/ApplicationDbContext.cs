using AssetManagement.API.DAL.Services.InstitutionContextService;
using AssetManagement.Entities.Models;

namespace AssetManagement.API.DAL.DatabaseContext;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IInstitutionContextService? institutionContext = null) : DbContext(options)
{
    public DbSet<InstitutionsModel> InstitutionsModel { get; set; }
    public DbSet<BranchesModel> BranchesModel { get; set; }
    public DbSet<AssetTypesModel> AssetTypesModel { get; set; }
    public DbSet<AssetCategoriesModel> AssetCategoriesModel { get; set; }
    public DbSet<AssetsModel> AssetsModel { get; set; }
    public DbSet<MaintenancesModel> MaintenancesModel { get; set; }
    public DbSet<SubscriptionsModel> SubscriptionsModel { get; set; }
    public DbSet<VendorsModel> VendorsModel { get; set; }
    public DbSet<RolesModel> RolesModel { get; set; }
    public DbSet<UsersModel> UsersModel { get; set; }

    public DbSet<UserRolesModel> UserRolesModel { get; set; }
    public DbSet<AddressesModel> AddressesModel { get; set; }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply query filters for institution-specific entities
        var institutionId = institutionContext?.InstitutionId;

        if (institutionId == null) return;
        
        modelBuilder.Entity<UsersModel>()
            .HasIndex(u => u.NormalizedEmail)
            .IsUnique();

        modelBuilder.Entity<BranchesModel>()
            .HasQueryFilter(b => b.InstitutionId == institutionId);

        modelBuilder.Entity<AssetCategoriesModel>()
            .HasQueryFilter(c => c.InstitutionId == institutionId);

        modelBuilder.Entity<AssetsModel>()
            .HasQueryFilter(a => a.InstitutionId == institutionId);

        modelBuilder.Entity<MaintenancesModel>()
            .HasQueryFilter(m => m.InstitutionId == institutionId);

        modelBuilder.Entity<SubscriptionsModel>()
            .HasQueryFilter(s => s.InstitutionId == institutionId);

        modelBuilder.Entity<VendorsModel>()
            .HasQueryFilter(v => v.InstitutionId == institutionId);

        modelBuilder.Entity<AssetsModel>()
            .Property(a => a.DepreciationMethod)
            .HasConversion<string>();
    }
}















// public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IInstitutionContextService? institutionContext = null) : base(options)
//     {
//         _institutionContext = institutionContext;
//     }

// public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IInstitutionContextService? institutionContext = null) : DbContext(options)
// {
//     
//     
//     public DbSet<Institution> Institutions => Set<Institution>();
//     public DbSet<User> Users => Set<User>();
//     public DbSet<InstitutionUser> InstitutionUsers => Set<InstitutionUser>();
//     public DbSet<Branch> Branches => Set<Branch>();
//     public DbSet<Vendor> Vendors => Set<Vendor>();
//     public DbSet<AssetType> AssetTypes => Set<AssetType>();
//     public DbSet<AssetCategory> AssetCategories => Set<AssetCategory>();
//     public DbSet<Asset> Assets => Set<Asset>();
//     public DbSet<MaintenanceLog> MaintenanceLogs => Set<MaintenanceLog>();
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//
//         // Apply query filters for institution-specific entities
//         var institutionId = _institutionContext?.InstitutionId;
//         
//         
//
//         if (institutionId != null)
//         {
//             modelBuilder.Entity<Branch>().HasQueryFilter(b => b.InstitutionId == institutionId);
//             modelBuilder.Entity<AssetCategory>().HasQueryFilter(c => c.InstitutionId == institutionId);
//             modelBuilder.Entity<Asset>().HasQueryFilter(a => a.InstitutionId == institutionId);
//             modelBuilder.Entity<Maintenance>().HasQueryFilter(m => m.InstitutionId == institutionId);
//             modelBuilder.Entity<Subscription>().HasQueryFilter(s => s.InstitutionId == institutionId);
//             modelBuilder.Entity<Vendor>().HasQueryFilter(v => v.InstitutionId == institutionId);
//         }
//         
//         
//
//         // // Example: restrict cascade deletes to prevent accidental tenant data loss
//         // modelBuilder.Entity<Institution>()
//         //     .HasMany(i => i.Branches)
//         //     .WithOne(b => b.Institution)
//         //     .HasForeignKey(b => b.InstitutionId)
//         //     .OnDelete(DeleteBehavior.Cascade);
//         //
//         // modelBuilder.Entity<InstitutionUser>()
//         //     .HasIndex(iu => new { iu.InstitutionId, iu.UserId })
//         //     .IsUnique();
//         //
//         // // Optional: ensure SanityAssetId uniqueness within institution
//         // modelBuilder.Entity<Asset>()
//         //     .HasIndex(a => new { a.InstitutionId, a.SanityAssetId })
//         //     .IsUnique(false);
//     }
// }