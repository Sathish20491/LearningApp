using PTTS.BL.Common;
using PTTS.BL.Entities;
using PTTS.BL.Exceptions;
using PTTS.DAL.Tables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PTTS.BL.Controllers
{
    public class UserCreationController : aController
    {
        public ClsUserDetails CheckUserIDPassword(string userID, string password)
        {
            ValidationException valex = new ValidationException();
            UserMaster Table = new UserMaster();
            if (userID.IsNullOrWhiteSpace() && password.IsNullOrWhiteSpace())
            {
                valex.AddError("Please Enter UserID And Password");
            }
            else if (userID.IsNullOrWhiteSpace())
            {
                valex.AddError("Please Enter UserID");
            }
            else if (password.IsNullOrWhiteSpace())
            {
                valex.AddError("Please Enter Password");
            }
            if (valex.errorList.IsNotEmpty())
                throw valex;

            DataSet ds = Table.GetUserDetailsByCredentials(userID, Encrypt(password));

            if (IsvalidDataset(ds))
            {
                if (ds.Tables[0].Rows[0]["nID"].IsNotNull())
                {
                    int id = ds.Tables[0].Rows[0]["nID"].ToString().ToInt();

                    if (id > 0)
                    {
                        ClsUserDetails details = new ClsUserDetails();
                        //details.UserRowID = id;

                        if (ds.Tables[0].Rows[0]["nID"].IsNotNull())
                            details.UserID = ds.Tables[0].Rows[0]["nID"].ToString();

                        if (ds.Tables[0].Rows[0]["cFullName"].IsNotNull())
                            details.UserName = ds.Tables[0].Rows[0]["cFullName"].ToString();

                        if (ds.Tables[0].Rows[0]["cShortID"].IsNotNull())
                            details.ShortID = ds.Tables[0].Rows[0]["cShortID"].ToString();

                        if (ds.Tables[0].Rows[0]["cMailId"].IsNotNull())
                            details.Email = ds.Tables[0].Rows[0]["cMailId"].ToString();

                        if (ds.Tables[0].Rows[0]["cContactNumber"].IsNotNull())
                            details.ContactNo = ds.Tables[0].Rows[0]["cContactNumber"].ToString();

                        if (ds.Tables[0].Rows[0]["cPassword"].IsNotNull())
                            details.Password = Decrypt(ds.Tables[0].Rows[0]["cPassword"].ToString());

                        if (ds.Tables[0].Rows[0]["nLevelID"].IsNotNull())
                            details.LeveID = Convert.ToInt32(ds.Tables[0].Rows[0]["nLevelID"].ToString());

                        if (ds.Tables[0].Rows[0]["nDesignationID"].IsNotNull())
                            details.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDesignationID"].ToString());

                        if (ds.Tables[0].Rows[0]["nDepartmentID"].IsNotNull())
                            details.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDepartmentID"].ToString());

                        return details;
                    }
                }
            }
            else
            {
                valex.AddError("Incorrect User Name or Password");
                throw valex;
            }

            return null;
        }

        public List<ClsUserDetails> GetAllUserDetails()
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.GetAllUserDetails();
            List<ClsUserDetails> User_List = new List<ClsUserDetails>();
            ClsUserDetails details;

            if (HasData(ds, 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i] != null)
                    {
                        details = new ClsUserDetails();
                        details.UserID = ds.Tables[0].Rows[i]["nID"].ToString();
                        details.UserName = ds.Tables[0].Rows[i]["cFullName"].ToString();
                        details.ShortID = ds.Tables[0].Rows[i]["cShortID"].ToString();
                        details.Email = ds.Tables[0].Rows[i]["cMailId"].ToString();
                        details.ContactNo = ds.Tables[0].Rows[i]["cContactNumber"].ToString();
                        details.Password = Decrypt(ds.Tables[0].Rows[i]["cPassword"].ToString());
                        details.LeveID = Convert.ToInt32(ds.Tables[0].Rows[i]["nLevelID"].ToString());
                        details.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[i]["nDesignationID"].ToString());
                        details.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["nDepartmentID"].ToString());
                        details.RoleIds = Convert.ToInt32(ds.Tables[0].Rows[i]["nRoleID"].ToString());
                        details.LeveName = ds.Tables[0].Rows[i]["LevelName"].ToString();
                        details.DesignationName = ds.Tables[0].Rows[i]["Designation"].ToString();
                        details.DepartmentName = ds.Tables[0].Rows[i]["Department"].ToString();
                        details.OrgshortID = ds.Tables[0].Rows[i]["OrgShortId"].ToString();
                        User_List.Add(details);
                    }
                }
            }

            return User_List;
        }

        public ClsUserDetails GetUserDetailsByUserID(int argUserID)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.GetUserDetailsByUserID(argUserID);
            ClsUserDetails details = new ClsUserDetails();

            if (HasData(ds, 0))
            {
                if (ds.Tables[0].Rows[0] != null)
                {
                    details.UserID = ds.Tables[0].Rows[0]["nID"].ToString();
                    details.UserName = ds.Tables[0].Rows[0]["cFullName"].ToString();
                    details.ShortID = ds.Tables[0].Rows[0]["cShortID"].ToString();
                    details.Email = ds.Tables[0].Rows[0]["cMailId"].ToString();
                    details.ContactNo = ds.Tables[0].Rows[0]["cContactNumber"].ToString();
                    details.Password = Decrypt(ds.Tables[0].Rows[0]["cPassword"].ToString());
                    details.LeveID = Convert.ToInt32(ds.Tables[0].Rows[0]["nLevelID"].ToString());
                    details.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDesignationID"].ToString());
                    details.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDepartmentID"].ToString());
                }
            }

            return details;
        }

        public List<ClsUserDetails> UserSearchByID(int argUserID)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.SearchByUserID(argUserID);
            DataTable dt = ds.Tables[0];
            List<ClsUserDetails> ObjectDetails = new List<ClsUserDetails>();
            DataTable dtRemoveDuplicate = new DataTable();

            if (HasData(ds, 0))
            {
                if (ds.Tables[0].Rows[0] != null)
                {
                    dtRemoveDuplicate = ds.Tables[0];
                    string name = "", name1 = "";
                    for (int i = 0; i < dtRemoveDuplicate.Rows.Count; i++)
                    {
                        ClsUserDetails details = new ClsUserDetails();
                        details.UserID = dtRemoveDuplicate.Rows[i]["nID"].ToString();
                        details.ShortID = dtRemoveDuplicate.Rows[i]["cShortID"].ToString();
                        details.LonNumber = dtRemoveDuplicate.Rows[i]["cLONNumber"].ToString();
                        details.DesignationName = dtRemoveDuplicate.Rows[i]["cProcessDesc"].ToString();
                        details.Remark = dtRemoveDuplicate.Rows[i]["cRemarks"].ToString();
                        details.currentStep = dtRemoveDuplicate.Rows[i]["CurrentStep"].ToString();
                        details.ProcessID = Convert.ToInt32(dtRemoveDuplicate.Rows[i]["ProcessID"].ToString());
                        details.NsequenceNO = dtRemoveDuplicate.Rows[i]["nSequence"].ToString();
                        details.DepartmentName = dtRemoveDuplicate.Rows[i]["DepartmentName"].ToString();
                        details.DCreatedOn = dtRemoveDuplicate.Rows[i]["dCreatedOn"].ToString();
                        details.DLastModification = dtRemoveDuplicate.Rows[i]["LastMadification"].ToString();
                        details.NtotalDays = dtRemoveDuplicate.Rows[i]["TotNoDays"].ToString();
                        details.PRNumbers = dtRemoveDuplicate.Rows[i]["PRNumber"].ToString();
                        details.colors = "#f0f0f5";

                        if (i < dtRemoveDuplicate.Rows.Count - 1)
                        {
                            if (details.LonNumber == dtRemoveDuplicate.Rows[i + 1]["cLONNumber"].ToString())
                            {
                                details.hidds = "none";
                                name = dtRemoveDuplicate.Rows[i]["cLONNumber"].ToString();
                            }
                            else
                            {
                                details.hidds = "none";
                            }
                        }
                        else
                        {
                            details.hidds = "none";
                        }
                        ObjectDetails.Add(details);
                    }
                }
            }
            return ObjectDetails;
        }

        protected DataTable DeleteDuplicateFromDataTable(DataTable dtDuplicate,
                                                      string columnName)
        {
            Hashtable hashT = new Hashtable();
            ArrayList arrDuplicate = new ArrayList();
            foreach (DataRow row in dtDuplicate.Rows)
            {
                if (hashT.Contains(row[columnName]))
                    arrDuplicate.Add(row);
                else
                    hashT.Add(row[columnName], string.Empty);
            }
            foreach (DataRow row in arrDuplicate)
                dtDuplicate.Rows.Remove(row);

            return dtDuplicate;
        }

        public ClsUserDetails GetUserSearchByID(int argUserID)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.GetUserSearchByUserID(argUserID);
            ClsUserDetails details = new ClsUserDetails();

            if (HasData(ds, 0))
            {
                if (ds.Tables[0].Rows[0] != null)
                {
                    details.UserID = ds.Tables[0].Rows[0]["nID"].ToString();
                    details.UserName = ds.Tables[0].Rows[0]["cFullName"].ToString();
                    details.ShortID = ds.Tables[0].Rows[0]["cShortID"].ToString();
                    details.Email = ds.Tables[0].Rows[0]["cMailId"].ToString();
                    details.ContactNo = ds.Tables[0].Rows[0]["cContactNumber"].ToString();
                    details.Password = Decrypt(ds.Tables[0].Rows[0]["cPassword"].ToString());
                    details.LeveID = Convert.ToInt32(ds.Tables[0].Rows[0]["nLevelID"].ToString());
                    details.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDesignationID"].ToString());
                    details.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[0]["nDepartmentID"].ToString());
                }
            }

            return details;
        }

        public void CreateUser(string argUserName, string argUserShortID, string argPassword, int argDepartID, string argEmail, int argDesignationID, int argLevelID, string argContact, int argCreatedBy)
        {
            ValidationException error = new ValidationException();
            UserMaster Table = new UserMaster();

            if (Validations.IsEmpty(argUserShortID))
                error.AddError("Enter Short ID");

            if (Validations.IsEmpty(argUserName))
                error.AddError("Enter User Name");

            if (Validations.IsEmpty(argPassword))
                error.AddError("Enter Password");

            if (Validations.IsEmpty(argContact))
                error.AddError("Enter Mobile Number");

            if (Validations.IsEmpty(argEmail))
                error.AddError("Enter Email");

            if (!Validations.IsEmail(argEmail, true).IsValid)
                error.AddError("Enter Valid Email ID");

            //if (!Validations.IsNumber(argContact))
            //    error.AddError("Enter Number only");

            if (!error.isValid)
                throw error;

            DataSet DSUserCheck = Table.CheckshortID(argUserShortID);

            if (DSUserCheck != null && DSUserCheck.Tables.Count > 0)
            
                if (int.Parse(DSUserCheck.Tables[0].Rows[0]["Cnt"].ToString()) > 0)
                    throw new UserNonAvailabiltyException();
                else
                    Table.Connection.beginTransact();

            try
            {
                Table.CreateUser(argUserName, argUserShortID, argPassword.Encrypt(), argDepartID, argContact, argEmail, argDesignationID, argLevelID, argCreatedBy);
                Table.Connection.commit();
            }
            catch (Exception ex)
            {
                Table.Connection.rollback();
                throw ex;
            }
        }
        public void UpdateUserDetails(int argnID, string argUsername, string argUserShortID, string argPassword, int argDepartID, string argContactNumber, string argEmail, int argDesignationID, int argLevelID, int argCreatedBy)
        {
            ValidationException error = new ValidationException();
            UserMaster Table = new UserMaster();

            if (Validations.IsEmpty(argUserShortID))
                error.AddError("Enter Short ID");

            if (Validations.IsEmpty(argUsername))
                error.AddError("Enter User Name");

            if (Validations.IsEmpty(argPassword))
                error.AddError("Enter Password");

            if (Validations.IsEmpty(argContactNumber))
                error.AddError("Enter ContactNumber");

            if (Validations.IsEmpty(argEmail))
                error.AddError("Enter Email");

            if (!Validations.IsEmail(argEmail, true).IsValid)
                error.AddError("Enter Valid Email ID");

            if (!Validations.IsNumber(argContactNumber))
                error.AddError("Enter Number only");

            if (!error.isValid)
                throw error;

            DataSet DSUserCheck = Table.CheckshortID(argUsername);

            if (DSUserCheck != null && DSUserCheck.Tables.Count > 0)

                if (int.Parse(DSUserCheck.Tables[0].Rows[0]["Cnt"].ToString()) > 0)
                    throw new UserNonAvailabiltyException();
                else
                    Table.Connection.beginTransact();
            try
            {
                Table.UpdateUserDetails(argnID, argUsername, argUserShortID, argPassword.Encrypt(), argDepartID, argContactNumber, argEmail, argDesignationID, argLevelID, argCreatedBy);
                Table.Connection.commit();
            }
            catch (Exception ex)
            {
                Table.Connection.rollback();
                throw ex;
            }
        }

        public void DeleteUser(int argnID)
        {
            UserMaster Table = new UserMaster();
            Table.DeleteUser(argnID);
        }

        //Tamilpavai
        public List<ClsUserProcesscs> UserMenuProcess(int UserID)
        {
            UserMaster Tables = new UserMaster();
            DataSet ds = Tables.UserMenuProcess(UserID);
            List<ClsUserProcesscs> ToolsList = new List<ClsUserProcesscs>();
            ClsUserProcesscs DetailList;

            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i] != null)
                    {
                        DetailList = new ClsUserProcesscs();
                        DetailList.USerID = Convert.ToInt32(ds.Tables[0].Rows[i]["USerID"].ToString());
                        DetailList.ShortID = ds.Tables[0].Rows[i]["ShortID"].ToString();
                        DetailList.FullName = ds.Tables[0].Rows[i]["FullName"].ToString();
                        DetailList.DepartmentID = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentID"].ToString());
                        DetailList.DesignationID = Convert.ToInt32(ds.Tables[0].Rows[i]["DesignationID"].ToString());
                        DetailList.RoleName = ds.Tables[0].Rows[i]["RoleName"].ToString();
                        DetailList.RoleID = Convert.ToInt32(ds.Tables[0].Rows[i]["RoleID"].ToString());
                        DetailList.ProcessID = Convert.ToInt32(ds.Tables[0].Rows[i]["ProcessID"].ToString());
                        DetailList.ProcessKey = ds.Tables[0].Rows[i]["ProcessKey"].ToString();
                        DetailList.ProcessDesc = ds.Tables[0].Rows[i]["ProcessDesc"].ToString();

                        ToolsList.Add(DetailList);
                    }
                }
                return ToolsList;
            }
        }

        public DataSet GetPermission(int argnID)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = new DataSet();

            ds = Table.UserMenuProcess(argnID);

            return ds;
        }

        public List<ClsUserProcesscs> GetAllUserPermission()
        {
            UserMaster Tables = new UserMaster();
            DataSet ds = Tables.GetAllUserPermission();
            List<ClsUserProcesscs> _list = new List<ClsUserProcesscs>();
            ClsUserProcesscs DetailList;

            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i] != null)
                    {
                        DetailList = new ClsUserProcesscs();
                        DetailList.ProcessID = Convert.ToInt32(ds.Tables[0].Rows[i]["ProcessID"].ToString());
                        DetailList.ProcessKey = ds.Tables[0].Rows[i]["ProcessKey"].ToString();
                        DetailList.ProcessDesc = ds.Tables[0].Rows[i]["ProcessDesc"].ToString();
                        _list.Add(DetailList);
                    }
                }
                return _list;
            }
        }

        public List<ClsUserDetails> GetPreviousStageDetails(int argUserID, int stageDetails, string LonNumber)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.GetpreviousDetails(argUserID, stageDetails, LonNumber);
            List<ClsUserDetails> ObjectDetails = new List<ClsUserDetails>();
            if (HasData(ds, 0))
            {
                if (ds.Tables[0].Rows[0] != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ClsUserDetails details = new ClsUserDetails();
                        details.UserID = ds.Tables[0].Rows[i]["nID"].ToString();
                        ObjectDetails.Add(details);
                    }
                }
            }
            return ObjectDetails;
        }

        public List<ClsUserDetails> UserSearchbyDateWise(int argUserID, string argFromdate, string argTodate)
        {
            UserMaster Table = new UserMaster();
            DataSet ds = Table.SearchByUserDateWise(argUserID, argFromdate, argTodate);
            DataTable dt = ds.Tables[0].Copy();
            List<ClsUserDetails> ObjectDetails = new List<ClsUserDetails>();
            DataTable dtRemoveDuplicate = new DataTable();

            if (HasData(ds, 0))
            {
                //string[] listvalues2;
                if (ds.Tables[0].Rows[0] != null)
                {
                    dtRemoveDuplicate = DeleteDuplicateFromDataTable(dt, "cLonNumber");
                    for (int i = 0; i < dtRemoveDuplicate.Rows.Count; i++)
                    {
                        ClsUserDetails details = new ClsUserDetails();
                        details.UserID = dtRemoveDuplicate.Rows[i]["nID"].ToString();
                        details.ShortID = ds.Tables[0].Rows[i]["cShortID"].ToString();
                        details.LonNumber = dtRemoveDuplicate.Rows[i]["cLONNumber"].ToString();
                        details.DesignationName = dtRemoveDuplicate.Rows[i]["cProcessDesc"].ToString();
                        details.Remark = dtRemoveDuplicate.Rows[i]["cRemarks"].ToString();
                        details.currentStep = dtRemoveDuplicate.Rows[i]["CurrentStep"].ToString();
                        details.ProcessID = Convert.ToInt32(dtRemoveDuplicate.Rows[i]["ProcessID"].ToString());
                        details.NsequenceNO = dtRemoveDuplicate.Rows[i]["nSequence"].ToString();
                        details.DepartmentName = dtRemoveDuplicate.Rows[i]["DepartmentName"].ToString();
                        details.DCreatedOn = dtRemoveDuplicate.Rows[i]["dCreatedOn"].ToString();
                        details.DLastModification = dtRemoveDuplicate.Rows[i]["LastMadification"].ToString();
                        details.NtotalDays = dtRemoveDuplicate.Rows[i]["TotNoDays"].ToString();

                        ObjectDetails.Add(details);
                    }
                }
            }

            return ObjectDetails;
        }
    }
}