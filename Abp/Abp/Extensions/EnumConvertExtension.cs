using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace Abp.Extensions
{
    /// <summary>
    /// Enum转换扩展类
    /// </summary>
    public static class EnumConvertExtension
    {
        /// <summary>
        /// 获取Enum值的Description属性值
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <param name="isInstead">没有Description属性，是否使用枚举值替代</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue, bool isInstead = true)
        {
            Type enumType = enumValue.GetType();
            string enumName = Enum.GetName(enumType, enumValue);
            return enumName.InnerGetDescription(enumType, isInstead);
        }

        /// <summary>
        /// 获取Enum值的Description属性值
        /// </summary>
        /// <param name="enumName">枚举值</param>
        /// <param name="enumType">枚举Type</param>
        /// <param name="isInstead">没有Description属性，是否使用枚举值替代</param>
        /// <returns></returns>
        private static string InnerGetDescription(this string enumName,Type enumType, bool isInstead = true)
        {
            if (enumName.IsNullOrEmpty())
            {
                return null;
            }
            FieldInfo field = enumType.GetField(enumName);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            //无属性，可用枚举值替换
            if (attribute == null && isInstead)
            {
                return enumName;
            }
            return attribute == null ? null : attribute.Description;
        }

        /// <summary>
        /// 将枚举转换为键值对集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>key:枚举name,值:枚举value</returns>
        public async static Task<Dictionary<int, string>> EnumToDictionary(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                //传入的不是枚举
                throw new AggregateException();
            }

            Array enumValueArray = Enum.GetValues(enumType);

            string cacheKey = enumType.Name + "_EnumToDictionary_cache";
            AsyncThreadSafeObjectCache<Dictionary<int, string>> cache = new AsyncThreadSafeObjectCache<Dictionary<int, string>>(MemoryCache.Default, TimeSpan.FromSeconds(120));
            Dictionary<int, string> enumDict = await cache.GetAsync(cacheKey, () =>
            {
                enumDict = new Dictionary<int, string>();
                foreach (var enumValue in enumValueArray)
                {
                    int key = Convert.ToInt32(enumValue);//value值
                    string value = Enum.GetName(enumType, enumValue).InnerGetDescription(enumType);
                    enumDict.Add(key, value);
                }
                cache.Set(cacheKey, enumDict);
                return Task.FromResult(enumDict);
            });

            return enumDict;
        }
    }
}
