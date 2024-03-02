using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDT668_HID_CMD
{
    public partial class ProgressBars : Form
    {
        private bool flag;
        private int time;


        public ProgressBars(string name, bool flag, int time1)
        {
            InitializeComponent();
            this.Text = name;
            this.flag = flag;
            time = time1;

        }

        private void ProgressBars_Load(object sender, EventArgs e)
        {
            if (flag) { i = 0; timer1.Interval = time; timer1.Enabled = true; }
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i + 5;
            progressBar1.Value = i;
            //如果執行時間超過，則this.DialogResult = DialogResult.No;
            if (i >= 100)
            {
                this.DialogResult = DialogResult.No;
                timer1.Enabled = false;
                this.Close();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
