using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Utils
{
    /// <summary>
    /// 字符串类型转换辅助类
    /// </summary>
    public static class StringUpdateType
    {
        /// <summary>
        /// 将字符串转换为int类型
        /// </summary>
        public static int ToInt(this string str) => Convert.ToInt32(str);
        /// <summary>
        /// 将16进制字符串转换为16进制int类型
        /// </summary>
        public static int ToInt16(this string str) => Convert.ToInt32(str, 16);
        /// <summary>
        /// 将字符串转换为short类型
        /// </summary>
        public static short ToShort(this string str) => Convert.ToInt16(str);
        /// <summary>
        /// 将字符串转换为bool类型
        /// </summary>
        public static bool ToBool(this string str) => Convert.ToBoolean(str);
        /// <summary>
        /// 将字符串转换为double类型
        /// </summary>
        public static double ToDouble(this string str) => Convert.ToDouble(str);
        /// <summary>
        /// 将字符串转换为byte类型
        /// </summary>
        public static byte ToByte(this string str) => Convert.ToByte(str);

    }
}
