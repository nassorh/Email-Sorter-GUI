using EmailSorter_V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Email_Sorter_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DELETE CODE
            //Hub win2 = new Hub(emailSession);
            //win2.Show();
            //DEL CODE
            InitializeComponent();
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ClearEmailText(object sender, MouseButtonEventArgs e)
        {
            if (String.Equals(Email.Text, "Email"))
            {
                Email.Clear();
            }
        }

        private void ClearPasswordText(object sender, MouseButtonEventArgs e)
        {
            Password.Clear(); 
        }

        private void EmailError()
        {
            Email.BorderBrush = System.Windows.Media.Brushes.Red;
            ToolTipService.SetIsEnabled(Email, true);
        }

        private void PasswordError()
        {
            Password.BorderBrush = System.Windows.Media.Brushes.Red;
            ToolTipService.SetIsEnabled(Password, true);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Reset Error
            ToolTipService.SetIsEnabled(Email, false);
            Email.BorderBrush = System.Windows.Media.Brushes.Transparent;
            ToolTipService.SetIsEnabled(Password, false);
            Password.BorderBrush = System.Windows.Media.Brushes.Transparent;
            //Input Validation 
            if (String.IsNullOrEmpty(Email.Text))
            {
                this.EmailError();
            }
            else if (String.IsNullOrEmpty(Password.Password.ToString()))
            {
                this.PasswordError();
            }
            else
            {
                //Email Validation
                EmailSorter emailSession = new EmailSorter();
                emailSession.EmailAdr = Email.Text;
                emailSession.Password = Password.Password.ToString();

                if (String.Equals(emailSession.EmailAdr, "-1"))
                {
                    this.EmailError();
                }
                else
                {
                    //Login
                    if (String.Equals(emailSession.Login(), "Invaild Cred"))
                    {
                        this.EmailError();
                        this.PasswordError();
                    }
                    else
                    {
                        this.Hide();
                        Console.WriteLine("THIS IS SESSION BEFORE PASS");
                        Console.WriteLine(emailSession.EmailAdr);
                        Hub win2 = new Hub(emailSession);
                        win2.Show();
                    }

                }
            }
        }
    }
}
