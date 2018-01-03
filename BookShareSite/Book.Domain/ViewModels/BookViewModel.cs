using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.ViewModels
{
    public class BookViewModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string OriginTitle { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public List<string> Author { get; set; }
        public List<string> Translator { get; set; }
    }
}
