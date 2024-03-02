using MerryTestFramework.testitem.Forms;
using MerryTestFramework.testitem.Other;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryTestFramework.testitem.Headset
{
    /// <summary>
    /// 按键测试类
    /// </summary>
    public class ButtonTest
    {
        /// <summary>
        /// 下指令按键测试
        /// </summary>
        /// <param name="command">command对象</param>
        /// <param name="action">下指令并且获取回传值的整个动作（下指令并且获取回传值事件）例：()=>{ command.WriteSendReturn() } </param>
        /// <param name="readdata">按键操作对应指令返回值</param>
        /// <param name="name">按键操作对应窗口名</param>
        /// <returns></returns>
        public bool Buttontest(Command command, Action action, string readdata, string name)
        {
            var flag = true;
            var buttonMonitor = Task.Run(() =>
            {
                Thread.Sleep(50);
                while (flag)
                {

                    action.Invoke();
                    if (command.ReturnValue == readdata)
                    {
                        msgbox.DialogResult = DialogResult.OK;
                    }
                    Thread.Sleep(100);
                }
            });
            var result = ProgressBarsBox(name);
            flag = false;
            return result;
        }
        BaseConversion bc = new BaseConversion();
        /// <summary>
        /// 下指令按键测试
        /// </summary>
        /// <param name="func">下指令并且获取回传值的整个动作（下指令并且获取回传值事件）例：()=>{ command.WriteSendReturn(); return command.ReturnValue == readdata } </param>
        /// <param name="name">按键操作对应窗口名</param>
        /// <returns></returns>
        public bool Buttontest(Func<bool> func, string name)
        {
            var flag = true;
            var buttonMonitor = Task.Run(() =>
            {
                Thread.Sleep(50);
                while (flag)
                {
                    if (func.Invoke())
                    {
                        msgbox.DialogResult = DialogResult.OK;
                    }
                    Thread.Sleep(100);
                }
            });
            var result = ProgressBarsBox(name);
            flag = false;
            return result;
        }
        ProgressBars msgbox;
        #region 进度条
        private bool ProgressBarsBox(string name)
        {
            try
            {
                msgbox = new ProgressBars(name);
                if (msgbox.ShowDialog() == DialogResult.OK)
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion
    }
}
