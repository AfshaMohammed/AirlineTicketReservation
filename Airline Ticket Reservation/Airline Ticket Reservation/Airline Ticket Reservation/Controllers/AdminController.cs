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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult EditFlight(string FlightNum, string userype)
        {

            DataTable sdr = new Bal().getFlightDetails(FlightNum).Tables[0];
            var flighDetails = new Add_New_FlightViewModel();

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
            return View(@"~\Views\Admin\Edit_New_Flight.cshtml", flighDetails);
        }

        public ActionResult UpdateFlight(string FlightNum, string userype, string isDelete, string isEdit, string source, string dest, string arrdt, string arrti, string depdt, string depti, string amt, string seats)
        {
            if (isDelete == "True")
            {
                new Bal().deleteFlightDetails(FlightNum);
            }
            else
            {

                new Bal().updateFlightDetails(source, dest, arrdt, arrti, depdt, depti, amt, seats, FlightNum);
            }
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
            return View(@"~\Views\Admin\View_Flight_Details.cshtml", model);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View(@"~\Views\Admin\Home.cshtml");
        }

        public ActionResult AddNewFlight()
        {
            return View(@"~\Views\Admin\Add_New_Flight.cshtml");
        }

        [HttpPost]
        public ActionResult AddNewFlight(Add_New_FlightViewModel model)
        {
            int i = new Bal().AddFlight(model.txtfcompany, model.txtfno, model.txtsource, model.txtdestination, model.txtarrdate, model.txtArrivTime, model.txtdepdate, model.txtdeptime, model.txtamt, model.txtnoofseats);
            if (i > 0)
                TempData["message"] = "Flight Details Inserted Successfully";
            return View(@"~\Views\Admin\Add_New_Flight.cshtml", model);
        }

        public ActionResult ManagerRegistration()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult ManagerRegistration(ManagerRegistrationViewModel model)
        {
            string password = GeneratePassword(6);

            int i = new Bal().ManagerRegistration(model.txtfname, model.txlname, model.txtEmail, model.txtMob, model.ddlgender, model.ddlcountry, model.ddlstate, model.txtaddress, password);
            if (i > 0)
                TempData["message"] = "Registration Successfully Completed";
            return View(@"~\Views\Admin\ManagerRegistration.cshtml", model);
        }

        public string GeneratePassword(int lenth)
        {
            string _allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[lenth];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < lenth; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        
        private void sendMail(string email, string password)
        {
            MailMessage sendmail = new MailMessage();
            sendmail.From = new MailAddress("mafsha030@gmail.com");
            sendmail.To.Add(email);
            sendmail.Body = "Welcome to Airline!" + "<br/><br/>" + "You are successfully registered" + "<br/><br/>" + "UserName:" +email+ "Password:" + "";
            sendmail.Subject = "Airline";
            sendmail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("mafsha030@gmail.com", "Afsha123");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(sendmail));
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

            return View(@"~\Views\Admin\View_Flight_Details.cshtml", model);
        }
        public ActionResult ViewManager()
        {
            var model = new List<ManagerRegistrationViewModel>();

            DataTable dt = new Bal().getManagerDetails().Tables[0];


            foreach (DataRow row in dt.Rows)
            {
                ManagerRegistrationViewModel man = new ManagerRegistrationViewModel();
                man.txtfname = Convert.ToString(row["FirstName"]);
                man.txlname = Convert.ToString(row["LastName"]);
                man.txtEmail = Convert.ToString(row["EmailId"]);
                man.txtMob = Convert.ToString(row["MobileNo"]);
                man.ddlgender = Convert.ToString(row["Gender"]);
                man.ddlcountry = Convert.ToString(row["Country"]);
                man.ddlstate = Convert.ToString(row["State"]);
                man.txtaddress = Convert.ToString(row["Address"]);

                model.Add(man);
            }

            return View(@"~\Views\Admin\View_Managers.cshtml", model);
        }
    }
}
