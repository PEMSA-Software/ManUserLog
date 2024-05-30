using Comun;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ManUserLog
{
  public class _ManUserLogAccesoADatos
  {
    public AccesoADatos.AccesoADatos cadDatos = new AccesoADatos.AccesoADatos();
    public SqlConnection cone = new SqlConnection();


    public List<UsuariosSys> ConsultaUsuarios()
    {
      string[] arrParam = new string[1];
      string strSQL = "SELECT UsuariosSys.CodigoUsuario, IP, Puerto, ISNULL(UsuariosWin.CodigoUsuario, '@@@USER-NOT-FOUNT') as 'CodigoUsuario' FROM UsuariosSys LEFT JOIN " + "UsuariosWin ON UsuariosSys.Puerto = UsuariosWin.NoPuerto WHERE IP <> '' AND Puerto <> 0";
      SqlDataReader sqlDataReader = this.cadDatos.SelectDR(Sistema.sysDBBase, strSQL, arrParam, CommandBehavior.SingleResult);
      List<UsuariosSys> usuariosSysList = new List<UsuariosSys>();
      while (sqlDataReader.Read())
        usuariosSysList.Add(new UsuariosSys()
        {
          CodigoUsuario = sqlDataReader["CodigoUsuario"].ToString(),
          IP = sqlDataReader["IP"].ToString(),
          Puerto = (int) sqlDataReader["Puerto"],
          CodigoUsuarioWin = sqlDataReader[3].ToString()
        });
      return usuariosSysList;
    }

    public void InUpDeSQL(
      string strSQL,
      bool CierraBD,
      ref int ErrNumber,
      ref string ErrDescr,
      ref string ErrLinea)
    {
      SqlConnection sqlConnection = new SqlConnection();
      SqlCommand sqlCommand = new SqlCommand();
      try
      {
        if (sqlConnection.State != ConnectionState.Open)
        {
          sqlConnection.ConnectionString = this.cadDatos.cnString(Sistema.sysDBBase);
          sqlCommand.Connection = sqlConnection;
          sqlConnection.Open();
        }
        else
          sqlCommand.Connection = sqlConnection;
      }
      catch (SqlException ex)
      {
        ErrNumber = ex.Number;
        ErrDescr = ex.Message;
        ErrLinea = ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" en "));
      }
      try
      {
        sqlCommand.CommandText = strSQL;
        sqlCommand.ExecuteNonQuery();
        if (!CierraBD)
          return;
        sqlConnection.Close();
      }
      catch (SqlException ex)
      {
        ErrNumber = ex.Number;
        ErrDescr = ex.Message;
        ErrLinea = ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" en "));
      }
    }

    public string DesbloqueaUsuario(string strLlave)
    {
      string str = "";
      string strSQL = string.Format("UPDATE UsuariosSys SET IP = '', Puerto = 0 WHERE CodigoUsuario = '{0}'", (object) strLlave);
      int ErrNumber = 0;
      string ErrDescr = "";
      string ErrLinea = "";
      this.InUpDeSQL(strSQL, true, ref ErrNumber, ref ErrDescr, ref ErrLinea);
      switch (ErrNumber)
      {
        case 0:
          return str;
        case 2601:
          str = "Nombre ya registrado";
          goto case 0;
        default:
          str = "Ha ocurrido un error, avise a su administrador";
          this.cadDatos.RegistroBitacora2("ManUserLog", "Update", ErrLinea, ErrNumber, ErrDescr);
          goto case 0;
      }
    }
  }
}
