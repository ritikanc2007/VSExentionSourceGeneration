using Microsoft.EntityFrameworkCore;

namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class AwakenedSystemContext : DbContext
{
    public AwakenedSystemContext()
    {
    }

    public AwakenedSystemContext(DbContextOptions<AwakenedSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAttribute> AssetAttributes { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<AttributeMaster> AttributeMasters { get; set; }

    public virtual DbSet<Beacon> Beacons { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchAttribute> BranchAttributes { get; set; }

    public virtual DbSet<BusinessType> BusinessTypes { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationFloor> LocationFloors { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Receiver> Receivers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAttribute> UserAttributes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=AwakenedSystem;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Line1).HasMaxLength(250);
            entity.Property(e => e.Line2).HasMaxLength(250);
            entity.Property(e => e.Pin).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Country)
                .HasConstraintName("FK_Address_Country");
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.ToTable("Asset");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExternalId).HasMaxLength(50);
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AssetType).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asset_AssetType");

            entity.HasOne(d => d.Beacon).WithMany(p => p.Assets)
                .HasForeignKey(d => d.BeaconId)
                .HasConstraintName("FK_Asset_Beacons");

            entity.HasOne(d => d.Branch).WithMany(p => p.Assets)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Asset_Branch");
        });

        modelBuilder.Entity<AssetAttribute>(entity =>
        {
            entity.HasKey(e => new { e.AssetId, e.AtttributeName });

            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AtttributeName).HasMaxLength(50);

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAttributes)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetAttributes_Asset");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.ToTable("AssetType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Branch).WithMany(p => p.AssetTypes)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_AssetType_Branch");
        });

        modelBuilder.Entity<AttributeMaster>(entity =>
        {
            entity.ToTable("AttributeMaster");

            entity.Property(e => e.AttributeName).HasMaxLength(50);
            entity.Property(e => e.AttributeType).HasMaxLength(50);
            entity.Property(e => e.DataType).HasMaxLength(50);
            entity.Property(e => e.IsRequired)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Beacon>(entity =>
        {
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.DumpTimeStamp).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProviderCode).HasMaxLength(50);
            entity.Property(e => e.UniqueId).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Branch).WithMany(p => p.Beacons)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Beacons_Branch");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.HasIndex(e => e.Name, "branch_unique_name").IsUnique();

            entity.Property(e => e.BranchCode).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Branches)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Branch_Address");

            entity.HasOne(d => d.BusinessTypeNavigation).WithMany(p => p.Branches)
                .HasForeignKey(d => d.BusinessType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_BusinessType");

            entity.HasOne(d => d.Contact).WithMany(p => p.Branches)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_Branch_Contact");

            entity.HasOne(d => d.Organization).WithMany(p => p.Branches)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_Organization");
        });

        modelBuilder.Entity<BranchAttribute>(entity =>
        {
            entity.HasKey(e => new { e.BranchId, e.AttributeId }).HasName("PK_BranchOrderAttibutes");

            entity.HasOne(d => d.Attribute).WithMany(p => p.BranchAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchAttibutes_AttributeMaster");

            entity.HasOne(d => d.Branch).WithMany(p => p.BranchAttributes)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_BranchAttributes_Branch");
        });

        modelBuilder.Entity<BusinessType>(entity =>
        {
            entity.ToTable("BusinessType");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_City_State");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.Property(e => e.PrimaryEmail).HasMaxLength(250);
            entity.Property(e => e.PrimaryFax)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.PrimaryPhone).HasMaxLength(50);
            entity.Property(e => e.SecondaryEmail).HasMaxLength(250);
            entity.Property(e => e.SecondaryFax)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.SecondaryPhone).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LocationFloor>(entity =>
        {
            entity.ToTable("LocationFloor");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("Organization");

            entity.HasIndex(e => e.Name, "UQ_ORGANIZATION_NAME").IsUnique();

            entity.Property(e => e.ContactPerson).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LanguageId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'en-US')");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OrganizationCode).HasMaxLength(50);
            entity.Property(e => e.TimezoneId).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Organization_Address");

            entity.HasOne(d => d.BusinessTypeNavigation).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.BusinessType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organization_BusinessType");

            entity.HasOne(d => d.Contact).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Organization_Contact1");
        });

        modelBuilder.Entity<Receiver>(entity =>
        {
            entity.ToTable("Receiver");

            entity.HasIndex(e => new { e.UniqueId, e.IsActive, e.IsDeleted }, "UQ_UniqueId").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DumpTimeStamp).HasColumnType("datetime");
            entity.Property(e => e.EnvFactor)
                .HasDefaultValueSql("((2))")
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("Env_Factor");
            entity.Property(e => e.ImmediateEvent).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Label).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NearEvent).HasDefaultValueSql("((1))");
            entity.Property(e => e.RssiatOneMeter).HasColumnName("RSSIAtOneMeter");
            entity.Property(e => e.Rssifilter).HasColumnName("RSSIFilter");
            entity.Property(e => e.UniqueId)
                .HasMaxLength(50)
                .HasColumnName("UniqueID");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.XValue)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("X_Value");
            entity.Property(e => e.YValue)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("Y_Value");

            entity.HasOne(d => d.Branch).WithMany(p => p.Receivers)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Receiver_Branch");

            entity.HasOne(d => d.Location).WithMany(p => p.Receivers)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Receiver_Location");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_State_Country");
        });

        modelBuilder.Entity<TimeZone>(entity =>
        {
            entity.ToTable("TimeZone");

            entity.Property(e => e.TimeZoneId).HasMaxLength(100);
            entity.Property(e => e.BaseUtcOffset).HasMaxLength(50);
            entity.Property(e => e.DaylightName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StandardName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "user_unique_username").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(150);

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_User_Address");

            entity.HasOne(d => d.Contact).WithMany(p => p.Users)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_User_Contact");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_UserRoles");
                        j.ToTable("UserRole");
                    });
        });

        modelBuilder.Entity<UserAttribute>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AttributeId }).HasName("PK_UserAttibutes");

            entity.Property(e => e.AttributeId).HasColumnName("AttributeID");

            entity.HasOne(d => d.Attribute).WithMany(p => p.UserAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAttibutes_AttributeMaster");

            entity.HasOne(d => d.User).WithMany(p => p.UserAttributes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserAttibutes_User");
        });

        //OnModelCreatingPartial(modelBuilder);
    }


}