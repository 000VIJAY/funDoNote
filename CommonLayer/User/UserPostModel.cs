using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "First Name Start with capital letter and has minimum three character")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]{0,}$")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9._]+@[A-Za-z0-9]+.[a-z]{2,5}$", ErrorMessage = "Please enter proper email 'ex-Vijay78@gmail.com'")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+=-])[a-zA-Z0-9!@#$%^&*()_+=-]{8,}$", ErrorMessage = "Password is not valid 1.Min 8 Character , 2.Atleast 1 special character[@,#,$],3.Atleast 1 digit[0-9],4.Atleast 1 Capital Letter[A-Z] ")]
        public string Password { get; set; }
       [Required]
        public string ConfirmPassword { get; set; }
    }
}
