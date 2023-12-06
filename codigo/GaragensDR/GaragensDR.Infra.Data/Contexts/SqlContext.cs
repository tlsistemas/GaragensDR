using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace GaragensDR.Infra.Data.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<GaragemDTO> Garagens{ get; set; }
        public DbSet<PassagemDTO> Passagens { get; set; }
        public DbSet<FormaPagamentoDTO> FormasPagamento { get; set; }


        public IDbContextTransaction Transaction { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GaragemMap());
            modelBuilder.ApplyConfiguration(new PassagemMap());
            modelBuilder.ApplyConfiguration(new FormaPagamentoMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging();
        }
        public IDbContextTransaction InitTransaction()
        {
            if (Transaction == null) Transaction = Database.BeginTransaction();
            return Transaction;
        }

        private void RollBack()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
            }
        }

        private void Save()
        {
            try
            {
                ChangeTracker.DetectChanges();
                SaveChanges();
            }
            catch (Exception ex)
            {
                RollBack();
                throw new Exception(ex.Message);
            }
        }

        private void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public void SendChanges()
        {
            Save();
            Commit();
        }
    }
}
