using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web
{
    public static class InMemoryDB
    {
        static InMemoryDB()
        {
            Borrowers=new List<Borrower>();
        }

        public static List<Borrower> Borrowers { get; set; }
        
         
    }
}