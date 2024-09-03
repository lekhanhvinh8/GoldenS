using GoldenS.Domain;
using Microsoft.EntityFrameworkCore;

namespace GoldenS.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NonKeyTable> NonKeyTable { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);



            modelBuilder.Entity<NonKeyTable>().HasKey(n => n.Status);

        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Database.UseTransaction(null);
            Database.AutoTransactionsEnabled = false;
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}