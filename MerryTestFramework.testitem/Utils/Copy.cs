using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Utils
{
    /// <summary>
    /// 引用类型深拷贝辅助类
    /// </summary>
    public static class Copy
    {
        /// <summary>
        /// 深拷贝
        /// 注意：T必须标识为可序列化[Serializable]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T obj)
            where T : class
        {
            try
            {
                if (obj == null)
                {
                    return null;
                }

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    binaryFormatter.Serialize(stream, obj);
                    stream.Position = 0;
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
