using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PTTS.DAL
{
	public class DBHelper : IDisposable
	{
		private SqlConnection conn = null;
		private SqlCommand comm = null;
		private SqlTransaction trans = null;
		private bool retvalueadded = false;
		private bool transactOpened = false;
		private void openConnection()
		{
			if (this.conn != null && this.conn.State == ConnectionState.Closed)
			{
				this.conn.Open();
			}
		}
		private void closeConnection()
		{
			if (this.conn != null && this.conn.State == ConnectionState.Open)
			{
				this.conn.Close();
			}
		}
		public DBHelper() : this(string.Empty)
		{
		}
        public DBHelper(string commandText)
            : this(commandText, CommandType.Text, "ConnStr")
		{
		}
		public DBHelper(string commandText, string connectionkey) : this(commandText, CommandType.Text, connectionkey)
		{
		}
        public DBHelper(string commandText, CommandType type)
            : this(commandText, type, "ConnStr")
		{
		}
		public DBHelper(string commandText, CommandType type, string connectionkey)
		{
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionkey].ConnectionString);
            this.comm = new SqlCommand(commandText, this.conn);
			this.comm.CommandType = type;
		}
		public DBHelper beginTransact()
		{
			if (!this.transactOpened)
			{
				this.openConnection();
				this.trans = this.conn.BeginTransaction();
				this.comm.Transaction = this.trans;
				this.transactOpened = true;
			}
			return this;
		}
		public void saveTransPoint(string pointName)
		{
			if (this.transactOpened)
			{
				if (this.trans != null)
				{
					this.trans.Save(pointName);
				}
			}
		}
		public void commit()
		{
			if (this.transactOpened)
			{
				if (this.trans != null)
				{
					this.trans.Commit();
				}
				this.closeConnection();
				this.transactOpened = false;
			}
		}
		public void rollback()
		{
			if (this.transactOpened)
			{
				if (this.trans != null)
				{
					this.trans.Rollback();
				}
				this.closeConnection();
				this.transactOpened = false;
			}
		}
		public void rollback(string pointName)
		{
			if (this.transactOpened)
			{
				if (this.trans != null)
				{
					this.trans.Rollback(pointName);
				}
				this.transactOpened = false;
			}
		}
		public DBHelper addIn(string parameterName, object parameterValue)
		{
			SqlParameter param = new SqlParameter(parameterName, parameterValue);
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addIn(string parameterName, object parameterValue, SqlDbType type)
		{
			SqlParameter param = new SqlParameter(parameterName, parameterValue);
			param.SqlDbType = type;
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addIn(string parameterName, object parameterValue, SqlDbType type, int size)
		{
			SqlParameter param = new SqlParameter(parameterName, parameterValue);
			param.SqlDbType = type;
			param.Size = size;
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addIn(string parameterName, object parameterValue, SqlDbType type, byte precision, byte scale)
		{
			SqlParameter param = new SqlParameter(parameterName, parameterValue);
			param.SqlDbType = type;
			param.Precision = precision;
			param.Scale = scale;
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addOut(string parameterName, SqlDbType type, int size)
		{
			SqlParameter param = new SqlParameter();
			param.ParameterName = parameterName;
			param.Direction = ParameterDirection.Output;
			param.SqlDbType = type;
			param.Size = size;
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addOut(string parameterName, SqlDbType type, byte precision, byte scale)
		{
			SqlParameter param = new SqlParameter();
			param.ParameterName = parameterName;
			param.Direction = ParameterDirection.Output;
			param.SqlDbType = type;
			param.Precision = precision;
			param.Scale = scale;
			this.comm.Parameters.Add(param);
			return this;
		}
		public DBHelper addReturn()
		{
			SqlParameter param = new SqlParameter();
			param.ParameterName = "@DBHELPER_RETURN_VALUE";
			param.Direction = ParameterDirection.ReturnValue;
			this.comm.Parameters.Add(param);
			return this;
		}
		public int getReturned()
		{
			return int.Parse(this.getValue("@DBHELPER_RETURN_VALUE").ToString());
		}
		public object getValue(string parameterName)
		{
			return this.comm.Parameters[parameterName].Value;
		}
		public DBHelper CreateNewCommand(string commandText)
		{
			return this.CreateNewCommand(commandText, CommandType.Text);
		}
		public DBHelper CreateNewCommand(string commandText, CommandType commandType)
		{
			this.comm.CommandText = commandText;
			this.comm.Parameters.Clear();
			this.comm.CommandType = commandType;
			return this;
		}
		public int Execute()
		{
			this.openConnection();
			int retval = this.comm.ExecuteNonQuery();
			if (!this.transactOpened)
			{
				this.closeConnection();
			}
			return retval;
		}
		public DataTable ExecuteDataTable()
		{
			SqlDataAdapter adap = new SqlDataAdapter(this.comm);
			DataTable dtable = new DataTable();
			adap.Fill(dtable);
			return dtable;
		}
		public DataSet ExecuteDataSet()
		{
			SqlDataAdapter adap = new SqlDataAdapter(this.comm);
			DataSet dset = new DataSet();
			adap.Fill(dset);
			return dset;
		}
		public DataRow ExecuteDataRow()
		{
			DataRow retRow = null;
			DataTable dtable = this.ExecuteDataTable();
			if (dtable.Rows.Count > 0)
			{
				retRow = dtable.Rows[0];
			}
			return retRow;
		}
		public object ExecuteScalar()
		{
			this.openConnection();
			object retval = this.comm.ExecuteScalar();
			if (!this.transactOpened)
			{
				this.closeConnection();
			}
			return retval;
		}
		public bool HasRows()
		{
			DataTable dtable = this.ExecuteDataTable();
			int cnt = dtable.Rows.Count;
			dtable.Dispose();
			return cnt > 0;
		}
		public void Dispose()
		{
			if (this.trans != null)
			{
				this.trans.Dispose();
			}
			if (this.comm != null)
			{
				this.comm.Dispose();
			}
			this.closeConnection();
		}
		public DBHelper addInIf(bool condition, string parameterName, object parameterValue)
		{
			DBHelper result;
			if (condition)
			{
				result = this.addIn(parameterName, parameterValue);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBHelper addInIf(bool condition, string parameterName, object parameterValue, SqlDbType type)
		{
			DBHelper result;
			if (condition)
			{
				result = this.addIn(parameterName, parameterValue, type);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBHelper addInIf(bool condition, string parameterName, object parameterValue, SqlDbType type, int size)
		{
			DBHelper result;
			if (condition)
			{
				result = this.addIn(parameterName, parameterValue, type, size);
			}
			else
			{
				result = this;
			}
			return result;
		}
		public DBHelper addInIf(bool condition, string parameterName, object parameterValue, SqlDbType type, byte precision, byte scale)
		{
			DBHelper result;
			if (condition)
			{
				result = this.addIn(parameterName, parameterValue, type, precision, scale);
			}
			else
			{
				result = this;
			}
			return result;
		}
	}
}
