using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
//using Org.BouncyCastle.Crypto.Tls;
using ActiveUp.Net.Mail;
using System.Reflection;
using System.Collections;
using EmailSorter_V2;
using System.Diagnostics;

namespace EmailSorter_V2
{
    public class EmailSorter
    {
        //mailbox
        private Mailbox inbox;

        //Login Creds
        private string email;
        private string password;
        
        //Defines client
        private Imap4Client client = new Imap4Client();

        //Define Dict
        private IDictionary<string, int> emailCount = new Dictionary<string, int>();

        //EmailId
        private int[] ids;

        //Getter and Setters
        public Imap4Client Client
        {
            get { return client; }
        }
        public IDictionary<string, int> EmailCount
        {
            get { return emailCount; }
        }
        public string EmailAdr
        {
            get {return email; }
            set {
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(value);
                        email = value;
                    }
                    catch (FormatException)
                    {
                        email = "-1";
                }
            }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        //Methods 
        public string Login()
        {
            try
            {
                Console.WriteLine("Loggin");
                //Connect to server PORT 993
                client.ConnectSsl("imap.gmail.com", 993);

                //Login
                Console.WriteLine(email, password);
                client.Login(email, password);

                return "0";
            }
            catch(Imap4Exception)
            {
                return "Invaild Cred";
            }
        }

        public int Logout()
        {
            client.Disconnect();
            return 0;
        }

        public int FetchFrom(int start, int amount)
        {
            Stopwatch sw = new Stopwatch();
            //Fetches all emails
            inbox = client.SelectMailbox("inbox");
            //Console.WriteLine("Fetching Ids");
            ids = inbox.Search("ALL");
            Console.WriteLine(ids.Length);

            //Validation for any emails in inbox
            if (ids.Length > 0)
            {
                //Loops though emails
                Console.WriteLine("Fetching Froms");
                //ActiveUp.Net.Mail.Message msg = null;
                for (int i = 0; i < amount; i++) //ids.Length
                {
                    if (i < ids.Length)
                    {
                        try
                        {
                            string from = inbox.Fetch.HeaderString(ids[start]);
                            from = cleanHeader(from);

                            //Add To Dict
                            if (emailCount.ContainsKey(from))
                            {
                                Console.WriteLine("Adding To Dict");
                                emailCount[from] += 1;
                                start++;
                            }
                            else
                            {
                                Console.WriteLine("Incrementing A Value By One");
                                emailCount[from] = 1;
                                start++;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error");
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                //Console.WriteLine("No Emails Found");
                return -1;
            }
            return start;
        }

        public string moveToTrash(string from)
        {
            Console.WriteLine("Deleteing Email");
            foreach (int x in ids)
            {
                Console.WriteLine("Checking Email...");
                Console.WriteLine(cleanHeader(inbox.Fetch.HeaderString(ids[x])));
                Console.WriteLine(from);
                if (from.Contains(cleanHeader(inbox.Fetch.HeaderString(ids[x]))))
                {
                    
                   //Move To Trash
                   Console.WriteLine("Moving Email...");
                    client.Command("copy " + ids[x] + " [Gmail]/Bin");
                }
            }
            return "0";
        }

        public void printDict()
        {
            foreach (KeyValuePair<string,int> entry in emailCount)
            {
                //Console.WriteLine("Keys {0} Value{1}", entry.Key, entry.Value);
            }
        }
        private string cleanHeader(string from)
        {
            //Clean Header
            int indexFrom = from.IndexOf("From: ");
            from = from.Substring(indexFrom, from.Length - indexFrom);
            indexFrom = from.IndexOf("From: ");
            int index = from.IndexOf(">");
            from = from.Substring(indexFrom, index - indexFrom);
            from = from.Replace("<", " ");
            return from;
        }
    }
}
