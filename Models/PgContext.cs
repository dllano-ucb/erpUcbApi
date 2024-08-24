using Microsoft.EntityFrameworkCore;

namespace erpUcbApi.Models {
    public class PgContext : DbContext {
        public PgContext(DbContextOptions<PgContext> options) : base(options) {

        }

        public DbSet<Users> Users { get; set; }

        
    }
}