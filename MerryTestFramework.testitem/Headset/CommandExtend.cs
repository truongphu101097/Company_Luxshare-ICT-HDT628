using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Headset
{
    /// <summary>
    /// 指令帮助类（扩展形式）
    /// </summary>
    public static class CommandExtend
    {
        private static Command command = new Command();
        /// <summary>
        /// 延时(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="delay">延时（毫秒）</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity Delay(this CommandEntity ce, int delay)
        {
            if (!ce.success) return ce;
            Thread.Sleep(delay);
            return ce;
        }

        /// <summary>
        /// 使用write下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity WriteSend(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.WriteSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用write下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="handle">句柄对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity WriteSend(this CommandEntity ce, string value, int length, IntPtr handle)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.success = command.WriteSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }


        /// <summary>
        /// 使用GetReport下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetReportSend(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.GetReportSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用GetReport下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="handle">句柄对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetReportSend(this CommandEntity ce, string value, int length, IntPtr handle)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.success = command.GetReportSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用SetReportSend下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity SetReportSend(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.SetReportSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }

        /// <summary>
        /// 使用SetReportSend下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="handle">句柄对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity SetReportSend(this CommandEntity ce, string value, int length, IntPtr handle)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.success = command.SetReportSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }


        /// <summary>
        /// 使用SetFeatureSend下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity SetFeatureSend(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.SetFeatureSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }


        /// <summary>
        /// 使用SetFeatureSend下下指令(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="handle">句柄对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity SetFeatureSend(this CommandEntity ce, string value, int length, IntPtr handle)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.success = command.SetFeatureSend(ce.value, ce.length, ce.handle);
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }


        /// <summary>
        /// 使用Write下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity WriteReturn(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.WriteReturn(ce.value, ce.length, ce.readdata, ce.indexes, ce.path, ce.handle);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用Write下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="readdata">指令返回值标识值</param>
        /// <param name="indexes">指令返回值索引</param>
        /// <param name="path">句柄通道地址</param>
        /// <param name="handle">句柄通道指针</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity WriteReturn(this CommandEntity ce, string value, int length, string readdata, string indexes, string path, IntPtr handle)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.readdata = readdata;
                ce.indexes = indexes;
                ce.path = path;

                ce.success = command.WriteReturn(ce.value, ce.length, ce.readdata, ce.indexes, ce.path, ce.handle);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用GetFeature下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="indexes">指令返回值索引</param>
        /// <param name="handle">句柄通道指针</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetFeatureReturn(this CommandEntity ce, string value, int length, IntPtr handle, string indexes)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.indexes = indexes;
                ce.success = command.GetFeatureReturn(ce.value, ce.length, ce.handle, ce.indexes);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }

        /// <summary>
        /// 使用GetFeature下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetFeatureReturn(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.GetFeatureReturn(ce.value, ce.length, ce.handle, ce.indexes);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }

        /// <summary>
        /// 使用GetReport下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetReportReturn(this CommandEntity ce)
        {
            if (!ce.success) return ce;
            try
            {
                ce.success = command.GetReportReturn(ce.value, ce.length, ce.handle, ce.indexes);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 使用GetReport下指令并获取回传值(扩展)
        /// </summary>
        /// <param name="ce">指令实体对象</param>    
        /// <param name="value">指令</param>
        /// <param name="length">指令长度</param>
        /// <param name="indexes">指令返回值索引</param>
        /// <param name="handle">句柄通道指针</param>
        /// <returns>指令实体对象</returns>
        public static CommandEntity GetReportReturn(this CommandEntity ce, string value, int length, IntPtr handle, string indexes)
        {
            if (!ce.success) return ce;
            try
            {
                ce.value = value;
                ce.length = length;
                ce.handle = handle;
                ce.indexes = indexes;
                ce.success = command.GetReportReturn(ce.value, ce.length, ce.handle, ce.indexes);
                ce.result = command.ReturnValue;
                ce.allresult = command.ALLReturnValue;
            }
            catch
            {
                ce.success = false;
            }
            return ce;
        }
        /// <summary>
        /// 指令实体
        /// </summary>
        public class CommandEntity
        {
            /// <summary>
            /// 指令
            /// </summary>
            public string value { get; set; }
            /// <summary>
            /// 指令长度
            /// </summary>
            public int length { get; set; }
            /// <summary>
            /// 句柄地址
            /// </summary>
            public string path { get; set; }
            /// <summary>
            /// 句柄
            /// </summary>
            public IntPtr handle { get; set; }
            /// <summary>
            /// 指令返回值标识值
            /// </summary>
            public string readdata { get; set; }
            /// <summary>
            /// 指令返回值下标
            /// </summary>
            public string indexes { get; set; }

            /// <summary>
            /// 成功与否
            /// </summary>
            public bool success { get; set; } = true;
            /// <summary>
            /// 回传值
            /// </summary>
            public string result { get; set; }
            /// <summary>
            /// 回传值
            /// </summary>
            public string allresult { get; set; }
        }

    }
}
