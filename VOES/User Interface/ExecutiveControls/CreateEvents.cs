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
    public partial class CreateEvents : UserControl
    {
        public CreateEvents()
        {
            InitializeComponent();
            resetAll();
        }


        private void resetValue()
        {
            NameLabel.Text = " ";
            StartDateLabel.Text = " ";
            locationLabel.Text = " ";
            richTextBox2.Text = " ";
            pictureBox1.Image = null;
            richTextBox2.ReadOnly = true;
        }

        private void resetAll()
        {
            
            textBox1.Text = null;
            textBox2.Text = null;
            richTextBox1.Text = null;
            dateTimePicker1.CustomFormat = " ";
            imageLocation = null;
            resetValue();
            checkBox1.Checked = false;
            readOnly(false);
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();


        }

        private void readOnly(bool Switch)
        {
            if(Switch == true)
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                richTextBox1.ReadOnly = true;
            }
            else
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                richTextBox1.ReadOnly = false;
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
                    pictureBox1.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            switch (status)
            {
                case true:
                    if (string.IsNullOrWhiteSpace(textBox1.Text) == true || string.IsNullOrWhiteSpace(textBox2.Text) == true || pictureBox1.ImageLocation == null)
                    {
                        MessageBox.Show("Please fill all * field!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        checkBox1.Checked = false;
                        if (pictureBox1.ImageLocation == null)
                        {
                            errorProvider4.SetError(this.bunifuButton1, "Attach an image of this image!");
                            errorProvider4.Icon = Properties.Resources.error_icon;

                        }
                        else
                        {
                            errorProvider4.SetError(this.bunifuButton1, "Valid");
                            errorProvider4.Icon = Properties.Resources.correct_icon;
                        }
                    }
                    else
                    {
                        NameLabel.Text = textBox1.Text;
                        StartDateLabel.Text = dateTimePicker1.Text;
                        locationLabel.Text = textBox2.Text;
                        richTextBox2.Text = richTextBox1.Text;
                        richTextBox2.ReadOnly = true;
                        pictureBox1.ImageLocation = imageLocation;
                        readOnly(true);
                    }
                    break;
                default:
                    resetValue();
                    readOnly(false);
                    checkBox1.Checked = false;
                    break;
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameLabel.Text) == false || string.IsNullOrWhiteSpace(StartDateLabel.Text) == false || string.IsNullOrWhiteSpace(locationLabel.Text) == false || pictureBox1.ImageLocation != null)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into events_data (event_name, total_expense, total_fund) values (@event_name, 0.00, 0.00); INSERT INTO events (event_name, picture, start_date, description, location, status, addedby,additiondate)VALUES (@event_name, @picture, @start_date, @description, @location, 'Ongoing', @addedby,getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@event_name", NameLabel.Text);
                cmd.Parameters.AddWithValue("@picture", SavePhoto());
                cmd.Parameters.AddWithValue("@start_date", StartDateLabel.Text);
                cmd.Parameters.AddWithValue("@description", richTextBox2.Text);
                cmd.Parameters.AddWithValue("@location", locationLabel.Text);  
                cmd.Parameters.AddWithValue("@addedby", Dashboard.getMember().Id);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                con.Close();
                if (a > 0)
                {
                    MessageBox.Show("Event Created Successfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetAll();
                }
                else
                {
                    MessageBox.Show("An error occured to create this event, Please try again!","VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Cannot insert without any * field value!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Enter event name please!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dateTimePicker1.Text) == true)
            {
                errorProvider2.SetError(this.dateTimePicker1, "Enter starting date of this event!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider2.SetError(this.dateTimePicker1, "Valid");
                errorProvider2.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) == true)
            {
                errorProvider3.SetError(this.textBox2, "Enter event location please!");
                errorProvider3.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider3.SetError(this.textBox2, "Valid");
                errorProvider3.Icon = Properties.Resources.correct_icon;
            }
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
