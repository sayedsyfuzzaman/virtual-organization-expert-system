using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface.VolunteerControls
{
    public partial class Applications : UserControl
    {
        public Applications()
        {
            InitializeComponent();
            sendApplication1.Hide();
            allApplications1.Hide();
        }

        internal void LoadData()
        {
            allApplications1.BindGridView();
            allApplications1.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            sendApplication1.Show();
            allApplications1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            allApplications1.BindGridView();
            allApplications1.Show();
            sendApplication1.Hide();
        }
    }
}
