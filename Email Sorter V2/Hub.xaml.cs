using EmailSorter_V2;
using System;
using System.Collections.Generic;
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

namespace Email_Sorter_V2
{
    /// <summary>
    /// Interaction logic for Hub.xaml
    /// </summary>
    public partial class Hub : Window
    {
        private EmailSorter user;

        public Hub(EmailSorter user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void DashboardOpen(object sender, MouseButtonEventArgs e)
        {
            //Open Dashboard
            Console.WriteLine(user.EmailAdr);
        }

        private void InboxOpen(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Window1 inbox = new Window1(user);
            inbox.Show();
        }

        private void SettingsOpen(object sender, MouseButtonEventArgs e)
        {
            //Open Setting
        }

        private void LogoutOpen(object sender, MouseButtonEventArgs e)
        {
            //Hide Page
            this.Hide();
            //Open Login Page
        }

        private void ExitOpen(object sender, MouseButtonEventArgs e)
        {
            //Closes the app
            this.Close();
        }
    }
}
