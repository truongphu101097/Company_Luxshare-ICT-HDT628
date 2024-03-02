using MerryDllFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryDllFramework_Debug
{
    class Program
    {
        static void Main(string[] args)
        {

            MerryDll dll = new MerryDll();
            Console.WriteLine(dll.Start(new List<string>(),IntPtr.Zero));
            new List<string>()
                {

                    "Reset_Headset",
                  /*  "Reset_Headset",*/
                
             /*       "HeadsetArbiterID",*/
                   /*  "VloDown",
                    "VolUp",*/
                    /*"GetHeadsetPIDVID",
                    "GetHeadsetAVFW",
                    "GetHeadsetID"*/
                }.ForEach(item =>
                {
                    Console.WriteLine(item + ":" + dll.Run(item));
                });
            //int i = 1;
            //while (i > 0)
            //{
            //    Console.WriteLine($"当前时间为{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}");
            //    new List<string>()
            //    {
            //        "GetBatteryLevel",
            //        "NTCCheck",
            //    }.ForEach(item =>
            //    {
            //        if (dll.Run(item).Equals("False"))
            //        {
            //            Console.WriteLine($"停止时间为{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}");
            //            i = 0;
            //        }
            //        else
            //        {
            //            Console.WriteLine(item + ":" + dll.Run(item));
            //        }

            //    });
            //}
            //"GetDongleAVFW",
            //"GetDongleID",
            //"GetDongleMCUFW",
            //"GetHeadsetAVFW",
            //"Pair",
            //"GetHeadsetID",
            //"TestMuteGameChatButton",
            //"TestVolumeButton",
            //"GetBatteryVoltage",
            //"PowerOn",
            //"ResetHeadset",
            //"PowerOff"

            Console.ReadKey();

        }
    }
}
