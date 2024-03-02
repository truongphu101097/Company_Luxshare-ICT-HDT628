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
    public partial class Selected : Form
    {
        public string i;
        public Selected()
        {
            InitializeComponent();
        }

        private void Selected_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = "1";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i = "2";
            this.Close();
        }
    }
}
