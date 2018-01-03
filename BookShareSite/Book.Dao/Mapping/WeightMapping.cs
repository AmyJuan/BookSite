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
    public class WeightMapping : EntityTypeConfiguration<WeightEntity>
    {
        public WeightMapping()
        {
            ToTable("Weight", SCHEMA);

            HasKey(t => t.ID);
            Property(t => t.WeightType);
            Property(t => t.Value);
        }
    }
}
