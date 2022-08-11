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
using System.IO;
using VOES.Controllers;
using System.Collections;
using System.Configuration;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class AllMembers : UserControl
    {
        private ArrayList memberarr = new ArrayList();
        bool isSysAdmin = false;

        public bool IsSysAdmin
        {
            get { return isSysAdmin; }
            set { isSysAdmin = value; }
        }


        public AllMembers()
        {
            InitializeComponent();
            selectedMemberDetails1.Hide();
        }
        

        internal void LoadData()
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
        }

        internal void BindALLGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (Member allMember in memberarr)
            {
                dataGridView1.Rows.Add(new Object[]{
                    allMember.Id,
                    allMember.Name,
                    allMember.Phone,
                    allMember.Designation
                });
            }
        }

        private void BindExecutiveGridView()
        {
            foreach (Member allMember in memberarr)
            {
                if (allMember.Usertype == "Executive" || allMember.Usertype == "President")
                {
                    dataGridView1.Rows.Add(new Object[]{
                    allMember.Id,
                    allMember.Name,
                    allMember.Phone,
                    allMember.Designation
                    });
                }
            }
        }

        private void BindVolunteerGridView()
        {
            foreach (Member allMember in memberarr)
            {
                if (allMember.Usertype == "Volunteer")
                {
                    dataGridView1.Rows.Add(new Object[]{
                    allMember.Id,
                    allMember.Name,
                    allMember.Phone,
                    allMember.Designation
                    
                    });
                }
            }
        }

        private void SearchMember(String id)
        {
            foreach (Member allMember in memberarr)
            {
                if (allMember.Id == id)
                {
                    dataGridView1.Rows.Add(new Object[]{
                    allMember.Id,
                    allMember.Name,
                    allMember.Phone,
                    allMember.Designation
                    });
                }
            }
        }

        private String GetUsertype(String id)
        {
            foreach (Member allMember in memberarr)
            {
                if (allMember.Id.Equals(id))
                {
                    return allMember.Usertype;
                }
            }
            return null;
        }


        private void deleteMember(String id)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from members where id=@id and id!=@user";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@user", Dashboard.getMember().Id);
            con.Open();
            int a = cmd.ExecuteNonQuery();//0 1
            if (a >= 0)
            {
                if (id != Dashboard.getMember().Id)
                {
                    MessageBox.Show("Member removed Successfully!", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();
                    memberarr.Clear();
                    LoadData();
                    BindALLGridView();
                }
                else
                {
                    MessageBox.Show("You cannot remove your account! ", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("An error occured to remove member at this moment!\nPlease try again.", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if(dataGridView1.Columns[e.ColumnIndex].Name == "Action")
            {
                if(MessageBox.Show("Are you sure want to remove this member from this system?","VOES",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    if (GetUsertype(Dashboard.getMember().Id).Equals("President") == true || isSysAdmin == true)
                    {
                        deleteMember(id);
                    }
                    else if (GetUsertype(Dashboard.getMember().Id).Equals("Executive") == true)
                    {
                        if (GetUsertype(id).Equals("Volunteer") == true)
                        {
                            deleteMember(id);
                        }
                        else
                        {
                            MessageBox.Show("You cannot remove any Executive or System Administrator", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot remove any member", "VOES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Details")
            {
                String id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Member member = new Member();
                foreach (Member allMember in memberarr)
                {
                    if (allMember.Id == id)
                    {
                        member = allMember;
                        SelectedMember = member;
                        break;
                    }
                }
                selectedMemberDetails1.loadData(SelectedMember);
                selectedMemberDetails1.Show();
            }
        }

        internal static Member SelectedMember { get; set; }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            memberarr.Clear();
            LoadData();
            BindALLGridView();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            memberarr.Clear();
            LoadData();
            BindExecutiveGridView();
        }

        private void AllMembers_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            memberarr.Clear();
            LoadData();
            BindVolunteerGridView();
    
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (memberarr.Count > 0)
            {
                SearchMember(bunifuTextBox1.Text.ToString());
            }
            else
            {
                LoadData();
                SearchMember(bunifuTextBox1.Text.ToString());
            }
        }


    }
}
