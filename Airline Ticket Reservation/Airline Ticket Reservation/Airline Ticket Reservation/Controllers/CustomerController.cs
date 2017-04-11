using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airline_Ticket_Reservation.Models;
using System.Data;
using BusinessAccessLayer;
using System.Data.SqlClient;

namespace Airline_Ticket_Reservation.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Home()
        {
            return View(@"~\Views\Customer\Home.cshtml");
        }
        public ActionResult Message()
        {
            return View(@"~\Views\Customers\Message.cshtml");
        }
        [HttpPost]


        public ActionResult Index()
        {
            return View();
        }

//        #region
//        dropdwon binding
//        public ActionResult BookFlight(BookingFlightViewModel model)
//        {
//            DataTable dt = new Bal().getFlightCompany().Tables[0];
//            model.flightNames = new List<SelectListItem>();
//            List<SelectListItem> lst = new List<SelectListItem>();
//            foreach (DataRow row in dt.Rows)
//            {
//                SelectListItem bookFlight = new SelectListItem();
//                bookFlight.Text = Convert.ToString(row["FlightCompany"]);
//                bookFlight.Value = Convert.ToString(row["FlightCompany"]);
//                lst.Add(bookFlight);
//                model.flightNames.Add(bookFlight);
//            }
//            bookingid();

