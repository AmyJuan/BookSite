namespace Book.Dao
{
    using Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class BookDsConfiguration
    {
        public IEnumerable<object> Populate()
        {
            return new List<object>
            {
                new UserMapping(),
                new BookMapping(),
                new CollectionMapping(),
                new ReviewMapping(),
                new WeightMapping(),
                new RuleMapping()
            };
        }
    }
}
