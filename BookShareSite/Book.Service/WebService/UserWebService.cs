using Book.Dao;
using Book.Dao.Entity;
using Book.Dao.Repository;
using Book.IService.ViewModels;
using Book.IService.WebService;
using Book.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Service.ModelConverter;
using Book.Common;
using System.Reflection;
using Book.Service.Core;

namespace Book.Service.WebService
{
    public class UserWebService : IUserWebService
    {
        private Logger logger = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IBookDBRepository mDb = new BookDBRepository();

        public string GetUserLists()
        {
            try
            {
                logger.Info("Start Load User List");
                var result = this.mDb.All<UserEntity>().ToList();
                //var result = this.mDb.Filter<UserEntity,object>(c=>c.ID == "").ToList().ToViewModel();
                logger.Info("End Load User List count: {0}", result.Count);
                return SerializerHelper.SerializeByJsonConvert(result.Select(c => c.ID));
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return SerializerHelper.SerializeByJsonConvert("0");
            }
        }

        public string GetUser(string userId)
        {
            try
            {
                var result = this.mDb.Filter<UserEntity>(c => c.ID == userId)?.FirstOrDefault().ToViewModel();
                return SerializerHelper.SerializeByJsonConvert(result);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
            return string.Empty;
        }

        public string Save(List<UserViewModel> users)
        {
            var index = 0;
            foreach (var item in users)
            {
                try
                {
                    UserEntity entity = item.ToEntity();
                    if (!this.mDb.Contains<UserEntity>(c => c.ID == entity.ID))
                    {
                        this.mDb.Add(entity);
                    }
                    index++;
                }
                catch (Exception e)
                {
                }
            }
            return SerializerHelper.SerializeByJsonConvert(index == users.Count);
        }

        public void TestCore()
        {
            bool isTransaction;//是否是事务型数据
            String filePath = "C:\\practice\\testInput.txt";//测试数据文件地址
            String tableFilePath = "C:\\practice\\testInput2.txt"; //关系表型数据文件地址
            double minSup;  //最小支持度阈值
            double minConf; // 最小置信度率
            double delta;    //最大支持度差别阈值
            double[] mis;//多项目的最小支持度数,括号中的下标代表的是商品的ID
            MSApriori tool;   //msApriori算法工具类 

            minConf = 0.3;  //为了测试的方便，取一个偏低的置信度值0.3
            minSup = 0.1;
            delta = 0.5;

            mis = new double[] { -1, 0.1, 0.1, 0.1, 0.1, 0.1 }; //每项的支持度率都默认为0.1，第一项不使用
            isTransaction = true;

            isTransaction = true;
            tool = new MSApriori(filePath, minConf, delta, mis, isTransaction);
            tool.calFItems();
            logger.Info("----------Other----------");

            isTransaction = false; //重新初始化数据
            tool = new MSApriori(tableFilePath, minConf, minSup, isTransaction);
            tool.calFItems();
        }
    }
}
