using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.WebService
{
    public interface IReviewWebService
    {
        string Save(List<ReviewViewModel> reviews);
        string Load(string bookid);
        string LoadBookAndReview(string bookId, string userId);
    }
}
