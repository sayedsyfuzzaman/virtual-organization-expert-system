using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOES.Controllers;
using System.IO;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class SelectedMemberDetails : UserControl
    {
        public SelectedMemberDetails()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            this.Hide();
            resetLabel();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        internal void loadData(Member member)
        {
            resetLabel();
            bunifuPictureBox1.Image = GetPhoto(member.Image);
            idLabel.Text = member.Id;
            NameLabel.Text = member.Name;
            MobileLabel.Text = member.Phone;
            EmailLabel.Text = member.Email;
            DesignationLabel.Text = member.Designation;
            UserTypeLabel.Text = member.Usertype;
            DOBLabel.Text = member.Dob;
            BGLabel.Text = member.Bloodgroup;
            AddressLabel.Text = member.Address;
            addedbyLabel.Text = member.Addedby;
            additiondateLabel.Text = member.Additiondate;
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void resetLabel()
        {
            bunifuPictureBox1.Image = null;
            idLabel.Text = "";
            NameLabel.Text = "";
            MobileLabel.Text = "";
            EmailLabel.Text = "";
            DesignationLabel.Text = "";
            UserTypeLabel.Text = "";
            DOBLabel.Text = "";
            BGLabel.Text = "";
            AddressLabel.Text = "";
            addedbyLabel.Text = "";
            additiondateLabel.Text = "";
        }
    }
}
