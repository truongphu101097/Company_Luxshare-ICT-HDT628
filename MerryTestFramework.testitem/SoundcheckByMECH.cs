using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using MerryTestFramework.testitem.Computer;
using System.Drawing;

namespace MerryTestFramework.testitem
{
    public class SoundcheckByMECH
    {
        SoundCheck16 sc = new SoundCheck16();
        private WindowControl fw = new WindowControl();
        string Msg = "";
        string savepath = @".\Soundcheck\" + DateTime.Now.ToString("yyyy-MM-dd");

        public bool opensoundcheck(string soundchckpath, string sqcpath)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = soundchckpath;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                if (!connectToServer())
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
        public bool controllwindow(string windowname, string skey,Point point)
        {
            string text = "";
            try
            {
                
                return fw.SendKeyToWindow(windowname, false, skey,point, out text);                           
           
            }
            catch
            {
                return false;
            }
        }
        public bool startsoundcheck(string SN)
        {
            sc.SendSN(SN);
            Msg = sc.GetMsg();
            if (!this.Msg.Contains("Serial number set ok.")) return false;
            sc.RunSequence();
            Msg = sc.GetMsg();
            return Msg.Contains("ok");
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
        private bool connectToServer()
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
        public bool getsoundcheckresult()
        {
            return sc.GetResult();

        }
        public void closesc()
        {
            sc.closeServer();
            sc.ExitSoundCheck();
        }
    }
}
