using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Airline_Ticket_Reservation.Models;
using System.Data;
using BusinessAccessLayer;

namespace Airline_Ticket_Reservation.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View(@"~\Views\Manager\Home.cshtml");
        }

        public ActionResult FlighDetails()
        {
            var model = new List<Add_New_FlightViewModel>();

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
                addFlight.txtnoofseats = Convert.ToString(row["NumberOfSeats"]);

                model.Add(addFlight);
            }

            return View(@"~\Views\Manager\View_Flight_Details.cshtml", model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            int i = new Bal().ChangePassword2(model.uname, model.old, model.newpwd);
            if (i > 0)
                TempData["message"] = "Password Update Successfully Completed";
            return View(@"~\Views\Manager\ChangePassword.cshtml", model);
        }

        public ActionResult ViewReservations()
        {
            var model = new List<ViewReservations>();

            DataTable dt = new Bal().ViewAllReservations().Tables[0];


            foreach (DataRow row in dt.Rows)
            {
                ViewReservations view = new ViewReservations();
                view.bookingId = Convert.ToString(row["BookingID"]);
                view.username = Convert.ToString(row["UserName"]);
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

            return View(@"~\Views\Manager\View_Reservations.cshtml", model);
        }
    }
}
