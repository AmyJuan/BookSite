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
    public class RuleMapping : EntityTypeConfiguration<RuleEntity>
    {
        public RuleMapping()
        {
            ToTable("Rule", SCHEMA);

            HasKey(t => t.ID);
            Property(t => t.MinSup);
            Property(t => t.MinConf);
            Property(t => t.Delta);
            Property(t => t.UserId);
        }
    }
}
