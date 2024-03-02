using MESDLL;
using SwATE_Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryTestFramework.testitem.Other
{
    /// <summary>
    /// MES操作类
    /// </summary>
    public class MES

    {

        #region 参数区
     
        private string sMESUrl = @"http://10.55.2.114:1101/Service_ATE_Manager/ATE_Manager.svc";
        private struct _MES
        {
            internal int enable;
            internal string userName;
            internal string station;
            internal SwATE mesInst;
            internal string sLastSN;
            internal int iFailCount;
        }
        private bool retestflag = false;
        private _MES mMes;
        private static MESBDA mes = new MESBDA();
        #endregion
        /// <summary>
        /// 登陆数据库
        /// </summary>
        /// <param name="station">站别</param>
        /// <param name="userID">工号</param>
        public bool checkUserAndStation(string station, string userID, out string message)
        {

            mMes = new _MES();
            mMes.mesInst = new SwATE(sMESUrl);
            string str = mMes.mesInst.checkEmpNo(userID, station + "Test");
            message = str;
            if (str.Contains("OK")) return true;
            return false;

        }


        /// <summary>
        ///  检查条码的合法性 
        /// </summary>
        /// <param name="works">工单</param>
        /// <param name="SN">SN</param>
        /// <param name="station">站别</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public bool checkSNT4(string works, string SN, string station, out string message)
        {
            message = "SN码不合法";
            if (!ReTest(SN)) return false;
            if (mes.IsBelongToMoNumber(works, SN, out message))
            {
                try
                {
                    message = mMes.mesInst.checkSN_Station(SN, station + "Test");
                    if (message.Contains("OK")) return true;
                    message = message.Split(':')[1];
                }
                catch (Exception)
                {
                    message = "SN码不合法";
                }
            }
            return false;
        }

        /// <summary>
        /// 过站 T4
        /// </summary>
        /// <param name="flag">测试是否成功</param>
        /// <param name="SN">SN号</param>
        /// <param name="ErrorCode"></param>
        /// <param name="userID">工号</param>
        /// <param name="station">站别</param>
        public bool MEST4(bool flag, string SN, string ErrorCode, string userID, string station)
        {
            try
            {
                var str = mMes.mesInst.sendTestResult(userID, SN, station + "Test", flag ? "OK;" : "NG;" + ErrorCode);//过站接口
                return str.Contains("OK");
            }
            catch
            {
                MessageBox.Show("上传出错，请检查网络！");
            }
            return false;
        }
        /// <summary>
        /// 上传条码 
        /// </summary>
        /// <param name="SN">SN号</param>
        /// <param name="ErrorCode"></param>
        /// <param name="works">工单</param>
        public bool sendSNtoMEST4(string SN, string ErrorCode, string works)
        {
            return mes.IsExistMoBaseAndSerialNumber(works, SN, out ErrorCode) && mes.UploadKingStonSN(works, SN);
        }

        /// <summary>
        /// 检查条码的合法性 T3
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool checkSNT3(string SN, string station)
        {
            if (mes.testHeavyIndustry(SN))
            {
                retestflag = true;
                return true;
            }
            else
            {
                retestflag = false;
            }
            if (mes.IsBindSecurityCode(SN, false))
            {
                if (mMes.mesInst.checkSN_Station(SN, station).Contains("OK")) return true;
            }
            return false;

        }
        /// <summary>
        /// 上传条码过站 T3
        /// </summary>
        /// <param name="ErrorFlag"></param>
        /// <param name="SN"></param>
        /// <param name="ErrorCode"></param>
        /// <param name="userID"></param>
        /// <param name="station"></param>
        /// <param name="works"></param>
        /// <returns></returns>
        public bool sendSNtoMEST3(bool ErrorFlag, string SN, string ErrorCode, string userID, string station, string works)
        {


            string str = "";
            string strResult = ErrorFlag ? "OK;" : "NG;" + ErrorCode;
            try
            {

                if (retestflag)
                {
                    if (mes.insertStopT3(SN, userID)) return true;
                    else return false;
                }
                str = mMes.mesInst.sendTestResult(userID, SN, station, strResult);//过站接口
                if (str.Contains("OK") && ErrorFlag == true)
                {
                    if (mes.IsExistMoBaseAndSerialNumber(works, SN, out ErrorCode))
                    {
                        if (mes.UploadKingStonSN(works, SN)) return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("上传出错，请检查网络！");
            }
            return false;
        }
        #region 重工方法 

        private bool ReTest(string sn)
        {
            if (mes.testHeavyIndustry(sn))
            {
                if (mes.selectTThree(sn))
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        public bool TEST(string SN, string works)
        {
            return mes.UploadKingStonSN(works, SN);
        }
        public bool TEST1(string SN, string works)
        {
            return mes.InsertFeilBD(SN, works);
        }
        public string TEST2(string works)
        {
            return mes.getSucceedBD(works);
        }

    }
}
