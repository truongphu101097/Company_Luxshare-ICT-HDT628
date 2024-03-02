using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerryTestFramework.testitem.Utils
{
    /// <summary>
    /// 数据转换
    /// </summary>
    public class DataConversion
    {
        #region 移除數組后幾項 changes(string[] name, int num)
        public string[] changes(string[] name, int num)
        {
            string[] name1 = new string[7];
            for (int i = 0; i < name.Length - num; i++)
            {
                name1[i] = name[i];
            }

            return name1;
        }
        #endregion
        #region 將字符串數組合併為字符串 change(string[] name)
        public string change(string[] name)
        {
            string name1 = "";
            for (int i = 0; i < name.Length; i++)
            {

                name1 = name1 + name[i] + ",";
            }

            return name1;

        }
        #endregion
        #region 將16進制字符串轉換為byte數組 GetByteArray(string shex)
        public byte[] GetByteArray(string shex, int lenght)
        {
            string[] ssArray = shex.Split(' ');
            List<byte> bytList = new List<byte>();
            int i = 0;
            foreach (var s in ssArray)
            {   //将十六进制的字符串转换成数值  
                bytList.Add(Convert.ToByte(s, 16));
                i++;
            }
            for (int j = i; j < lenght; j++)
            {
                bytList.Add(Convert.ToByte("0"));
            }
            return bytList.ToArray();
        }
        #endregion
        #region 將byte數組轉換為16進制字符串 byteToHexStr(byte[] bytes)
        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2") + " ";
                }
            }
            returnStr = returnStr.Trim();
            return returnStr;
        }

        #endregion
        #region 將空格隔開的字符串轉換為int 數組
        public int[] GetintArray(string ar)
        {
            string[] strArray = ar.Split(' ');
            int[] intArray;
            intArray = Array.ConvertAll<String, int>(strArray, s => int.Parse(s));
            return intArray;
        }
        #endregion
        #region 將16進制字符串轉換為 int 
        public int Hex2Ten(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten;
        }

        private int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }
        #endregion
        #region 将指令集转为指令数组
        public string[] clist(string str)
        {
            return str.Split('-');
        }
        #endregion
        #region 将linkedlist指定节点转为数组
        public string[] changelinkedlist(LinkedList<string[]> clist, int i)
        {
            string[] arr = new string[2];
            int m = 0;
            foreach (string[] item in clist)
            {
                if (m != i) { m++; continue; }
                arr = (string[])item.Clone();
            }
            return arr;
        }
        #endregion
    }
}
