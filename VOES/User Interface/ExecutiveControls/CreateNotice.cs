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
    public partial class CreateNotice : UserControl
    {
        public CreateNotice()
        {
            InitializeComponent();
        }

        public void resetlabel()
        {
            textBox1.Clear();
            richTextBox1.Clear();
            errorProvider1.Clear();
            errorProvider2.Clear();
        }
        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure want to remove this member from this system?","VOES",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                resetlabel();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Subject can not be empty!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text) == true)
            {
                errorProvider2.SetError(this.richTextBox1, "Body can not be empty!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider2.SetError(this.richTextBox1, "Valid");
                errorProvider2.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            if (string.IsNullOrWhiteSpace(textBox1.Text)==false && string.IsNullOrWhiteSpace(richTextBox1.Text)==false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "INSERT INTO notices (subject, body, postedby, postdate)VALUES (@subject, @body, @postedby,getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@subject", textBox1.Text);
                cmd.Parameters.AddWithValue("@body", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@postedby", Dashboard.getMember().Id);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Notice created successfully!", "VOES", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    resetlabel();
                }
                else
                {
                    MessageBox.Show("An error occured to create this notice! Please try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all * fields!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
