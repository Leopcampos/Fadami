using Fadami.Mappings;
using Fadami.Models;
using Microsoft.EntityFrameworkCore;

namespace Fadami.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.Property(e => e.BLAtivo)
                    .HasColumnName("BL_ATIVO")
                    .HasColumnType("bit");
            });
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}