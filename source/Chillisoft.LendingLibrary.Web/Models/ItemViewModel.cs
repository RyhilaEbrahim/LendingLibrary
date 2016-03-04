using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chillisoft.LendingLibrary.Web.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }
     
    }
}