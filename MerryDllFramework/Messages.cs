using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryDllFramework
{
    public partial class Messages : Form
    {
        private string Mes { get; set; }
        private int Interval { get; set; }
        public Messages(string mes, int interval)
        {
            InitializeComponent();
            Mes = mes;
            Interval = interval;
        }

        private void btnYES_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            lblMes.Text = Mes;
            timer1.Interval = Interval;
            timer1.Enabled = true;
            this.btnYES.Enabled = false;
            button1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnYES.Enabled = true;
            btnYES.Focus();
            timer1.Enabled = false;
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnYES_Click(null, null);
            }
            else if (e.KeyCode == Keys.N)
            {
                btnNO_Click(null, null);
            }
        }
    }
}
