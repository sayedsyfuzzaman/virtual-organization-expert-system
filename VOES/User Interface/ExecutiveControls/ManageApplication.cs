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

namespace VOES.ExecutiveControls
{
    public partial class ManageApplication : UserControl
    {
        public ManageApplication()
        {
            InitializeComponent();
            resetAll();
        }

        DataTable data = new DataTable();
        internal void BindGridView()
        { 
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter("select FORMAT (senddate, 'dd MMMM,yy') as 'Date', subject as 'Subject' from applications order by senddate desc", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void resetLabel()
        {
            subjectLabel.Text = null;
            id_label.Text = null;
            senddateLabel.Text = null;
            repliedbyLabel.Text = null;
            replydateLabel.Text = null;
            richTextBox1.Text = null;
            richTextBox2.Text = null;
            richTextBox3.Text = null;

        }
        private void resetAll()
        {
            subjectLabel.Hide();
            id_label.Hide();
            senddateLabel.Hide();
            repliedbyLabel.Hide();
            replydateLabel.Hide();
            richTextBox1.Hide();
            richTextBox2.Hide();
            richTextBox3.Hide();
            bunifuImageButton1.Hide();
            bunifuLabel6.Hide();
            bunifuLabel10.Hide();
        }

        String date;
        String subject;
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            resetLabel();
            date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            subject = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

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
                if (string.IsNullOrWhiteSpace(dr.GetValue(5).ToString()))
                {
                    repliedbyLabel.Hide();
                    replydateLabel.Hide();
                    richTextBox2.Hide();

                    richTextBox3.Show();
                    bunifuLabel10.Show();
                    bunifuLabel6.Show();
                    bunifuImageButton1.Show();
                    subjectLabel.Show();
                    id_label.Show();
                    senddateLabel.Show();
                    richTextBox1.Show();

                    subjectLabel.Text = dr.GetValue(1).ToString();
                    id_label.Text = dr.GetValue(3).ToString();
                    senddateLabel.Text = dr.GetValue(4).ToString();
                    richTextBox1.Text = dr.GetValue(2).ToString();
                }
                else
                {

                    bunifuImageButton1.Hide();
                    richTextBox3.Hide();

                    subjectLabel.Text = dr.GetValue(1).ToString();
                    id_label.Text = dr.GetValue(3).ToString();
                    senddateLabel.Text = dr.GetValue(4).ToString();

                    repliedbyLabel.Show();
                    replydateLabel.Show();
                    richTextBox1.Show();
                    richTextBox2.Show();
                    subjectLabel.Show();
                    id_label.Show();
                    senddateLabel.Show();
                    bunifuLabel10.Show();
                    bunifuLabel6.Show();

                    replydateLabel.Text = dr.GetValue(7).ToString();
                    repliedbyLabel.Text = dr.GetValue(6).ToString();
                    richTextBox1.Text = dr.GetValue(2).ToString();
                    richTextBox2.Text = dr.GetValue(5).ToString();
                }
            }
            con.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(id_label.Text) == false && string.IsNullOrWhiteSpace(senddateLabel.Text) == false && string.IsNullOrWhiteSpace(subjectLabel.Text) == false)
            {
                if (MessageBox.Show("After sending message, It can not be undone. \nAre you sure want to send this message?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                    SqlConnection con = new SqlConnection(cs);
                    string query = "update applications set reply = @reply, replydate = getdate(), repliedby = @repliedby where subject = @subject and sendedby = @sendedby";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@senddate", senddateLabel.Text);
                    cmd.Parameters.AddWithValue("@subject", subjectLabel.Text);
                    cmd.Parameters.AddWithValue("@sendedby", id_label.Text);
                    cmd.Parameters.AddWithValue("@reply", richTextBox3.Text);
                    cmd.Parameters.AddWithValue("@repliedby", Dashboard.getMember().Id);

                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    con.Close();

                    if (a > 0)
                    {
                        BindGridView();
                        resetAll();
                        MessageBox.Show("Message Sent!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("An Error Occured to Send Message!\n Try Again Later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }

            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[Subject] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }
    }
}