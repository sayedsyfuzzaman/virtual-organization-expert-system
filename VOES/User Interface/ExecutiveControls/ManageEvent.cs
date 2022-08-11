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
using VOES.Utils;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class ManageEvent : UserControl
    {
        public ManageEvent()
        {
            InitializeComponent();
        }

        internal void fillcombo()
        {
            comboBox1.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select event_name from events where status = 'Ongoing'order by additiondate desc;";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetValue(0).ToString());
            }
            con.Close();
        }

        String committee, activity_report;
        double prevExpense = 0, prevFund = 0;
        private void loadData()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from events_data where event_name = @event_name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prevExpense = Convert.ToDouble(dr.GetDecimal(2));
                prevFund = Convert.ToDouble(dr.GetDecimal(3));
                richTextBox1.Text = dr.GetValue(4).ToString();
                richTextBox2.Text = dr.GetValue(5).ToString();
                
            }
            con.Close();
            committee = richTextBox1.Text;
            activity_report = richTextBox2.Text;
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == true)
            {
                comboBox1.Focus();
                errorProvider5.SetError(this.comboBox1, "Please select an event first.");
                errorProvider5.Icon = Properties.Resources.error_icon;
            }
            else
            {
                errorProvider5.SetError(this.comboBox1, "Valid");
                errorProvider5.Icon = Properties.Resources.correct_icon;
            }
        }



        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == false && string.IsNullOrWhiteSpace(textBox1.Text) == false && Validation.isDouble(textBox1.Text) == true && string.IsNullOrWhiteSpace(textBox2.Text) == false)
            {
                double amount = Convert.ToDouble(textBox1.Text);
                String sector = textBox2.Text;
                double totalExpense = prevExpense + amount;

                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "update events_data set total_expense = @total_expense  where event_name = @event_name; insert into event_expense_log values(@event_name, @amount, @sector, getdate(), @addedby)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@total_expense", totalExpense);
                cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sector", sector);
                cmd.Parameters.AddWithValue("@addedby", Dashboard.getMember().Id);


                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                con.Close();
                if (a >= 0)
                {
                    MessageBox.Show("Expense recorded successfully!", "VOES",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = null;
                    textBox2.Text = null;
                    errorProvider1.Clear();
                    errorProvider2.Clear();
                    loadData();
                }
                else
                {
                    MessageBox.Show("An error occured to record expense info! Please try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Please fill all field with valid information!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) == false && string.IsNullOrWhiteSpace(textBox3.Text) == false && Validation.isDouble(textBox3.Text) == true && string.IsNullOrWhiteSpace(textBox4.Text) == false)
            {
                double amount = Convert.ToDouble(textBox3.Text);
                String sector = textBox4.Text;
                double totalFund = prevFund + amount;

                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "update events_data set total_fund = @total_fund  where event_name = @event_name; insert into event_funding_log values(@event_name, @amount, @sector, getdate(), @addedby) ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@total_fund", totalFund);
                cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@sector", sector);
                cmd.Parameters.AddWithValue("@addedby", Dashboard.getMember().Id);


                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                con.Close();
                if (a >= 0)
                {
                    MessageBox.Show("Fundraise information recorded successfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Text = null;
                    textBox4.Text = null;
                    errorProvider3.Clear();
                    errorProvider4.Clear();
                    loadData();
                }
                else
                {
                    MessageBox.Show("An error occured to record Fundraise info! Please try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all field with valid information!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboBox1.Text) == false)
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT COUNT(*) AS RowCnt FROM events_data where event_name = @event_name";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
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

                if (isNull == false)
                {
                    query = "update events_data set committee = @committee, activity_report = @activity_report  where event_name = @event_name;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@committee", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@activity_report", richTextBox2.Text);
                    cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);


                    con.Open();
                    int a = cmd.ExecuteNonQuery();//0 1
                    con.Close();
                    if (a >= 0)
                    {
                        MessageBox.Show("Informations updated Syccessfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        committee = richTextBox1.Text;
                        activity_report = richTextBox2.Text;
                        errorProvider5.Clear();
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("An error occured to update the information!\n Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (isNull == true)
                {
                    query = "insert into events_data (event_name,committee, activity_report) values (@event_name, @committee, @activity_report);";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@committee", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@activity_report", richTextBox2.Text);


                    con.Open();
                    int a = cmd.ExecuteNonQuery();//0 1
                    con.Close();
                    if (a >= 0)
                    {
                        MessageBox.Show("Informations updated Syccessfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        committee = richTextBox1.Text;
                        activity_report = richTextBox2.Text;
                        errorProvider5.Clear();
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("An error occured to update the information!\n Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all field with valid information!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            errorProvider1.Clear();
            errorProvider2.Clear();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            textBox3.Text = null;
            textBox4.Text = null;
            errorProvider3.Clear();
            errorProvider4.Clear();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            errorProvider5.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            loadData();
        }
        internal void resetcontrol()
        {
            comboBox1.SelectedItem = null;
            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) == true || Validation.isDouble(textBox1.Text) == false)
            {
                errorProvider1.SetError(this.textBox1, "Enter valid amount please!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.textBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) == true)
            {
                errorProvider2.SetError(this.textBox2, "Enter valid expense sector please!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider2.SetError(this.textBox2, "Valid");
                errorProvider2.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) == true || Validation.isDouble(textBox3.Text) == false)
            {
                errorProvider3.SetError(this.textBox3, "Enter valid amount please!");
                errorProvider3.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider3.SetError(this.textBox3, "Valid");
                errorProvider3.Icon = Properties.Resources.correct_icon;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) == true)
            {
                errorProvider4.SetError(this.textBox4, "Enter valid amount please!");
                errorProvider4.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider4.SetError(this.textBox4, "Valid");
                errorProvider4.Icon = Properties.Resources.correct_icon;
            }
        }
    }
}
