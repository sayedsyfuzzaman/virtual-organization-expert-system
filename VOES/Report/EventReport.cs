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
using Microsoft.Reporting.WinForms;

namespace VOES.Report
{
    public partial class EventReport : UserControl
    {
        public EventReport()
        {
            InitializeComponent();
        }

        internal void fillcombo()
        {
            comboBox1.Items.Clear();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "select event_name from events order by additiondate desc;";
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetValue(0).ToString());
            }
            con.Close();
            comboBox1.Focus();
        }

        private DataTable EventInfo()
        {
            DataTable dt = new DataTable();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            String query = "select ed.event_name, e.start_date, e.end_date, e.location, e.status, ed.total_expense, ed.total_fund, ed.committee from events e, events_data ed where e.event_name = ed.event_name and ed.event_name = @event_name;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);
            con.Close();
            return dt;
        }


        private DataTable ExpenseLog()
        {
            DataTable dt = new DataTable();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            String query = "select FORMAT (date, 'MMM dd-yyyy') as date, amount, sector, addedby from event_expense_log where event_name = @event_name;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);
            con.Close();
            return dt;
        }

        private DataTable FundingLog()
        {
            DataTable dt = new DataTable();
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            String query = "select FORMAT (date, 'MMM dd-yyyy') as date, amount, sector, addedby from event_funding_log where event_name = @event_name;";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@event_name", comboBox1.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);
            con.Close();
            return dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportDataSource evventinfo = new ReportDataSource("DataSet_EventInfo", EventInfo());
            ReportDataSource expenselog = new ReportDataSource("DataSet_ExpenseLog", ExpenseLog());
            ReportDataSource fundinglog = new ReportDataSource("DataSet_FundingLog", FundingLog());

            
            reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(evventinfo);
            reportViewer1.LocalReport.DataSources.Add(expenselog);
            reportViewer1.LocalReport.DataSources.Add(fundinglog);
            reportViewer1.RefreshReport();


        }

        
    }
}
