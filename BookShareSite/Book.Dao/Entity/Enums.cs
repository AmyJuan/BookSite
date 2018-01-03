using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Entity
{
    [Serializable]
    public enum BookStatus
    {
        Wish = 0,
        Reading = 1,
        Read = 2,
        Add = 3
    }

    public enum WeightType
    {
        AddBook = 0,
        MarkBook  =1,
        WordLess30 = 2,
        WordM30L100 = 3,
        WordMore100 = 4,
    }
}
