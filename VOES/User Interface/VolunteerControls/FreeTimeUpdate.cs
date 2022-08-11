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

namespace VOES.User_Interface.VolunteerControls
{
    public partial class FreeTimeUpdate : UserControl
    {
        bool isNull = false;
        public FreeTimeUpdate()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            AutoFillTextbox();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT COUNT(*) AS RowCnt FROM availability where id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Dashboard.getMember().Id);
             isNull = false;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

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
                //get free time
                con = new SqlConnection(cs);
                query = "SELECT * from availability where id = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Dashboard.getMember().Id);

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    textBox1.Text = dr.GetValue(1).ToString();
                    textBox2.Text = dr.GetValue(2).ToString();
                    textBox3.Text = dr.GetValue(3).ToString();
                    textBox4.Text = dr.GetValue(4).ToString();
                    textBox5.Text = dr.GetValue(5).ToString();
                    textBox6.Text = dr.GetValue(6).ToString();
                    textBox7.Text = dr.GetValue(7).ToString();
                    textBox8.Text = dr.GetValue(8).ToString();
                    textBox9.Text = dr.GetValue(9).ToString();
                    textBox10.Text = dr.GetValue(10).ToString();
                    textBox11.Text = dr.GetValue(11).ToString();
                    textBox12.Text = dr.GetValue(12).ToString();
                    textBox13.Text = dr.GetValue(13).ToString();
                    textBox14.Text = dr.GetValue(14).ToString();
                }
                con.Close();
            }

        }

        private void AutoFillTextbox()
        {
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();

            autotext.Add("1:00 AM");
            autotext.Add("2:00 AM");
            autotext.Add("3:00 AM");
            autotext.Add("4:00 AM");
            autotext.Add("5:00 AM");
            autotext.Add("6:00 AM");
            autotext.Add("7:00 AM");
            autotext.Add("8:00 AM");
            autotext.Add("9:00 AM");
            autotext.Add("10:00 AM");
            autotext.Add("11:00 AM");
            autotext.Add("12:00 AM");


            autotext.Add("1:00 PM");
            autotext.Add("2:00 PM");
            autotext.Add("3:00 PM");
            autotext.Add("4:00 PM");
            autotext.Add("5:00 PM");
            autotext.Add("6:00 PM");
            autotext.Add("7:00 PM");
            autotext.Add("8:00 PM");
            autotext.Add("9:00 PM");
            autotext.Add("10:00 PM");
            autotext.Add("11:00 PM");
            autotext.Add("12:00 PM");

            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = autotext;

            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = autotext;

            textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = autotext;


            textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = autotext;

            textBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox5.AutoCompleteCustomSource = autotext;

            textBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox6.AutoCompleteCustomSource = autotext;

            textBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = autotext;

            textBox8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox8.AutoCompleteCustomSource = autotext;

            textBox9.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox9.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox9.AutoCompleteCustomSource = autotext;

            textBox10.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox10.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox10.AutoCompleteCustomSource = autotext;


            textBox11.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox11.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox11.AutoCompleteCustomSource = autotext;

            textBox12.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox12.AutoCompleteCustomSource = autotext;

            textBox13.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox13.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox13.AutoCompleteCustomSource = autotext;

            textBox14.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox14.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox14.AutoCompleteCustomSource = autotext;
        }


        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            String query = "INSERT INTO availability VALUES(@id, @from1,@to1, @from2,@to2,@from3,@to3, @from4,@to4, @from5,@to5,@from6,@to6,@from7,@to7);";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Dashboard.getMember().Id);

            cmd.Parameters.AddWithValue("@from1", textBox1.Text);
            cmd.Parameters.AddWithValue("@to1", textBox2.Text);

            cmd.Parameters.AddWithValue("@from2", textBox3.Text);
            cmd.Parameters.AddWithValue("@to2", textBox4.Text);

            cmd.Parameters.AddWithValue("@from3", textBox5.Text);
            cmd.Parameters.AddWithValue("@to3", textBox6.Text);

            cmd.Parameters.AddWithValue("@from4", textBox7.Text);
            cmd.Parameters.AddWithValue("@to4", textBox8.Text);

            cmd.Parameters.AddWithValue("@from5", textBox9.Text);
            cmd.Parameters.AddWithValue("@to5", textBox10.Text);

            cmd.Parameters.AddWithValue("@from6", textBox11.Text);
            cmd.Parameters.AddWithValue("@to6", textBox12.Text);

            cmd.Parameters.AddWithValue("@from7", textBox13.Text);
            cmd.Parameters.AddWithValue("@to7", textBox14.Text);

            if (isNull == true)
            {
                bunifuButton2.Hide();

                con.Open();
                int b = cmd.ExecuteNonQuery();//0 1
                con.Close();
                if (b > 0)
                {
                    MessageBox.Show("Free time updated!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("An error occured", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                String query1 = "delete from availability where id = @id;";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@id", Dashboard.getMember().Id);

                con.Open();
                int x = cmd1.ExecuteNonQuery();//0 1
                con.Close();
                if (x > 0)
                {
                    con.Open();
                    int b = cmd.ExecuteNonQuery();//0 1
                    con.Close();
                    if (b > 0)
                    {
                        MessageBox.Show("Free time updated!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("An error occured", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry!! Cant update your data!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
