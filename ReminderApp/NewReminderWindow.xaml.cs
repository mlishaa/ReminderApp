using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for NewReminderWindow.xaml
    /// </summary>
    public partial class NewReminderWindow : Window
    {
        private string connectionDB = ConfigurationManager.ConnectionStrings["ReminderApp.Properties.Settings.DBReminderConnectionString"].ConnectionString;

        bool isNew;
        public Reminder newReminder;
        public NewReminderWindow()
        {
            InitializeComponent();
            isNew = true;
            newReminder = new Reminder();
            
        }

        public NewReminderWindow(Reminder EditedReminder)
        {
            InitializeComponent();
            isNew = false;
            newReminder = EditedReminder;
            LoadReminderInfo();
            btnSaveReminder.Content = "Modifiy";
        }



        public void LoadReminderInfo()
        {
            txtShortDes.Text = newReminder.ShortDes;
            txtLongDes.Text = newReminder.LongDes;
            dPDueDate.SelectedDate = newReminder.DueDate;
        }




        private void btnSaveReminder_Click(object sender, RoutedEventArgs e)
        {
            if(isNew)
            try
            {
                if (CollectTableDate())
                {
                    newReminder.ShortDes = txtShortDes.Text;
                    newReminder.LongDes = txtLongDes.Text;
                    newReminder.DueDate = dPDueDate.SelectedDate.Value;

                  
                    this.DialogResult = true;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // here if it's not new and there's something changed in the information


            // else do nothing
        }

        private bool CollectTableDate()
        {
            StringBuilder msg = new StringBuilder();
            if (string.IsNullOrEmpty(txtShortDes.Text)
                || string.IsNullOrEmpty(txtLongDes.Text) ||
                string.IsNullOrEmpty(dPDueDate.SelectedDate.ToString()))
            {
                MessageBox.Show("Remindder description is not compelete","Info",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return false;
            }
            else
                return true;
        }

      
    }

 }

