using Book.Dao.Entity;
using Book.IService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Service.Util;

namespace Collection.Service.ModelConverter
{
    public static class CollectionConverter
    {
        public static CollectionEntity ToEntity(this CollectionViewModel vm)
        {
            CollectionEntity entity = new CollectionEntity();
            entity.ID = vm.ID;
            entity.UserId = vm.UserId;
            entity.BookId = vm.BookId;
            entity.Status = (BookStatus)vm.Status;
            entity.Updated = vm.Updated == 0 ? DateTime.UtcNow : DateTimeUtil.ConvertTime(vm.Updated);
            return entity;
        }

        public static CollectionViewModel ToViewModel(this CollectionEntity entity)
        {
            CollectionViewModel vm = new CollectionViewModel();
            vm.ID = entity.ID;
            vm.UserId = entity.UserId;
            vm.BookId = entity.BookId;
            vm.Status = (VMBookStatus)entity.Status;
            vm.Updated = DateTimeUtil.ConvertTime(entity.Updated);
            return vm;
        }

        public static List<CollectionEntity> ToEntity(this List<CollectionViewModel> models)
        {
            if (models == null)
            {
                return null;
            }
            List<CollectionEntity> entities = new List<CollectionEntity>();
            foreach (var model in models)
            {
                entities.Add(model.ToEntity());
            }
            return entities;
        }

        public static List<CollectionViewModel> ToViewModel(this List<CollectionEntity> entities)
        {
            if (entities == null)
            {
                return null;
            }
            List<CollectionViewModel> viewModels = new List<CollectionViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(entity.ToViewModel());
            }
            return viewModels;
        }
    }
}
