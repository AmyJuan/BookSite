using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Service.Core
{
    // 频繁项集
    public class FrequentItem : IComparable<FrequentItem>
    {
        private String[] idArray; // 频繁项集的集合ID

        private int count;// 频繁项集的支持度计数

        private int length; //频繁项集的长度，1项集或是2项集，亦或是3项集

        public FrequentItem(String[] idArray, int count)
        {
            this.idArray = idArray;
            this.count = count;
            length = idArray.Length;
        }

        public String[] getIdArray()
        {
            return idArray;
        }

        public void setIdArray(String[] idArray)
        {
            this.idArray = idArray;
        }

        public int getCount()
        {
            return count;
        }

        public void setCount(int count)
        {
            this.count = count;
        }

        public int getLength()
        {
            return length;
        }

        public void setLength(int length)
        {
            this.length = length;
        }
        public int CompareTo(FrequentItem o)
        {
            // TODO Auto-generated method stub
            int int1 = int.Parse(this.getIdArray()[0]);
            int int2 = int.Parse(o.getIdArray()[0]);

            return int1.CompareTo(int2);
        }
    }
}
