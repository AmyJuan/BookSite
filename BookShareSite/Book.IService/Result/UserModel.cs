using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.Result
{
    [Serializable]
    public class UserModel
    {
        public List<BookViewModel> Books { get; set; }
        public List<CollectionViewModel> Collections { get; set; }
    }
}
