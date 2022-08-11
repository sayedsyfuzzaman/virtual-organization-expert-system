using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class financials : UserControl
    {
        public financials()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            internalDonations1.LoadData();
            internalDonations1.Show();
            externalDonationsFront1.Hide();
            internalDonationLog1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            internalDonations1.LoadData();
            internalDonations1.Show();
            externalDonationsFront1.Hide();
            internalDonationLog1.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            externalDonationsFront1.LoadData();
            externalDonationsFront1.Show();
            internalDonations1.Hide();
            internalDonationLog1.Hide();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            internalDonationLog1.LoadData();
            internalDonationLog1.Show();
            internalDonations1.Hide();
            externalDonationsFront1.Hide();
        }
    }
}
