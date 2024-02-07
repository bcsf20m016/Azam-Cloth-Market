using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreSignUpDTO
    {

        [Required(ErrorMessage = "User Name required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number required!")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Store Name required!")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Country Name required!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Store Type required!")]
        public string StoreType { get; set; }

        


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
