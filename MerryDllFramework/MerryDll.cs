using HDT668_HID_CMD;
using MerryKing;
using MerryTest.testitem;
using MESDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApplication1;
using MerryTestFramework.testitem.Headset;
using NAudio.CoreAudioApi;
using BlueNinjaSoftware.HIDLib;
using Microsoft.Win32.SafeHandles;
using System.Management;
using System.Threading;
using NAudio.CoreAudioApi;
using MerryTestFramework.testitem.Computer;
using MerryTestFramework.testitem.Headset;
using MerryTestFramework.testitem.Other;
using MerryTestFramework.testitem.Utils;
using System.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MerryTestFramework.testitem;
using System.IO.Ports;
using PC_VolumeControl;
using WindowsFormsApplication1;
using System.Windows.Forms;
using MerryTest.testitem;
using NAudio.CoreAudioApi;
using SwATE_Net;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Xml.Linq;


namespace MerryDllFramework
{
    public class MerryDll : IMerryDll
    {
        /// <summary>
        /// 必要，因为反射不会自动创建构造函数
        /// </summary>
        public MerryDll()
        {

        }
        public string[] GetDllInfo()
        {
            string dllname = "DLL 名称        ：HDT628";
            string dllfunction = "Dll功能说明 ：HDT628机型测试模块";
            string dllHistoryVersion = "历史Dll版本：V23.06.27.0";
            string dllVersion = "当前Dll版本：V23.08.21.0";
            string[] info = { dllname,
                dllfunction,
                dllHistoryVersion,
                dllVersion};
            return info;
        }
        private Data data = new Data();
        MatchTarget MatchTarget = new MatchTarget();
        Dictionary<string, object> Config;
        public object Interface(Dictionary<string, object> keyValues) => Config = keyValues;

        #region 通用DLL类实例化区 ：实例化MerryTestFramework.testitem.dll中帮助类
        //private MerryTestFramework.testitem.Headset.Command Command = new MerryTestFramework.testitem.Headset.Command();
        public Command Command = new Command();
        public GetHandle Gethandle = new GetHandle();
        private MerryTestFramework.testitem.Headset.ButtonTest Button = new MerryTestFramework.testitem.Headset.ButtonTest();
        private MerryTestFramework.testitem.Headset.OldButtonTest OldButton = new MerryTestFramework.testitem.Headset.OldButtonTest();
        Selected Selected;
        public Constant con = new Constant();
        readonly VolumeTest VolumeTestPlan = new VolumeTest();
        // 天线通道（经示波器调测，通道要加1才能正确显示）
        public static string CH1 = "0";  // 原值为 "0"
        public static string CH20 = "20"; // 原值为 "20"
        public static string CH38 = "38"; // 原值为 "38"
        // 天线
        public static string ANT1 = "0"; // 天线1
        public static string ANT2 = "1"; // 天线2
        #endregion

        #region 主程序调用方法区
        public bool CheckType(string type)
        {
            return type == data._type;
        }

