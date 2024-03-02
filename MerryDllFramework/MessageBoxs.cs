using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace MerryTest.testitem
{
    /// <summary>
    /// 弹窗
    /// </summary>
    public partial class MessageBoxs : Form
    {
        #region 对外部分

        public static bool messagebox(string msg, string color = "", string title = "MessageBox")
        {
            MessageBoxs messageboxss = new MessageBoxs();
            messageboxss.True_button.Visible = true;
            messageboxss.False_button.Visible = true;
            messageboxss.message = msg;
            messageboxss.Text = title;
            if(color.Trim().Length > 1)
            {
                messageboxss.color = Color.FromName(color);
                if(messageboxss.color == Color.Black)
                {
                    messageboxss.color = Color.Transparent;
                }
            }
            messageboxss.ShowDialog();
            var result = messageboxss.DialogResult;//先关闭会获取不到值
            return result == DialogResult.OK;
        }
        public static bool BarCodeBox(string message, int length, out string barcode)
        {
            MessageBoxs messageboxss = new MessageBoxs();
            messageboxss.message = message;
            messageboxss.length = length;
            messageboxss.uploadmes = true;
            messageboxss.BarCode_textBox.Visible = true;
            messageboxss.ShowDialog();
            var result = messageboxss.DialogResult;//先关闭会获取不到值
            barcode = messageboxss.barcode;//先关闭会获取不到值
            messageboxss.Dispose();
            return result == DialogResult.OK;
        }

        #endregion

        #region 窗体部分


        /// <summary>
        /// 窗体名
        /// </summary>
        string names = "";
        /// <summary>
        /// 窗体信息
        /// </summary>
        string message = "";
        /// <summary>
        /// 窗体条码框长度
        /// </summary>
        int length = 0;
        /// <summary>
        /// 条码框输入值
        /// </summary>
        string barcode = "";
        /// <summary>
        /// 窗体颜色
        /// </summary>
        Color color = Color.Transparent;
        /// <summary>
        /// 不良SN
        /// </summary>
        string SN = "";
        /// <summary>
        /// 是否Y,N检测按键
        /// </summary>
        bool uploadmes = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        MessageBoxs()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }
        private void messageBox_Load(object sender, EventArgs e)
        {
            Text = names;
            Message_label.Text = message;
            Message_label.BackColor = color;
            TopLevel = true;
            TopMost = true;

        }
        private void button1_Click(object sender, EventArgs e)
        {


            DialogResult = DialogResult.OK;
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (length > 0) if (BarCode_textBox.Text.Length != length)
                {
                    System.Windows.Forms.MessageBox.Show("条码长度不正确");
                    return;
                }
            barcode = BarCode_textBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void text_SN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (text_SN.Visible)
            {
                if (text_SN.Text.Length != 4)
                {
                    System.Windows.Forms.MessageBox.Show("不良SN码必须4位");
                    return;
                }

                SN = text_SN.Text;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void MessageBoxs_KeyDown(object sender, KeyEventArgs e)
        {
            if (uploadmes) return;
            if (e.KeyCode == Keys.N)
            {
                button2_Click(null, null);
                return;
            }
            if (e.KeyCode == Keys.Y)
            {
                button1_Click(null, null);
                return;
            }

        }
        #endregion

    }
}

