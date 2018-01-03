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
    public class BookMapping : EntityTypeConfiguration<BookEntity>
    {
        public BookMapping()
        {
            ToTable("Book", SCHEMA);

            HasKey(t => t.ID);
            Property(t => t.Title).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.OriginTitle).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Summary).HasColumnType(Max_NVAR);
            Property(t => t.Image).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Authors).HasColumnType(Max_NVAR);
            Property(t => t.Translators).HasColumnType(Max_NVAR);
            Property(t => t.Create).HasColumnType(DATETIME2);
        }
    }
}
