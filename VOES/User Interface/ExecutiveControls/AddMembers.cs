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
using VOES.Controllers;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class AddMembers : UserControl
    {
        public AddMembers()
        {
            InitializeComponent();
            resetAll();
        }

        private void resetValue()
        {
            NameLabel.Text = " ";
            MobileLabel.Text = " ";
            EmailLabel.Text = " ";
            DesignationLabel.Text = " ";
            UserTypeLabel.Text = " ";
            DOBLabel.Text = " ";
            BGLabel.Text = " ";
            AddressLabel.Text = " ";
            usernameLabel.Text = " ";
            passwordLabel.Text = " ";
            bunifuPictureBox1.Image = null;
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
            resetValue();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();


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
            catch(Exception)
            {
                MessageBox.Show("An Error Occured!!", "VOES", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    if (string.IsNullOrWhiteSpace(textBox1.Text) == true || string.IsNullOrWhiteSpace(textBox2.Text) == true || string.IsNullOrWhiteSpace(textBox3.Text) == true || string.IsNullOrWhiteSpace(textBox4.Text) == true || string.IsNullOrWhiteSpace(textBox5.Text) == true || string.IsNullOrWhiteSpace(textBox6.Text) == true || string.IsNullOrWhiteSpace(comboBox1.Text) == true || bunifuPictureBox1.ImageLocation == null || string.IsNullOrWhiteSpace(dateTimePicker1.Text) == true )
                    {
                        MessageBox.Show("Please fill all * field!","VOES",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        checkBox1.Checked = false;
                    }
                    else
                    {
                        NameLabel.Text = textBox1.Text;
                        MobileLabel.Text = textBox2.Text;
                        EmailLabel.Text = textBox3.Text;
                        DesignationLabel.Text = textBox4.Text;
                        UserTypeLabel.Text = comboBox1.Text;
                        DOBLabel.Text = dateTimePicker1.Text;
                        BGLabel.Text = comboBox2.Text;
                        AddressLabel.Text = richTextBox1.Text;
                        usernameLabel.Text = textBox5.Text;
                        passwordLabel.Text = textBox6.Text;
                        bunifuPictureBox1.ImageLocation = imageLocation;
                    }
                    
                    break;
                default:
                    checkBox1.Checked = false;
                    resetValue();
                    break;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Enter your name please!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
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


        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            if (string.IsNullOrWhiteSpace(NameLabel.Text) == false || string.IsNullOrWhiteSpace(MobileLabel.Text) == false || string.IsNullOrWhiteSpace(EmailLabel.Text) == false || string.IsNullOrWhiteSpace(DesignationLabel.Text) == false || string.IsNullOrWhiteSpace(UserTypeLabel.Text) == false || string.IsNullOrWhiteSpace(usernameLabel.Text) == false || string.IsNullOrWhiteSpace(passwordLabel.Text) == false || bunifuPictureBox1.ImageLocation != null || string.IsNullOrWhiteSpace(dateTimePicker1.Text) == false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "INSERT INTO members (id,name,phone,email,designation,address,dob,bloodgroup,usertype,username,pass,image,addedby,additiondate)VALUES (@id,@name,@phone,@email,@designation,@address,@dob,@bloodgroup,@usertype,@username,@pass,@image,@addedby,getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                String id = IdGenerator();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", NameLabel.Text);
                cmd.Parameters.AddWithValue("@phone", MobileLabel.Text);
                cmd.Parameters.AddWithValue("@email", EmailLabel.Text);
                cmd.Parameters.AddWithValue("@designation", DesignationLabel.Text);
                cmd.Parameters.AddWithValue("@address", AddressLabel.Text);
                cmd.Parameters.AddWithValue("@dob", DOBLabel.Text);
                cmd.Parameters.AddWithValue("@bloodgroup", BGLabel.Text);
                cmd.Parameters.AddWithValue("@usertype", UserTypeLabel.Text);
                cmd.Parameters.AddWithValue("@username", usernameLabel.Text);
                cmd.Parameters.AddWithValue("@pass", passwordLabel.Text);
                cmd.Parameters.AddWithValue("@image", SavePhoto());
                cmd.Parameters.AddWithValue("@addedby", Dashboard.getMember().Id);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Member Added Successfully!\nMembers Id : " + id,"VOES",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    checkBox1.Checked = false;
                    resetAll();
                }
                else
                {
                    MessageBox.Show("An error occured to add this member!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {

                MessageBox.Show("Sorry! Please enter informations first!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) == true)
            {
                errorProvider2.SetError(this.textBox2, "Enter your mobile no please!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox2, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) == true)
            {
                errorProvider3.SetError(this.textBox3, "Enter your email please!");
                errorProvider3.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox3, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) == true)
            {
                errorProvider4.SetError(this.textBox4, "Enter your designation please!");
                errorProvider4.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox4, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == true)
            {
                errorProvider5.SetError(this.comboBox1, "Select your user type please!");
                errorProvider5.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.comboBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text) == true)
            {
                errorProvider6.SetError(this.textBox5, "Enter username please!");
                errorProvider6.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox5, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) == true)
            {
                errorProvider7.SetError(this.textBox6, "Enter password please!");
                errorProvider7.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox6, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to discard?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                resetAll();
            }
        }
    }
}
