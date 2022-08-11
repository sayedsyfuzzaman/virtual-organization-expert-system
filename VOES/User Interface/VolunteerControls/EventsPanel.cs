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
    public partial class EventsPanel : UserControl
    {
        public EventsPanel()
        {
            InitializeComponent();
        }

        internal void loadData()
        {
            allEvents1.LoadData();
            allEvents1.Show();
        }
    }
}
