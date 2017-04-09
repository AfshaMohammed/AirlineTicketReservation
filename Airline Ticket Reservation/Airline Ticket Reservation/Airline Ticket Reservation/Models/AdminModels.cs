using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Airline_Ticket_Reservation.Models
{
    public class AdminModels
    {

    }
    public class Add_New_FlightViewModel
    {
        [Required(ErrorMessage = "Company Name can not be blank")]
        public string txtfcompany { get; set; }

        [Required(ErrorMessage = "Seats can not be blank")]
        public string txtnoofseats { get; set; }

        [Required(ErrorMessage = "Flight Number can not be blank")]
        public string txtfno { get; set; }

        [Required(ErrorMessage = "Amount can not be blank")]
        [RegularExpression("[0-9]*", ErrorMessage = "Amount is not Valid")]
        public string txtamt { get; set; }

        [Required(ErrorMessage = "Arrival Date can not be blank")]
        public string txtarrdate { get; set; }

        [Required(ErrorMessage = "Arrival Time can not be blank")]
        public string txtArrivTime { get; set; }

        [Required(ErrorMessage = "Depature Date can not be blank")]
        public string txtdepdate { get; set; }

        [Required(ErrorMessage = "Depature Time can not be blank")]
        public string txtdeptime { get; set; }

        [Required(ErrorMessage = "Source can not be blank")]
        public string txtsource { get; set; }

        [Required(ErrorMessage = "Destination can not be blank")]
        public string txtdestination { get; set; }
    }   

    public class ManagerRegistrationViewModel
    {
        [Required(ErrorMessage = "First Name can not be blank")]
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