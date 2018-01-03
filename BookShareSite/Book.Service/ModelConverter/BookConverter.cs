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
    public static class BookConverter
    {
        public static BookEntity ToEntity(this BookViewModel vm)
        {
            BookEntity entity = new BookEntity();
            entity.ID = vm.ID;
            entity.Title = vm.Title;
            entity.Image = vm.Image;
            entity.OriginTitle = vm.OriginTitle;
            entity.Summary = vm.Summary;
            entity.Authors = SerializerHelper.SerializeByJsonConvert(vm.Authors);
            entity.Translators = SerializerHelper.SerializeByJsonConvert(vm.Translators);
            entity.Create = vm.Create == 0 ? DateTime.UtcNow : DateTimeUtil.ConvertTime(vm.Create);
            return entity;
        }

        public static BookViewModel ToViewModel(this BookEntity entity)
        {
            BookViewModel vm = new BookViewModel();
            vm.ID = entity.ID;
            vm.Title = entity.Title;
            vm.Image = entity.Image;
            vm.OriginTitle = entity.OriginTitle;
            vm.Summary = entity.Summary;
            vm.Authors = SerializerHelper.DeserializeByJsonConvert<List<string>>(entity.Authors);
            vm.Translators = SerializerHelper.DeserializeByJsonConvert<List<string>>(entity.Translators);
            vm.Create = DateTimeUtil.ConvertTime(entity.Create);
            return vm;
        }

        public static List<BookEntity> ToEntity(this List<BookViewModel> models)
        {
            if (models == null)
            {
                return null;
            }
            List<BookEntity> entities = new List<BookEntity>();
            foreach (var model in models)
            {
                entities.Add(model.ToEntity());
            }
            return entities;
        }

        public static List<BookViewModel> ToViewModel(this List<BookEntity> entities)
        {
            if (entities == null)
            {
                return null;
            }
            List<BookViewModel> viewModels = new List<BookViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(entity.ToViewModel());
            }
            return viewModels;
        }
    }
}
