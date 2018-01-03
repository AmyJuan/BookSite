using Book.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;


namespace Book.Service.Core
{
    public class MSApriori
    {
        // 前件判断的结果值，用于关联规则的推导  
        public static readonly int PREFIX_NOT_SUB = -1;
        public static readonly int PREFIX_EQUAL = 1;
        public static readonly int PREFIX_IS_SUB = 2;

        private bool isTransaction; // 是否读取的是事务型数据  
        private int initFItemNum;// 最大频繁k项集的k值  
        private String filePath;// 事务数据文件地址  
        private double minSup;   // 最小支持度阈值  
        private double minConf; // 最小置信度率  
        private double delta;  // 最大支持度差别阈值 
        private double[] mis;  // 多项目的最小支持度数,括号中的下标代表的是商品的ID  
        private List<String[]> totalGoodsIDs;// 每个事务中的商品ID  
        private List<String[]> transactionDatas;   // 关系表数据所转化的事务数据  
        private List<FrequentItem> resultItem;  // 过程中计算出来的所有频繁项集列表 
        private List<String[]> resultItemID;// 过程中计算出来频繁项集的ID集合 
        private Dictionary<String, int> attr2Num;  // 属性到数字的映射图  
        private Dictionary<int, String> num2Attr; // 数字id对应属性的映射图  
        private Dictionary<String, int[]> fItem2Id;  // 频繁项集所覆盖的id数值  
        private Logger logger = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);

        /** 
         * 事务型数据关联挖掘算法 
         *  
         * @param filePath 
         * @param minConf 
         * @param delta 
         * @param mis 
         * @param isTransaction 
         */
        public MSApriori(String filePath, double minConf, double delta,
                double[] mis, bool isTransaction)
        {
            this.filePath = filePath;
            this.minConf = minConf;
            this.delta = delta;
            this.mis = mis;
            this.isTransaction = isTransaction;
            this.fItem2Id = new Dictionary<String, int[]>();

            readDataFile();
        }

        /** 
         * 非事务型关联挖掘 
         *  
         * @param filePath 
         * @param minConf 
         * @param minSup 
         * @param isTransaction 
         */
        public MSApriori(String filePath, double minConf, double minSup,
                bool isTransaction)
        {
            this.filePath = filePath;
            this.minConf = minConf;
            this.minSup = minSup;
            this.isTransaction = isTransaction;
            this.delta = 1.0;
            this.fItem2Id = new Dictionary<String, int[]>();

            readRDBMSData(filePath);
        }

        /** 
         * 从文件中读取数据 
         */
        private void readDataFile()
        {
            String[] temp = null;
            List<String[]> dataArray;

            dataArray = readLine(filePath);
            totalGoodsIDs = new List<String[]>();

            foreach (var array in dataArray)
            {
                temp = new String[array.Length - 1];
                Array.Copy(array, 1, temp, 0, array.Length - 1);
                //System.arraycopy(array, 1, temp, 0, array.Length - 1);
                totalGoodsIDs.Add(temp);// 将事务ID加入列表吧中  
            }
        }

        /** 
         * 从文件中逐行读数据 
         *  
         * @param filePath 
         *            数据文件地址 
         * @return 
         */
        private List<String[]> readLine(String filePath)
        {
            //File file = new File(filePath);
            List<String[]> dataArray = new List<String[]>();

            try
            {
                StreamReader reader = File.OpenText(filePath);
                String str;
                String[] tempArray;
                while ((str = reader.ReadLine()) != null)
                {
                    tempArray = str.Split(' ');
                    dataArray.Add(tempArray);
                }
                reader.Close();
            }
            catch (IOException e)
            {
                logger.Error(e.Message);
            }

            return dataArray;
        }

        /** 
         * 计算频繁项集 
         */
        public void calFItems()
        {
            FrequentItem fItem;

            computeLink();
            printFItems();

            if (isTransaction)
            {
                fItem = resultItem.LastOrDefault();
                // 取出最后一个频繁项集做关联规则的推导  
                logger.Info("The last Frequently item result：");
                printAttachRuls(fItem.getIdArray());
            }
        }

