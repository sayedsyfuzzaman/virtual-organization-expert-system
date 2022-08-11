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
using VOES.Report;

namespace VOES.ExecutiveControls
{
    public partial class ExecutiveMenu : UserControl
    {
        public ExecutiveMenu()
        {
            InitializeComponent();           
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            members1.Hide();
            donation1.Hide();
            financials1.Hide();
            availableVolunteer1.Hide();
            events1.Hide();
            noticePanel1.Hide();
            manageApplication1.Hide();
            homeExecutive1.Show();
            eventReport1.Hide();
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
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            members1.load();
            noticePanel1.Hide();
            donation1.Hide();
            financials1.Hide();
            availableVolunteer1.Hide();
            events1.Hide();
            members1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Show();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            noticePanel1.Hide();
            donation1.Hide();
            financials1.Hide();
            availableVolunteer1.Hide();
            members1.Hide();
            events1.loadData();
            events1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Show();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            noticePanel1.Hide();
            members1.Hide();
            financials1.Hide();
            availableVolunteer1.Hide();
            events1.Hide();
            donation1.Hide();
            eventReport1.fillcombo();
            eventReport1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Show();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            noticePanel1.Hide();
            members1.Hide();
            donation1.load();
            financials1.Hide();
            availableVolunteer1.Hide();
            events1.Hide();
            donation1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Show();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            members1.Hide();
            noticePanel1.loadNotice();
            donation1.Hide();
            financials1.Hide();
            availableVolunteer1.Hide();
            events1.Hide();
            noticePanel1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Show();
            panel9.Hide();
            panel10.Hide();
            noticePanel1.Hide();
            donation1.Hide();
            members1.Hide();
            financials1.loadData();
            availableVolunteer1.Hide();
            events1.Hide();
            financials1.Show();
            manageApplication1.Hide();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Show();
            panel10.Hide();
            noticePanel1.Hide();
            donation1.Hide();
            financials1.Hide();
            members1.Hide();
            events1.Hide();
            availableVolunteer1.Hide();
            manageApplication1.BindGridView();
            manageApplication1.Show();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Show();
            noticePanel1.Hide();
            donation1.Hide();
            members1.Hide();
            financials1.Hide();
            events1.Hide();
            manageApplication1.Hide();
            availableVolunteer1.Show();
            homeExecutive1.Hide();
            eventReport1.Hide();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            homeExecutive1.loadData();
            homeExecutive1.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();
            panel9.Hide();
            panel10.Hide();
            noticePanel1.Hide();
            donation1.Hide();
            members1.Hide();
            financials1.Hide();
            events1.Hide();
            manageApplication1.Hide();
            availableVolunteer1.Hide();
            eventReport1.Hide();
        }
    }
}
