using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryDllFramework
{
    public partial class Color : Form
    {
        public Color()
        {
            InitializeComponent();
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        System.Environment.Exit(0);
                        return;
                    }
                }
            }
  
        }

        private void Color_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }
        public void DisplayInfo(string Workorder,string ItemNo,string FW,string PicPath)
        {
            textBox1.Text = Workorder;
            textBox2.Text = ItemNo;
            textBox3.Text = FW;
            pictureBox1.Image = Image.FromFile(PicPath);
        }
    }
}
