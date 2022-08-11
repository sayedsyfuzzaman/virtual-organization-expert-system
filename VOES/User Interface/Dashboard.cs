using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOES.Controllers;
using VOES.User_Interface;

namespace VOES
{
    public partial class Dashboard : Form
    {
        private static Member member = new Member();
        public Dashboard(Member m)
        {
            InitializeComponent();
            member = m;
            volunteerMenu1.Hide();
            executiveMenu1.Hide();
            bunifuImageButton2.Image = GetPhoto(member.Image);

            if (member.Usertype == "Executive" || member.Usertype == "President")
            {
                executiveMenu1.loadData();
                executiveMenu1.Show();
            }
            else if(member.Usertype == "Volunteer")
            {
                volunteerMenu1.loadData();
                volunteerMenu1.Show();
            }
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }


        internal static Member getMember()
        {
            return member;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        
    }
}
