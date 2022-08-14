using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewInvoice.Models
{

    public class DbCon : DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<currencies> currencies { get; set; }
        public DbSet<vendor> vendors { get; set; }
        public DbSet<project> projects { get; set; }
        public DbSet<purchaseorder> purchaseorders { get; set; }
        public DbSet<doc> docs { get; set; }
        public DbSet<invoice> invoices { get; set; }
        public DbSet<approver> approvers { get; set; }

    }

    public class user
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public virtual List<invoice> Invoices { get; set; }
    }

    public class currencies
    {
        [Key]
        public string symbol { get; set; }
        public string name { get; set; }
        public virtual List<invoice> invoices { get; set; }
    }
    public class vendor
    {
        [Key]
        public int vendornumber { get; set; }
        public string name { get; set; }
        public virtual List<invoice> invoices { get; set; }
    }

    public class project
    {
        [Key]
        public string projectnumber { get; set; }
        public string projectname { get; set; }
        public virtual user projectmang { get; set; }

    }

    public class purchaseorder
    {
        [Key]
        public string ordernumber { get; set; }
        public string date { get; set; }

    }

    public class doc
    {
        public int id { get; set; }
        public string path { get; set; }
        public virtual invoice invoice { get; set; }
    }



    public class invoice
    {
        [Key]
        public string invoicenumber { get; set; }
        public float value { get; set; }

        public string description { get; set; }

        public string state { get; set; }

        public int creator_key { get; set; }
        public virtual user creator { get; set; }
        public int delete_state { get; set; }

        public virtual List<doc> docs { get; set; }
        public virtual currencies currency { get; set; }
        public virtual vendor vendor { get; set; }
        public virtual project projectnumber { get; set; }
        public virtual purchaseorder purchaseorder { get; set; }
        public virtual List<approver> Approver { get; set; }

    }

    public class approver
    {
        public int id { get; set; }
        public string decision { get; set; }

        public virtual invoice invoice { get; set; }
        public string invoice_key { get; set; }

        public virtual user user { get; set; }
        public int user_key { get; set; }
    }
}