using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Airline_Ticket_Reservation.Models;
using System.Data;
using BusinessAccessLayer;

namespace Airline_Ticket_Reservation.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }


      


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            DataSet ds = new Bal().login(model.type, model.uname, model.pwd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (model.type == "Admin")
                {
                    Session["aname"] = model.uname;
                    //return Redirect(@"~\Views\Admin\Add_New_Flight?userTyp=" + model.type);
                    return RedirectToAction("Home", "Admin", new { userype = model.type });                    
                }
                if (model.type == "Manager")
                {
                    Session["mname"] = model.uname;
                    return RedirectToAction("Home", "Manager", new { userype = model.type });                    
                    //return Redirect(@"~\Views\Manager\Add_New_Flight.cshtml");
                }
                if (model.type == "User")
                {
                    Session["uname"] = model.uname;
                    Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    return RedirectToAction("Home", "Customer", new { userype = model.type });                    
                }
            }
            return View(model);
        }

        public ActionResult Guest()
        {
            return RedirectToAction("Home", "Customer", new { userype = "Guest" });
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home", new { logout = "true" });
        }
        
        //
        // GET: /Account/Register

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            string password = GeneratePassword(6);

            int i = new Bal().UserRegistration(model.txtfname, model.txlname, model.txtEmail, model.txtMob, model.ddlgender, model.ddlcountry, model.ddlstate, model.txtaddress, password);
            if (i > 0)
                TempData["message"] = "Registration Successfully Completed";
            return View(@"~\Views\Registration.cshtml", model);
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
        

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
