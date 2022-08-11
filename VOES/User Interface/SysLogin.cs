using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface
{
    public partial class SysLogin : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public SysLogin()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Enter your username please!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Enter your username please!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bool status = bunifuCheckBox1.Checked;
            switch (status)
            {
                case true:
                    textBox1.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox1.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == false && string.IsNullOrWhiteSpace(textBox2.Text) == false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select count(*) as rowCnt from system_administrator where private_property = @pp and nid_no = @nid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pp", textBox1.Text);
                cmd.Parameters.AddWithValue("@nid", textBox2.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetInt32(0) > 0)
                    {
                        SystemAdministrator sys = new SystemAdministrator();
                        sys.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid information", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Please fillup all fields", "Failed to login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
