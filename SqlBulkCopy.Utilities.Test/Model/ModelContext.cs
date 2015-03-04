using System.Data.Entity;
using SqlBulkCopy.Utilities.Test.Model.Configurations;

namespace SqlBulkCopy.Utilities.Test.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<AccessLog> AccessLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessLogConfiguration());
        }
    }
}
