using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM_Management_CoreRestApi.Data.Model
{
    public partial class AtmManagmentContext : DbContext
    {
        private DbContextOptions<AtmManagmentContext> options;

        public AtmManagmentContext(DbContextOptions<AtmManagmentContext> options)
            :base(options)
        {
            
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Terminal> Terminal { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"User ID = postgres;Password=1;Server=localhost;Port=5432;Database=atm;Integrated Security=true; Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "atm_mng");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Terminal_ID_seq\"'::regclass)");

                entity.Property(e => e.AccountNo).HasColumnType("varchar");

                entity.Property(e => e.Balance).HasColumnType("money");
            });

            modelBuilder.Entity<Terminal>(entity =>
            {
                entity.ToTable("terminal", "atm_mng");

                entity.Property(e => e.TerminalId).HasDefaultValueSql("nextval('\"Terminal_ID_seq\"'::regclass)");

                entity.Property(e => e.Desc).HasColumnType("varchar");

                entity.Property(e => e.TerminalCode).HasColumnType("varchar");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Transactions", "atm_mng");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CardBrand).HasColumnType("varchar");

                entity.Property(e => e.Lastupdate)
                    .HasColumnName("lastupdate")
                    .HasColumnType("varchar");

                entity.Property(e => e.ReqDateTime).HasColumnType("varchar");

                entity.Property(e => e.Rrn)
                    .HasColumnName("RRN")
                    .HasColumnType("varchar");

                entity.Property(e => e.TermId).HasColumnType("varchar");
            });

            modelBuilder.HasSequence("Acc_id_seq").HasMax(99999999);

            modelBuilder.HasSequence("Term_id_seq").HasMax(9999999999);
        }
    }
}
