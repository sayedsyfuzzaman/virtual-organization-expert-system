using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOES.User_Interface.ExecutiveControls;

namespace VOES.User_Interface
{
    public partial class events : UserControl
    {
        public events()
        {
            InitializeComponent();
        }
        internal void loadData()
        {
            allEvents1.LoadData();
            allEvents1.Show();
            createEvents1.Hide();
            manageEvent1.Hide();
            event_history1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            allEvents1.LoadData();
            allEvents1.Show();
            manageEvent1.Hide();
            createEvents1.Hide();
            event_history1.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            allEvents1.Hide();
            createEvents1.Show();
            manageEvent1.Hide();
            event_history1.Hide();
        }

        private void createEvents1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            allEvents1.Hide();
            createEvents1.Hide();
            manageEvent1.resetcontrol();
            manageEvent1.fillcombo();
            manageEvent1.Show();
            event_history1.Hide();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            allEvents1.Hide();
            createEvents1.Hide();
            manageEvent1.Hide();
            event_history1.BindExpenseLog();
            event_history1.Show();
            
        }
    }
}