        /** 
         * 输出频繁项集 
         */
        private void printFItems()
        {
            if (isTransaction)
            {
                logger.Info("Object Result:");
            }
            else {
                logger.Info("Role Result:");
            }

            // 输出频繁项集  
            for (int k = 1; k <= initFItemNum; k++)
            {
                logger.Info("Frequently" + k + "Item：");
                var outer = "";
                var inter = "";
                foreach (FrequentItem i in resultItem)
                {
                    if (i.getLength() == k)
                    {
                        //logger.Info("{");
                        inter = "{";
                        for (var j = 0; j < i.getIdArray().Length; j++)
                        {
                            var t = i.getIdArray()[j];
                            if (!isTransaction)
                            {
                                // 如果原本是非事务型数据，需要重新做替换  
                                t = num2Attr[int.Parse(t)];
                            }

                            inter += t + ",";
                            //logger.Info(t + ",");
                        }
                        inter += "},";
                        outer += inter;
                    }
                }

                logger.Info(outer + " ");
            }
        }

        /** 
         * 项集进行连接运算 
         */
        private void computeLink()
        {
            // 连接计算的终止数，k项集必须算到k-1子项集为止  
            int endNum = 0;
            // 当前已经进行连接运算到几项集,开始时就是1项集  
            int currentNum = 1;
            // 商品，1频繁项集映射图  
            //Hashtable<String, FrequentItem> itemMap = new Hashtable<String, FrequentItem>();
            Hashtable itemMap = new Hashtable();
            FrequentItem tempItem;
            // 初始列表  
            List<FrequentItem> list = new List<FrequentItem>();
            // 经过连接运算后产生的结果项集  
            resultItem = new List<FrequentItem>();
            resultItemID = new List<String[]>();
            // 商品ID的种类  
            List<String> idType = new List<String>();
            foreach (String[] a in totalGoodsIDs)
            {
                foreach (String s in a)
                {
                    if (!idType.Contains(s))
                    {
                        tempItem = new FrequentItem(new String[] { s }, 1);
                        idType.Add(s);
                        resultItemID.Add(new String[] { s });
                    }
                    else {
                        // 支持度计数加1  
                        tempItem = (FrequentItem)itemMap[s];
                        tempItem.setCount(tempItem.getCount() + 1);
                    }
                    if (itemMap.ContainsKey(s))
                    {
                        //itemMap.Remove(s);
                        //itemMap.Add(s, tempItem);
                    }
                    else {
                        itemMap.Add(s, tempItem);
                    }
                }
            }
            // 将初始频繁项集转入到列表中，以便继续做连接运算  
            foreach (DictionaryEntry entry in itemMap)
            {
                tempItem = (FrequentItem)entry.Value;

                // 判断1频繁项集是否满足支持度阈值的条件  
                if (judgeFItem(tempItem.getIdArray()))
                {
                    list.Add(tempItem);
                }
            }

            // 按照商品ID进行排序，否则连接计算结果将会不一致，将会减少  
            list.Sort();
            resultItem.AddRange(list);

            String[] array1;
            String[] array2;
            String[] resultArray;
            List<String> tempIds;
            List<String[]> resultContainer;
            // 总共要算到endNum项集  
            endNum = list.Count() - 1;
            initFItemNum = list.Count() - 1;

            while (currentNum < endNum)
            {
                resultContainer = new List<string[]>();
                for (int i = 0; i < list.Count() - 1; i++)
                {
                    tempItem = list[i];
                    array1 = tempItem.getIdArray();

                    for (int j = i + 1; j < list.Count(); j++)
                    {
                        tempIds = new List<string>();
                        array2 = list[j].getIdArray();

                        for (int k = 0; k < array1.Length; k++)
                        {
                            // 如果对应位置上的值相等的时候，只取其中一个值，做了一个连接删除操作  
                            if (array1[k].Equals(array2[k]))
                            {
                                tempIds.Add(array1[k]);
                            }
                            else {
                                tempIds.Add(array1[k]);
                                tempIds.Add(array2[k]);
                            }
                        }

                        //resultArray = new String[tempIds.Count()];
                        resultArray = tempIds.ToArray();
                        //tempIds.AddRange(resultArray);

                        bool isContain = false;
                        // 过滤不符合条件的的ID数组，包括重复的和长度不符合要求的  
                        if (resultArray.Length == (array1.Length + 1))
                        {
                            isContain = isIDArrayContains(resultContainer,
                                    resultArray);
                            if (!isContain)
                            {
                                resultContainer.Add(resultArray);
                            }
                        }
                    }
                }

                // 做频繁项集的剪枝处理，必须保证新的频繁项集的子项集也必须是频繁项集  
                list = cutItem(resultContainer);
                currentNum++;
            }
        }

