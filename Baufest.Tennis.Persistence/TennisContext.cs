using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace Baufest.Tennis.Persistence
{
    public class TennisContext : DbContext
    {
        public TennisContext() : base("Tennis")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            MapearAssembly(modelBuilder);
        }

        private void MapearAssembly(DbModelBuilder modelBuilder)
        {
            var entitiesTypes = Assembly.Load("Baufest.Tennis.Domain").GetTypes().Where(type => type.Namespace == "Baufest.Tennis.Domain.Entities");
            var metodo = modelBuilder.GetType().GetMethod("Entity");
            foreach (var entityType in entitiesTypes)
            {
                metodo.MakeGenericMethod(entityType).Invoke(modelBuilder, null);
            }
        }
    }
}
