using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactList.Models
{
     public class Contact
        {

         [Key]
            public Guid Id { get; set; }
            [Required]
            public String FIO { get; set; }
            public string FirstSymbol { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }

            public Contact(string fio, string email, string phone)
            {
                this.FIO = fio;
                this.Email = email;
                this.Phone = phone;
                this.Id = Guid.NewGuid();
                this.FirstSymbol = FIO.Substring(0, 1);
            }
            public Contact()
            {
            }

        }

        public class Context : DbContext
        {
            public DbSet<Contact> Contacts { get; set; }
        }
    }


