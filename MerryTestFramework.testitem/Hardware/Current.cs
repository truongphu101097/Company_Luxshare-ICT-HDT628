﻿using MerryTestFramework.testitem.Computer;
using System;
using System.IO.Ports;
using System.Threading;

namespace MerryTestFramework.testitem.Hardware
{
    /// <summary>
    /// 获取8342电流表值
    /// </summary>
    public class Current
    {
        #region 8342电流表
        SerialPort port = new SerialPort();
        /// <summary>
        /// 读取电流表的值
        /// </summary>
        /// <param name="port">8342串口</param>
        /// <returns></returns>
        private double ReadValue(SerialPort port)
        {
            string Value = "";
            double V = 0;
            port.WriteLine(":VAL1?");
            Thread.Sleep(200);
            try
            {
                while (port.BytesToRead > 0)
                {

                    Value = port.ReadLine();
                    Thread.Sleep(50);

                }
                double.TryParse(Value, out V);
                return V;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 初始化电流表
        /// </summary>
        /// <param name="sPortName">串口名</param>
        /// <param name="range">档位</param>
        /// <param name="Type">类别</param>
        /// <returns></returns>
        private bool CurrentInit(string sPortName, string range, int Type)
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Dispose();
                    port.Close();
                }
                port.PortName = sPortName;
                port.BaudRate = 9600;
                port.Parity = Parity.None;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.ReadTimeout = 500;
                port.Open();
                port.WriteLine("*IDN?");
                if (!port.IsOpen) port.Open();
                Thread.Sleep(50);
                switch (Type)
                {
                    case 1: port.Write(":CONF:CURR:DC " + range + "\r\n"); break;
                    case 2: port.Write(":CONF:VOLT:DC " + range + "\r\n"); break;
                    case 3: port.Write(":CONF:VOLT:AC " + range + "\r\n"); break;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断值是否在区间内
        /// </summary>
        /// <param name="Value">值</param>
        /// <param name="lowerlimit">数值下限</param>
        /// <param name="upperlimit">数值上限</param>
        /// <returns></returns>
        private bool IsOk(string Value, string lowerlimit, string upperlimit)
        {
            double V1 = 0;
            double V2 = 0;
            double V3 = 0;
            if (!double.TryParse(Value, out V1)) return false;
            if (!double.TryParse(lowerlimit, out V2)) return false;
            if (!double.TryParse(upperlimit, out V3)) return false;
            return (V1 >= V2 && V1 <= V3);
        }
        string oldrange = "";
        int oldcurrent = 0;
        private bool CurrentTests(string sPortName, string lowerlimit, string upperlimit, string range, int current, out string Value)
        {
            Value = "err";
            double value = 0;
            var newrange = range;
            var delay = "2000";
            //去掉延时后面的数据
            if (range.Contains("延时"))
            {
                int a = range.IndexOf("延时");
                newrange = range.Substring(0, a);
                delay = range.Substring(a + 2, range.Length - a - 2);
            }

            if ((oldrange != newrange) || (current != oldcurrent))
            {
                if (!CurrentInit(sPortName, newrange, current)) return false;

                oldrange = newrange;
                oldcurrent = current;
            }
            Thread.Sleep(Convert.ToInt32(delay));
            if (!port.IsOpen) port.Open();
            for (int i = 0; i < 20; i++)
            {
                value = ReadValue(port);
                if (value != 0) break;
                Thread.Sleep(200);
            }
            switch (newrange)
            {

                case "5": if (current == 1) value = value * 1000; break;
                case "0.5": value = value * 1000; break;
                case "0.0005": value = value * 1000000; break;
            }
            Value = value.ToString("f2");
            return IsOk(Value, lowerlimit, upperlimit);
        }
        #endregion

        #region 8342电流表对外方法
        /// <summary>
        /// 8342电流表对外方法
        /// </summary>
        /// <param name="sPortName">串口名</param>
        /// <param name="lowerlimit">电流最大限定值</param>
        /// <param name="upperlimit">电流最小限定值</param>
        /// <param name="range">指令</param>
        /// <param name="type">类别（ 1.电流测试 2.直流电压测试 3.交流电压测试 ）</param>
        /// <param name="Value">实测测试电流值</param>
        /// <returns></returns>
        public bool CurrentTest(string sPortName, string lowerlimit, string upperlimit, string range, int type, out string Value)
        {
            Value = "err";
            try
            {
                //仅切换档位
                if (range.Contains("切换"))
                {
                    if (!CurrentInit(sPortName, range.Replace("切换", ""), type)) return false;
                    return true;
                }
                //测试电流并且附带弹窗
                if (range.Contains("-"))
                {
                    string[] arr = range.Split('-');
                    if (new MessageBox().JudgeBox(arr[1]))
                    {
                        if (CurrentTests(sPortName, lowerlimit, upperlimit, arr[0], type, out Value)) return true;
                        return false;
                    }
                    return false;
                }
                //测试电流
                if (CurrentTests(sPortName, lowerlimit, upperlimit, range, type, out Value)) return true;
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
          
            }

        }
        #endregion

    }
}
