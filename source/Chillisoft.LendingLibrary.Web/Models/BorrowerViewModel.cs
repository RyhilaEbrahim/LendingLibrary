using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chillisoft.LendingLibrary.Web.Models
{
    public class BorrowerViewModel
    {
        public int Id { get; set; }
        [DisplayName ("First Name")]
  
        public string FirstName { get; set; }
   
        public string Surname { get; set; }
    
        public string Email { get; set; }
        [DisplayName("Title")]
        public int TitleId { get; set; }
        [DisplayName("Title")]
        public string TitleDescription { get; set; }
        public List<SelectListItem> TitlesSelectList { get; set; }
        public string ContactNumber { get; set; }
        public byte[] Photo { get; set; }
        
      

    }
}