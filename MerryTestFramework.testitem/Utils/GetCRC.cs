using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Other
{
    /// <summary>
    /// 获取CRC加密码
    /// </summary>
    public class GetCRC
    {
        private BaseConversion bc = new BaseConversion();
        /// <summary>
        /// CRC-16/CCITT-FALSE加密
        /// </summary>
        /// <param name="shex">需要加密的CRC16进制字符串</param>
        /// <param name="lenth">字符串需要转换的Byte长度</param>
        /// <returns></returns>
        public int crc16_CCITTFALSE(string shex, int lenth)
        {
            var bytes = bc.GetByteArray(shex, lenth);
            var crc = 0xFFFF;
            for (var j = 0; j < lenth; j++)
            {
                crc = (((ushort)crc >> 8) | (crc << 8)) & 0xffff;
                crc ^= (bytes[j] & 0xff);// byte to int, trunc sign
                crc ^= ((crc & 0xff) >> 4);
                crc ^= (crc << 12) & 0xffff;
                crc ^= ((crc & 0xFF) << 5) & 0xffff;
            }
            crc &= 0xffff;
            return crc;
        }
    }
}
