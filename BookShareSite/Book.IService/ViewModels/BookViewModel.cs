using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.ViewModels
{
    [Serializable]
    public class BookViewModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string OriginTitle { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Translators { get; set; }
        public long Create { get; set; }
    }
}
