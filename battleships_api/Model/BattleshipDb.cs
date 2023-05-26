using Microsoft.EntityFrameworkCore;

class BattleshipDb : DbContext
{
    public BattleshipDb(DbContextOptions<BattleshipDb> options)
        : base(options) { }

    public DbSet<Battleship> Todos => Set<Battleship>();
}