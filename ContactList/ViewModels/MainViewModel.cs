using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Commands;
using System.Collections.ObjectModel;
using ContactList.Models;
using System.Windows;
using System.Windows.Input;
using System.Data;
using System.IO;
using System.Data.Entity;


namespace ContactList.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        Context db = new Context();
        public ObservableCollection<ContactViewModel> ContactsList { get; set; }
        public ObservableCollection<MainViewModel> Alphabet { get; set; }

        #region Constructor

        public string Symbol{get;set;}

        public MainViewModel()
        {
            this.ContactsList = new ObservableCollection<ContactViewModel>();
            this.Alphabet = new ObservableCollection<MainViewModel>();
            
        }

        public void Add(ContactViewModel cvm){
            bool b = false;
                foreach (var w in Alphabet)
                {
                    if (w.Symbol == cvm.FirstSymbol)
                    {
                        w.ContactsList.Add(cvm);
                        b = true;
                        OnPropertyChanged("Items");
                        break;
                    }
                }
                if (b == false)
                {
                    MainViewModel mvm = new MainViewModel(){Symbol=cvm.FirstSymbol};
                    mvm.ContactsList.Add(cvm);
                    Alphabet.Add(mvm);
                    OnPropertyChanged("Items");
                    OnPropertyChanged("Symbol");
                }
                db.Entry(cvm.contact).State = EntityState.Added;
                db.SaveChanges();
                Order();
        }
        public void Delete(ContactViewModel[] Listcwm) 
        {
            try
            {
                for(int i=0;i<Listcwm.Length;i++)
                {
                    Delete(Listcwm[i]);
                }
            }
            catch (System.ArgumentOutOfRangeException e) { }
            
        }

        public void Delete(MainViewModel m)
        {
            Alphabet.Remove(m);
        }
        public void Delete(ContactViewModel cvm)
        {
            foreach (var w in Alphabet)
                {
                    if (w.Symbol == cvm.FirstSymbol)
                    {
                        w.ContactsList.Remove(cvm);

                        if (w.ContactsList.Count == 0)
                            Delete(w);
                        OnPropertyChanged("Items");
                        OnPropertyChanged("Symbol");
                        db.Entry(cvm.contact).State = EntityState.Deleted;
                        db.SaveChanges();
                        break;
                    }
                }
        }
        public void Refresh(ContactViewModel cwm)
        {
            Delete(cwm);
            Add(cwm);
        }
        public void Order() 
        {
            Alphabet=new ObservableCollection<MainViewModel>(Alphabet.OrderBy(s => s.Symbol));
            
            foreach (var c in Alphabet)
            {
                c.ContactsList=new ObservableCollection<ContactViewModel>(c.ContactsList.OrderBy(co => co.FIO));
            }
            OnPropertyChanged("Items");
            OnPropertyChanged("Symbol");
        }
        public IList<object> Items
        {
            get
            {
                IList<object> ChildNodes = new List<object>();
                foreach (var contact in ContactsList)
                {
                    ChildNodes.Add(contact);
                }
                foreach (var word in Alphabet)
                {
                    ChildNodes.Add(word);
                }
                return ChildNodes;
            }
           
        }

        #endregion
    }
    }

