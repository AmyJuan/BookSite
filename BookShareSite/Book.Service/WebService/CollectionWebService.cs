using Book.IService.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.IService.ViewModels;
using Book.Dao.Repository;
using Book.Dao.Entity;
using Book.Service.Common;
using Collection.Service.ModelConverter;

namespace Book.Service.WebService
{
    public class CollectionWebService : ICollectionWebService
    {
        private readonly IBookDBRepository mDb = new BookDBRepository();
        public string GetCollectionLists()
        {
            try
            {
                var result = this.mDb.All<CollectionEntity>().ToList();
                return SerializerHelper.SerializeByJsonConvert(result.FirstOrDefault().ToViewModel());
            }
            catch (Exception e)
            {
                return SerializerHelper.SerializeByJsonConvert(string.Empty);
            }
        }

        public string Save(List<CollectionViewModel> books)
        {
            var index = 0;
            foreach (var item in books)
            {
                try
                {
                    CollectionEntity entity = item.ToEntity();
                    if (!this.mDb.Contains<CollectionEntity>(c => c.ID == entity.ID))
                    {
                        this.mDb.Add(entity);
                    }
                    index++;
                }
                catch (Exception e)
                {
                }
            }
            return SerializerHelper.SerializeByJsonConvert(index == books.Count);
        }
    }
}