//            return View(@"~\Views\Customer\BookFlight.cshtml", model);
//        }
//#end

        public void bookingid()
        {
            SqlDataReader sdr = new Bal().getBookingId();
            if (sdr.Read())
            {
                int i = Convert.ToInt32(sdr[0].ToString()) + 1;
                Session["bookingid"] = "Booking0" + i.ToString();
            }            
        }
        
        public ActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Booking(BookingFlightViewModel model)
        {
            bookingid();
            int i = new Bal().UserBooking(Session["bookingid"].ToString(), Session["uname"].ToString(), model.txtfcompany, model.txtfno, model.txtsource, model.txtdestination, model.txtbookingdate, model.txtarrdate, model.txtArrivTime, model.txtdepdate, model.txtdeptime, Convert.ToInt32(model.txtbookingseats), model.txtamt, model.txttotamt);
            if (i > 0)
            {
                TempData["message"] = "Flight Booking Completed\n Please Pay The Amount";
            Session["totalamountt"] = model.txttotamt;
            //return View(@"~\Views\Customer\Payment.cshtml", model);
            return RedirectToAction("Payment");
        }
        else
            return RedirectToAction("Message");
        }

        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Payment model)
        {
            int i = new Bal().payment(Session["bookingid"].ToString(), Session["uname"].ToString(), Session["totalamountt"].ToString(), model.cardtype, model.cardno, model.cvv, model.cname, "Accepted", model.edate);
            if (i > 0)
                TempData["message"] = "Booking & Payment Successfully Completed";
            return RedirectToAction("Home");
        }
    
        private void sendMail(string email, string bookingid)
        {
            MailMessage sendmail = new MailMessage();
            sendmail.From = new MailAddress("mafsha030@gmailcom");
            sendmail.To.Add(email);
            sendmail.Body = "Welcome to Airline" + "<br/><br/>" + "Have a happy and safe Journey!" + "<br/> Your Booking Id is <br/>" + bookingid + "";
            sendmail.Subject = "Airline";
            sendmail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("mafsha030@gmail.com", "Afsha123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(sendmail);
        }
        public ActionResult History()
        {
            var model = new List<ViewReservations>();

            DataTable dt = new Bal().ViewReservation(Session["uname"].ToString()).Tables[0];


            foreach (DataRow row in dt.Rows)
            {
                ViewReservations view = new ViewReservations();
                view.bookingId = Convert.ToString(row["BookingID"]);
                view.bookingDate = Convert.ToString(row["BookingDate"]);
                view.company = Convert.ToString(row["FlightName"]);
                view.number = Convert.ToString(row["FlightNumber"]);
                view.source = Convert.ToString(row["Source"]);
                view.destination = Convert.ToString(row["Destination"]);
                view.arrivalDate = Convert.ToString(row["ArrivalDate"]);
                view.arrivTime = Convert.ToString(row["Arrivaltime"]);
                view.depDate = Convert.ToString(row["DepatureDate"]);
                view.depTime = Convert.ToString(row["Deptime"]);
                view.seats = Convert.ToString(row["NumberOfSeatsBooking"]);
                view.price = Convert.ToString(row["Price"]);
                view.amount = Convert.ToString(row["Amount"]);
                view.status = Convert.ToString(row["Status"]);

                model.Add(view);
            }
            return View(@"~\Views\Customer\ViewReservationdetails.cshtml", model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            int i = new Bal().ChangePassword(model.uname, model.old, model.newpwd);
            if (i > 0)
                TempData["message"] = "Password Update Successfully Completed";
            return View(@"~\Views\Customer\ChangePassword.cshtml", model);

        }

        public ActionResult Search()
        {
            //now its working  mama don't do any changes
            var model = new List<Add_New_FlightViewModel>();
            if (Session["txtsource"] != null)
            {
                string from = Convert.ToString(Session["txtsource"]);
                string to = Convert.ToString(Session["txtdestination"]);
                DataTable dt = new Bal().SearchFlightDetails(from, to).Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Add_New_FlightViewModel Flight = new Add_New_FlightViewModel();
                    Flight.txtfcompany = Convert.ToString(row["FlightCompany"]);
                    Flight.txtfno = Convert.ToString(row["FlightNumber"]);
                    Flight.txtsource = Convert.ToString(row["Source"]);
                    Flight.txtdestination = Convert.ToString(row["Destination"]);
                    Flight.txtarrdate = Convert.ToString(row["ArrivalDate"]);
                    Flight.txtArrivTime = Convert.ToString(row["Arrivaltime"]);
                    Flight.txtdepdate = Convert.ToString(row["DepatureDate"]);
                    Flight.txtdeptime = Convert.ToString(row["Deptime"]);
                    Flight.txtamt = Convert.ToString(row["PerSeatAmount"]);
                    Flight.txtnoofseats = Convert.ToString(row["AvailableSeats"]);

                    model.Add(Flight);
                }
            }

            else
            {
                DataTable dt = new Bal().getFlightDetails().Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Add_New_FlightViewModel addFlight = new Add_New_FlightViewModel();
                    addFlight.txtfcompany = Convert.ToString(row["FlightCompany"]);
                    addFlight.txtfno = Convert.ToString(row["FlightNumber"]);
                    addFlight.txtsource = Convert.ToString(row["Source"]);
                    addFlight.txtdestination = Convert.ToString(row["Destination"]);
                    addFlight.txtarrdate = Convert.ToString(row["ArrivalDate"]);
                    addFlight.txtArrivTime = Convert.ToString(row["Arrivaltime"]);
                    addFlight.txtdepdate = Convert.ToString(row["DepatureDate"]);
                    addFlight.txtdeptime = Convert.ToString(row["Deptime"]);
                    addFlight.txtamt = Convert.ToString(row["PerSeatAmount"]);
                    addFlight.txtnoofseats = Convert.ToString(row["AvailableSeats"]);

                    model.Add(addFlight);
                }
            }
            return View(@"~\Views\Customer\SearchFlight.cshtml", model);
        }

        [HttpPost]
        public ActionResult Search2(Add_New_FlightViewModel s)
        {
            string fromstring = string.Empty;
            string tostring = string.Empty;

            string from=Request["txtsource"];
            string to=Request["txtdestination"];
           
            if (from!= ""&&from!=null)
            {
                Session["txtsource"] = from;
                
                fromstring = Session["txtsource"].ToString();
            }
            if (to != "" && to != null)
            {
                Session["txtdestination"] = to;
                tostring = Session["txtdestination"].ToString();
            }
            var model = new List<Add_New_FlightViewModel>();
            if (fromstring != null && tostring != null)
            {
                DataTable dt = new Bal().SearchFlightDetails(s.txtsource, s.txtdestination).Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Add_New_FlightViewModel Flight = new Add_New_FlightViewModel();
                    Flight.txtfcompany = Convert.ToString(row["FlightCompany"]);
                    Flight.txtfno = Convert.ToString(row["FlightNumber"]);
                    Flight.txtsource = Convert.ToString(row["Source"]);
                    Flight.txtdestination = Convert.ToString(row["Destination"]);
                    Flight.txtarrdate = Convert.ToString(row["ArrivalDate"]);
                    Flight.txtArrivTime = Convert.ToString(row["Arrivaltime"]);
                    Flight.txtdepdate = Convert.ToString(row["DepatureDate"]);
                    Flight.txtdeptime = Convert.ToString(row["Deptime"]);
                    Flight.txtamt = Convert.ToString(row["PerSeatAmount"]);
                    Flight.txtnoofseats = Convert.ToString(row["NumberOfSeats"]);
                    Flight.txtavailseats = Convert.ToString(row["AvailableSeats"]);

                    model.Add(Flight);
                }
            }
            ViewBag.source = from;
            ViewBag.destination = to;
            //return View(@"~\Views\Customer\SearchFlight.cshtml", model);
            return RedirectToAction("Search");
        }

        public ActionResult FlightDetailss(string FlightNum, string userype)
        {
            DataTable sdr = new Bal().getFlightDetails(FlightNum).Tables[0];
            var flighDetails = new BookingFlightViewModel();

            foreach (DataRow row in sdr.Rows)
            {
                if (FlightNum == row["FlightNumber"].ToString())
                {
                    flighDetails.txtfcompany = row["FlightCompany"].ToString();
                    flighDetails.txtsource = row["Source"].ToString();
                    flighDetails.txtdestination = row["Destination"].ToString();
                    flighDetails.txtarrdate = row["ArrivalDate"].ToString();
                    flighDetails.txtArrivTime = row["Arrivaltime"].ToString();
                    flighDetails.txtdepdate = row["DepatureDate"].ToString();
                    flighDetails.txtdeptime = row["Deptime"].ToString();
                    flighDetails.txtnoofseats = row["NumberOfSeats"].ToString();
                    flighDetails.txtamt = row["PerSeatAmount"].ToString();
                    flighDetails.txtfno = row["FlightNumber"].ToString();
                }
            }
           
            return View(@"~\Views\Customer\FlightBooking.cshtml", flighDetails);
    }
}
