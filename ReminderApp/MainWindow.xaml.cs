using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        private string connectionDB = ConfigurationManager.ConnectionStrings["ReminderApp.Properties.Settings.DBReminderConnectionString"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            PopulateListView();
        }

        private void PopulateListView()
        {
            //List<string> mylist = new List<string>();
            string command = @"select * from Reminders";
            DataSet sd = new DataSet();
            using(SqlConnection con=new SqlConnection(connectionDB))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(sd, "short");
                foreach(DataRow data in sd.Tables["short"].Rows)
                {
                    listShortDescription.Items.Add(data["Name"].ToString());

                }
                listShortDescription.SelectedValuePath = ".[Name]";
             //   listShortDescription.DisplayMemberPath = ".[Name]";


            }


        }

        private void listShortDescription_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            string getLong = listShortDescription.SelectedItem.ToString();

            string command = @"select LongDescription from Reminders where Name like '" + getLong + "' ";
            DataSet sd = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionDB))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(sd, "short");
                txtLongDescription.Text = sd.Tables["short"].Rows.ToString();
            }
        }
    }
}
