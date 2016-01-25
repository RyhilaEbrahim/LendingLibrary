using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chillisoft.LendingLibrary.Web.Models
{
    public class TestViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public SelectList Items { get; set; }
        [Required]
        public string SelectItem { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public bool Email { get; set; }



        //            public IEnumerable<SelectListItem> Items=new List<SelectListItem> {"Hello","Goodbye"}; 
    }
}