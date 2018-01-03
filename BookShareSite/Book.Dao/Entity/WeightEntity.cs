using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Entity
{
    public class WeightEntity
    {
        public string ID { get; set; }
        public WeightType WeightType { get; set; }
        public int Value { get; set; }
    }
}
