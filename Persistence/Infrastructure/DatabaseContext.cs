using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<RoleEntity> Roles { get; set; }
}