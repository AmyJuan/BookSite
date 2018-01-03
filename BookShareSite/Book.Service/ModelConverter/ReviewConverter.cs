using Book.Dao.Entity;
using Book.IService.ViewModels;
using Book.Service.Common;
using Book.Service.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.ModelConverter
{
    public static class ReviewConverter
    {
        public static ReviewEntity ToEntity(this ReviewViewModel vm)
        {
            ReviewEntity entity = new ReviewEntity();
            entity.ID = vm.ID;
            entity.UserId = vm.UserId;
            entity.BookId = vm.BookId;
            entity.Content = vm.Content;
            entity.Updated = vm.Updated == 0 ? DateTime.UtcNow : DateTimeUtil.ConvertTime(vm.Updated);
            return entity;
        }

        public static ReviewViewModel ToViewModel(this ReviewEntity entity)
        {
            ReviewViewModel vm = new ReviewViewModel();
            vm.ID = entity.ID;
            vm.BookId = entity.BookId;
            vm.Image = entity.User.Image;
            vm.UserId = entity.UserId;
            vm.UserName = entity.User.Name;
            vm.Content = entity.Content;
            vm.Updated = DateTimeUtil.ConvertTime(entity.Updated);
            return vm;
        }

        public static List<ReviewEntity> ToEntity(this List<ReviewViewModel> models)
        {
            if (models == null)
            {
                return null;
            }
            List<ReviewEntity> entities = new List<ReviewEntity>();
            foreach (var model in models)
            {
                entities.Add(model.ToEntity());
            }
            return entities;
        }

        public static List<ReviewViewModel> ToViewModel(this List<ReviewEntity> entities)
        {
            if (entities == null)
            {
                return null;
            }
            List<ReviewViewModel> viewModels = new List<ReviewViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(entity.ToViewModel());
            }
            return viewModels;
        }
    }
}
