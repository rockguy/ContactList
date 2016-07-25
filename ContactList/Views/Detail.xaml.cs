using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ContactList.ViewModels;
using ContactList.Models;

namespace ContactList.Views
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Window
    {
        private bool IsNew;
        private ContactViewModel InputContact;
        private Contact Contact;
        private MainViewModel ListOfContacts;

        public Detail()
        {
            InitializeComponent();
        }

        public Detail(ContactViewModel p, MainViewModel p1)
        {
            InitializeComponent();
            InputContact = p;
            ListOfContacts = p1;
            // TODO: Complete member initialization
            FIO.Text = p.FIO;
            Phone.Text = p.Phone;
            Email.Text = p.Email;
            IsNew = false;
        }

        public Detail(MainViewModel p)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            ListOfContacts = p;
            IsNew = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!IsNew)
            {
                ListOfContacts.Delete(InputContact);
                InputContact.FIO = FIO.Text;
                InputContact.Phone = Phone.Text;
                InputContact.Email = Email.Text;
                ListOfContacts.Add(InputContact);
            }
            else 
            {
                Contact = new Contact(FIO.Text,Email.Text,Phone.Text);
                ListOfContacts.Add(new ContactViewModel(Contact));
            }
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
