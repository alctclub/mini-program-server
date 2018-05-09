using ALCT.Wechat.Mini.Program.Models;
using Microsoft.EntityFrameworkCore;


namespace ALCT.Wechat.Mini.Program.Databases
{
    public class MPDbContext : DbContext
    {
        
        public MPDbContext()
        {

        }

        public MPDbContext(DbContextOptions<MPDbContext> options) : base(options)
        {

        }

        public DbSet<Token> Token { get; set; }
        public DbSet<Member> Member { get; set; }
    }
}