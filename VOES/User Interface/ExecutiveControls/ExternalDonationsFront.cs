using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class ExternalDonationsFront : UserControl
    {
        public ExternalDonationsFront()
        {
            InitializeComponent();
        }

        DataTable data = new DataTable();
        internal void LoadData()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select donor_name as 'Donor Name', phone as 'Phone', email as 'Email', amount as 'Amount', company as 'Company', FORMAT (date, 'dd MMMM,yy') as 'Payment Date' from external_donation_log order by date desc;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[Phone] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
    }
}
