using Book.Dao.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Book.Dao.Mapping.Constants;

namespace Book.Dao.Mapping
{
    public class CollectionMapping : EntityTypeConfiguration<CollectionEntity>
    {
        public CollectionMapping()
        {
            ToTable("Collection", SCHEMA);

            HasKey(t => t.ID);
            HasRequired(t => t.Book).WithMany().HasForeignKey(d => d.BookId);
            HasRequired(t => t.User).WithMany().HasForeignKey(d => d.UserId);
            Property(t => t.Status);
            Property(t => t.Updated).HasColumnType(DATETIME2);
            Property(t => t.Weight);
        }
    }
}
