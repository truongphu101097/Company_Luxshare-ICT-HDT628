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
    public partial class View : Form
    {
        public string i;
        public View()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void View_Load(object sender, EventArgs e)
        {
            view();
        }

        private void view()
        {
            if (i == "1")
            {
                label1.Text = "机型：HDT628\n名称:G933s Gaming Headset";


            }
            if (i == "2")
            {
                label1.Text = "机型：HDT628\n名称: G935 Gaming Headset";

            }
        }
    }
}
