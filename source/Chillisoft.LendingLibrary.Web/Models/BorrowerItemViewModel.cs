using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chillisoft.LendingLibrary.Web.Models
{
    public class BorrowerItemViewModel
    {
        public int Id { get; set; }
        [DisplayName("Item")]
        public int ItemId { get; set; }
        [DisplayName("Item")]
        public string ItemDescription { get; set; }
        public List<SelectListItem> ItemSelectListItems { get; set; }
        public List<SelectListItem> BorrowersSelectListItems { get; set; }
        [DisplayName("Borrower")]
        public int BorrowerId { get; set; }
        [DisplayName("First Name")]
     
        public string BorrowerFirstName { get; set; }
        [DisplayName("Surname")]
        public string BorrowerSurname { get; set; }
        [DisplayName("Email")]
        public string BorrowerEmail { get; set; }
        [DisplayName("TitleId")]
        public int TitleId { get; set; }
        [DisplayName("Title")]
        public string BorrowerTitleDescription { get; set; }

        public List<SelectListItem> TitlesSelectList { get; set; }

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