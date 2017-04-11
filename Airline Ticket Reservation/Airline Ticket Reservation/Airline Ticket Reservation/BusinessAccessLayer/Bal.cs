using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class Bal
    {
        Dal dal = new Dal();
        public DataSet login(string type, string uname, string pwd)
        {
           return dal.login(type, uname, pwd);
        }

        public int UserRegistration(string fname, string lname, string email, string ph, string gender, string country, string state, string add, string pwd)
        {
            return dal.UserRegistration(fname, lname, email, ph, gender, country, state, add, pwd);
        }

        public int ManagerRegistration(string fname, string lname, string email, string ph, string gender, string country, string state, string add, string pwd)
        {
            return dal.ManagerRegistration(fname, lname, email, ph, gender, country, state, add, pwd);
        }

        public int AddFlight(string company, string fnumber, string sour, string dest, string arrdt, string arrti, string depdt, string depti, string amt, string seats)
        {
            return dal.AddFlight(company, fnumber, sour, dest, arrdt, arrti, depdt, depti, amt, seats);
        }

        public DataSet getFlightDetails()
        {
            return dal.getFlightDetails();
        }

        public int updateFlightDetails(string source, string dest, string arrdt, string arrti, string depdt, string depti, string amt, string seats, string id)
        {
            return dal.updateFlightDetails(source, dest, arrdt, arrti, depdt, depti, amt, seats, id);
        }
        public int deleteFlightDetails(string id)
        {
            return dal.deleteFlightDetails(id);
        }

        public DataSet getFlightCompany()
        {
            return dal.getFlightCompany();
        }

        public DataSet getFlightNumber(string company)
        {
            return dal.getFlightNumber(company);
        }

        public SqlDataReader getBookingId()
        {
            return dal.getBookingId();
        }

        public DataSet getFlightDetails(string number)
        {
            return dal.getFlightDetails(number);
        }

        public int UserBooking(string bookingid, string uname, string fname, string fnum, string sour, string dest, string bdate, string arrdt, string arrtime, string depdate, string deptime, int seats, string price, string amt)
        {
            return dal.UserBooking(bookingid, uname, fname, fnum, sour, dest, bdate, arrdt, arrtime, depdate, deptime, seats, price, amt);
        }

        public int payment(string bookingid, string uname,string amount, string cardtype, string cardno, string cvv, string cardholder, string status, string expiredate)
        {
            return dal.payment(bookingid, uname,amount, cardtype, cardno, cvv, cardholder, status, expiredate);
        }

        public DataSet SearchFlightDetails(string source, string dest)
        {
            return dal.SearchFlightDetails(source, dest);
        }

        public DataSet ViewReservation(string uname)
        {
            return dal.ViewReservation(uname);
        }

        public int CancelReservation(string bookingid, string fname, string fno, string seats)
        {
            return dal.CancelReservation(bookingid,fname,fno,seats);
        }

        public int ChangePassword(string uname, string old, string newp)
        {
            return dal.ChangePassword(uname,old,newp);
        }

        public DataSet ViewAllReservations()
        {
            return dal.ViewAllReservations();
        }

        public DataSet getAllReservationForFlightCompany(string company)
        {
            return dal.getAllReservationForFlightCompany(company);
        }

        public DataSet getAllReservationForFlightCompanyAndNumber(string company, string number)
        {
            return dal.getAllReservationForFlightCompanyAndNumber(company,number);
        }

        public int ChangePassword2(string uname, string old, string newp)
        {
            return dal.ChangePassword2(uname, old, newp);
        }

        public DataSet ViewReservation(string uname, string date)
        {
            return dal.ViewReservation(uname,date);
        }

        public DataSet getManagerDetails()
        {
            return dal.getManagerDetails();
        }
    }
}
