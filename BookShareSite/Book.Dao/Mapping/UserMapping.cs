namespace Book.Dao.Mapping
{
    using Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Book.Dao.Mapping.Constants;
    public class UserMapping : EntityTypeConfiguration<UserEntity>
    {
        public UserMapping()
        {
            ToTable("User", SCHEMA);

            HasKey(t => t.ID);
            Property(t => t.Name).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Password).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Alt).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Uid).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Image).HasMaxLength(SHORTCHARLENGTH);
            Property(t => t.Create).HasColumnType(DATETIME2);
        }
    }
}
