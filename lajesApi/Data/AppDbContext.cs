using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    DbSet<Laje> Lajes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;port=3306;database=lajes;user=root;password=Local1234");
        
        base.OnConfiguring(optionsBuilder);
    }
}