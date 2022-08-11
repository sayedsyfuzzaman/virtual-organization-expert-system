using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface.ExecutiveControls
{
    public partial class NoticePanel : UserControl
    {
        public NoticePanel()
        {
            InitializeComponent();
        }

        internal void loadNotice()
        {
            notices1.loadData();
            notices1.BindALLGridView();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            createNotice1.Show();
            notices1.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            loadNotice();
            notices1.Show();
            createNotice1.Hide();
        }
    }
}
