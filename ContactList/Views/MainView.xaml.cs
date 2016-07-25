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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ContactList.ViewModels;
using ContactList.Models;
using System.Collections.ObjectModel;


namespace ContactList.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public List<ContactViewModel> CurrentContacts = new List<ContactViewModel>();
        public List<TextBlock> CurrentTextBlocks = new List<TextBlock>();
        public MainViewModel ListOfContacts = new MainViewModel() { Symbol = "Список контакт" };
        bool CtrlPressed
        {
            get
            {
                return System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl);
            }
        }
        public MainView()
        {
            InitializeComponent();
            Context db = new Context();

            List<Contact> Contacts = new List<Contact>();
            
             foreach (var c in db.Contacts)
             {
                 Contacts.Add((Contact)c);
             }
             if (Contacts.Count == 0)
             {
                 MessageBox.Show("Ваш список контактов пуст. Чтобы оценить работу приложения мы добавили несколько Контактов за вас =)");
                 Contacts.Add(new Contact("Петров Феофан Некифорович","На руси такие не водятся", "И до изобретения телефона далековато"));
                 Contacts.Add(new Contact("Петров Сергей Владимирович", "somefun@yande.ru", "12345678910"));
             }
            var alphabet = (from c in Contacts
                            select c.FirstSymbol).Distinct();

            foreach (var a in alphabet)
            {
                MainViewModel viewModel = new MainViewModel() { Symbol = a.ToString() };

                foreach (Contact c in Contacts.Where(q => q.FirstSymbol.Equals(a.ToString())))
                {
                    viewModel.ContactsList.Add(new ContactViewModel(c));
                }
                ListOfContacts.Alphabet.Add(viewModel);
            }

            treeView.Items.Add(ListOfContacts);
            ListOfContacts.Order();
            
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
            Detail detail = new Detail(CurrentContacts.First(),ListOfContacts);
            detail.Show();
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Detail detail = new Detail(ListOfContacts);
            detail.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Удалить выбранные объекты?", "Удаление", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ListOfContacts.Delete(CurrentContacts.ToArray());
            }
            else if(result==MessageBoxResult.No)
            {
                CurrentContacts.RemoveAll(c=>c!=null);
                ClearSelection(CurrentTextBlocks);
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            try
            {
                if (CtrlPressed)
                {
                    CurrentContacts.Add((ContactViewModel)treeView.SelectedItem);
                    
                    stackPanel.Children[0].Focusable=true;
                    Edit.IsEnabled = false;
                    Delete.IsEnabled = true;
                }
                else
                {
                    CurrentContacts.RemoveAll(con => con != null);
                    CurrentContacts.Add((ContactViewModel)treeView.SelectedItem);
                    Edit.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
            }
            catch { Edit.IsEnabled = false; Delete.IsEnabled = false; }
        }

        

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock t = sender as TextBlock;
            if (!CtrlPressed)
            {
                ClearSelection(CurrentTextBlocks);
            }
            CurrentTextBlocks.Add(t);
            t.Background = Brushes.Blue;
        }

        public void ClearSelection(List<TextBlock> l)
        {
            foreach (var q in l)
            {
                q.Background = null;
            }
            l.RemoveAll(c => c != null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Good bye");
        }

       
    }
    
}
