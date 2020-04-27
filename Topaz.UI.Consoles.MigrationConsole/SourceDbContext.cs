using Topaz.Data;
using Microsoft.EntityFrameworkCore;

namespace Topaz.UI.Consoles.MigrationConsole
{
    public class SourceDbContext : TopazDbContext
    {
        public SourceDbContext(DbContextOptions<SourceDbContext> options) : base(options) { }
    }
}