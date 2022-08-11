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
    public partial class event_history : UserControl
    {
        public event_history()
        {
            InitializeComponent();
        }

        DataTable data = new DataTable();
        internal void BindExpenseLog()
        {
            bunifuLabel1.Text = "Expense History";
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter("select event_name as 'Event Name', amount as 'Amount', sector as 'Expense Sector', FORMAT (date, 'hh:mm tt, MMM dd yyyy') as 'Date', addedby as 'Added By' from event_expense_log order by date desc;", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[Event Name] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
        
        internal void BindFundingLog()
        {
            bunifuLabel1.Text = "Funding History";
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter("select event_name as 'Event Name', amount as 'Amount', sector as 'Expense Sector', FORMAT (date, 'hh:mm tt, MMM dd yyyy') as 'Date', addedby as 'Added By' from event_funding_log order by date desc;", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            BindExpenseLog();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            BindFundingLog();
        }
    }
}
