using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Book.Service.Common
{
    public static class SerializerHelper
    {
        /// <summary>
        /// 利用Newtonsoft.Json.dll中JsonConvert进行序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreDataMember"></param>
        /// <returns></returns>
        //public static string SerializeByJsonConvert(Object data/*, bool ignoreDataMember = true*/)
        public static string SerializeByJsonConvert(Object data, IEnumerable<string> ignoreProps = null)
        {
            try
            {
                var settings = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
                if (ignoreProps != null) { settings.ContractResolver = new IgnorePropertiesContractResolver(ignoreProps); }
                return JsonConvert.SerializeObject(data, settings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 利用Newtonsoft.Json.dll中JsonConvert进行反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreDataMember"></param>
        /// <returns></returns>
        public static T DeserializeByJsonConvert<T>(string data/*, bool ignoreDataMember = true*/)
        {
            try
            {
                var settings = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
                //if (ignoreDataMember) { settings.ContractResolver = AveContractResolver.Instance; }
                return JsonConvert.DeserializeObject<T>(data, settings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 利用Newtonsoft.Json.dll中JsonConvert进行序列化, Ignore null value
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreDataMember"></param>
        /// <returns></returns>
        public static string SerializeByJsonConvertIgnoreNull(Object data, IEnumerable<string> ignoreProps = null)
        {
            try
            {
                var settings = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
                if (ignoreProps != null) { settings.ContractResolver = new IgnorePropertiesContractResolver(ignoreProps); }
                //if (ignoreDataMember) { settings.ContractResolver = AveContractResolver.Instance; }
                return JsonConvert.SerializeObject(data, settings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class IgnorePropertiesContractResolver : DefaultContractResolver
    {
        private List<string> _props = new List<string>();

        public IgnorePropertiesContractResolver(IEnumerable<string> props)
        {
            if (props != null)
            {
                _props.AddRange(props);
            }
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);
            if (_props.Contains(member.Name))
            {
                result.ShouldSerialize = i => false;
                //result.ShouldDeserialize = i => false;
            }
            return result;
        }
    }
}
