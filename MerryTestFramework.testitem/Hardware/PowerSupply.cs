using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Hardware
{

    /// <summary>
    /// 调节电源供给器
    /// </summary>
    public class PowerSupply
    {
        SerialPort serialPort1;
        /// <summary>
        /// 调节2303电源供给器
        /// </summary>
        /// <param name="comportName">串口名</param>
        /// <param name="commands">指令</param>
        /// <returns></returns>
        public bool SET2303s(string comportName, string commands)
        {
            try
            {
                if (serialPort1 == null)
                {
                    serialPort1 = new SerialPort();
                    serialPort1.PortName = comportName;
                    serialPort1.BaudRate = 9600;
                    Thread.Sleep(50);
                    serialPort1.Open();
                }
                string[] arr = commands.Split('-');
                var command = new
                {
                    volt = arr[0],
                    current = arr[1],
                    ch = arr[2]
                };
                serialPort1.WriteLine("OUT0");
                serialPort1.WriteLine("ISET" + command.ch + ":" + command.current);
                serialPort1.WriteLine("VSET" + command.ch + ":" + command.volt);
                serialPort1.WriteLine("OUT1");
                serialPort1.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