        public string GetMessage(string key)
        {
            return data.messagedic[key];
        }
        public bool StartRun()
        {
            data.formsData.Add("");
            return true;
        }
        bool flag = false;
        int i;
        public bool Start(List<string> formsData, IntPtr _handle)
        {
            data.handle = _handle;
            data.formsData = formsData;
            data.formsData.Add("");
            Selected = new Selected();
            Selected.ShowDialog();
            Thread t = new Thread(ShowInfo);
            t.Start();
         
            flag = true;
            return true;
        }
        private void ShowInfo()
        {
            while (true)
            {
                if (flag)
                {
                    flag = false;
                    View View = new View();
                    View.i = Selected.i;
                    View.ShowDialog();
                }
                Thread.Sleep(1000);
            }
        }
        public string Run(string message)
        {
            try
            {
                data.CloseHandel();
                data.OpenHandel(data.RXPID, data.RXVID, data.TXPID, data.TXVID);          
                Info.HeadsetHandle = data.headsethandle1;
                Info.DongleHandle = data.donglehandel3;
                Info.HeadsetPath = data.headsetpath1;
                Info.DonglePath = data.donglepath3;

                Thread.Sleep(200);
                string[] str = message.Split('-');
                Info.TpTestname = str[0];
                switch (str[0])
                {
                    case "Pair": return Pair().ToString();
                    case "Get_Handle": return Get_Handle().ToString();
                    case "Reset_Headset": return Reset_Headset().ToString();
                    case "MCU_Version": return MCU_Version().ToString();
                    case "DongletVendorName": return DongletVendorName().ToString();
                    case "HeadsetVendorName": return HeadsetVendorName().ToString();
                    case "RXGetHeadsetName": return RXGetHeadsetName().ToString();
                    case "TXGetHeadsetName": return TXGetHeadsetName().ToString();
                    case "CheckG": return CheckG().ToString();
                    case "GetHeadsetPIDVID": return GetHeadsetPIDVID().ToString();
                    case "GetDonglePIDVID": return GetDonglePIDVID().ToString();
                    case "RXAvena": return RXAvena().ToString();
                    case "TXAvena": return TXAvena().ToString();
                    case "VloDown": return VloDown().ToString();//VolUp
                    case "VolUp": return VolUp().ToString();//VolUp Checkmute DongleArbiterID
                    case "Checkmute": return Checkmute().ToString();
                    case "CheckG_Off": return CheckG_Off().ToString();
                    case "Delay5000": return Delay5000().ToString();
                    case "GetBatteryVoltage": return GetBatteryVoltage().ToString();                 
                    case "HeadsetArbiterIDDongleArbiterID": return HeadsetArbiterIDDongleArbiterID().ToString();
                    case "Checkconnect_Dongle": return Checkconnect_Dongle().ToString();
                    case "Check_mic": return Check_mic().ToString();
                    case "Check_LED_mic": return Check_LED_mic().ToString();
                    /* 耳机天线选择 ant1 ant2 */
                    /* 耳机通道选择 ch1 ch20 ch38 */
                    case "GetTXVidPid": return GetVidPid("TX");  // 获取TX VID和PID
                    case "GetRXVidPid": return GetVidPid("RX");  // 获取RX VID和PID    
                    case "headsetAnt1Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH1).ToString();
                    case "headsetAnt1Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH20).ToString();
                    case "headsetAnt1Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT1, CH38).ToString();
                    case "headsetAnt2Ch1": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH1).ToString();
                    case "headsetAnt2Ch20": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH20).ToString();
                    case "headsetAnt2Ch38": return SetRFChannel(data.RXVID, data.RXPID, ANT2, CH38).ToString();
                    /* dongle天线选择 ant1 ant2 */
                    /* dongle通道选择 ch1 ch20 ch38 */
                    case "dongleAnt1Ch1": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH1).ToString();
                    case "dongleAnt1Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH20).ToString();
                    case "dongleAnt1Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT1, CH38).ToString();
                    case "dongleAnt2Ch1": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH1).ToString();
                    case "dongleAnt2Ch20": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH20).ToString();
                    case "dongleAnt2Ch38": return SetRFChannel(data.TXVID, data.TXPID, ANT2, CH38).ToString();
                    default: return "False";
                }
            }
            catch
            {
                return "False";
            }
        }
        #endregion

        #region 被指令调用方法区

        #region 定义结构体存储数据
        // 定义结构体存储数据
        struct Info
        {
            /// <summary>
            /// Set Report ID指令
            /// </summary>
            public static string SetCommand;

            /// <summary>
            /// Get Report ID指令
            /// </summary>
            public static string GetCommand;

            /// <summary>
            /// 存储颜色
            /// </summary>
            public static string color;

            /// <summary>
            /// 存储AB1568 FW版本
            /// </summary>
            public static string AB1568;

            /// <summary>
            /// 存储Headset Handle
            /// </summary>
            public static IntPtr HeadsetHandle;

            /// <summary>
            /// 存储Dongle Handle
            /// </summary>
            public static IntPtr DongleHandle;

            /// <summary>
            /// 存储写入的BD
            /// </summary>
            public static string BD;

            /// <summary>
            /// 存储Headset BD
            /// </summary>
            public static string HeadsetBD;

            /// <summary>
            /// 存储Dongle BD
            /// </summary>
            public static string DongleBD;


            public static string HeadsetPath;

            public static string DonglePath;

            /// <summary>
            /// 存储触摸测试下标值
            /// </summary>
            public static string index;

            /// <summary>
            /// 存储TP基础值上限
            /// </summary>
            public static string[] BasicUpper;

            /// <summary>
            /// 存储TP基础值下限
            /// </summary>
            public static string[] BasicLower;

            /// <summary>
            /// 存储TP触摸结果名称
            /// </summary>
            public static string TpTestname;

            /// <summary>
            /// 存储TP触摸结果
            /// </summary>
            public static string TpTouchValues;

            public static string[] RGB;

            public static string Lux_R;

            public static string Lux_L;

        }
        #endregion


        #region LED测试
        string GetVidPid(string item = "TX")
        {
            try
            {
                string vid;
                string pid;
                HIDDevice device;
                if (item == "TX")
                {
                    device = HIDManagement.GetDevices(Convert.ToUInt16(data.TXVID, 16), Convert.ToUInt16(data.TXPID, 16), true)[0];
                }
                else
                {
                    device = HIDManagement.GetDevices(Convert.ToUInt16(data.RXVID, 16), Convert.ToUInt16(data.RXPID, 16), true)[0];
                }
                vid = Convert.ToString(device.VendorID, 16).PadLeft(4, '0');
                pid = Convert.ToString(device.ProductID, 16).PadLeft(4, '0');
                return $"VID:{vid.ToUpper()}|PID:{pid.ToUpper()}";
            }
            catch
            {
                return "False";
            }
        }
     
        public string GetBatteryVoltage()
        {
            bool flag = false;
            var value = "11 FF 08 0F";
            var indexs = "4 5";
            var returnvalue = "11 FF 08 0F";

            var batteryVol = "";

            if (Command.WriteReturn(value, 20, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                foreach (var item in Command.ReturnValue.Split(' '))
                {
                    flag = true;
                    batteryVol = String.Concat(batteryVol, item);

                }
            }
            else
            {
                flag = false;
            }


            if (!flag)
            {
                return false.ToString();
            }
            return batteryVol = Convert.ToString(Convert.ToDouble(Convert.ToInt32(batteryVol, 16)) / 1000.000);

        }
        // 天线选择
        public static bool SetRFChannel(string vid, string pid, string ch, string channel)
        {
            return SCPI.avnera.Main(new string[] { vid, pid, ch, channel }) == 1;
        }

        public string GetHeadsetPIDVID()
        {
            string pathHeadset = data.headsetpath1;
     
            //string fw = "";
            string[] item = pathHeadset.Split('&');

            string[] vidItem = item[0].Split('_');
            string[] pidItem = item[1].Split('_');

            string PIDVID = "P" + vidItem[1].ToUpper() + "V" + pidItem[1].ToUpper();

            return PIDVID;

        }
        
        public string GetDonglePIDVID()
        {
            string pathDongle = data.donglepath1;

            //string fw = "";
            string[] item = pathDongle.Split('&');

            string[] vidItem = item[0].Split('_');
            string[] pidItem = item[1].Split('_');

            string PIDVID = "P" + vidItem[1].ToUpper() + "V" + pidItem[1].ToUpper();

            return PIDVID;

        }
        public string MCU_Version()
        {
            var value = "06 BB 02 01";
            var returnvalue = "06 BB 02 01";
            var indexs = "4 5 6";
            if (Command.SetReportReturn(value, 20, returnvalue, indexs, data.headsetpath1, data.headsethandle1))
            {
                var fw = "";
                string[] item = Command.ReturnValue.Split(' ');

                fw += item[0] + item[1] + item[2];

                //Console.WriteLine($"{fw}");
                return fw;
            }
            return false.ToString();

        }
        public string RXAvena()
        {
            var value = "06 88 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            var returnvalue = "06 88 07";
            var indexs = "3 4 5";
            if (Command.SetReportReturn(value, 20, returnvalue, indexs, data.headsetpath1, data.headsethandle1))
            {
                var fw = "";
                string[] item = Command.ReturnValue.Split(' ');

                fw += item[0] + item[1] + item[2];

                //Console.WriteLine($"{fw}");
                return fw;
            }
            return false.ToString();

        }
        private bool VloDown()
        {
            return VolumeTestPlan.volumetest(false, "Vui lòng vặn giảm âm lượng\n下调音量");
        }
        private bool VolUp()
        {
            return VolumeTestPlan.volumetest(true, "Vui lòng vặn tăng âm lượng\n上调音量");
        }
        public string TXAvena()//VloDown
        {
            var value = "11 FF 02 1F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            var returnvalue = "11 FF 02 1F 00 55 31 20 29";
            var indexs = "10 11";
            if (Command.SetReportReturn(value, 20, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                var fw = "";
                string[] item = Command.ReturnValue.Split(' ');

                fw += item[0] + item[1];

                //Console.WriteLine($"{fw}");
                return fw;
            }
            return false.ToString();
        }
        public static bool IsUsbDeviceConnected(string pid, string vid)
        {
            using (var searcher =
              new ManagementObjectSearcher(@"Select * From Win32_USBControllerDevice"))
            {
                using (var collection = searcher.Get())
                {
                    foreach (var device in collection)
                    {
                        var usbDevice = Convert.ToString(device);

                        if (usbDevice.Contains(pid) && usbDevice.Contains(vid))
                            return true;
                    }
                }
            }
            return false;
        }
        public string RXGetHeadsetName()
        {
            if (Selected.i== "1") 
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID), (ushort)con.Hex2Ten(data.RXPID), true);
                HIDDevice hidDevice = devList[0];
                string nameItem = hidDevice.ProductName;
                if(nameItem == "G933s Gaming Headset Battery Charger")
                {   
                    return nameItem.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng xem lại phiên bản tai nghe\n请查看耳机版本");
                }
            }
            if (Selected.i == "2")
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID), (ushort)con.Hex2Ten(data.RXPID), true);
                HIDDevice hidDevice = devList[0];

                string nameItem = hidDevice.ProductName;
            
                if (nameItem == "G935 Gaming Headset Battery Charger")
                {
                    return nameItem.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng xem lại phiên bản tai nghe\n请查看耳机版本");
                }
            }
            return false.ToString();
        }
        public string HeadsetVendorName()
        {

            IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.RXVID), (ushort)con.Hex2Ten(data.RXPID), true);
            HIDDevice hidDevice = devList[0];
            string rever = hidDevice.Manufacturer+"";
            if (rever == null)
            {
                return false.ToString();
            }
            return rever;
        }
        public string DongletVendorName()
        {
            IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.TXVID), (ushort)con.Hex2Ten(data.TXPID), true);
            HIDDevice hidDevice = devList[0];
            string rever = hidDevice.Manufacturer + "";

            if (rever == null)
            {
                return false.ToString();
            }
            return rever;

        }
        string ID_HEADSET;
        string ID_DONGE;
        string deviceName;
        public string HeadsetArbiterIDDongleArbiterID()
        {

            var cmd2 = "ff 0a 00 fd 04 00 00 05 81 0d b1 04";
            var indexheadsetID = "11 12 13 14";
            var cmd3 = "ff 0a 00 fd 04 00 00 05 81 d4 c0 04";
           
           
            if (Command.SetFeatureSend(cmd2, 64, data.headsethandle2))
            {

                if (Command.GetFeatureReturn(cmd2, 64, data.headsethandle2, indexheadsetID))
                {
                    string bkb = Command.ReturnValue;
                    string[] kbk1 = bkb.Split(' ');
                    ID_HEADSET = kbk1[0] + kbk1[1] + kbk1[2] + kbk1[3];
                }

            }
            if (Command.SetFeatureSend(cmd3, 64, data.donglehandel4))
            {
                if (Command.GetFeatureReturn(cmd3, 64, data.donglehandel4, indexheadsetID))
                {
                    string bkb = Command.ReturnValue;
                    string[] kbk1 = bkb.Split(' ');
                    ID_DONGE = kbk1[0] + kbk1[1] + kbk1[2] + kbk1[3];// 10 66 73 11
                    if (ID_HEADSET == ID_DONGE)
                    {
                        return ID_DONGE;
                    }
                }
            }
            return false.ToString();
           
        }
        public string Checkconnect_Dongle()
        {

            DateTime startTime = DateTime.Now;
            TimeSpan duration = TimeSpan.FromSeconds(10);
            while (true)
            {
               
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

                // Lấy danh sách tất cả các thiết bị âm thanh đang được kết nối
                MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

                // Lặp qua danh sách các thiết bị và kiểm tra tên hiển thị của từng thiết bị
                foreach (MMDevice device in devices)
                {
                    if (device.DataFlow == DataFlow.Render && device.State == DeviceState.Active)
                    {
                        deviceName = device.FriendlyName;

                    }
                }
                if (deviceName.Contains("G933s Gaming Headset") || deviceName.Contains("G935 Gaming Headset"))
                {
                    break;                              
                }
                if (DateTime.Now - startTime > duration)
                {
                    break;
                }
            }
            if (deviceName.Contains("G933s Gaming Headset") || deviceName.Contains("G935 Gaming Headset"))
            {
                return true.ToString();
            }
            return false.ToString();
        }
        public string TXGetHeadsetName()
        {
            if (Selected.i == "1")
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.TXVID), (ushort)con.Hex2Ten(data.TXPID), true);
                HIDDevice hidDevice = devList[0];

                string nameItem = hidDevice.ProductName;
                if (nameItem == "G933s Gaming Headset")
                {
                    return nameItem.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng xem lại phiên bản tai nghe\n请查看耳机版本");
                }
            }
            if (Selected.i == "2")
            {
                IList<HIDDevice> devList = HIDManagement.GetDevices((ushort)con.Hex2Ten(data.TXVID), (ushort)con.Hex2Ten(data.TXPID), true);
                HIDDevice hidDevice = devList[0];

                string nameItem = hidDevice.ProductName;

                if (nameItem == "G935 Gaming Headset")
                {
                    return nameItem.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng xem lại phiên bản tai nghe\n请查看耳机版本");
                }
            }
            return false.ToString();

        }
        public string Pair()
        {
       
            if (!MerryDllFramework.MatchTarget.Set_ChannelPair(data.TXPID, data.TXVID, data.RXPID, data.RXVID, data.messagedic["channel"])) return false.ToString(); 
            if (!Command.SetFeatureSend("FF 04 00 40", 64, data.headsethandle2)) return false.ToString();
            if (!Command.SetFeatureSend("FF 04 00 40", 64, data.donglehandel4)) return false.ToString();   
            int i = 0;
            while (true)
            {
                Thread.Sleep(300);
                if (!Command.SetFeatureSend("FF 09 00 FD 04 00 00 05 81 AA C6 01", 64, data.donglehandel4)) return false.ToString();
                if (!Command.GetFeatureReturn("FF 09 00 FD 04 00 00 05 81 AA C6 01", 64, data.donglehandel4, "11")) return false.ToString();
                if (Command.ReturnValue == "01")
                {
                    if (!Command.SetFeatureSend("ff 0a 00 fd 04 00 00 05 81 d4 c0 04", 64, data.donglehandel4)) return false.ToString();
                    if (Command.GetFeatureReturn("ff 0a 00 fd 04 00 00 05 81 d4 c0 04", 64, data.donglehandel4, "11 12 13 14"))//获取加密狗ID
                    {
                        ID_DONGE = Command.ReturnValue;
                    }
                    if (!Command.SetFeatureSend("ff 0a 00 fd 04 00 00 05 81 0d b1 04", 64, data.headsethandle2)) return false.ToString();
                    if (Command.GetFeatureReturn("ff 0a 00 fd 04 00 00 05 81 0d b1 04", 64, data.headsethandle2, "11 12 13 14"))//获取耳机ID
                    {
                        ID_HEADSET = Command.ReturnValue;
                        if (ID_HEADSET == ID_DONGE)//比较HEADSET的ID和DONGLE的ID
                        {
                            return true.ToString();
                        }
                    }
                }
                if (i > 20) return false.ToString();
                i++;
                 
            }
        }
        public string Checkmute()
        {
            if (!Command.SetReportSend("11 FF 05 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, data.donglehandel3))return false.ToString() ;
            Thread.Sleep(100);
            if (!Command.WriteSend("06 88 04 01 00", 20, data.headsethandle1)) return false.ToString() ;
            Thread.Sleep(100);
            if (!OldButton.Buttontest("00", "4", "Vui lòng gạt Mic xuống\n请将MIC推到底 ", data.headsetpath1)) return false.ToString() ;
            Thread.Sleep(100);
            if (!OldButton.Buttontest("01", "1", "Vui lòng ấn nút Mute\n请按 MUTE 按钮", data.donglepath2)) return false.ToString() ;
            return true.ToString();
        }
        public string Check_LED_mic()
        {
            var valueTestModeOn = "11 FF 05 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            var Value_Mix = "06 88 04 01 00";
            var Value_Mix1 = "00";
            var Value_Mix2 = "01";
            var indexs = "4";
            Thread.Sleep(300);
            if (Command.SetReportSend(valueTestModeOn, 20, data.donglehandel3))
            {
                Thread.Sleep(300);
                if (Command.SetReportSend(valueTestModeOn, 20, data.donglehandel3))
                {
                    Thread.Sleep(300);
                    if (Command.WriteSend(Value_Mix, 20, data.headsethandle1))
                    {
                        Thread.Sleep(300);
                        var valueTestModeOn1 = "11 ff 05 2f 00";
                        Command.SetReportSend(valueTestModeOn1, 20, data.donglehandel3);
                        if (OldButton.Buttontest(Value_Mix1, indexs, "Vui lòng gạt Mic xuống\n请将MIC推到底 ", data.headsetpath1))
                        {

                            DialogResult result = MessageBox.Show("Vui lòng kiểm tra đèn MIC tắt không?/请检查MIC灯是否熄灭", "Xác nhận", MessageBoxButtons.YesNo);

                            // Kiểm tra kết quả
                            if (result == DialogResult.Yes)
                            {
                                if (Command.SetReportSend(valueTestModeOn, 20, data.donglehandel3))
                                {
                                    Thread.Sleep(300);
                                    if (Command.WriteSend(Value_Mix, 20, data.headsethandle1))
                                    {
                                        var valueTestModeOn2 = "11 ff 05 2f 00";
                                        Command.SetReportSend(valueTestModeOn2, 20, data.donglehandel3);
                                        if (OldButton.Buttontest(Value_Mix2, indexs, "Vui lòng gạt Mic lên\n请向上推麦克风杆", data.headsetpath1))
                                        {
                                            DialogResult result2 = MessageBox.Show("Vui lòng kiểm tra đèn MIC bật không?/请检查MIC灯是否亮起", "Xác nhận", MessageBoxButtons.YesNo);


                                            if (result2 == DialogResult.Yes)
                                            {
                                                return true.ToString();
                                            }
                                            else if (result2 == DialogResult.No)
                                            {
                                                return false.ToString();
                                            }
                                        }
                                    }
                                }

                            }
                            else if (result == DialogResult.No)
                            {
                                return false.ToString();
                            }
                        }
                    }
                }
                return false.ToString();
            }
            else
            {
                return false.ToString();
            }
        }
        public string Check_mic()
        {
            if (!Command.SetReportSend("11 FF 05 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, data.donglehandel3)) return false.ToString();
            Thread.Sleep(100);
            if (!Command.WriteSend("06 88 04 01 00", 20, data.headsethandle1)) return false.ToString();
            Thread.Sleep(100);
            if (!Command.SetReportSend("11 ff 05 2f 00", 20, data.donglehandel3))return false.ToString();
            Thread.Sleep(100);
            if (!OldButton.Buttontest("01", "4", "Vui lòng gạt Mic lên\n请将MIC推到底 ", data.headsetpath1)) return false.ToString();
            return true.ToString();
        }
        public string CheckG_Off()
        {
            var valueTestModeOn = "11 ff 05 2f 00";
            if (Command.SetReportSend(valueTestModeOn, 20, data.donglehandel3))
            {
                return true.ToString();
            }
            return false.ToString();
        }
        public string CheckG()
        {
            var revalueMute = "04";
            var revalueGame = "02";
            var revalueChat = "01";
            var indexs = "4";
            if (!Command.SetReportSend("11 FF 05 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, data.donglehandel3)) return false.ToString();
            if (!Command.SetReportSend("11 FF 05 2F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", 20, data.donglehandel3)) return true.ToString();
            if (!OldButton.Buttontest(revalueMute, indexs, "Vui lòng ấn nút G3\n请按 G3 按钮", Info.DonglePath)) return false.ToString();
            if (!OldButton.Buttontest(revalueGame, indexs, "Vui lòng ấn nút G2\n请按 G2 按钮", Info.DonglePath)) return false.ToString();
            if (!OldButton.Buttontest(revalueChat, indexs, "Vui lòng ấn nút G1\n请按 G1 按钮", Info.DonglePath)) return false.ToString();
            return true.ToString();
        }
        public string Reset_Headset()
        {
            if (!Command.SetReportSend("06 88 04", 20, data.headsethandle1))return false.ToString();
            return true.ToString();
          
        }
        public string Get_Handle()
        {
            var value = "11 FF 08";
            var returnvalue = "11 FF 08 00";
            var indexs = "4 5 6";
            if (Command.SetReportReturn(value, 20, returnvalue, indexs, data.donglepath3, data.donglehandel3))
            {
                var fw = "";
                string[] item = Command.ReturnValue.Split(' ');

                fw += item[0] + item[1] + item[2];

                if (fw!=null)
                {
                    return true.ToString();
                }
            }
            return false.ToString();
        }
        public string Delay5000()
        {
            Thread.Sleep(5000);
            return true.ToString();
        }
        //last program
      
        #endregion
        #region 进度条
        // 弹窗调用的dll
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );
        static ProgressBars msgbox;
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="name">进度条显示的内容</param>
        /// <param name="time">进度条每一格的间隔时间</param>
        /// <returns></returns>
        private bool MessgBox(string name, int time)
        {
            try
            {
                msgbox = new ProgressBars(name, true, time);
                if (msgbox.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 结束进度条
        /// </summary>
        public void Stop()
        {
            IntPtr ptr = FindWindow(null, "aaa");
            if (ptr != IntPtr.Zero)
                Thread.Sleep(50);
            msgbox.DialogResult = DialogResult.OK;
            Thread.Sleep(50);
            PostMessage(ptr, 0x10, 0, 0);
            return;
        }
        #endregion

        #region 读取ini
        // 读ini文件的dll
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpszReturnBuffer, int nSize, string lpFileName);

        // 根据传入的节点读取该节点下的所有内容
        public static Dictionary<string, string> GetKeys(string iniFile, string category)
        {
            byte[] buffer = new byte[2048];
            GetPrivateProfileSection(category, buffer, 2048, iniFile);
            String[] tmp = Encoding.Default.GetString(buffer).Trim('\0').Split('\0');
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (String entry in tmp)
            {
                string[] v = entry.Split('=');
                result.Add(v[0], v[1]);
            }
            return result;
        }

        /// <summary>
        /// 读取内容并进行处理
        /// </summary>
        /// <param name="data">读取的节点名称</param>
        /// <returns></returns>
        public static string Read(string data)
        {
            try
            {
                string value = "";
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\TP test upper and lower limits.ini"; // 获取读取文件的路径
                Dictionary<string, string> content = GetKeys(path, data); // 根据传入的节点在该文件中读取
                foreach (var item in content)
                {
                    value += item.Value + ",";
                }
                return value;
            }
            catch (Exception err)
            {
                return "False";
            }
        }
        #endregion

        #endregion
    }
}
