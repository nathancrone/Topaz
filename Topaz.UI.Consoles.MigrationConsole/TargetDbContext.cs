using Topaz.Data;
using Microsoft.EntityFrameworkCore;

namespace Topaz.UI.Consoles.MigrationConsole
{
    public class TargetDbContext : TopazDbContext
    {
        public TargetDbContext(DbContextOptions<TargetDbContext> options) : base(options) { }
    }
}