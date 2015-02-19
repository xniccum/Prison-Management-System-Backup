using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PMSTest
{
    /// <summary>
    /// This is a class designed to handle all interactions with a database server. 
    /// It has the dummy username and password hard-coded as instance variables for the Prison Management Project of winter 2015
    /// To use, construct the object, then run openConnection() to connect to the database.
    /// Once connected the method verifyUsernamePassword(string, string) returns a boolean if the username and password are stored 
    /// in the User table of the database. 
    /// </summary>
    public class SQLhandler
    {
        private string dummyUsername = "333Winter2014Prisoner";
        private string dummyPassword = "prisoner";
        string connectionString;
        SqlConnection dbConnection;
        private Boolean dbConnectionOpen = false;
        private Boolean userLoggedIn = false;
        private string userUsername = "";
        private string userPassword = "";
        public Dictionary<string,string[]> parameterNames;
        public Dictionary<string, SqlDbType[]> parameterTypes;

        
        /// <summary>
        /// Constructer. Takes no paramaters.
        /// </summary>
        public SQLhandler()
        {
            connectionString= "Data Source=titan.csse.rose-hulman.edu;Initial Catalog=PMS; User ID=" + dummyUsername + "; password=" + dummyPassword + ";";
            openConnection();
            loadParameters();
        }



        /// <summary>
        /// Creates a connection to the database using the default dummy username and password. 
        /// All database interaction shoudl be done through this user. 
        /// </summary>
        /// <returns> Returns false if the connection could not be made, and true if it was successfully created</returns>
        public Boolean openConnection()
        {
            dbConnection = new SqlConnection();

            connectionString = "Data Source=titan.csse.rose-hulman.edu;Initial Catalog=PMS; User ID=" + dummyUsername + "; password=" + dummyPassword + ";";
            dbConnection = new SqlConnection(connectionString);
            try
            {
                dbConnection.Open();
                dbConnectionOpen = true;
                return true;
            }
            catch
            {
                return false;
            }

        }
             public Boolean isUserLoggedIn()
        {

            return this.userLoggedIn;
        }

        public Boolean isConnected()
        {

            return dbConnectionOpen;
        }
        public Boolean logOut()
        {
            if (!this.userLoggedIn || !this.dbConnectionOpen)
            {
                return false;
            }
            this.userLoggedIn = false;
            this.userUsername = "";
            this.userPassword = "";
            return true;

        }

        private DataTable executeSproc(SqlCommand command)
        {
            if (!dbConnectionOpen || !userLoggedIn)
                return new DataTable();
            SqlDataReader dataRead;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@AuthUsername", SqlDbType.VarChar);
            command.Parameters.Add("@AuthPassword", SqlDbType.VarChar);
            command.Parameters["@AuthUsername"].Value = userUsername;
            command.Parameters["@AuthPassword"].Value = userPassword;
            dataRead = command.ExecuteReader();

            DataTable returnTable = new DataTable();
            returnTable.Load(dataRead);
            return returnTable;

        }


        /// <summary>
        /// This method verifies a username and password. Currently does not use any sort of encryption. 
        /// This method will not run if there is not a valid connection to the database. 
        /// </summary>
        /// <param name="username">username as spelled in the User table.</param>
        /// <param name="password">password as spelled in the User table</param>
        /// <returns> returns a boolean corresponding to the validity of the Username/Password passed</returns>
        public Boolean verifyUsernamePassword(string username, string password)
        {
            if (!dbConnectionOpen)
                return false;

            SqlCommand verificationCommand = new SqlCommand();
            verificationCommand.CommandText = "dbo.pms_checkUsernamePassword";
            verificationCommand.CommandType = CommandType.StoredProcedure;
            verificationCommand.Connection = dbConnection;
            verificationCommand.Parameters.Add("@Username", SqlDbType.NVarChar);
            verificationCommand.Parameters.Add("@Password", SqlDbType.NVarChar);
            verificationCommand.Parameters["@Username"].Value = username;
            verificationCommand.Parameters["@Password"].Value = password;

            Object returned = verificationCommand.ExecuteScalar();
            if (returned.ToString() == "1")
            {
                userLoggedIn = true;
                this.userUsername = username;
                this.userPassword = password;
                return true;
            }

            return false;
        }

        public int checkUsernamePermissions(string username)
        {
            using (SqlCommand sprocCommand = new SqlCommand())
            {
                sprocCommand.CommandText = "dbo.pms_getPermissions";
                sprocCommand.CommandType = CommandType.StoredProcedure;
                sprocCommand.Connection = dbConnection;
                sprocCommand.Parameters.Add("@Username", SqlDbType.VarChar);
                sprocCommand.Parameters["@Username"].Value = username;

                Object returned = sprocCommand.ExecuteScalar();
                if (returned.ToString() == "1")
                    return 1;
                else if (returned.ToString() == "2")
                    return 2;
                else
                    return 0;
            }

        }


        public DataTable getGuardsDataTable()
        {
            using (SqlCommand sprocCommand = new SqlCommand("dbo.pms_guards", dbConnection))
            {

                return executeSproc(sprocCommand);
            }
        }
        
        public DataTable getAltercationTable()
        {
            using (SqlCommand sprocCommand = new SqlCommand("dbo.pms_altercations", dbConnection))
            {
                return executeSproc(sprocCommand);
            }
        }

        public DataTable getCellTable()
        {
            using (SqlCommand sprocCommand = new SqlCommand("dbo.pms_cell", dbConnection))
            {
                return executeSproc(sprocCommand);
            }
        }

        public DataTable getPrisonersTable()
        {

            using (SqlCommand sprocCommand = new SqlCommand("dbo.pms_getAllPrisoners", dbConnection))
            {
                return executeSproc(sprocCommand);
            }
        }

        public DataTable getShiftsTable()
        {

            using (SqlCommand sprocCommand = new SqlCommand("dbo.shift_view", dbConnection))
            {
                return executeSproc(sprocCommand);
            }

        }

        public DataTable getAllUsersTable()
        {
            using (SqlCommand sprocCommand = new SqlCommand("dbo.pms_users", dbConnection))
            {
                return executeSproc(sprocCommand);
            }
        }

        public DataTable runSproc(string name){
            using (SqlCommand sprocCommand = new SqlCommand(name, dbConnection))
            {
                return executeSproc(sprocCommand);
            }
        }
        /// <summary>
        /// Requires User to be logged in
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable runParamSproc_Datatable(string name, string[] data)
        {
            if (!dbConnectionOpen)
                return new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandText = name;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = dbConnection;
            for (int i = 0; i < this.parameterTypes[name].Length; i++)
            {
                command.Parameters.Add(this.parameterNames[name][i], this.parameterTypes[name][i]);
                if (this.parameterTypes[name][i] == SqlDbType.SmallInt)
                {
                    if (data[i] == null)
                    {
                        command.Parameters[this.parameterNames[name][i]].Value = null;
                    }
                    else
                    {
                        try
                        {
                            command.Parameters[this.parameterNames[name][i]].Value = Convert.ToInt32(data[i]);
                        }
                        catch
                        {
                            return new DataTable();
                        }
                    }
                }
                else if (this.parameterTypes[name][i] == SqlDbType.Time)
                {
                    try
                    {
                        command.Parameters[this.parameterNames[name][i]].Value = TimeSpan.Parse(data[i]);
                       
                    }
                    catch
                    {
                        return new DataTable();
                    }
                }
                else
                {
                    command.Parameters[this.parameterNames[name][i]].Value = data[i];
                }
            }
            command.Parameters.Add("@AuthUsername",SqlDbType.VarChar);
            command.Parameters.Add("@AuthPassword", SqlDbType.VarChar);
            command.Parameters["@AuthUsername"].Value = userUsername;
            command.Parameters["@AuthPassword"].Value = userPassword;
            SqlDataReader reader = command.ExecuteReader();
            DataTable returnTable =  new DataTable();
            returnTable.Load(reader);
            return returnTable;
        }

        public Boolean runParamSproc_Boolean(string name, string[] data)
        {
            if (!dbConnectionOpen)
                return false;
            SqlCommand command = new SqlCommand();
            command.CommandText = name;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = dbConnection;
            for (int i = 0; i < this.parameterTypes[name].Length; i++)
            {
                command.Parameters.Add(this.parameterNames[name][i], this.parameterTypes[name][i]);
                if (this.parameterTypes[name][i] == SqlDbType.SmallInt)
                {
                    if (data[i] == null)
                    {
                        command.Parameters[this.parameterNames[name][i]].Value = null;
                    }
                    else
                    {
                        try
                        {
                            command.Parameters[this.parameterNames[name][i]].Value = Convert.ToInt32(data[i]);
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
                else if (this.parameterTypes[name][i] == SqlDbType.Time)
                {
                    try
                    {
                        command.Parameters[this.parameterNames[name][i]].Value = TimeSpan.Parse(data[i]);

                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    command.Parameters[this.parameterNames[name][i]].Value = data[i];
                }
            }
            command.Parameters.Add("@AuthUsername", SqlDbType.VarChar);
            command.Parameters.Add("@AuthPassword", SqlDbType.VarChar);
            command.Parameters["@AuthUsername"].Value = userUsername;
            command.Parameters["@AuthPassword"].Value = userPassword;
            SqlDataReader reader = command.ExecuteReader();
            DataTable returnTable = new DataTable();
            returnTable.Load(reader);
            return true;
        }

        private void loadParameters()
        {
            this.parameterNames = new Dictionary<string, string[]>();
            this.parameterTypes = new Dictionary<string, SqlDbType[]>();

            this.parameterNames.Add("dbo.addCell", new String[] { "@cellNo", "@block" });
            this.parameterTypes.Add("dbo.addCell", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar });

            this.parameterNames.Add("dbo.addGuard", new String[] { "@username", "@scheduleID" });
            this.parameterTypes.Add("dbo.addGuard", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.addPrisoner", new String[] { "@pFName", "@pMName" , "@pLName" , "@crime", "@crimeDescription", "@desiredCell"});
            this.parameterTypes.Add("dbo.addPrisoner", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.createJob", new String[] { "@jobName", "@capacity", "@schedule"});
            this.parameterTypes.Add("dbo.createJob", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.SmallInt, SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.pms_getAltercation", new String[] { "@altID" });
            this.parameterTypes.Add("dbo.pms_getAltercation", new SqlDbType[] { SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.pms_getPrisoner", new String[] { "@prisonerID"});
            this.parameterTypes.Add("dbo.pms_getPrisoner", new SqlDbType[] { SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.pms_getUser", new String[] { "@username"});
            this.parameterTypes.Add("dbo.pms_getUser", new SqlDbType[] { SqlDbType.VarChar });

            this.parameterNames.Add("dbo.pms_getPermissions", new String[] { "@username" });
            this.parameterTypes.Add("dbo.pms_getPermissions", new SqlDbType[] { SqlDbType.VarChar });

            this.parameterNames.Add("dbo.pms_checkUsernamePassword", new String[] { "@Username" , "@Password" });
            this.parameterTypes.Add("dbo.pms_checkUsernamePassword", new SqlDbType[] { SqlDbType.NVarChar, SqlDbType.NVarChar});

            this.parameterNames.Add("dbo.pms_deletePrisoner", new String[] { "@prisonerID" });
            this.parameterTypes.Add("dbo.pms_deletePrisoner", new SqlDbType[] { SqlDbType.VarChar });

            this.parameterNames.Add("dbo.pms_deleteUser", new String[] { "@dropUsername" });
            this.parameterTypes.Add("dbo.pms_deleteUser", new SqlDbType[] { SqlDbType.VarChar });

            this.parameterNames.Add("dbo.movePrisonerToCell", new String[] { "@prisonerID", "@desiredCell" });
            this.parameterTypes.Add("dbo.movePrisonerToCell", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.pms_registerUser", new String[] { "@fname", "@mname", "@lname", "@desusername", "@despassword" });
            this.parameterTypes.Add("dbo.pms_registerUser", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar });

            this.parameterNames.Add("dbo.pms_addAltercation", new String[] { "@Prisoner1ID", "@Prisoner2ID", "@Type", "@Description"});
            this.parameterTypes.Add("dbo.pms_addAltercation", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.VarChar, SqlDbType.VarChar});

            this.parameterNames.Add("dbo.shift_add", new String[] { "@StartTime", "@EndTime" });
            this.parameterTypes.Add("dbo.shift_add", new SqlDbType[] { SqlDbType.Time, SqlDbType.Time });

            this.parameterNames.Add("dbo.shift_update", new String[] { "@shiftID", "@StartTime", "@EndTime" });
            this.parameterTypes.Add("dbo.shift_update", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.Time, SqlDbType.Time });

            this.parameterNames.Add("dbo.shift_delete", new String[] { "@shiftID"});
            this.parameterTypes.Add("dbo.shift_delete", new SqlDbType[] { SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.jws_add", new String[] { "@ScheduleID", "@jobName" });
            this.parameterTypes.Add("dbo.jws_add", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar });

            this.parameterNames.Add("dbo.jws_delete", new String[] { "@ScheduleID", "@jobName" });
            this.parameterTypes.Add("dbo.jws_delete", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar });


            

            this.parameterNames.Add("dbo.schedule_insert", new String[] { "@sunshift", "@monshift", "@tuesshift", "@wedshift", "@thursshift", "@frishift", "@satshift" });
            this.parameterTypes.Add("dbo.schedule_insert", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.schedule_update", new String[] { "@scheduleID", "@sunshift", "@monshift", "@tuesshift", "@wedshift", "@thursshift", "@frishift", "@satshift" });
            this.parameterTypes.Add("dbo.schedule_update", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, SqlDbType.SmallInt, });

            this.parameterNames.Add("dbo.schedule_delete", new String[] { "@scheduleID" });
            this.parameterTypes.Add("dbo.schedule_delete", new SqlDbType[] { SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.guardSchedule_update", new String[] { "@guardID", "@ScheduleID" });
            this.parameterTypes.Add("dbo.guardSchedule_update", new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.guard_delete", new String[] { "@guardID"});
            this.parameterTypes.Add("dbo.guard_delete", new SqlDbType[] { SqlDbType.SmallInt});

            this.parameterNames.Add("dbo.job_update", new String[] { "@jobName", "@newcapacity" });
            this.parameterTypes.Add("dbo.job_update", new SqlDbType[] { SqlDbType.VarChar, SqlDbType.SmallInt });

            this.parameterNames.Add("dbo.job_delete", new String[] { "@jobName"});
            this.parameterTypes.Add("dbo.job_delete", new SqlDbType[] { SqlDbType.VarChar});

        }
    }



}
