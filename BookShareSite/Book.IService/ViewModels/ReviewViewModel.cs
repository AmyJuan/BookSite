using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.ViewModels
{
    public class ReviewViewModel
    {
        public string ID { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public long Updated { get; set; }
        public string Content { get; set; }
    }
}
