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
using Xamarin.Forms.Dynamic;

namespace Email_Sorter_V2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private EmailSorter user;
        private int amount;
        private int emailfetchpointer = 0;
        private int pageNum = -1;
        private List<KeyValuePair<int, int>> pagePointers = new List<KeyValuePair<int, int>>();

        public Window1(EmailSorter user)
        {
            InitializeComponent();
            this.user = user;
            amount = 10;
            if (String.Equals(user.FetchFrom(emailfetchpointer, amount), "-1"))
            {
                Console.WriteLine("Error");
            }
            else
            {
                emailfetchpointer = user.FetchFrom(emailfetchpointer, amount);
            }
            addToListView(0,user.EmailCount.Count-1);
            pagePointers.Add(new KeyValuePair<int, int>(0, user.EmailCount.Count - 1));
            pageNum++;
        }

        private void DashboardOpen(object sender, MouseButtonEventArgs e)
        {
            //Open Dashboard
            this.Hide();
            Hub inbox = new Hub(user);
            inbox.Show();
        }

        private void InboxOpen(object sender, MouseButtonEventArgs e)
        {
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

        private void EmailListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int NextPageNum = pageNum + 1;
                user.EmailCount.ElementAt(NextPageNum);
                addToListView(pagePointers[NextPageNum].Key, pagePointers[NextPageNum].Value);
            }
            catch
            {
                emailfetchpointer = user.FetchFrom(emailfetchpointer, amount);
                addToListView(EmailListView.Items.Count + 1, user.EmailCount.Count);
                pagePointers.Add(new KeyValuePair<int, int>(EmailListView.Items.Count + 1, user.EmailCount.Count+1));
            }
            pageNum++;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            int lastPageNum = pageNum - 1;
            if (lastPageNum > -1)
            {
                addToListView(pagePointers[lastPageNum].Key, pagePointers[lastPageNum].Value);
                pageNum--;
            } 
        }
        private void addToListView(int start, int end)
        {
            end = end - start;
            EmailListView.Items.Clear();
            for (int i = 0; i < end; i++)
            {
                EmailListView.Items.Add(user.EmailCount.ElementAt(start));
                start++;
            }
        }

        private void noEmailsFound()
        {
            EmailListView.Items.Clear();
        }

        private void MoveToTrashButton_Click(object sender, RoutedEventArgs e)
        {
            string from = EmailListView.SelectedItem.ToString();
            Console.WriteLine("Moving To Trash");
            user.moveToTrash(from);
            EmailListView.Items.Remove(from);
            Console.WriteLine("Done");
            //user.moveToTrash(from);
        }
  
    }
}
