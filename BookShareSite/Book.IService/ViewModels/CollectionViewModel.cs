using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.ViewModels
{
    public class CollectionViewModel
    {
        public string ID { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
        public VMBookStatus Status { get; set; }
        public long Updated { get; set; }
    }
}
