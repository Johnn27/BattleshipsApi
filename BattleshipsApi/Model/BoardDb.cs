using Microsoft.EntityFrameworkCore;

class BoardDb : DbContext
{
    public BoardDb(DbContextOptions<BoardDb> options)
        : base(options) { }

    public DbSet<Board> Boards => Set<Board>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Board>()
        .HasMany(p => p.BattleshipIds)
        .WithOne(i => i.Board)
        .HasForeignKey(k => k.BoardId);
}
}