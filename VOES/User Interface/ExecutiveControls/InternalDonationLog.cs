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
    public partial class InternalDonationLog : UserControl
    {
        public InternalDonationLog()
        {
            InitializeComponent();
        }


        DataTable data = new DataTable();
        internal void LoadData()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select i.members_id as 'ID', m.name as 'Name', i.payment as 'Payment', i.pre_balance as 'Pre-Balance', i.curr_balance as 'Curr-Balance',  FORMAT (i.payment_date, 'dd MMMM,yy') as 'Date' from internal_donation_log i , members m where i.members_id = m.id order by payment_date desc;";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[ID] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
    }
}
