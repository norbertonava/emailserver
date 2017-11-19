using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace EmailServer.Core
{
    /// <summary>
    /// Summary description for WMySqlCommand.
    /// </summary>
    internal class WMySqlCommand : IDisposable
    {
        private MySqlCommand m_SqlCmd = null;
        private string m_connStr = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="commandText">Command text.</param>
        public WMySqlCommand(string connectionString, string commandText)
        {
            m_connStr = connectionString;

            m_SqlCmd = new MySqlCommand(commandText);
            m_SqlCmd.CommandType = CommandType.StoredProcedure;
            m_SqlCmd.CommandTimeout = 180;
        }

        #region function Dispose

        public void Dispose()
        {
            if (m_SqlCmd != null)
            {
                m_SqlCmd.Dispose();
            }
        }

        #endregion


        #region function AddParameter

        /// <summary>
        /// Adds parameter to Sql Command.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="dbType">Parameter datatype.</param>
        /// <param name="value">Parameter value.</param>
        public void AddParameter(string name, MySqlDbType dbType, object value)
        {
            MySqlDbType dbTyp = dbType;
            object val = value;

            if (val is DateTime)
            {
                DateTime date = (DateTime)value;
                val = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            }

            if (dbType == MySqlDbType.Guid)
            {
                dbTyp = MySqlDbType.VarChar;
                string guid = val.ToString();
                //if (guid.Length < 1)
                //{
                //    return;
                //}
            }

            m_SqlCmd.Parameters.Add(name, dbTyp).Value = val;
        }

        #endregion

        #region fucntion Execute

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <returns></returns>
        public DataSet Execute()
        {
            DataSet dsRetVal = null;

            using (MySqlConnection con = new MySqlConnection(m_connStr))
            {
                con.Open();
                m_SqlCmd.Connection = con;

                dsRetVal = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter(m_SqlCmd);
                adapter.Fill(dsRetVal);

                adapter.Dispose();
            }

            return dsRetVal;
        }

        #endregion


        #region Properties Implementaion

        /// <summary>
        /// Gets or sets command timeout time.
        /// </summary>
        public int CommandTimeout
        {
            get { return m_SqlCmd.CommandTimeout; }

            set { m_SqlCmd.CommandTimeout = value; }
        }

        /// <summary>
        /// Gets or sets command type.
        /// </summary>
        public CommandType CommandType
        {
            get { return m_SqlCmd.CommandType; }

            set { m_SqlCmd.CommandType = value; }
        }

        #endregion

    }
}