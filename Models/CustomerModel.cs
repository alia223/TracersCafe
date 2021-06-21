using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TracersCafe.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public string Address2 { get; set; }
        [Required]
        public string Address3 { get; set; }
        [Required]
        public string Address4 { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public int Age { get; set; }
    }
}