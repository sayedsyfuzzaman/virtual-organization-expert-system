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
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class InternalDonations : UserControl
    {

        public InternalDonations()
        {
            InitializeComponent();
        }

        DataTable data = new DataTable();
        internal void LoadData()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select f.members_id as 'ID', m.name as 'Name', m.phone as 'Phone Number', m.designation as 'Designation', f.balance as 'Balance' from members m, members_financial_info f where f.members_id = m.id;";
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
