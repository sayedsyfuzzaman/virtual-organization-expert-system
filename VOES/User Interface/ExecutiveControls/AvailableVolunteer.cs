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
    public partial class AvailableVolunteer : UserControl
    {
        public AvailableVolunteer()
        {
            InitializeComponent();
        }

        DataTable data = new DataTable();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "Saturday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_sat as 'From', a.to_sat as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Sunday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_sun as 'From', a.to_sun as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Monday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_mon as 'From', a.to_mon as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Tuesday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_tue as 'From', a.to_tue as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Wednesday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_wed as 'From', a.to_wed as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Thursday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_thu as 'From', a.to_thu as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
            else if (comboBox1.SelectedItem.ToString() == "Friday")
            {
                data.Clear();
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "select a.id as 'ID', m.name as 'Name', a.from_fri as 'From', a.to_fri as 'To' from availability a, members m where a.id = m.id;";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                con.Open();
                data.Clear();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                con.Close();
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[From] LIKE '%{0}%'", bunifuTextBox1.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = data.DefaultView;
            dv.RowFilter = string.Format("[To] LIKE '%{0}%'", bunifuTextBox2.Text);
            dataGridView1.DataSource = dv.ToTable();
        }


    }
}
