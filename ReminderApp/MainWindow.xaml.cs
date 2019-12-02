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
using Tulpep.NotificationWindow;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        private ContextMenu mnu;
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
            using (SqlConnection con=new SqlConnection(connectionDB))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(sd, "short");
                 // listShortDescription.ItemsSource = sd.Tables["short"].Rows;
                foreach (DataRow data in sd.Tables["short"].Rows)
                {
                    listShortDescription.Items.Add(data["Name"].ToString());

                }
             //   listShortDescription.SelectedValuePath = ".[Name]";
              // listShortDescription.DisplayMemberPath = ".[Name]";


            }


        }

        private void clearDataField()
        {
            txtLongDescription.Text = "";
            listShortDescription.SelectedIndex = -1;
            DueDate.Text = "";
        }


        private void listShortDescription_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listShortDescription.SelectedIndex < listShortDescription.Items.Count)
            {
                btnUpdate.IsEnabled = true;
            }
            else
            {
                btnUpdate.IsEnabled = false;
            }

            if (string.IsNullOrEmpty(listShortDescription.SelectedItem.ToString()))
                MessageBox.Show("Please select an item");
            else
            {
                string getLong = listShortDescription.SelectedItem.ToString();

                string command = @"select * from Reminders where Name like '" + getLong + "' ";
                DataSet sd = new DataSet();
                using (SqlConnection con = new SqlConnection(connectionDB))
                {
                    SqlCommand cmd = new SqlCommand(command, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(sd, "short");
                    foreach (DataRow data in sd.Tables["short"].Rows)
                    {
                        txtLongDescription.Text = (data["LongDescription"].ToString());
                        DateTime date = (DateTime)data["DueDate"];
                        DueDate.Text = date.ToString("dddd, dd MMMM yyyy");

                    }
                }
            }
        }


        // save to dataBase
       

        private void btnAddReminder_Click(object sender, RoutedEventArgs e)
        {
            NewReminderWindow newReminder = new NewReminderWindow();
            if (newReminder.ShowDialog() == true)
            {
                // insert new reminder into table
                Reminder r = newReminder.newReminder;
                string command = @"insert into Reminders(Name,LongDescription,DueDate) Values(" +
                           @"'" + r.ShortDes + @"','" +
                    r.LongDes +@"','" + r.DueDate + @"')";
                try { 
                using (SqlConnection con = new SqlConnection(connectionDB))
                {
                    SqlCommand cmd = new SqlCommand(command, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    listShortDescription.Items.Clear();
                    PopulateListView();
                    listShortDescription.Items.Refresh();
                }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

             }

        }

        private void listShortDescription_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    PopupNotifier notifiy = new PopupNotifier();        
        //    notifiy.TitleText = listShortDescription.SelectedItem.ToString();
        //    notifiy.Popup();


        //}

        private void btnUpdate_Click_1(object sender, RoutedEventArgs e)
        {
            string getLong = listShortDescription.SelectedItem.ToString();

           

            string command = @"select * from Reminders where Name = '" + getLong + "' ";
            DataSet sd = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionDB))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(sd, "short");

                Reminder r = new Reminder();
                foreach (DataRow data in sd.Tables["short"].Rows)
                {
                    r.ShortDes = data["Name"].ToString();
                    r.LongDes = data["LongDescription"].ToString();
                    r.DueDate = DateTime.Parse(data["DueDate"].ToString());
                }

                NewReminderWindow ModifiedReminder = new NewReminderWindow(r);
                ModifiedReminder.ShowDialog();
                //if (ModifiedReminder.ShowDialog().Value)
                //{

                //}

            }
        }
    }
}
