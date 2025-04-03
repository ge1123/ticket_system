using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 設定複合主鍵
            modelBuilder.Entity<TicketCategory>()
                .HasKey(tc => new { tc.TicketId, tc.CategoryId });

            // 設定關聯關係
            modelBuilder.Entity<TicketCategory>()
                .HasOne(tc => tc.Ticket)
                .WithMany(t => t.Categories)
                .HasForeignKey(tc => tc.TicketId);

            modelBuilder.Entity<TicketCategory>()
                .HasOne(tc => tc.Category)
                .WithMany(c => c.TicketCategories)
                .HasForeignKey(tc => tc.CategoryId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Ticket)
                .WithMany(t => t.OrderItems)
                .HasForeignKey(oi => oi.TicketId);

            // 設定索引
            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.TicketNumber)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // 設定預設值
            modelBuilder.Entity<Ticket>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<TicketCategory>()
                .Property(tc => tc.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
} 