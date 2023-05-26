using Microsoft.EntityFrameworkCore;

class BoardDb : DbContext
{
    public BoardDb(DbContextOptions<BoardDb> options)
        : base(options) { }

    public DbSet<Board> Todos => Set<Board>();
}