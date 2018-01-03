using Book.Dao.Entity;
using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Service.Util;

namespace Book.Service.ModelConverter
{
    public static class UserConverter
    {
        public static UserEntity ToEntity(this UserViewModel vm)
        {
            UserEntity entity = new UserEntity();
            entity.ID = vm.ID;
            entity.Name = vm.Name;
            entity.Password = vm.Password ?? "123456";
            entity.Uid = vm.Uid;
            entity.Image = vm.Image;
            entity.Alt = vm.Alt;
            entity.Create = vm.Create == 0 ? DateTime.UtcNow : DateTimeUtil.ConvertTime(vm.Create);
            return entity;
        }

        public static UserViewModel ToViewModel(this UserEntity entity)
        {
            UserViewModel vm = new UserViewModel();
            vm.Name = entity.Name;
            vm.ID = entity.ID;
            vm.Password = entity.Password;
            vm.Uid = entity.Uid;
            vm.Image = entity.Image;
            vm.Alt = entity.Alt;
            vm.Create = DateTimeUtil.ConvertTime(entity.Create);
            return vm;
        }

        public static List<UserEntity> ToEntity(this List<UserViewModel> models)
        {
            if (models == null)
            {
                return null;
            }
            List<UserEntity> entities = new List<UserEntity>();
            foreach (var model in models)
            {
                entities.Add(model.ToEntity());
            }
            return entities;
        }

        public static List<UserViewModel> ToViewModel(this List<UserEntity> entities)
        {
            if (entities == null)
            {
                return null;
            }
            List<UserViewModel> viewModels = new List<UserViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(entity.ToViewModel());
            }
            return viewModels;
        }
    }
}
