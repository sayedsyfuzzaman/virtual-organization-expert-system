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
    public partial class Notices : UserControl
    {
        private ArrayList noticearr = new ArrayList();
        public Notices()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            DisableEdit();
            if (Dashboard.getMember().Usertype == "Volunteer")
            {
                bunifuButton1.Hide();
                bunifuButton2.Hide();
                bunifuButton3.Hide();
                bunifuButton4.Hide();
            }
            noticearr.Clear();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select serial, subject, body, postedby,  FORMAT (postdate, 'dd MMMM,yy') as date from notices order by postdate desc;";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Notice notice = new Notice();
                notice.Serial = dr.GetValue(0).ToString();
                notice.Subject = dr.GetValue(1).ToString();
                notice.Body = dr.GetValue(2).ToString();
                notice.Postedby = dr.GetValue(3).ToString();
                notice.Postdate = dr.GetValue(4).ToString();

                noticearr.Add(notice);
            }
            con.Close();
        }


        internal void BindALLGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (Notice allNotice in noticearr)
            {
                dataGridView1.Rows.Add(new Object[]{
                    allNotice.Postdate,
                    allNotice.Subject
                });
            }
        }



        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DisableEdit();
            if (Dashboard.getMember().Usertype == "Volunteer")
            {
                bunifuButton1.Hide();
                bunifuButton2.Hide();
                bunifuButton3.Hide();
                bunifuButton4.Hide();
            }
            String date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String subject = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            foreach (Notice allNotice in noticearr)
            {
                if (allNotice.Subject.Equals(subject) && allNotice.Postdate.Equals(date))
                {
                    bunifuLabel1.Text = allNotice.Subject;
                    bunifuLabel4.Text = allNotice.Postedby;
                    bunifuLabel5.Text = allNotice.Postdate;
                    richTextBox1.Text = allNotice.Body;
                    break;
                }
            }
        }

        private void resetLabel()
        {
            bunifuLabel1.Text = "Title";
            bunifuLabel4.Text = "ID";
            bunifuLabel5.Text = "dd-MMMM";
            richTextBox1.Clear();

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to remove this notice?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                String date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                String subject = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                SqlConnection con = new SqlConnection(cs);
                string query = "delete from notices where FORMAT (postdate, 'dd MMMM,yy')  = @postdate and subject = @subject";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@postdate", date);
                cmd.Parameters.AddWithValue("@subject", subject);
                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                con.Close();
                if (a > 0)
                {
                    MessageBox.Show("Notice Deleted Successfully!","VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();
                    noticearr.Clear();
                    resetLabel();
                    loadData();
                    BindALLGridView();
                }
                else
                {
                    MessageBox.Show("An error occured to delete this notice!\nPlease try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisableEdit()
        {
            bunifuButton2.Show();
            bunifuButton1.Show();
            bunifuButton3.Hide();
            bunifuButton4.Hide();
            richTextBox1.ReadOnly = true;
            richTextBox1.BorderStyle = BorderStyle.None;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifuButton2.Hide();
            bunifuButton1.Hide();
            bunifuButton3.Show();
            bunifuButton4.Show();
            richTextBox1.ReadOnly = false;
            richTextBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DisableEdit();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to edit this notice?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                String date = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                String subject = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                String body = richTextBox1.Text;

                SqlConnection con = new SqlConnection(cs);


                string query = "update notices set body = @body where FORMAT (postdate, 'dd-MMMM') = @postdate and subject = @subject";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@postdate", date);
                cmd.Parameters.AddWithValue("@subject", subject);
                cmd.Parameters.AddWithValue("@body", body);
                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
                    MessageBox.Show("Notice Updated Successfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();
                    noticearr.Clear();
                    DisableEdit();
                    loadData();
                    BindALLGridView();
                }
                else
                {
                    MessageBox.Show("An error occured to update this notice!\nPlease try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
