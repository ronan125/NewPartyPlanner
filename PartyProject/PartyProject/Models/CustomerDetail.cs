namespace PartyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CustomerDetail
    {
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "First Name is Required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Town is required")]
        public string Town { get; set; }

        public int County { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public Nullable<int> Phone { get; set; }

        public virtual tblLocation tblLocation { get; set; }
    }
}