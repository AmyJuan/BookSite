using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.ViewModels
{
    [Serializable]
    public class UserViewModel
    {
        public string ID { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Alt { get; set; }
        public string Image { get; set; }
        public long Create { get; set; }
    }
}
