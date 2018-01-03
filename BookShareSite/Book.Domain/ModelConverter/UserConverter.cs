using Book.Dao.Entity;
using Book.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.ModelConverter
{
    public static class UserConverter
    {
        public static UserEntity ToEntity(this UserViewModel vm)
        {
            UserEntity entity = new UserEntity();
            entity.ID = new Guid();
            entity.Name = vm.Name;
            entity.Password = "123456";
            return entity;
        }

        public static UserViewModel ToViewModel(this UserEntity entity)
        {
            UserViewModel vm = new UserViewModel();
            vm.Name = entity.Name;
            return vm;
        }
    }
}
