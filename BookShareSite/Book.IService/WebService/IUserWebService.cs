namespace Book.IService.WebService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModels;
    public interface IUserWebService
    {
        string GetUser(string userId);
        string GetUserLists();

        string Save(List<UserViewModel> users);

        void TestCore();

    }
}
