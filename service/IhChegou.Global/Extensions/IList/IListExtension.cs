using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Global.Extensions.IList
{
    public static class IListExtension
    {
        public static void SafeInstance<T>(this IList<T> list)
        {
            list = list ?? new List<T>();
        }

        public static void AddIfNotNull<T>(this IList<T> list, T value)
        {
            if (value != null)
                list.Add(value);
        }

        public static void AddRangeIfNotNull<T>(this IList<T> list, IList<T> value)
        {
            foreach (var item in value)
            {
                if (item != null)
                    list.Add(item);
            }

        }
        private static MemoryCache memoryCache = new MemoryCache("ListRowCount");
        public static IList<T> SetRowCount<T>(this IList<T> list, int value)
        {
            memoryCache.Add(list.GetHashCode().ToString(), value, DateTime.Now.AddMinutes(3));
            return list;
        }
        public static int RowCount<T>(this IList<T> list)
        {
            var count = memoryCache[list.GetHashCode().ToString()];
            if(count == null)
            {
                return 0;
            }
            return (int)count;
        }        
        public static bool IsEmpty<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                return true;
            return false;
        }

    }
}
