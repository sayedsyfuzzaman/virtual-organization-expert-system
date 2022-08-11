using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface
{
    public partial class SystemAdministrator : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public SystemAdministrator()
        {
            InitializeComponent();
            allMembers1.IsSysAdmin = true;
            allMembers1.LoadData();
            allMembers1.BindALLGridView();
            dateTimePicker1.CustomFormat = " ";
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            bunifuPictureBox1.Image.Save(ms, bunifuPictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        private String IdGenerator()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT COUNT(*) AS RowCnt FROM members;";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            bool isNull = false;

            while (dr.Read())
            {
                int RowCnt = dr.GetInt32(0);
                if (RowCnt == 0)
                {
                    isNull = true;
                    break;
                }
            }
            con.Close();


            int initialId = 0;
            if (isNull == false)
            {
                query = "select max(serial) from members";
                cmd = new SqlCommand(query, con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        initialId = dr.GetInt32(0);
                        break;
                    }
                }
                con.Close();
            }


            DateTime dt = DateTime.Today;
            String year = dt.ToString("yyyy");
            int month = dt.Month;
            initialId++;
            String id = year.Substring(2, 2) + "-" + initialId + "-" + month;
            return id;
        }

        private void resetAll()
        {
            imageLocation = null;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            dateTimePicker1.CustomFormat = " ";
            richTextBox1.Text = null;
            bunifuPictureBox1.ImageLocation = "";
            bunifuPictureBox1.Image = null;
            imageLocation = null;
        }
        
        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true || string.IsNullOrWhiteSpace(textBox2.Text) == true || string.IsNullOrWhiteSpace(textBox3.Text) == true || string.IsNullOrWhiteSpace(textBox4.Text) == true || string.IsNullOrWhiteSpace(textBox5.Text) == true || string.IsNullOrWhiteSpace(textBox6.Text) == true || string.IsNullOrWhiteSpace(comboBox1.Text) == true || bunifuPictureBox1.ImageLocation == null || string.IsNullOrWhiteSpace(dateTimePicker1.Text) == true)
            {
                MessageBox.Show("Sorry! Please enter informations first!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "INSERT INTO members (id,name,phone,email,designation,address,dob,bloodgroup,usertype,username,pass,image,addedby,additiondate)VALUES (@id,@name,@phone,@email,@designation,@address,@dob,@bloodgroup,@usertype,@username,@pass,@image,@addedby,getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                String id = IdGenerator();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@designation", textBox3.Text);
                cmd.Parameters.AddWithValue("@address", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@bloodgroup", comboBox2.Text);
                cmd.Parameters.AddWithValue("@usertype", comboBox1.Text);
                cmd.Parameters.AddWithValue("@username", textBox5.Text);
                cmd.Parameters.AddWithValue("@pass", textBox6.Text);
                cmd.Parameters.AddWithValue("@image", SavePhoto());
                cmd.Parameters.AddWithValue("@addedby", "System Administrator");

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Member Added Successfully!\nMembers Id : " + id, "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetAll();
                    allMembers1.LoadData();
                    allMembers1.BindALLGridView();
                    dateTimePicker1.CustomFormat = " ";
                }
                else
                {
                    MessageBox.Show("An error occured to add this member!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }
        String imageLocation = "";
        private void bunifuButton1_Click_1(object sender, EventArgs e)
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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
        }
    }
}
