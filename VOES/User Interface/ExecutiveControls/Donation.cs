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
using VOES.Controllers;
using System.Collections;
using VOES.Utils;

namespace VOES.ExecutiveControls
{
    public partial class Donation : UserControl
    {
        private ArrayList memberarr = new ArrayList();
        public Donation()
        {
            InitializeComponent();
            
        }

        internal void load()
        {
            memberarr.Clear();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from members order by serial desc";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Member member = new Member();
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
                memberarr.Add(member);
            }
            con.Close();

            AutoFillTextbox();
            autoFillPhone();
            
        }

        private void AutoFillTextbox()
        {
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
            foreach (Member allMember in memberarr)
            {
                autotext.Add(allMember.Id);
            }
            bunifuTextBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            bunifuTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            bunifuTextBox1.AutoCompleteCustomSource = autotext;
        }

        private Boolean isMember(String Id)
        {
            foreach (Member allMember in memberarr)
            {
                if (allMember.Id.Equals(Id))
                {
                    return true;
                }
                
            }
            return false;
        }

        private void resetInternalFields()
        {
            bunifuTextBox1.Text = null;
            bunifuTextBox2.Text = null;
            comboBox2.SelectedItem = null;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();

        }

        private void resetExternalFields()
        {
            bunifuTextBox3.Text = null;
            bunifuTextBox4.Text = null;
            bunifuTextBox5.Text = null;
            bunifuTextBox6.Text = null;
            bunifuTextBox7.Text = null;
            richTextBox1.Text = null;
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            if (string.IsNullOrWhiteSpace(bunifuTextBox1.Text) == false && isMember(bunifuTextBox1.Text) == true && Validation.isDouble(bunifuTextBox2.Text) == true && string.IsNullOrWhiteSpace(comboBox2.Text) == false)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT COUNT(*) AS RowCnt FROM members_financial_info where members_id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", bunifuTextBox1.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                bool isNull = false;
                double prevBalance = 0d;


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
                    query = "SELECT balance FROM members_financial_info where members_id = @members_id;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@members_id", bunifuTextBox1.Text);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        prevBalance = Convert.ToDouble(dr.GetDecimal(0));
                    }
                    con.Close();

                    query = "update members_financial_info set balance = @balance where members_id = @members_id;";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@balance", prevBalance + Convert.ToDouble(bunifuTextBox2.Text));
                    cmd.Parameters.AddWithValue("@members_id", bunifuTextBox1.Text);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();//0 1
                    con.Close();
                    if (a > 0)
                    {

                        query = "insert into internal_donation_log(members_id, payment, selected_month, pre_balance, curr_balance, payment_date, recieved_by) values (@id, @payment, @month, @pre_bal, @curr_bal, getdate(), @user);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", bunifuTextBox1.Text);
                        cmd.Parameters.AddWithValue("@payment", bunifuTextBox2.Text);
                        cmd.Parameters.AddWithValue("@month", bunifuTextBox3.Text);
                        cmd.Parameters.AddWithValue("@pre_bal", prevBalance);
                        cmd.Parameters.AddWithValue("@curr_bal", prevBalance + Convert.ToDouble(bunifuTextBox2.Text));
                        cmd.Parameters.AddWithValue("@user", Dashboard.getMember().Id);
                        con.Open();
                        int b = cmd.ExecuteNonQuery();//0 1
                        con.Close();
                        if (b > 0)
                        {
                            MessageBox.Show("Payment Successful!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            resetInternalFields();
                        }
                        else
                        {
                            MessageBox.Show("An error occured to payment! Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("An error occured to payment! Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (isNull == true)
                {
                    query = "insert into members_financial_info (members_id,balance) values (@members_id, @balance);";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@balance", prevBalance + Convert.ToDouble(bunifuTextBox2.Text));
                    cmd.Parameters.AddWithValue("@members_id", bunifuTextBox1.Text);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();//0 1
                    if (a > 0)
                    {
                        con.Close();
                        query = "insert into internal_donation_log(members_id, payment, selected_month, pre_balance, curr_balance, payment_date, recieved_by) values (@id, @payment, @month, @pre_bal, @curr_bal, getdate(), @user);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", bunifuTextBox1.Text);
                        cmd.Parameters.AddWithValue("@payment", bunifuTextBox2.Text);
                        cmd.Parameters.AddWithValue("@month", bunifuTextBox3.Text);
                        cmd.Parameters.AddWithValue("@pre_bal", prevBalance);
                        cmd.Parameters.AddWithValue("@curr_bal", prevBalance + Convert.ToDouble(bunifuTextBox2.Text));
                        cmd.Parameters.AddWithValue("@user", Dashboard.getMember().Id);
                        con.Open();
                        int b = cmd.ExecuteNonQuery();//0 1
                        con.Close();
                        if (b > 0)
                        {
                            MessageBox.Show("Payment Successful!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            resetInternalFields();
                        }
                        else
                        {
                            MessageBox.Show("An error occured to payment! Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("An error occured to payment! Please try again later.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all field with valid data!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void autoFillPhone()
        {
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select phone from external_donation_log";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@phone", bunifuTextBox4.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                autotext.Add(dr.GetValue(0).ToString());
            }
            con.Close();
            bunifuTextBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            bunifuTextBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            bunifuTextBox4.AutoCompleteCustomSource = autotext;
        }
              
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            if (string.IsNullOrWhiteSpace(bunifuTextBox3.Text) == false && string.IsNullOrWhiteSpace(bunifuTextBox4.Text) == false && string.IsNullOrWhiteSpace(bunifuTextBox6.Text) == false && Validation.isDouble(bunifuTextBox6.Text) == true)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "INSERT INTO external_donation_log (donor_name,phone, email, amount, company, comment, date, recievedby) values (@donor_name,@phone, @email, @amount, @company, @comment, getdate(), @recievedby)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@donor_name", bunifuTextBox3.Text);
                cmd.Parameters.AddWithValue("@phone", bunifuTextBox4.Text);
                cmd.Parameters.AddWithValue("@email", bunifuTextBox5.Text);
                cmd.Parameters.AddWithValue("@amount", bunifuTextBox6.Text);
                cmd.Parameters.AddWithValue("@company", bunifuTextBox7.Text);
                cmd.Parameters.AddWithValue("@comment", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@recievedby", Dashboard.getMember().Id);

                con.Open();
                int a = cmd.ExecuteNonQuery();//0 1
                if (a > 0)
                {
                    MessageBox.Show("Donation Successfull!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetExternalFields();
                    autoFillPhone();
                }
                else
                {
                    MessageBox.Show("An error occured to take donation at this moment! \nPlease try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Please fill all * field with valid information!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bunifuTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox1.Text) == true  || isMember(bunifuTextBox1.Text) == false)
            {
                errorProvider1.SetError(this.bunifuTextBox1, "Enter valid id of the donor!");
                errorProvider1.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider1.SetError(this.bunifuTextBox1, "Valid");
                errorProvider1.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox2.Text) == true || Validation.isDouble(bunifuTextBox2.Text) == false)
            {
                errorProvider2.SetError(this.bunifuTextBox2, "Enter valid amount to donate!");
                errorProvider2.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider2.SetError(this.bunifuTextBox2, "Valid");
                errorProvider2.Icon = Properties.Resources.correct_icon;
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox2.Text) == true)
            {
                errorProvider3.SetError(this.comboBox2, "Select month to date!");
                errorProvider3.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider3.SetError(this.comboBox2, "Valid");
                errorProvider3.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to discard?", "VOES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                resetInternalFields();
            }
        }

        private void bunifuTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox3.Text) == true)
            {
                errorProvider4.SetError(this.bunifuTextBox3, "Enter a valid donor name!");
                errorProvider4.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider4.SetError(this.bunifuTextBox3, "Valid");
                errorProvider4.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuTextBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox4.Text) == true)
            {
                errorProvider5.SetError(this.bunifuButton5, "Enter a valid phone number!");
                errorProvider5.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider5.SetError(this.bunifuButton5, "Valid");
                errorProvider5.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuTextBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bunifuTextBox6.Text) == true || Validation.isDouble(bunifuTextBox6.Text) == false)
            {
                errorProvider6.SetError(this.bunifuTextBox6, "Enter valid amount to donate!");
                errorProvider6.Icon = Properties.Resources.error_icon;

            }
            else
            {
                errorProvider6.SetError(this.bunifuTextBox6, "Valid");
                errorProvider6.Icon = Properties.Resources.correct_icon;
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            bunifuTextBox3.Text = null;
            bunifuTextBox5.Text = null;
            bunifuTextBox6.Text = null;
            bunifuTextBox7.Text = null;
            richTextBox1.Text = null;

            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select donor_name, email, company, comment from external_donation_log where date = (select max(date) from external_donation_log where phone = @phone);";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@phone", bunifuTextBox4.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                bunifuTextBox3.Text = dr.GetValue(0).ToString();
                bunifuTextBox5.Text = dr.GetValue(1).ToString();
                bunifuTextBox7.Text = dr.GetValue(2).ToString();
                richTextBox1.Text = dr.GetValue(3).ToString();
            }
            con.Close();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            resetExternalFields();
        }
    }
}
