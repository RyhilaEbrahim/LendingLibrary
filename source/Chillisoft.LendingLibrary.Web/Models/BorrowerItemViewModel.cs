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
        public int ItemId { get; set; }
        [DisplayName("Item")]
        public string ItemDescription { get; set; }
        public List<SelectListItem> ItemSelectListItems { get; set; }
        public List<SelectListItem> BorrowersSelectListItems { get; set; }
       public int BorrowerId { get; set; }
        [DisplayName("First Name")]
     
        public string FirstName { get; set; }
     
        public string Surname { get; set; }
   
        public string Email { get; set; }
        [DisplayName("TitleId")]
        public int TitleId { get; set; }
        [DisplayName("Title")]
        public string TitleDescription { get; set; }
        public List<SelectListItem> TitlesSelectList { get; set; }
        public string ContactNumber { get; set; }
        public byte[] Photo { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateBorrowed { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DateReturned { get; set; }



    }
}