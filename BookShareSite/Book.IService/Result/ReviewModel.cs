using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.Result
{
    [Serializable]
    public class ReviewModel
    {
        public List<ReviewViewModel> Reviews { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
