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
using VOES.Controllers;

namespace VOES.User_Interface
{
    public partial class Login : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)==false && string.IsNullOrWhiteSpace(textBox2.Text)==false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from members where username = @user and pass = @pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Member member = new Member();
                while (dr.Read())
                {
                    member.Id = dr.GetValue(1).ToString();
                    member.Name = dr.GetValue(2).ToString();
                    member.Phone = dr.GetValue(3).ToString();
                    member.Email = dr.GetValue(4).ToString();
                    member.Designation = dr.GetValue(5).ToString();
                    member.Address = dr.GetValue(6).ToString();
                    member.Dob = dr.GetValue(7).ToString();
                    member.Bloodgroup = dr.GetValue(8).ToString();
                    member.Usertype = dr.GetValue(9).ToString();
                    member.Username = dr.GetValue(10).ToString();
                    member.Pass = dr.GetValue(11).ToString();
                    member.Image = (byte[])dr.GetValue(12);
                    member.Addedby = dr.GetValue(13).ToString();
                    member.Additiondate = dr.GetValue(14).ToString();
                }
                if (dr.HasRows == true)
                {
                    if (member.Usertype == "Executive" || member.Usertype == "President" || member.Usertype == "System Administrator" || member.Usertype == "Volunteer")
                    {
                        Dashboard dashboard = new Dashboard(member);
                        dashboard.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Please enter valid information", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();
            }
            else
            {
                MessageBox.Show("Please fillup all fields", "Failed to login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bool status = bunifuCheckBox1.Checked;
            switch (status)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
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
                errorProvider2.SetError(this.textBox2, "Enter your username please!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            SysLogin syslogin = new SysLogin();
            syslogin.Show();
            this.Hide();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