        /** 
         * 对频繁项集做剪枝步骤，必须保证新的频繁项集的子项集也必须是频繁项集 
         */
        private List<FrequentItem> cutItem(List<String[]> resultIds)
        {
            String[] temp;
            // 忽略的索引位置，以此构建子集  
            int igNoreIndex = 0;
            FrequentItem tempItem;
            // 剪枝生成新的频繁项集  
            List<FrequentItem> newItem = new List<FrequentItem>();
            // 不符合要求的id  
            List<String[]> deleteIdArray = new List<String[]>();
            // 子项集是否也为频繁子项集  
            bool isContain = true;

            foreach (String[] array in resultIds)
            {
                // 列举出其中的一个个的子项集，判断存在于频繁项集列表中  
                temp = new String[array.Length - 1];
                for (igNoreIndex = 0; igNoreIndex < array.Length; igNoreIndex++)
                {
                    isContain = true;
                    for (int j = 0, k = 0; j < array.Length; j++)
                    {
                        if (j != igNoreIndex)
                        {
                            temp[k] = array[j];
                            k++;
                        }
                    }

                    if (!isIDArrayContains(resultItemID, temp))
                    {
                        isContain = false;
                        break;
                    }
                }

                if (!isContain)
                {
                    deleteIdArray.Add(array);
                }
            }

            // 移除不符合条件的ID组合  
            resultIds.RemoveAll(s => deleteIdArray.Contains(s));

            // 移除支持度计数不够的id集合  
            int tempCount = 0;
            bool isSatisfied = false;
            foreach (String[] array in resultIds)
            {
                isSatisfied = judgeFItem(array);

                // 如果此频繁项集满足多支持度阈值限制条件和支持度差别限制条件，则添加入结果集中  
                if (isSatisfied)
                {
                    tempItem = new FrequentItem(array, tempCount);
                    newItem.Add(tempItem);
                    resultItemID.Add(array);
                    resultItem.Add(tempItem);
                }
            }

            return newItem;
        }

        /** 
         * 判断列表结果中是否已经包含此数组 
         *  
         * @param container 
         *            ID数组容器 
         * @param array 
         *            待比较数组 
         * @return 
         */
        private bool isIDArrayContains(List<String[]> container,
                String[] array)
        {
            bool isContain = true;
            if (container.Count() == 0)
            {
                isContain = false;
                return isContain;
            }

            foreach (String[] s in container)
            {
                // 比较的视乎必须保证长度一样  
                if (s.Length != array.Length)
                {
                    continue;
                }

                isContain = true;
                for (int i = 0; i < s.Length; i++)
                {
                    // 只要有一个id不等，就算不相等  
                    if (s[i] != array[i])
                    {
                        isContain = false;
                        break;
                    }
                }

                // 如果已经判断是包含在容器中时，直接退出  
                if (isContain)
                {
                    break;
                }
            }

            return isContain;
        }

