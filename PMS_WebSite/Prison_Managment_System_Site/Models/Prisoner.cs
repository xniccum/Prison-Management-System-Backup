using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prison_Managment_System_Site.Models
{
    public class Prisoner
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(20)]
        [DisplayName("First Name")]
        public string first_name { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(20)]
        [DisplayName("Middle Name")]
        public string middle_name { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        [StringLength(20)]
        [DisplayName("Last Name")]
        public string last_name { get; set; }
        [Required(ErrorMessage = "Must Select Crime")]
        [DisplayName("Crime")]
        public string crime { get; set; }
        [DisplayName("Description")]
        public string crime_description { get; set; }

    }
}