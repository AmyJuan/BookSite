using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Entity
{
    public class RuleEntity
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public double MinSup { get; set; }//最小支持度阈值
        public double MinConf { get; set; }// 最小置信度率
        public double Delta { get; set; }//最大支持度差别阈值
    }
}