        /** 
         * 判断一个频繁项集是否满足条件 
         *  
         * @param frequentItem 
         *            待判断频繁项集 
         * @return 
         */
        private bool judgeFItem(String[] frequentItem)
        {
            bool isSatisfied = true;
            int id;
            int count;
            double tempMinSup;
            // 最小的支持度阈值  
            double minMis = int.MaxValue;
            // 最大的支持度阈值  
            double maxMis = -int.MaxValue;

            // 如果是事务型数据，用mis数组判断，如果不是统一用同样的最小支持度阈值判断  
            if (isTransaction)
            {
                // 寻找频繁项集中的最小支持度阈值  
                for (int i = 0; i < frequentItem.Length; i++)
                {
                    id = i + 1;

                    if (mis[id] < minMis)
                    {
                        minMis = mis[id];
                    }

                    if (mis[id] > maxMis)
                    {
                        maxMis = mis[id];
                    }
                }
            }
            else {
                minMis = minSup;
                maxMis = minSup;
            }

            count = calSupportCount(frequentItem);
            tempMinSup = 1.0 * count / totalGoodsIDs.Count();
            // 判断频繁项集的支持度阈值是否超过最小的支持度阈值  
            if (tempMinSup < minMis)
            {
                isSatisfied = false;
            }

            // 如果误差超过了最大支持度差别，也算不满足条件  
            if (Math.Abs(maxMis - minMis) > delta)
            {
                isSatisfied = false;
            }

            return isSatisfied;
        }

        /** 
         * 统计候选频繁项集的支持度数，利用他的子集进行技术，无须扫描整个数据集 
         *  
         * @param frequentItem 
         *            待计算频繁项集 
         * @return 
         */
        private int calSupportCount(String[] frequentItem)
        {
            int count = 0;
            int[] ids;
            String key;
            String[] array;
            List<int> newIds;

            key = "";
            for (int i = 1; i < frequentItem.Length; i++)
            {
                key += frequentItem[i];
            }

            newIds = new List<int>();
            // 找出所属的事务ID  
            ids = fItem2Id.ContainsKey(key) ? fItem2Id[key] : null;

            // 如果没有找到子项集的事务id，则全盘扫描数据集  
            if (ids == null || ids.Length == 0)
            {
                for (int j = 0; j < totalGoodsIDs.Count(); j++)
                {
                    array = totalGoodsIDs[j];
                    if (isStrArrayContain(array, frequentItem))
                    {
                        count++;
                        newIds.Add(j);
                    }
                }
            }
            else {
                foreach (int index in ids)
                {
                    array = totalGoodsIDs[index];
                    if (isStrArrayContain(array, frequentItem))
                    {
                        count++;
                        newIds.Add(index);
                    }
                }
            }

            ids = new int[count];
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = newIds[i];
            }

            key = frequentItem[0] + key;
            // 将所求值存入图中，便于下次的计数  
            if (!fItem2Id.ContainsKey(key))
            {
                fItem2Id.Add(key, ids);
            }


