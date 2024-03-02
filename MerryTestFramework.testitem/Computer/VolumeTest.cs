using MerryTestFramework.testitem.Forms;
using System.Windows.Forms;

namespace MerryTestFramework.testitem.Computer
{
    /// <summary>
    /// 音量测试帮助类
    /// </summary>
    public class VolumeTest
    {
        #region 音量测试
        /// <summary>
        /// 音量测试
        /// </summary>
        /// <param name="flag">true时音量加，false是音量减</param>
        /// <param name="name">窗口名</param>
        /// <returns></returns>
        public bool volumetest(bool flag, string name)
        {
            ProgressBars pb = new ProgressBars(name);
            if (flag)
            {
                pb.Show();
                pb.VolumeUpTestNO();

            }
            else
            {
                pb.Show();
                pb.VolumeDownTestNO();
            }

            if (pb.DialogResult == DialogResult.Yes)
            {
                pb.Dispose();
                return true;
            }
            else
            {
                pb.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 音量测试-不允许回退
        /// </summary>
        /// <param name="flag">true时音量加，false是音量减</param>
        /// <param name="name">窗口名</param>
        /// <returns></returns>
        public bool volumetestNot(bool flag, string name)
        {
            ProgressBars pb = new ProgressBars(name);
            if (flag)
            {
                pb.Show();
                pb.VolumeUpTest();
            }
            else
            {
                pb.Show();
                pb.VolumeDownTest();
            }
            if (pb.DialogResult == DialogResult.Yes)
            {
                pb.Dispose();
                return true;
            }
            else
            {
                pb.Dispose();
                return false;
            }
        }
        #endregion
    }
}
