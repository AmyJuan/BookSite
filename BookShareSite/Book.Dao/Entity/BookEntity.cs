using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Entity
{
    public class BookEntity
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string OriginTitle { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string Authors { get; set; }
        public string Translators { get; set; }
        public DateTime Create { get; set; }
    }
}
