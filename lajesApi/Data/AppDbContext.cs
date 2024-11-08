using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Slab> slabs { get; set; }
    public DbSet<Budget> budgets{ get; set; }
    public DbSet<BudgetSlab> budgetSlabs{ get; set; }
    public DbSet<Costumer> costumers{ get; set; }
    public DbSet<RefreshToken> refreshTokens { get; set; }
    public DbSet<User> users{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        base.OnConfiguring(optionsBuilder);
    }
}