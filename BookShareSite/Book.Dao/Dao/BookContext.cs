using Book.Dao.Entity;
using Book.Dao.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Dao
{
    public class BookContext : DbContext
    {
        static BookContext()
        {
            Database.SetInitializer<BookContext>(null);//new CreateDatabaseIfNotExists<BookContext>()
        }
        public BookContext() : base("name=BookContext")
        {
        }

        public BookContext(string connStr) : base(connStr)
        {

        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<BookEntity> Book { get; set; }
        public DbSet<CollectionEntity> Collection { get; set; }
        public DbSet<WeightEntity> Wright { get; set; }
        public DbSet<ReviewEntity> Review { get; set; }
        public DbSet<RuleEntity> Rule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configuration = new BookDsConfiguration();
            var genericMethod = (from m in typeof(ConfigurationRegistrar).GetMethods()
                                 where m.Name == "Add"
                                     && m.GetParameters().Length == 1
                                     && m.GetParameters()[0].ParameterType.IsGenericType
                                     && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                                 select m).FirstOrDefault();
            foreach (var obj in configuration.Populate())
            {
                var method = genericMethod.MakeGenericMethod(obj.GetType().BaseType.GenericTypeArguments[0]);
                method.Invoke(modelBuilder.Configurations, new[] { obj });
            }
        }
    }
}
