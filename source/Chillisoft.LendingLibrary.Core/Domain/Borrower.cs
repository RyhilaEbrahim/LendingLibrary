﻿namespace Chillisoft.LendingLibrary.Core.Domain
{
    public class Borrower: EntityBase
    {
       
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public virtual Title Title { get; set; }
        public int TitleId { get; set; }
         
       
    }
}