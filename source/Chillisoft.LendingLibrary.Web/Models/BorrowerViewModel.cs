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
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Title")]
        public int TitleId { get; set; }
        [DisplayName("Title")]
        public string TitleDescription { get; set; }
        public List<SelectListItem> TitlesSelectList { get; set; }

    }
}