using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAP_practice
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            desc1.BringToFront();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            desc1.BringToFront();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            map1.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel1.Width == 45)
            {
                panel1.Visible = false;
                panel1.Width = 253;
                SlideAnimator.ShowSync(panel1);
            }
            else
            {
                panel1.Visible = false;
                panel1.Width = 45;
                SlideAnimator.ShowSync(panel1);
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            help1.BringToFront();
        }
    }
}
