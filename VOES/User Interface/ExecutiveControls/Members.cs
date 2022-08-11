using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOES.Controllers;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class Members : UserControl
    {
        public Members()
        {
            InitializeComponent();
            addMembers1.Hide();
            allMembers1.Show();
        }

        internal void load()
        {
            allMembers1.LoadData();
            allMembers1.BindALLGridView();
        }

        private void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            addMembers1.Show();
            allMembers1.Hide();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            allMembers1.LoadData();
            allMembers1.BindALLGridView();
            allMembers1.Show();
            addMembers1.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            addMembers1.Show();
            allMembers1.Hide();
        }
    }
}
