using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    public DbSet<Slab> slabs { get; set; }
    public DbSet<Budget> budgets{ get; set; }
    public DbSet<BudgetSlab> budgetSlabs{ get; set; }
    public DbSet<Costumer> costumers{ get; set; }
    public DbSet<Freight> freights{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;port=3306;database=lajes;user=root;password=Local1234");
        
        base.OnConfiguring(optionsBuilder);
    }
}