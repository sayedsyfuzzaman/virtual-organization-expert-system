using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using VOES.Controllers;
using System.Collections;
using System.IO;

namespace VOES.User_Interface
{
    public partial class AllEvents : UserControl
    {
        private ArrayList eventarr = new ArrayList();
        public AllEvents()
        {
            InitializeComponent();
        }
        DataTable data = new DataTable();
        internal void LoadData()
        {
            
            eventarr.Clear();
            DisableEdit();
            if (Dashboard.getMember().Usertype == "Volunteer")
            {
                comboBox1.Enabled = false;
                bunifuButton8.Enabled = false;
                bunifuButton1.Hide();
                bunifuButton2.Hide();
                bunifuButton3.Hide();
                bunifuButton7.Hide();
            }
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from events order by serial desc";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter sda = new SqlDataAdapter("select event_name as 'Event Name' from events where status = 'Ongoing' order by serial desc", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Event events = new Event();
                events.EventName = dr.GetValue(1).ToString();
                events.Image = (byte[])dr.GetValue(2);
                events.StartDate = dr.GetValue(3).ToString();
                events.EndDate = dr.GetValue(4).ToString();
                events.Description = dr.GetValue(5).ToString();
                events.Location = dr.GetValue(6).ToString();
                events.Status = dr.GetValue(7).ToString();
                events.Addedby = dr.GetValue(8).ToString();
                events.AdditionDate = dr.GetValue(9).ToString();
                eventarr.Add(events);
            }
            con.Close();
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }


        internal void BindArchiveGridView()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter("select event_name as 'Event Name' from events where status = 'Archive' order by serial desc", con);
            data.Clear();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void DisableEdit()
        {
            bunifuButton2.Show();
            bunifuButton7.Show();
            bunifuButton3.Hide();
            bunifuButton1.Hide();
            richTextBox1.ReadOnly = true;
            richTextBox1.BorderStyle = BorderStyle.None;
        }


        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(eventarr.Count <= 0)
            {
                MessageBox.Show("No event found! please create event!");
            }
            else
            {
                String name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                foreach (Event allEvent in eventarr)
                {
                    if (allEvent.EventName.Equals(name))
                    {
                        comboBox1.Text = allEvent.Status;
                        if(allEvent.Status.Equals("Archive"))
                        {
                            comboBox1.Enabled = false;
                            bunifuButton8.Enabled = false;
                        }
                        else
                        {
                            comboBox1.Enabled = true;
                            bunifuButton8.Enabled = true;
                        }
                        pictureBox1.Image = GetPhoto(allEvent.Image);
                        bunifuLabel1.Text = allEvent.EventName;
                        bunifuLabel4.Text = allEvent.StartDate;
                        bunifuLabel5.Text = allEvent.EndDate;
                        bunifuLabel7.Text = allEvent.Location;
                        bunifuLabel9.Text = allEvent.Addedby;
                        richTextBox1.Text = allEvent.Description;
                        break;
                    }
                }
            }
            
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuButton2.Hide();
            bunifuButton7.Hide();
            bunifuButton3.Show();
            bunifuButton1.Show();
            richTextBox1.ReadOnly = false;
            richTextBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            BindArchiveGridView();
        }

        private void resetLabel()
        {
            pictureBox1.Image = null;
            bunifuLabel1.Text = "Double click on a row to see details";
            bunifuLabel4.Text = "";
            bunifuLabel5.Text = "";
            bunifuLabel7.Text = "";
            bunifuLabel9.Text = "";
            richTextBox1.Clear();

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to remove this notice?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                String name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                SqlConnection con = new SqlConnection(cs);
                string query = "delete from events where event_name = @name";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
                    MessageBox.Show("Data Deleted Successfully ! ");
                    eventarr.Clear();
                    resetLabel();
                    LoadData();
                    //BindALLGridView();
                }
                else
                {
                    MessageBox.Show("Data not Deleted ! ");
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to edit this notice?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                String name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                SqlConnection con = new SqlConnection(cs);


                string query = "update events set description = @description where event_name = @name";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@description", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@name", name);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a >= 0)
                {
                    MessageBox.Show("Data updated Successfully ! ");
                    
                    eventarr.Clear();
                    LoadData();
                    //BindALLGridView();
                }
                else
                {
                    MessageBox.Show("Data not Updated ! ");
                }
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DisableEdit();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[Event Name] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to change?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (comboBox1.Text == "Archive")
                {
                    string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                    String name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    SqlConnection con = new SqlConnection(cs);
                    string query = "update events set status = 'Archive', end_date = getdate() where event_name = @name;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", name);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();//0 1
                    if (a >= 0)
                    {
                        eventarr.Clear();
                        resetLabel();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Data not updated ! ");
                    }
                }

            }
        }
    }
}
