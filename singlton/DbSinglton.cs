using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sara_system.Models;

namespace sara_system.singlton
{
    public class DbSinglton
    {
        static private DbCon db;
        
        public DbCon GitDB()
        {
            if (db==null)
            {
                db = new DbCon();
                return db;
            }
            return db;
        }

    }
}