            return count;
        }

        /** 
         * 根据给定的频繁项集输出关联规则 
         *  
         * @param frequentItems 
         *            频繁项集 
         */
        public void printAttachRuls(String[] frequentItem)
        {
            // 关联规则前件,后件对  
            Dictionary<List<String>, List<String>> rules;
            // 前件搜索历史  
            Dictionary<List<String>, List<String>> searchHistory;
            List<String> prefix;
            List<String> suffix;

            rules = new Dictionary<List<String>, List<String>>();
            searchHistory = new Dictionary<List<String>, List<String>>();

            for (int i = 0; i < frequentItem.Length; i++)
            {
                suffix = new List<string>();
                for (int j = 0; j < frequentItem.Length; j++)
                {
                    suffix.Add(frequentItem[j]);
                }
                prefix = new List<string>();

                recusiveFindRules(rules, searchHistory, prefix, suffix);
            }

            // 依次输出找到的关联规则  
            foreach (var entry in rules)
            {
                prefix = entry.Key;
                suffix = entry.Value;

                printRuleDetail(prefix, suffix);
            }
        }

        /** 
         * 根据前件后件，输出关联规则 
         *  
         * @param prefix 
         * @param suffix 
         */
        private void printRuleDetail(List<String> prefix,
                List<String> suffix)
        {
            // {A}-->{B}的意思为在A的情况下发生B的概率
            var result = "{";
            //logger.Info("{");
            foreach (String s in prefix)
            {
                result += s + ", ";
                //logger.Info(s + ", ");
            }
            result += "}-->{";
            //logger.Info("}-->");
            //logger.Info("{");
            foreach (String s in suffix)
            {
                result += s + ", ";
                //logger.Info(s + ", ");
            }
            result += "}";
            logger.Info(result);
        }

        /** 
         * 递归扩展关联规则解 
         *  
         * @param rules 
         *            关联规则结果集 
         * @param history 
         *            前件搜索历史 
         * @param prefix 
         *            关联规则前件 
         * @param suffix 
         *            关联规则后件 
         */
        private void recusiveFindRules(
                Dictionary<List<String>, List<String>> rules,
                Dictionary<List<String>, List<String>> history,
                List<String> prefix, List<String> suffix)
        {
            int count1;
            int count2;
            int compareResult;
            // 置信度大小  
            double conf;
            String[] temp1;
            String[] temp2;
            List<String> copyPrefix;
            List<String> copySuffix;

            // 如果后件只有1个，则函数返回  
            if (suffix.Count() == 1)
            {
                return;
            }

            foreach (String s in suffix)
            {
                count1 = 0;
                count2 = 0;

                copyPrefix = new List<String>(prefix.ToArray());
                copyPrefix.Add(s);

                copySuffix = new List<String>(suffix.ToArray());
                // 将拷贝的后件移除添加的一项  
                copySuffix.Remove(s);

                compareResult = isSubSetInRules(history, copyPrefix);
                if (compareResult == PREFIX_EQUAL)
                {
                    // 如果曾经已经被搜索过，则跳过  
                    continue;
                }

                // 判断是否为子集，如果是子集则无tempIds.add(array1[k])需计算  
                compareResult = isSubSetInRules(rules, copyPrefix);
                if (compareResult == PREFIX_IS_SUB)
                {
                    rules.Add(copyPrefix, copySuffix);
                    // 加入到搜索历史中  
                    history.Add(copyPrefix, copySuffix);
                    recusiveFindRules(rules, history, copyPrefix, copySuffix);
                    continue;
                }

                // 暂时合并为总的集合  
                copySuffix.AddRange(copyPrefix);
                //temp1 = new String[copyPrefix.Count()];
                //temp2 = new String[copySuffix.Count()];
                temp1 = copyPrefix.ToArray();
                //copyPrefix.AddRange(temp1);
                temp2 = copySuffix.ToArray();
                //copySuffix.AddRange(temp2);
                // 之后再次移除之前天剑的前件  
                copySuffix.RemoveAll(c => copyPrefix.Contains(c));

                foreach (String[] a in totalGoodsIDs)
                {
                    if (isStrArrayContain(a, temp1))
                    {
                        count1++;

                        // 在group1的条件下，统计group2的事件发生次数  
                        if (isStrArrayContain(a, temp2))
                        {
                            count2++;
                        }
                    }
                }

                conf = 1.0 * count2 / count1;
                if (conf > minConf)
                {
                    // 设置此前件条件下，能导出关联规则  
                    rules.Add(copyPrefix, copySuffix);
                }

                // 加入到搜索历史中  
                history.Add(copyPrefix, copySuffix);
                recusiveFindRules(rules, history, copyPrefix, copySuffix);
            }
        }

        /** 
         * 判断当前的前件是否会关联规则的子集 
         *  
         * @param rules 
         *            当前已经判断出的关联规则 
         * @param prefix 
         *            待判断的前件 
         * @return 
         */
        private int isSubSetInRules(
                Dictionary<List<String>, List<String>> rules,
                List<String> prefix)
        {
            int result = PREFIX_NOT_SUB;
            String[] temp1;
            String[] temp2;
            List<String> tempPrefix;

            foreach (var entry in rules)
            {
                tempPrefix = entry.Key;

                //temp1 = new String[tempPrefix.Count()];
                //temp2 = new String[prefix.Count()];

                temp1 = tempPrefix.ToArray();
                //tempPrefix.AddRange(temp1);
                temp2 = prefix.ToArray();
                //prefix.AddRange(temp2);

                // 判断当前构造的前件是否已经是存在前件的子集  
                if (isStrArrayContain(temp2, temp1))
                {
                    if (temp2.Length == temp1.Length)
                    {
                        result = PREFIX_EQUAL;
                    }
                    else {
                        result = PREFIX_IS_SUB;
                    }
                }

                if (result == PREFIX_EQUAL)
                {
                    break;
                }
            }

            return result;
        }

        /** 
         * 数组array2是否包含于array1中，不需要完全一样 
         *  
         * @param array1 
         * @param array2 
         * @return 
         */
        private bool isStrArrayContain(String[] array1, String[] array2)
        {
            bool isContain = true;
            foreach (String s2 in array2)
            {
                isContain = false;
                foreach (String s1 in array1)
                {
                    // 只要s2字符存在于array1中，这个字符就算包含在array1中  
                    if (s2.Equals(s1))
                    {
                        isContain = true;
                        break;
                    }
                }

                // 一旦发现不包含的字符，则array2数组不包含于array1中  
                if (!isContain)
                {
                    break;
                }
            }

            return isContain;
        }

        /** 
         * 读关系表中的数据，并转化为事务数据 
         *  
         * @param filePath 
         */
        private void readRDBMSData(String filePath)
        {
            String str;
            // 属性名称行  
            String[] attrNames = null;
            String[] temp;
            String[] newRecord;
            List<String[]> datas = null;

            datas = readLine(filePath);

            // 获取首行  
            attrNames = datas.FirstOrDefault();
            this.transactionDatas = new List<string[]>();

            // 去除首行数据  
            for (int i = 1; i < datas.Count(); i++)
            {
                temp = datas[i];

                // 过滤掉首列id列  
                for (int j = 1; j < temp.Length; j++)
                {
                    str = "";
                    // 采用属性名+属性值的形式避免数据的重复  
                    str = attrNames[j] + ":" + temp[j];
                    temp[j] = str;
                }

                newRecord = new String[attrNames.Length - 1];
                Array.Copy(temp, 1, newRecord, 0, attrNames.Length - 1);
                this.transactionDatas.Add(newRecord);
            }

            attributeReplace();
            // 将事务数转到totalGoodsID中做统一处理  
            this.totalGoodsIDs = transactionDatas;
        }

        /** 
         * 属性值的替换，替换成数字的形式，以便进行频繁项的挖掘 
         */
        private void attributeReplace()
        {
            int currentValue = 1;
            String s;
            // 属性名到数字的映射图  
            attr2Num = new Dictionary<string, int>();
            num2Attr = new Dictionary<int, string>();

            // 按照1列列的方式来，从左往右边扫描,跳过列名称行和id列  
            for (int j = 0; j < transactionDatas.FirstOrDefault().Length; j++)
            {
                for (int i = 0; i < transactionDatas.Count(); i++)
                {
                    s = transactionDatas[i][j];

                    if (!attr2Num.ContainsKey(s))
                    {
                        attr2Num.Add(s, currentValue);
                        num2Attr.Add(currentValue, s);

                        transactionDatas[i][j] = currentValue + "";
                        currentValue++;
                    }
                    else {
                        transactionDatas[i][j] = attr2Num[s] + "";
                    }
                }
            }
        }
    }
}
