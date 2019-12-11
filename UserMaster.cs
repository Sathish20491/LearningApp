using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTTS.DAL.Tables
{
    public class UserMaster : aTable
    {
        /// <summary>
        /// To get the user details by its credentials
        /// </summary>
        /// <param name="argUsername">User name</param>
        /// <param name="argPassword">Password</param>
        /// <returns>It returns user details as dataset</returns>
        public DataSet GetUserDetailsByCredentials(string argUsername, string argPassword)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "CheckUserIDPassword");
            AddIn("@cShortID", argUsername);
            AddIn("@cPassword", argPassword);
            return GetAsDataSet();
        }

        /// <summary>
        /// It checks short ID already exist or not
        /// </summary>
        /// <param name="argShortID">Short ID</param>
        /// <returns>It return user details for particular short ID</returns>
        public DataSet CheckshortID(string argShortID)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "Check");
            AddIn("@cShortID", argShortID);
            return GetAsDataSet();
        }
        /// <summary>
        /// It get all user details withou constrain
        /// </summary>
        /// <returns>It returns all user details as dataset</returns>
        public DataSet GetAllUserDetails()
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SELECT");
            return GetAsDataSet();
        }

        /// <summary>
        /// It get user detail of particular user ID
        /// </summary>
        /// <param name="argUserID">User ID</param>
        /// <returns>It returns particular user detials as dataset</returns>
        public DataSet GetUserDetailsByUserID(int argUserID)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SelectByID");
            AddIn("@nID", argUserID);
            return GetAsDataSet();
        }
        public DataSet SearchByUserID(int argUserID)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SELECTUSERID");
            AddIn("@nID", argUserID);
            return GetAsDataSet();
        }
        public DataSet GetUserSearchByUserID(int argUserID)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SearchByID");
            AddIn("@nID", argUserID);
            return GetAsDataSet();
        }

        /// <summary>
        /// It save the user details in database
        /// </summary>
        /// <param name="argUsername">User Name</param>
        /// <param name="argUserShortID">User Short ID</param>
        /// <param name="argPassword">Password</param>
        /// <param name="argContactNumber">Contact Number</param>
        /// <param name="argEmail">Email</param>
        /// <param name="argCreatedBy">Created By</param>
        public void CreateUser(string argUsername, string argUserShortID, string argPassword, int argDepartID, string argContactNumber, string argEmail,int argDesignationID, int argLevelID, int argCreatedBy)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "INS");
            AddIn("@cFullName", argUsername);
            AddIn("@cShortID", argUserShortID);
            AddIn("@cPassword", argPassword);
            AddIn("@nDepartmentID", argDepartID);
            AddIn("@cContactNumber", argContactNumber);
            AddIn("@cMailId", argEmail);
            AddIn("@nDesignationID", argDesignationID);
            AddIn("@nLevelID", argLevelID);
            AddIn("@nCreatedBy", argCreatedBy);
            ExecuteNonQuery();

        }

        /// <summary>
        /// It Update the User details in Database.
        /// </summary>
        /// <param name="argnID">UserID</param>
        /// <param name="argUsername">Name of the user</param>
        /// <param name="argUserShortID">Short ID for the User</param>
        /// <param name="argPassword">Password for the user</param>
        /// <param name="argContactNumber">Contact number of the user</param>
        /// <param name="argEmail">Email ID of the user</param>
        /// <param name="argCreatedBy">Creator user ID</param>
        public void UpdateUserDetails(int argnID, string argUsername, string argUserShortID, string argPassword, int argDepartID, string argContactNumber, string argEmail, int argDesignationID, int argLevelID, int argCreatedBy)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "UPDATE");
            AddIn("@nID", argnID);
            AddIn("@cFullName", argUsername);
            AddIn("@cShortID", argUserShortID);
            AddIn("@cPassword", argPassword);
            AddIn("@nDepartmentID", argDepartID);
            AddIn("@cContactNumber", argContactNumber);
            AddIn("@cMailId", argEmail);
            AddIn("@nDesignationID", argDesignationID);
            AddIn("@nLevelID", argLevelID);
            AddIn("@nModifiedBy", argCreatedBy);
            ExecuteNonQuery();
        }

        /// <summary>
        /// Delete User details by UserID
        /// </summary>
        /// <param name="argnID">User ID</param>
        public void DeleteUser(int argnID)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "DELETE");
            AddIn("@nID", argnID);
            ExecuteNonQuery();
        }

        //Tamilpavai 

        public DataSet UserMenuProcess(int UserID)
        {
            InitCommand("SP_CURD_UserMenu");
            AddIn("@CRUD", "MENU");
            AddIn("@nUserID", UserID);            
            return GetAsDataSet();
        }

        public DataSet GetAllUserPermission()
        {
            InitCommand("SP_CURD_UserMenu");
            AddIn("@CRUD", "AllPermission");            
            return GetAsDataSet();
        }
        public DataSet GetpreviousDetails(int argUserID,int argPrevstageID,string argLonnumber)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SELECTUSERPREVIOUSSTAGE");
            AddIn("@nID", argUserID);
            AddIn("@SquenceNo", argPrevstageID);
            AddIn("@cLonNumber", argLonnumber);
            return GetAsDataSet();
        }
        public DataSet SearchByUserDateWise(int argUserID,string argFromdate,string argTodate)
        {
            InitCommand("sp_CRUD_User");
            AddIn("@CRUD", "SELECTUSERBYDATEWISE");
            AddIn("@nID", argUserID);
            AddIn("@dFromDate", argFromdate);
            AddIn("@dToDate",argTodate);
            return GetAsDataSet();
        }
    }
}
