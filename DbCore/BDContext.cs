using DbCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbCore
{
    public partial class BDContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public BDContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public BDContext(IConfiguration configuration, DbContextOptions<BDContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbContextOptions<BDContext> Options { get;  }

        public virtual DbSet<DeviceInfo> DeviceInfo { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Human> Human { get; set; }
        public virtual DbSet<NotifyRecords> NotifyRecords { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfo { get; set; }
        public virtual DbSet<SensorData> SensorData { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<UserDevices> UserDevices { get; set; }
        public virtual DbSet<UserRelations> UserRelations { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = Configuration.GetConnectionString("Db");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DeviceInfo>(entity =>
            {
                entity.ToTable("device_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.Nvalue)
                    .HasColumnName("nvalue")
                    .HasColumnType("numeric");

                entity.Property(e => e.Svalue).HasColumnName("svalue");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceInfo)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("device_info_fk");
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.ToTable("devices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.PhotoLink).HasColumnName("photo_link");
            });

            modelBuilder.Entity<Human>(entity =>
            {
                entity.ToTable("human");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic).HasColumnName("patronymic");

                entity.Property(e => e.RightHand)
                    .IsRequired()
                    .HasColumnName("right_hand")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Human)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("human_fk");
            });

            modelBuilder.Entity<NotifyRecords>(entity =>
            {
                entity.ToTable("notify_records");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Notification)
                    .IsRequired()
                    .HasColumnName("notification");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NotifyRecords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("notify_records_fk");
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.ToTable("personal_info");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PersonalInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("personal_info_fk");
            });

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.ToTable("sensor_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("numeric");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SensorData)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("sensor_data_fk");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.ToTable("sessions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Expire).HasColumnName("expire");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("sessions_fk");
            });

            modelBuilder.Entity<UserDevices>(entity =>
            {
                entity.ToTable("user_devices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.UserDevices)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("user_devices_d_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDevices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_devices_fk");
            });

            modelBuilder.Entity<UserRelations>(entity =>
            {
                entity.ToTable("user_relations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.UserDestId).HasColumnName("user_dest_id");

                entity.Property(e => e.UserId).HasColumnName("user_source_id");

                entity.HasOne(d => d.UserDest)
                    .WithMany(p => p.UserRelationsUserDest)
                    .HasForeignKey(d => d.UserDestId)
                    .HasConstraintName("user_relations_d_fk");

                entity.HasOne(d => d.UserSource)
                    .WithMany(p => p.UserRelationsUserSource)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_relations_fk");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id)
                    .HasName("users_un")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastLogin).HasColumnName("last_login");

                entity.Property(e => e.RegSecret)
                    .IsRequired()
                    .HasColumnName("reg_secret");
            });
        }
    }
}
