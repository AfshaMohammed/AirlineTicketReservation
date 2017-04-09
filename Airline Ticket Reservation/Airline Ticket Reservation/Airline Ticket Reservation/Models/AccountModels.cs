using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Airline_Ticket_Reservation.Models
{

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "User Name can not be blank")]
        public string uname { get; set; }

        [Required(ErrorMessage = "Old Password can not be blank")]
        public string old { get; set; }

        [Required(ErrorMessage = "New Password can not be blank")]
        public string newpwd { get; set; }

        [Required(ErrorMessage = "Confirm Password can not be blank")]
        [System.Web.Mvc.Compare("newpwd", ErrorMessage = "The new password and confirmation password do not match.")]
        public string cnpwd { get; set; }
    }
   
    public class LoginViewModel
    {
        public string type { get; set; }

        [Required(ErrorMessage = "User Name can not be blank")]
        public string uname { get; set; }

        [Required(ErrorMessage = "Password can not be blank")]
        public string pwd { get; set; }

        public List<Add_New_FlightViewModel> flightDetails { get; set; }

    }

    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name can not be blank")]
        [Display(Name = "First Name")]
        public string txtfname { get; set; }

        [Required(ErrorMessage = "Last Name can not be blank")]
        public string txlname { get; set; }

        [Required(ErrorMessage = "EmailID can not be blank")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "EmailId is not Valid")]
        public string txtEmail { get; set; }

        [Required(ErrorMessage = "Mobile Number can not be blank")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Mobile Number is not Valid")]
        public string txtMob { get; set; }

        [Required(ErrorMessage = "Gender can not be blank")]
        public string ddlgender { get; set; }

        [Required(ErrorMessage = "Country can not be blank")]
        public string ddlcountry { get; set; }

        [Required(ErrorMessage = "State can not be blank")]
        public string ddlstate { get; set; }

        [Required(ErrorMessage = "Address can not be blank")]
        public string txtaddress { get; set; }
    }
}
