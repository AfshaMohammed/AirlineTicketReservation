using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{    
    public class Dal
    {
        // SqlConnection con = new SqlConnection("data source=CHINNU-PC;database=AirlinesWith3Tier;integrated security=true");
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=Database1;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter sda;
        SqlDataReader sdr;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        int res;

        public DataSet login(string type, string uname, string pwd)
        {
            if (con.State == ConnectionState.Open)            
                con.Close();            
            con.Open();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;
            if (type == "Admin")
            {                
                cmd.CommandText = "select UserName,Password from Login where UserName='" + uname + "'and Password ='" + pwd + "'";                                
            }
            if (type == "Manager")
            {
                cmd.CommandText = "select EmailId,Password from ManagerRegistration where EmailId='" + uname + "'and Password ='" + pwd + "'";
            }
            if (type == "User")
            {
                cmd.CommandText = "select EmailId,Password,MobileNo from UserRegistration where EmailId='" + uname + "'and Password ='" + pwd + "'";
            }
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public SqlDataReader getCountry()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct(Country) from States";
            cmd.Connection = con;
            sdr = cmd.ExecuteReader();
            return sdr;
        }

        public SqlDataReader getStates(string country)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select State from States where Country='" + country + "'";
            cmd.Connection = con;
            sdr = cmd.ExecuteReader();
            return sdr;
        }

        public int UserRegistration(string fname, string lname, string email, string ph, string gender, string country, string state, string add, string pwd)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into UserRegistration values( '" + fname + "','" + lname + "','" + email + "','" + ph + "','" + gender + "','" + country + "','" + state + "','" + add + "','" + pwd + "')";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }
        public int ManagerRegistration(string fname, string lname, string email, string ph, string gender, string country, string state, string add, string pwd)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into ManagerRegistration values( '" + fname + "','" + lname + "','" + email + "','" + ph + "','" + gender + "','" + country + "','" + state + "','" + add + "','" + pwd + "')";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public int AddFlight(string company, string fnumber, string sour, string dest, string arrdt, string arrti, string depdt, string depti, string amt, string seats)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Addflight values( '" + company + "','" + fnumber + "','" + sour + "','" + dest + "','" + arrdt + "','" + arrti + "','" + depdt + "','" + depti + "','" + amt + "','" + seats + "','" + seats + "')";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public DataSet getFlightDetails()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Addflight";
            cmd.Connection = con;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public int updateFlightDetails(string source, string dest, string arrdt, string arrti, string depdt, string depti, string amt, string seats, string id)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update Addflight set ArrivalDate='" + arrdt + "',Arrivaltime='" + arrti + "',DepatureDate='" + depdt + "',Deptime='" + depti + "',PerSeatAmount='" + amt + "',NumberOfSeats='" + seats + "',Source='" + source + "', Destination='" + dest + "'where FlightNumber='" + id + "'";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public int deleteFlightDetails(string id)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from Addflight where FlightNumber='" + id + "'";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public DataSet getFlightCompany()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select distinct(FlightCompany) from Addflight";
            cmd.Connection = con;           
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
            
        }

        public DataSet getFlightNumber(string company)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select FlightNumber from Addflight where FlightCompany='" + company + "'";
            cmd.Connection = con;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public SqlDataReader getBookingId()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from UserBooking";
            cmd.Connection = con;
            sdr = cmd.ExecuteReader();
            return sdr;
        }

        public DataSet getFlightDetails(string number)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select FlightNumber,FlightCompany,ArrivalDate,DepatureDate,NumberOfSeats,Source,Destination,PerSeatAmount,Arrivaltime,Deptime from Addflight where FlightNumber='" + number + "'";
            cmd.Connection = con;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public int UserBooking(string bookingid, string uname, string fname, string fnum, string sour, string dest, string bdate, string arrdt, string arrtime, string depdate, string deptime, int seats, string price, string amt)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_FlightBooking";
            cmd.Parameters.AddWithValue("@BookingID", bookingid);
            cmd.Parameters.AddWithValue("@UserName", uname);
            cmd.Parameters.AddWithValue("@FlightName", fname);
            cmd.Parameters.AddWithValue("@FlightNumber", fnum);
            cmd.Parameters.AddWithValue("@BookingDate", bdate);
            cmd.Parameters.AddWithValue("@Source", sour);
            cmd.Parameters.AddWithValue("@Destination", dest);
            cmd.Parameters.AddWithValue("@ArrivalDate", arrdt);
            cmd.Parameters.AddWithValue("@Arrivaltime", arrtime);
            cmd.Parameters.AddWithValue("@DepatureDate", depdate);
            cmd.Parameters.AddWithValue("@Deptime", deptime);
            cmd.Parameters.AddWithValue("@NumberOfSeatsBooking", seats);            
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Amount", amt);
            cmd.Parameters.Add("@input", SqlDbType.Int);
            cmd.Parameters["@input"].Direction = ParameterDirection.Output;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            res = Convert.ToInt32(cmd.Parameters["@input"].Value);
            return res;
        }

        public int payment(string bookingid, string uname,string amount, string cardtype, string cardno, string cvv, string cardholder, string status, string expiredate)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            string query = "insert into UserPayment values('" + bookingid + "','" + uname + "','"+amount+"','" + cardtype + "' ," + cardno + "," + cvv + ",'" + cardholder + "','" + expiredate + "')";
            query += "update UserBooking set Status='" + status + "' where BookingID='" + bookingid + "'";
            cmd.CommandText = query;
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public DataSet SearchFlightDetails(string source, string dest)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Addflight where Source like '%'+'" + source + "'+'%' and Destination like '%'+'" + dest + "'+'%'";
            cmd.Connection = con;
            sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public DataSet ViewReservation(string uname)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from UserBooking where UserName='" + uname + "' and Status!='Pending'";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public int CancelReservation(string bookingid, string fname, string fno, string seats)
        {
            if (con.State == ConnectionState.Open)
                con.Close();           
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sp_CancelBooking";
            cmd.Parameters.AddWithValue("@BookingID", bookingid);
            cmd.Parameters.AddWithValue("@FlightName", fname);
            cmd.Parameters.AddWithValue("@FlightNumber", fno);
            cmd.Parameters.AddWithValue("@NumberOfSeatsBooking", seats);
            cmd.Connection = con;
            int i = cmd.ExecuteNonQuery();
            return i;
        }

        public int ChangePassword(string uname, string old, string newp)
        {
            if (con.State == ConnectionState.Open)
                con.Close(); 
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update UserRegistration set Password='" + newp +"' where EmailId='" + uname +"' and Password='" + old +"'";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public DataSet ViewAllReservations()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from UserBooking";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataSet getAllReservationForFlightCompany(string company)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from UserBooking where FlightName='" + company + "'";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataSet getAllReservationForFlightCompanyAndNumber(string company, string number)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from UserBooking where FlightName='" + company + "' and FlightNumber='" + number + "'";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public int ChangePassword2(string uname, string old, string newp)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update ManagerRegistration set Password='" + newp + "' where EmailId='" + uname + "' and Password='" + old + "'";
            cmd.Connection = con;
            res = cmd.ExecuteNonQuery();
            return res;
        }

        public DataSet ViewReservation(string uname,string date)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
             //between convert(varchar(10),GETDATE(),105) and DATEADD(mm,2,ArrivalDate)
            cmd.CommandText = "select * from UserBooking where UserName='" + uname + "' and ArrivalDate>='"+date+"'";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataSet getManagerDetails()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select FirstName, LastName, EmailId, MobileNo, Gender, Country, State, Address FROM ManagerRegistration";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}