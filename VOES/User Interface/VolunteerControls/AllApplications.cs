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

namespace VOES.User_Interface.VolunteerControls
{
    public partial class AllApplications : UserControl
    {
        public AllApplications()
        {
            InitializeComponent();
            bunifuLabel4.Hide();
            bunifuLabel5.Hide();
            richTextBox2.Hide();
        }
        DataTable data = new DataTable();
        internal void BindGridView()
        {
            
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter("select FORMAT (senddate, 'dd MMMM,yy') as 'Date', subject as 'Subject' from applications where sendedby = '"+Dashboard.getMember().Id+"' order by senddate desc", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String subject = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from applications where FORMAT (senddate, 'dd MMMM,yy')= @senddate and subject = @subject order by senddate desc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@senddate", date);
            cmd.Parameters.AddWithValue("@subject", subject);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if(string.IsNullOrWhiteSpace(dr.GetValue(5).ToString()))
                {
                    bunifuLabel4.Hide();
                    bunifuLabel5.Hide();
                    richTextBox2.Hide();

                    bunifuLabel1.Text = dr.GetValue(1).ToString();
                    bunifuLabel3.Text = dr.GetValue(3).ToString();
                    richTextBox1.Text = dr.GetValue(2).ToString();
                }
                else
                {
                    bunifuLabel1.Text = dr.GetValue(1).ToString();
                    bunifuLabel3.Text = dr.GetValue(3).ToString();
                    richTextBox1.Text = dr.GetValue(2).ToString();

                    bunifuLabel4.Show();
                    bunifuLabel5.Show();
                    richTextBox2.Show();

                    bunifuLabel5.Text = dr.GetValue(7).ToString();
                    bunifuLabel4.Text = dr.GetValue(6).ToString();
                    richTextBox2.Text = dr.GetValue(5).ToString();
                }
            }
            con.Close();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[Subject] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
    }
}
