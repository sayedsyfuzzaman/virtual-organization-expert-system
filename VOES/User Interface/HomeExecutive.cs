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
using System.IO;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class HomeExecutive : UserControl
    {
        public HomeExecutive()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            //get total number of member 
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT COUNT(*) AS memberCount FROM members";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                label2.Text = dr.GetInt32(0).ToString();
            }
            con.Close();


            //get total number of event 
            con = new SqlConnection(cs);
            query = "SELECT COUNT(*) AS eventCount FROM events";
            cmd = new SqlCommand(query, con);

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                label4.Text = dr.GetInt32(0).ToString();
            }
            con.Close();


            //get profile
            con = new SqlConnection(cs);
            query = "select name, phone, email, dob, bloodgroup, username, pass, address, image from members where id = @id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",Dashboard.getMember().Id);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                textBox1.Text = dr.GetValue(0).ToString();
                textBox2.Text = dr.GetValue(1).ToString();
                textBox3.Text = dr.GetValue(2).ToString();
                dateTimePicker1.Text = dr.GetValue(3).ToString();
                comboBox2.Text = dr.GetValue(4).ToString();
                textBox5.Text = dr.GetValue(5).ToString();
                textBox6.Text = dr.GetValue(6).ToString();
                richTextBox1.Text = dr.GetValue(7).ToString();
                bunifuPictureBox1.Image = GetPhoto((byte[])dr.GetValue(8));
            }
            con.Close();
        }


        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            bunifuPictureBox1.Image.Save(ms, bunifuPictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

            if (string.IsNullOrWhiteSpace(textBox1.Text) == true || string.IsNullOrWhiteSpace(textBox2.Text) == true || string.IsNullOrWhiteSpace(textBox3.Text) == true || string.IsNullOrWhiteSpace(textBox5.Text) == true || string.IsNullOrWhiteSpace(textBox6.Text) == true || bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Please fill all * field!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "update members set name = @name, phone = @phone, email = @email, dob = @dob, bloodgroup = @bloodgroup, username = @username, pass = @pass, address = @address, image = @image where id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Dashboard.getMember().Id);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@bloodgroup", comboBox2.Text);
                cmd.Parameters.AddWithValue("@username", textBox5.Text);
                cmd.Parameters.AddWithValue("@pass", textBox6.Text);
                cmd.Parameters.AddWithValue("@image", SavePhoto());
                cmd.Parameters.AddWithValue("@address", richTextBox1.Text);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Profile Updated!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                }
                else
                {
                    MessageBox.Show("An error occured to update profile!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        String imageLocation = "";
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image File (*.png;*.jpg) | *.png; *.jpg";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    bunifuPictureBox1.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured!!", "VOES", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            bool status = bunifuCheckBox1.Checked;
            switch (status)
            {
                case true:
                    textBox6.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox6.UseSystemPasswordChar = true;
                    break;
            }
        }


    }
}
