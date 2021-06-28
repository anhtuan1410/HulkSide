using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HulkSide.Models
{
    public partial class SampleDatabaseContext : DbContext
    {
        public SampleDatabaseContext()
        {
        }

        public SampleDatabaseContext(DbContextOptions<SampleDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Usergroup> Usergroups { get; set; }
        
        public virtual DbSet<Report_2256> Report_2256s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=SampleDatabase;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_dbo.users");

                entity.ToTable("users");

                entity.HasIndex(e => e.IdUserGroup, "IX_IdUserGroup");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.DeleteBy).HasColumnName("deleteBy");

                entity.Property(e => e.DeleteDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleteDate");

                entity.Property(e => e.EditedBy).HasColumnName("editedBy");

                entity.Property(e => e.EditedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("lastname");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .HasColumnName("note");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdUserGroupNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdUserGroup)
                    .HasConstraintName("FK_dbo.users_dbo.usergroups_IdUserGroup");
            });

            modelBuilder.Entity<Usergroup>(entity =>
            {
                entity.HasKey(e => e.IdUserGroup)
                    .HasName("PK_dbo.usergroups");

                entity.ToTable("usergroups");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate");

                entity.Property(e => e.DeleteBy).HasColumnName("deleteBy");

                entity.Property(e => e.DeleteDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleteDate");

                entity.Property(e => e.EditedBy).HasColumnName("editedBy");

                entity.Property(e => e.EditedDate).HasColumnType("datetime");

                entity.Property(e => e.Groupname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("groupname");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasColumnName("note");
            });

            //tuấn thêm
            modelBuilder.Entity<Report_2256>(en => { en.HasNoKey(); });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
