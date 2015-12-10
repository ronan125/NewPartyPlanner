//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PartyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class EntertainerDetail
    {
        public int EntertainerID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string Fistname { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string Town { get; set; }

        public int County { get; set; }
        public int Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        public int Skill { get; set; }
    
        public virtual tblLocation tblLocation { get; set; }
        public virtual tblSkill tblSkill { get; set; }
    }
}
