using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MerryDllFramework
{
    internal class MatchTarget
    {
        const string path = @".\TestItem\HDT628\hostapi_vmi.dll";
        #region 参数
        public enum Status
        {
            Status_BAD_CHIP_ID = -6,
            Status_SPI_ASSERT = -5,
            Status_ARGUMENT_ERROR = -4,
            Status_INVALID_HANDLE = -3,
            Status_UNIMPLEMENTED = -2,
            Status_FAILURE = -1,
            Status_SUCCESS = 0,
        }
        public enum DeviceType
        {
            DeviceType_Internal = 0, //< Internal USB
            DeviceType_Anteater = 1, //< Avnera USB to SPI converter
            DeviceType_Aardvark = 2, //< TotalPhase Aardvark
        }
        static int TX_index = -1;
        static int RX_index = -1;
        static Int32 dongle_handle = 0;
        static int headset_handle = 0;
        [DllImport("hostapi_vmi.dll")]
        public static extern Status systemVersionAppNameGet(int handle, ref byte Version, ref byte pAppName);

        [DllImport("hostapi_vmi.dll")]
        public static extern Status AvDllInitialize();
        [DllImportAttribute(path, EntryPoint = "AvDllListDevices", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern Status AvDllListDevices(DeviceType dev, int count, ref UInt16 devices, ref UInt32 unique_ids);
        [DllImportAttribute(path, EntryPoint = "AvDllOpenDevice", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern Status AvDllOpenDevice(DeviceType dev, int index, ref int pId);
        [DllImportAttribute(path, EntryPoint = "rfFixedChannelSetup", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern Status rfFixedChannelSetup(int handle, byte channel);
        [DllImportAttribute(path, EntryPoint = "AvDllCloseHandle", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern Status AvDllCloseHandle(int handle);
        [DllImport("hostapi_vmi.dll")]
        public static extern Status MemoryPeek(int handle, ushort addr, byte length, ref byte buffer);
        #endregion
        #region MyRegion
        static bool MatchTargetDevice(string TXpid, string TXvid, string RXpid, string RXvid)
        {

            uint Dpid, Dvid, HPid, HVid;
            Dpid = (uint)int.Parse(TXpid, System.Globalization.NumberStyles.AllowHexSpecifier);
            Dvid = (uint)int.Parse(TXvid, System.Globalization.NumberStyles.AllowHexSpecifier);
            HPid = (uint)int.Parse(RXpid, System.Globalization.NumberStyles.AllowHexSpecifier);
            HVid = (uint)int.Parse(RXvid, System.Globalization.NumberStyles.AllowHexSpecifier);

            Status status;
            UInt16[] portArray = new UInt16[5];
            UInt32[] deviceIDArray = new UInt32[5];
            uint deviceCount = 0;
            uint tmpvid, tmppid;
            status = AvDllListDevices(DeviceType.DeviceType_Internal, 5, ref portArray[0], ref deviceIDArray[0]);
            deviceCount = (UInt32)status;
            if (deviceCount == 0)
            {

                return false;
            }
            else
            {
                for (Int32 i = 0; i < deviceCount; i++)
                {
                    tmpvid = (deviceIDArray[i] >> 16) & 0xffff;
                    tmppid = deviceIDArray[i] & 0xffff;

                    if (tmpvid == Dvid && tmppid == Dpid)
                    {
                        TX_index = i;
                    }
                    else if (tmpvid == HVid && tmppid == HPid)
                    {
                        RX_index = i;
                    }
                }
                if ((TX_index == -1) & (RX_index == -1))
                {
                    return false;
                }

            }
            return true;
        }


        static bool OpenTXDevice()
        {
            Status status;
            status = AvDllOpenDevice(DeviceType.DeviceType_Internal, TX_index, ref dongle_handle);
            if (status == Status.Status_SUCCESS)
            {
                if (dongle_handle != 0)
                {
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;

        }
        static bool SetDongleRFChannel(string channel)
        {
            Status status;
            byte Dut_Channel = 0;
            Byte.TryParse(channel, out Dut_Channel);
            status = rfFixedChannelSetup(dongle_handle, Dut_Channel);
            if (status != Status.Status_SUCCESS)
            {
                return false;
            }
            else
            {
            }

            if (dongle_handle != 0)
            {
                Thread.Sleep(100);
                status = AvDllCloseHandle(dongle_handle);
            }
            return true;
        }
        static bool OpenRXDevice()
        {

            Status status;
            status = AvDllOpenDevice(DeviceType.DeviceType_Internal, RX_index, ref headset_handle);
            if (status == Status.Status_SUCCESS)
            {
                if (headset_handle != 0)
                {
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        static bool SetHeadsetRFChannel(string channel)
        {
            Status status;
            byte Dut_Channel = 0;
            Byte.TryParse(channel, out Dut_Channel);
            status = rfFixedChannelSetup(headset_handle, Dut_Channel);
            if (status != Status.Status_SUCCESS)
            {
                return false;
            }
            else
            {
            }

            if (headset_handle != 0)
            {
                Thread.Sleep(100);
                //status = AvDllCloseHandle(headset_handle);
                AvDllCloseHandle(headset_handle);
            }
            return true;
        }
        #endregion
        /// <summary>
        ///   设置天线通道（antenna）
        /// </summary>
        /// <param name="TXpid"></param>
        /// <param name="TXvid"></param>
        /// <param name="RXpid"></param>
        /// <param name="RXvid"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static bool Set_ChannelPair(string TXpid, string TXvid, string RXpid, string RXvid, string channel)
        {
            if (!MatchTargetDevice(TXpid, TXvid, RXpid, RXvid)) return false;
            Thread.Sleep(400);
            if (!OpenRXDevice()) return false;
            if (!SetHeadsetRFChannel(channel)) return false;
            Thread.Sleep(400);
            if (!OpenTXDevice()) return false;
            if (!SetDongleRFChannel(channel)) return false;
            return true;
        }

        #region 天线频道


        static int handle = 0;
        static int index = -1;
        static bool MatchTargetDevice(string pid, string vid)
        {

            uint Dpid, Dvid;
            Dpid = (uint)int.Parse(pid, System.Globalization.NumberStyles.AllowHexSpecifier);
            Dvid = (uint)int.Parse(vid, System.Globalization.NumberStyles.AllowHexSpecifier);

            Status status;
            UInt16[] portArray = new UInt16[5];
            UInt32[] deviceIDArray = new UInt32[5];
            uint deviceCount = 0;
            uint tmpvid, tmppid;
            status = AvDllListDevices(DeviceType.DeviceType_Internal, 5, ref portArray[0], ref deviceIDArray[0]);
            deviceCount = (UInt32)status;
            if (deviceCount == 0)
            {

                return false;
            }
            else
            {
                for (Int32 i = 0; i < deviceCount; i++)
                {
                    tmpvid = (deviceIDArray[i] >> 16) & 0xffff;
                    tmppid = deviceIDArray[i] & 0xffff;

                    if (tmpvid == Dvid && tmppid == Dpid)
                    {
                        index = i;
                    }
                }
                if ((index == -1))
                {
                    return false;
                }

            }
            return true;
        }
        static bool Open_Device()
        {
            Status status;
            status = AvDllOpenDevice(DeviceType.DeviceType_Internal, index, ref handle);
            if (status == Status.Status_SUCCESS)
            {
                if (handle != 0)
                {
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        static bool SetRFChannel(string channel)
        {
            Status status;
            byte Dut_Channel = 0;
            Byte.TryParse(channel, out Dut_Channel);
            status = rfFixedChannelSetup(handle, Dut_Channel);
            if (status != Status.Status_SUCCESS)
            {
                return false;
            }
            else
            {
            }

            if (handle != 0)
            {
                Thread.Sleep(100);
                //status = AvDllCloseHandle(headset_handle);
                AvDllCloseHandle(handle);
            }
            return true;
        }
        public static bool Set_DeviceRFChannel(string pid, string vid, string channel)
        {

            if (!MatchTargetDevice(pid, vid)) return false;
            if (!Open_Device()) return false;
            if (!SetRFChannel(channel)) return false;
            return true;

        }
        #endregion

    }
}
