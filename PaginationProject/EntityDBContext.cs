using Microsoft.EntityFrameworkCore;
using PaginationProject.Models;

namespace PaginationProject
{
    public partial class EntityDBContext : DbContext
    {
        public EntityDBContext()
        {
            Database.EnsureCreated();
        }

        public EntityDBContext(DbContextOptions<EntityDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<EntityByDb> Entities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityByDb>(entity =>
            {
                entity.HasKey(k => new { k.Id });

                entity.ToTable("EntityByDb");

                entity.HasIndex(e => new { e.Id }, "EntityByDb")
                    .IsUnique();

                entity.HasData(Initialize());
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private EntityByDb[] Initialize()
        {
            EntityByDb[] e = new EntityByDb[100000];
            for (int i = 0; i < e.Length; i++)
            {
                e[i] = new EntityByDb() {Id = i+1, Name = "Имя" + i, Value = i };
            }
            return e;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
