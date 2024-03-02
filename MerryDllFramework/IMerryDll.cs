

using System;
using System.Collections.Generic;

namespace MerryDllFramework
{
    internal interface IMerryDll
    {
        /// <summary>
        /// 验证当前Dll是否是主程序需要调用的Dll
        /// </summary>
        /// <param name="type">传入机型名</param>
        /// <returns>传入机型名与定义的当前机型名对比</returns>
        bool CheckType(string type);

        /// <summary>
        /// 获取弹窗信息
        /// </summary>
        /// <param name="key">对应Message.txt文件中key值</param>
        /// <returns>对应控制台Message.txt文件中value值</returns>
        string GetMessage(string key);
        /// <summary>
        /// 程序每次开始运行时触发
        /// </summary>
        /// <returns></returns>
        bool StartRun();

        /// <summary>
        /// 启动主程序时触发(放入某些每次启动时需要调用的方法并且传入参数)
        /// </summary>
        /// <param name="formsData">数据集合（本dll被实例化为主程序中对象，所以主程序formsData与本参数，以及本参数赋值后的参数，指向同一堆对象）</param>
        /// <param name="_handel">主程序主窗体句柄</param>
        /// <returns>启动是否成功</returns>
        bool Start(List<string> formsData, IntPtr _handel);

        /// <summary>
        /// 调用内部方法
        /// </summary>
        /// <param name="message">指令，决定调用哪个方法</param>
        /// <returns>方法调用后回传值</returns>
        string Run(string message);

    }
}
