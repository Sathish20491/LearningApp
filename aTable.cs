using System;
using System.Data;
using System.Data.SqlClient;
namespace PTTS.DAL
{
    public abstract class aTable
    {
        private DBHelper l_Helper = null;
        private bool l_bSelfOpened = false;
        private SqlCommand l_cCommand = null;
        public DBHelper Connection
        {
            get
            {
                this.openConnection();
                this.l_bSelfOpened = true;
                return this.l_Helper;
            }
            set
            {
                this.l_Helper = value;
            }
        }
        public void BeginTran()
        {
            this.Connection.beginTransact();
        }
        public void CommitTran()
        {
            this.Connection.commit();
        }
        public void RollbackTran()
        {
            this.Connection.rollback();
        }
        protected void InitCommand(string argQuery)
        {
            this.InitCommand(argQuery, CommandType.StoredProcedure);
        }
        protected void InitCommand(string argQuery, CommandType argCommandType)
        {
            this.Connection.CreateNewCommand(argQuery, argCommandType);
        }
        protected void AddIn(string argParamName, object argParamValue)
        {
            this.Connection.addIn(argParamName, argParamValue);
        }
        protected void AddIn(string argParamName, object argParamValue, SqlDbType argType)
        {
            this.Connection.addIn(argParamName, argParamValue, argType);
        }
        protected void AddOut(string argParamName, SqlDbType argType, int argSize)
        {
            this.Connection.addOut(argParamName, argType, argSize);
        }
        protected object GetParameterValue(string argParamName)
        {
            return this.Connection.getValue(argParamName);
        }
        protected DataSet GetAsDataSet()
        {
            return this.Connection.ExecuteDataSet();
        }
        protected void ExecuteNonQuery()
        {
            this.Connection.Execute();
        }
        protected object ExecuteScalar()
        {
            return Connection.ExecuteScalar();
        }
        protected void openConnection()
        {
            if (this.l_Helper == null)
            {
                this.l_Helper = new DBHelper();
                this.l_bSelfOpened = true;
            }
        }
        protected void closeConnection()
        {
            if (this.l_bSelfOpened)
            {
                this.l_Helper.Dispose();
            }
        }
        public void Dispose()
        {
            this.closeConnection();
        }
    }
}
