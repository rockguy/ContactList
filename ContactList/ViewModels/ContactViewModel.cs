using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Models;
using System.Windows.Input;
using ContactList.ViewModels;

namespace ContactList.ViewModels
{       
    public class ContactViewModel : ViewModelBase
    {
        public Contact contact;

        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }

        public string Id
        {
            get { return contact.Id.ToString(); }
            set
            {
                contact.Id = Guid.NewGuid();
                OnPropertyChanged("Id");
            }
        }

        public string FIO
        {
            get { return contact.FIO; }
            set
            {
                contact.FIO = value;
                OnPropertyChanged("FIO");
                contact.FirstSymbol = value.Substring(0, 1);
                OnPropertyChanged("FirstSymbol");
                OnPropertyChanged("Items");
                OnPropertyChanged("Symbol");
            }
        }
        public string FirstSymbol
        {
            get { return contact.FirstSymbol; }
            set
            {
                contact.FirstSymbol = value.Substring(0, 1);
                OnPropertyChanged("FirstSymbol");
            }
        }

        public string Phone
        {
            get { return contact.Phone; }
            set
            {
                contact.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get { return contact.Email; }
            set
            {
                contact.Email = value;
                OnPropertyChanged("Email");
            }
        }
        
        /*
        #region Commands

        #region Забрать

        private DelegateCommand edit;
        

        public ICommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new DelegateCommand(EditItem);
                }
                return edit;
            }
        }


        private void EditItem()
        {
            
            
        }

        #endregion
        
        #region Выдать

        private DelegateCommand giveItemCommand;

        public ICommand GiveItemCommand
        {
            get
            {
                if (giveItemCommand == null)
                {
                    giveItemCommand = new DelegateCommand(GiveItem, CanGiveItem);
                }
                return giveItemCommand;
            }
        }
        
        private void GiveItem()
        {
            Count--;
        }

        private bool CanGiveItem()
        {
            return Count > 0;
        }
        
        #endregion

          */
    }


    }

