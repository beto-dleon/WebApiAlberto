using Microsoft.EntityFrameworkCore;
using WebApiAlberto.Entitys;

namespace WebApiAlberto
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Computadoras> Computadoras { get; set; }

    }
}
