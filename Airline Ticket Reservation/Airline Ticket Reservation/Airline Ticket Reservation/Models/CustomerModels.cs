using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Airline_Ticket_Reservation.Models
{
    public class CustomerModels
    {
      
    }

    public class BookingFlightViewModel
    {
        public string txtfcompany { get; set; }
        public string txtnoofseats { get; set; }
        public string txtavailseats {get; set;}
        public string txtfno { get; set; }
        public string txtamt { get; set; }
        public string txtarrdate { get; set; }
        public string txtArrivTime { get; set; }
        public string txtdepdate { get; set; }
        public string txtdeptime { get; set; }
        public string txtsource { get; set; }
        public string txtdestination { get; set; }

        [Required(ErrorMessage = "Booking Date can not be blank")]
        public string txtbookingdate { get; set; }

        [Required(ErrorMessage = "Booking Seats can not be blank")]
        [RegularExpression("[0-9]*", ErrorMessage = "Enter Seats is not Valid")]
        public string txtbookingseats { get; set; }

        public string txttotamt { get; set; }

        public string bookingidd { get; set; }

        public List<SelectListItem> flightNames { get; set; }
    }

    public class FlightNames
    {
        public string flightName { get; set; }
    }

    public class Payment
    {
        public string amount { get; set; }

        [Required(ErrorMessage = "Customer Name can not be blank")]
        public string cname { get; set; }

        [Required(ErrorMessage = "CVV Number can not be blank")]
        [RegularExpression("[0-9]{3}", ErrorMessage = "CVV Number is not Valid")]
        public string cvv { get; set; }

        [Required(ErrorMessage = "Card Number can not be blank")]
        [RegularExpression("[0-9]{16}", ErrorMessage = "Card Number is not Valid")]
        public string cardno { get; set; }

        [Required(ErrorMessage = "Expiry Date can not be blank")]
        public string edate { get; set; }

        public string cardtype { get; set; }
    }

    public class ViewReservations
    {
        public string username { get; set; }
        public string bookingId { get; set; }
        public string company { get; set; }        
        public string number { get; set; }
        public string arrivalDate { get; set; }        
        public string arrivTime { get; set; }
        public string depDate { get; set; }
        public string depTime { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string bookingDate { get; set; }        
        public string seats { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
        public string status { get; set; }

        public List<SelectListItem> flightNames { get; set; }
    }
    public class Search
    {
        [Required(ErrorMessage = "Source can not be blank")]
        public string source { get; set; }

        [Required(ErrorMessage = "Destination can not be blank")]
        public string destination { get; set; }
    }
}
