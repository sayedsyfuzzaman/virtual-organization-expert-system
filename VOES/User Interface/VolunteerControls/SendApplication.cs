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
    public partial class SendApplication : UserControl
    {
        public SendApplication()
        {
            InitializeComponent();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            if (string.IsNullOrWhiteSpace(textBox1.Text) == false && string.IsNullOrWhiteSpace(richTextBox1.Text) == false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "INSERT INTO applications (subject, body, sendedby, senddate)VALUES (@subject, @body, @sendedby,getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@subject", textBox1.Text);
                cmd.Parameters.AddWithValue("@body", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@sendedby", Dashboard.getMember().Id);


                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Application Sended!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = null;
                    richTextBox1.Text = null;
                }
                else
                {
                    MessageBox.Show("An error occured to send application!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Please fill all field!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            richTextBox1.Text = null;
        }
    }
}
