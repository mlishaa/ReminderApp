using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for NewReminderWindow.xaml
    /// </summary>
    public partial class NewReminderWindow : Window
    {
        private string connectionDB = ConfigurationManager.ConnectionStrings["ReminderApp.Properties.Settings.DBReminderConnectionString"].ConnectionString;

        public NewReminderWindow()
        {
            InitializeComponent();
        }
























        private void btnSaveReminder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CollectTableDate())
                {
                    string command = @"INSERT INTO Reminders(Name,LongDescription,DueDate) VALUES('" + txtShortDes.Text + @"','" + txtLongDes.Text + @"','" + dPDueDate.SelectedDate.Value + @"')";
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["ReminderApp.Properties.Settings.DBReminderConnectionString"].ConnectionString;

                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO [dbo].[Reminders](Name,LongDescription,DueDate) VALUES(@sd,@ld,@d)";
                    cmd.Parameters.AddWithValue("@sd", txtShortDes.Text);
                    cmd.Parameters.AddWithValue("@ld", txtLongDes.Text);
                    cmd.Parameters.AddWithValue("@d", dPDueDate.SelectedDate.Value);
                    cmd.Connection = con;
                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        MessageBox.Show("yes");
                    }
                    con.Close();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CollectTableDate()
        {
            if (string.IsNullOrEmpty(txtShortDes.Text)
                || string.IsNullOrEmpty(txtLongDes.Text) ||
                string.IsNullOrEmpty(dPDueDate.SelectedDate.ToString()))
            {
                return false;
            }
            else
                return true;
        }
    }

 }

