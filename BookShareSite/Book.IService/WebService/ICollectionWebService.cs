using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.IService.WebService
{
    public interface ICollectionWebService
    {
        string GetCollectionLists();

        string Save(List<CollectionViewModel> books);
    }
}
