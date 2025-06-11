using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Contexts
{
    public interface IBaseContext
    {
        public DbSet<User> Users { get; set; }
    }
}
