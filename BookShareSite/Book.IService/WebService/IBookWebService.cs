using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.WebService
{
    public interface IBookWebService
    {
        string GetBookLists();

        string Save(List<BookViewModel> books);

        string load(int pageIndex, int pageSize, string userId);

        string GetBookAndReviews(string id);

        string GetBook(string id);
    }
}
