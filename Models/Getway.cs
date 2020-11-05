using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Immigration.ViewModel;

namespace Immigration.Models
{
    public class Getway
    {
        public List<UserViewModel> HotelUsers()
        {
            List<UserViewModel> uvList = new List<UserViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "exec HotelUsers";
            SqlCommand comnd = new SqlCommand(command, conn);
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                UserViewModel model = new UserViewModel();
                model.userId = Convert.ToInt32(reader["userId"].ToString());
                model.personName = reader["personName"].ToString();
                model.userName = reader["userName"].ToString();
                model.typeName = reader["typeName"].ToString();
                model.hotelName = reader["hotelName"].ToString();
                model.statusName = reader["statusName"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                uvList.Add(model);
            }
            conn.Close();
            return uvList;
        }
        public List<UserViewModel> ImmigrationUsers()
        {
            List<UserViewModel> uvList = new List<UserViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "exec ImmigrationUsers";
            SqlCommand comnd = new SqlCommand(command, conn);
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                UserViewModel model = new UserViewModel();
                model.userId = Convert.ToInt32(reader["userId"].ToString());
                model.personName = reader["personName"].ToString();
                model.userName = reader["userName"].ToString();
                model.typeName = reader["typeName"].ToString();
                model.statusName = reader["statusName"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                uvList.Add(model);
            }
            conn.Close();
            return uvList;
        }
        public List<HotelViewModel> HotelList()
        {
            List<HotelViewModel> hList = new List<HotelViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "exec HotelsList";
            SqlCommand comnd = new SqlCommand(command, conn);
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                HotelViewModel model = new HotelViewModel();
                model.hotelId = Convert.ToInt32(reader["hotelId"].ToString());
                model.hotelName = reader["hotelName"].ToString();
                model.hotelRegion = reader["hotelRegion"].ToString();
                model.capitalCity = reader["capitalCity"].ToString();
                model.districts = reader["districts"].ToString();
                model.hotelAddress = reader["hotelAddress"].ToString();
                model.contactPersonName = reader["contactPersonName"].ToString();
                model.contactNumber = reader["contactNumber"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                hList.Add(model);
            }
            conn.Close();
            return hList;
        }
        public List<GuestViewModel> GuestsList()
        {
            List<GuestViewModel> gList = new List<GuestViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "exec GuestsList";
            SqlCommand comnd = new SqlCommand(command, conn);
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                GuestViewModel model = new GuestViewModel();
                model.guestId = Convert.ToInt32(reader["guestId"].ToString());
                model.guestName = reader["guestName"].ToString();
                model.guestNationality = reader["guestNationality"].ToString();
                model.passportNumber = reader["passportNumber"].ToString();
                model.guestAddress = reader["guestAddress"].ToString();
                model.inDate = reader["inDate"].ToString();
                model.outDate = reader["outDate"].ToString();
                model.assignedRoomNumber = reader["assignedRoomNumber"].ToString();
                model.guestDocuments = reader["guestDocuments"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                gList.Add(model);
            }
            conn.Close();
            return gList;
        }
        public List<GuestNotSendModel> NotSendGuest(string today)
        {
            List<GuestNotSendModel> gList = new List<GuestNotSendModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spNotSend";            
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@today", today));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                GuestNotSendModel model = new GuestNotSendModel();
                model.No = Convert.ToInt32(reader["No"].ToString());
                model.hotelName = reader["hotelName"].ToString();        
                model.regionName = reader["regionName"].ToString();        
                gList.Add(model);
            }
            conn.Close();
            return gList;
        }
        public string UserRole(string userName)
        {
            string role="";
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spGetRole";            
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@userName", userName));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                role = reader["typeName"].ToString();
            }
            conn.Close();
            return role;
        }
        public List<HotelViewModel> SearchHotels(int regionId, string fromDate, string toDate)
        {
            List<HotelViewModel> hList = new List<HotelViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spSearchHotels";
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@region", regionId));
            comnd.Parameters.Add(new SqlParameter("@fromDate", fromDate));
            comnd.Parameters.Add(new SqlParameter("@toDate", toDate));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                HotelViewModel model = new HotelViewModel();
                model.hotelId = Convert.ToInt32(reader["hotelId"].ToString());
                model.hotelName = reader["hotelName"].ToString();
                model.hotelRegion = reader["hotelRegion"].ToString();
                model.capitalCity = reader["capitalCity"].ToString();
                model.districts = reader["districts"].ToString();
                model.hotelAddress = reader["hotelAddress"].ToString();
                model.contactPersonName = reader["contactPersonName"].ToString();
                model.contactNumber = reader["contactNumber"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                hList.Add(model);
            }
            conn.Close();
            return hList;
        }
        public List<GuestViewModel> SearchGuests(int countryId, string fromDate, string toDate)
        {
            List<GuestViewModel> gList = new List<GuestViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spSearchGuests";
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@country", countryId));
            comnd.Parameters.Add(new SqlParameter("@fromDate", fromDate));
            comnd.Parameters.Add(new SqlParameter("@toDate", toDate));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                GuestViewModel model = new GuestViewModel();
                model.guestId = Convert.ToInt32(reader["guestId"].ToString());
                model.guestName = reader["guestName"].ToString();
                model.guestNationality = reader["guestNationality"].ToString();
                model.passportNumber = reader["passportNumber"].ToString();
                model.guestAddress = reader["guestAddress"].ToString();
                model.inDate = reader["inDate"].ToString();
                model.outDate = reader["outDate"].ToString();
                model.assignedRoomNumber = reader["assignedRoomNumber"].ToString();
                model.guestDocuments = reader["guestDocuments"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                gList.Add(model);
            }
            conn.Close();
            return gList;
        }
        public List<GuestViewModel> SearchGuestsByCapitalDistrictAndHotel(int capital, int district, int hotel, string fromDate, string toDate)
        {
            List<GuestViewModel> gList = new List<GuestViewModel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spGuestsByCapitalDistrictAndHotel";
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@capital", capital));
            comnd.Parameters.Add(new SqlParameter("@district", district));
            comnd.Parameters.Add(new SqlParameter("@hotel", hotel));
            comnd.Parameters.Add(new SqlParameter("@fromDate", fromDate));
            comnd.Parameters.Add(new SqlParameter("@toDate", toDate));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                GuestViewModel model = new GuestViewModel();
                model.guestId = Convert.ToInt32(reader["guestId"].ToString());
                model.guestName = reader["guestName"].ToString();
                model.guestNationality = reader["guestNationality"].ToString();
                model.passportNumber = reader["passportNumber"].ToString();
                model.guestAddress = reader["guestAddress"].ToString();
                model.inDate = reader["inDate"].ToString();
                model.outDate = reader["outDate"].ToString();
                model.assignedRoomNumber = reader["assignedRoomNumber"].ToString();
                model.guestDocuments = reader["guestDocuments"].ToString();
                model.addedBy = reader["addedBy"].ToString();
                model.addedDate = reader["addedDate"].ToString();
                gList.Add(model);
            }
            conn.Close();
            return gList;
        }        
        public List<District> LoadDistricts(int id)
        {
            List<District> dList = new List<District>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spGetDistrictByRegionId";
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@capitalId", id));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                District model = new District();
                model.districtId = Convert.ToInt32(reader["districtId"].ToString());
                model.districtName = reader["districtName"].ToString();
                model.capitalId = Convert.ToInt32(reader["capitalId"].ToString());
                dList.Add(model);
            }
            conn.Close();
            return dList;
        }
        public List<LoadHotel> LoadHotelsByDistricts(int id)
        {
            List<LoadHotel> dList = new List<LoadHotel>();
            string connectionStirng = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionStirng);
            conn.Open();
            string command = "spGetHotelByDistrict";
            SqlCommand comnd = new SqlCommand(command, conn);
            comnd.CommandType = CommandType.StoredProcedure;
            comnd.Parameters.Add(new SqlParameter("@districtId", id));
            SqlDataReader reader = comnd.ExecuteReader();
            while (reader.Read())
            {
                LoadHotel model = new LoadHotel();
                model.hotelId = Convert.ToInt32(reader["hotelId"].ToString());
                model.hotelName = reader["hotelName"].ToString();                
                dList.Add(model);
            }
            conn.Close();
            return dList;
        }
    }
}