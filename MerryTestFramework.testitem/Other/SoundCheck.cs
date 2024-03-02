using MerryTestFramework.testitem.Computer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MerryTestFramework.testitem.Other
{
    /// <summary>
    /// SoundCheck操作类
    /// </summary>
    public class SoundCheck
    {
        private SoundCheck16 sc = new SoundCheck16();
        private WindowControl fw = new WindowControl();
        private string Msg = "";
        private string savepath = @".\Soundcheck\" + DateTime.Now.ToString("yyyy-MM-dd");
        private String sqcpath;
        /// <summary>
        /// 打开SoundCheck
        /// </summary>
        /// <param name="soundchckpath">SoundCheck exe路径</param>
        /// <param name="sqcpath">SoundCheck sqc文件路径</param>
        /// 
        public bool OpenSoundCheck(string soundchckpath, string sqcpath)
        {
            this.sqcpath = sqcpath;
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisProc.ProcessName.Contains("SoundCheck"))
                {
                    System.Windows.Forms.MessageBox.Show($"检测到线程:{thisProc.ProcessName}");
                    return false;
                }
            }
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = soundchckpath;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                if (!ConnectToServer())
                {
                    Msg = "与soundcheck建立连接失败，请检查端口是否一致";
                    return false;
                }
                if (!OpenSequence(sqcpath))
                {
                    Msg = "开启sqc文件失败,请检查文件路径是否正确";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 启动SoundCheck
        /// </summary>
        /// <param name="SN"></param>
        /// <returns></returns>
        public bool StartSoundCheck(string SN)
        {

            if (!sc.client.Connected)
            {
                for (int i = 0; i < 2; i++)
                {
                    sc.ConnectToServer();
                    this.Msg = this.sc.GetMsg();
                    if (!this.Msg.Contains("Connected to SoundCheck ok.")) continue;
                    Thread.Sleep(500);
                    if (!OpenSequence(sqcpath)) continue;
                    Thread.Sleep(3000);
                    break;
                }
            }
            sc.SendSN(SN);
            Msg = sc.GetMsg();
            if (!this.Msg.Contains("Serial number set ok.")) return false;
            sc.RunSequence();
            Msg = sc.GetMsg();
            return Msg.Contains("ok");
        }
        /// <summary>
        /// 获取SoundCheck结果
        /// </summary>
        public bool GetSoundCheckResult() => sc.GetResult();

        /// <summary>
        /// 关闭SoundCheck
        /// </summary>
        public void CloseSoundCheck()
        {
            //断开连接
            try
            {
                sc.closeServer();
                sc.ExitSoundCheck();
            }
            catch { }
            //关闭程序
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisProc.ProcessName.Contains("SoundCheck"))
                {
                    if (System.Windows.Forms.MessageBox.Show("是否关闭sound check", "提示", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            Thread.Sleep(3000);
                            thisProc.Kill();
                        }
                        catch
                        {


                        }

                    }
                }
            }
        }


        private bool OpenSequence(string Path)
        {
            sc.OpenSequence(Path);
            this.Msg = sc.GetMsg();
            if (this.Msg.Contains("Sequence Opened ok."))
            {
                return true;
            }
            return false;
        }
        private bool ConnectToServer()
        {
            Thread.Sleep(25000);
            sc.ConnectToServer();
            this.Msg = this.sc.GetMsg();
            if (this.Msg.Contains("Connected to SoundCheck ok."))
            {
                return true;
            }
            return false;
        }
    }
}
