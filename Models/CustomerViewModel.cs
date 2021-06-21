using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TracersCafe.Models
{
    public class CustomerViewModel
    {
        //Id of customer
        public int Id { get; set; }
        //Title of customer
        [Required]
        public string Title { get; set; }
        //First name of customer
        [Required]
        public List<SelectListItem> TitleList { get; set; }
        public string Firstname { get; set; }
        //Last name of customer
        [Required]
        public string Surname { get; set; }
        //Building Number of customer
        [Required]
        public string Address1 { get; set; }
        //Road Name of customer
        [Required]
        public string Address2 { get; set; }
        //Area of customer
        [Required]
        public string Address3 { get; set; }
        //City of customer
        [Required]
        public string Address4 { get; set; }
        //Title of customer
        [Required]
        public string PostCode { get; set; }
        //Telephone number of customer
        [Required]
        public string Telephone { get; set; }
        [Required]
        public int Age { get; set; }
    }
}