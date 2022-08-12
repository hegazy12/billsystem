using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace sara_system.Models
{
    public class DbCon : DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<currencies> currencies { get; set; } 
        public DbSet<vendor> vendors { get; set; }
        public DbSet<project> projects { get; set; }
        public DbSet<purchaseorder> purchaseorders { get; set;}
        public DbSet<doc> docs { get; set; }
        public DbSet<state> states { get; set; }

        public DbSet<invoice> invoices { get; set;}
        public DbSet<approver> approvers { get; set; }

    }

    public class user
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<approver> approvers { get; set; }
    }

    public class currencies
    {
        [Key]
        public string symbol { get; set; }
        public string name { get; set; }
        public List<invoice> invoices { get; set; }
    }
    public class vendor
    {
            [Key]
            public int vendornumber { get; set; }
            public string name { get; set; }
            public List<invoice> invoices { get; set; }
    }

    public class project
    {
            [Key]
            public string projectnumber { get; set; }
            public string projectname { get; set; }
            public user projectmang { get; set; }
            
    }

    public class purchaseorder
    {
        [Key]
        public string ordernumber {get; set;}
        public string created {get; set;}
       
    }

    public class doc
    {
        public int id { get; set; }
        public string path { get; set; }
        public invoice invoice { get; set; }
    }

    public class state
    {
        public int id { get; set; }
        public string statename { get; set; }
        public List<invoice> invoices { get; set; }
    }

    public class invoice{
        [Key]
        public string invoicenumber { get; set; }
        public float value { get; set; }

        public string description { get; set; }

        public user creator { get; set; }
        public List<doc> docs { get; set; }
        public currencies currency { get; set; }
        public vendor vendor { get; set; }
        public project projectnumber { get; set; }
        public purchaseorder purchaseorder { get; set; }
       
    }

    public class approver
    {
        public int id { get; set; }
        
        public string invoice { get ; set; }
        
        
        public int user { get; set; }
    }
}
