using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Headset
{
    /// <summary>
    /// 修改Command中回传值为某些固定格式
    /// </summary>
    public static class UpdateValue
    {
        /// <summary>
        /// 将Command方法中回传值修改为FW版本格式
        /// </summary>
        /// <param name="returnValues">ommand方法中回传值</param>
        /// <returns>FW版本格式</returns>
        public static string UpdateFW(this string returnValues)
        {
            return $"V{returnValues.Trim().Replace(" ", ".")}";
        }
        /// <summary>
        /// 将Command方法中回传值修改为电压格式
        /// </summary>
        /// <param name="returnValues">ommand方法中回传值</param>
        /// <returns>电压格式</returns>
        public static string UpdateVol(this string returnValues)
        {
            return (Convert.ToDouble((Convert.ToInt32(returnValues.Trim().Replace(" ", ""), 16))) / 1000).ToString();
        }

        /// <summary>
        /// 将Command方法中回传值修改为电量格式
        /// </summary>
        /// <param name="returnValues">ommand方法中回传值</param>
        /// <returns>电量格式</returns>
        public static string UpdateVol1(this string returnValues)
        {
            return $"{Convert.ToInt32(returnValues.Trim().Replace(" ", ""), 16)}%";
        }
    }
}
