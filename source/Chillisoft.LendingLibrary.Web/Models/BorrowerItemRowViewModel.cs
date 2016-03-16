using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chillisoft.LendingLibrary.Web.Models
{
    public class BorrowerItemRowViewModel
    {
        public int Id { get; set; }
     
        [DisplayName("Item")]
        public string ItemDescription { get; set; }
     
        public int BorrowerId { get; set; }

        [DisplayName("First Name")]

        public string BorrowerFirstName { get; set; }
        [DisplayName("Surname")]

        public string BorrowerSurname { get; set; }
        [DisplayName("Email")]
        public string BorrowerEmail { get; set; }

        [DisplayName("Title")]
        public string BorrowerTitleDescription { get; set; }
        [DisplayName("Contact Number")]
        public string BorrowerContactNumber { get; set; }
        public byte[] Photo { get; set; }
        [DisplayName("Date Borrowed")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBorrowed { get; set; }
        [DisplayName("Date Returned")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateReturned { get; set; }
    }
}