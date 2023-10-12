using Etiqa.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Etiqa.DataAccess
{
    public class EtiqaDbContext : DbContext
    {
        private static DbContextOptions dbContextOptions;

        public EtiqaDbContext() 
            : base(dbContextOptions)
        {    
        }

        public EtiqaDbContext(DbContextOptions<EtiqaDbContext> options) 
            : base(options)
        {
            dbContextOptions = options;
        }

        public DbSet<User> User { get; set; }

        public DbSet<UserSkill> UserSkill { get; set; }
    }
}