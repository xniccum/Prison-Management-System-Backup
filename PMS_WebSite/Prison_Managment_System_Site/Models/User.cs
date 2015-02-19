using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Prison_Managment_System_Site.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(20)]
        [DisplayName("First Name")]
        public string fname { get; set; }
        [Required(ErrorMessage = "Middle Name is Required")]
        [StringLength(20)]
        [DisplayName("Middle Name")]
        public string mname { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(20)]
        [DisplayName("Last Name")]
        public string lname { get; set; }
        [Required(ErrorMessage="Username is Required")]
        [StringLength(20)]
        [DisplayName("Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20)]
        [DisplayName("Password")]
        public string password { get; set; }
        [DisplayName("Permissions")]
        public int permissions { get; set; }

        public string FullName()
        {
            return fname + " " + mname + " " + lname;
        }
    }
}