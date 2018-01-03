using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Entity
{
    public class ReviewEntity
    {
        public string ID { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual BookEntity Book { get; set; }
        public DateTime Updated { get; set; }
        public string Content { get; set; }
    }
}
