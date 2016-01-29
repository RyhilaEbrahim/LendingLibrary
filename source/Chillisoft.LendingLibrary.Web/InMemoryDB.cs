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
            Titles=new List<Title>
            {
                new Title {Id = 1,Description = "Mr"},
                new Title {Id = 2,Description = "Mrs"},
                new Title {Id = 3,Description = "Dr"},
            };
        }

        public static List<Borrower> Borrowers { get; set; }
        public static List<Title> Titles { get; set; }
        
         
    }
}