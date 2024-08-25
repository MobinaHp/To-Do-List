using Microsoft.EntityFrameworkCore;

namespace ToDo.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<List> Lists { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<List>().ToTable("Lists");
            //modelBuilder.Entity<Task>().ToTable("Tasks");
            base.OnModelCreating(modelBuilder);
        }
    }
}
