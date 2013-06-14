using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public class SQLDatabase{
    private SqlCommand command;

    public void ClearParameters(){
        command.Parameters.Clear();
    }

    public void SetStoredProcName(string storedProc){
        if (storedProc == null)
            return;
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        conn.Open();
        command = new SqlCommand(storedProc, conn);
        command.CommandType = CommandType.StoredProcedure;
        conn.Close();
    }

    public void AddVariable(string procVar, Object replaceVar){
        command.Parameters.AddWithValue(procVar, replaceVar);
    }

    public void Exec(){
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
    }

    public int ExecReturnID(){
        SqlParameter param = command.Parameters.Add("@ReturnID", 0);
        param.Direction = ParameterDirection.Output;
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
        return Convert.ToInt32(command.Parameters["@ReturnID"].Value); ;
    }

    public int ExecReturnInt(string returnParam){
        Helper h = new Helper();
        SqlParameter param = command.Parameters.Add(returnParam, 0);
        param.Direction = ParameterDirection.Output;
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
        return h.ToInt(Convert.ToString(command.Parameters[returnParam].Value));
        /*
        object result = null;
        command.Connection.Open();
        result = command.ExecuteScalar();
        command.Connection.Close();
        return Int32.Parse(result.ToString());
        */
    }

    public float ExecReturnFloat(string returnParam){
        Helper h = new Helper();
        SqlParameter param = command.Parameters.Add(returnParam, 0);
        param.Direction = ParameterDirection.Output;
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
        return h.ToFloat(Convert.ToString(command.Parameters[returnParam].Value));
    }

    public string ExecReturnString(string returnParam){
        Helper h = new Helper();
        SqlParameter param = command.Parameters.Add(returnParam, 0);
        param.Direction = ParameterDirection.Output;
        command.Connection.Open();
        command.ExecuteNonQuery();
        command.Connection.Close();
        return Convert.ToString(command.Parameters[returnParam].Value);
    }

    public DataTable ExecReturnTable(){
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        return dt;
    }
}