using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VOES.User_Interface.VolunteerControls
{
    public partial class NoticeSection : UserControl
    {
        public NoticeSection()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            notices1.loadData();
            notices1.BindALLGridView();
            notices1.Show();
        }
    }
}
