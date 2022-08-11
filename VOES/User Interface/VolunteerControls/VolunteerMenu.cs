using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.VolunteerControls
{
    public partial class VolunteerMenu : UserControl
    {
        public VolunteerMenu()
        {
            InitializeComponent();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            freeTimeUpdate1.Hide();
            applications1.Hide();
            eventsPanel1.Hide();
            noticeSection1.Hide();
            homeExecutive1.Show();
        }

        internal void loadData()
        {
            homeExecutive1.loadData();
            homeExecutive1.Show();
        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            freeTimeUpdate1.Hide();
            applications1.Hide();
            eventsPanel1.loadData();
            eventsPanel1.Show();
            noticeSection1.Hide();
            homeExecutive1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Show();
            panel5.Hide();
            panel6.Hide();
            freeTimeUpdate1.Hide();
            applications1.Hide();
            eventsPanel1.Hide();
            noticeSection1.loadData();
            noticeSection1.Show();
            homeExecutive1.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
            panel6.Hide();
            freeTimeUpdate1.Hide();
            applications1.LoadData();
            applications1.Show();
            eventsPanel1.Hide();
            noticeSection1.Hide();
            homeExecutive1.Hide();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
            freeTimeUpdate1.loadData();
            freeTimeUpdate1.Show();
            applications1.Hide();
            eventsPanel1.Hide();
            noticeSection1.Hide();
            homeExecutive1.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            homeExecutive1.loadData();
            homeExecutive1.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            freeTimeUpdate1.Hide();
            applications1.Hide();
            eventsPanel1.Hide();
            noticeSection1.Hide();
        }
    }
}
