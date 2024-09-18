﻿using MonitoringWorks.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ManUserLog
{
  public class _ManUserLogAccesoADatos
  {
        public SqlConnection cone = new SqlConnection();
        public List<UsuariosSys_UsuariosWin> ConsultaUsuarios()
        {
            string[] arrParam = new string[1];
            string strSQL = "SELECT * FROM UsuariosSys LEFT JOIN UsuariosWin ON UsuariosSys.Puerto = UsuariosWin.NoPuerto WHERE IP <> '' AND Puerto <> 0";
            SqlDataReader sqlDataReader = ModGeneral.dr_dr(strSQL, arrParam, CommandBehavior.SingleResult);
            List<UsuariosSys_UsuariosWin> usuariosSysList = new List<UsuariosSys_UsuariosWin>();
            if (!ModGeneral.ExisteError() && sqlDataReader != null)
            {
                while (sqlDataReader.Read())
                    usuariosSysList.Add(new UsuariosSys_UsuariosWin()
                    {
                        CodigoUsuario = sqlDataReader["CodigoUsuario"].ToString(),
                        IP = sqlDataReader["IP"].ToString(),
                        Puerto = (int)sqlDataReader["Puerto"],
                        CodigoUsuarioWin = sqlDataReader["CodigoUsuarioWin"].Equals(DBNull.Value) ? "@@@USER-NOT-FOUNT" : ((string)sqlDataReader["CodigoUsuarioWin"]).Trim()
                    });
            }
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
                    sqlConnection.ConnectionString = ModGeneral.CnString(General.sysDBInUse);
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
            string strSQL = string.Format("UPDATE UsuariosSys SET IP = '', Puerto = 0 WHERE CodigoUsuario = '{0}'", (object)strLlave);
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
                    ModGeneral.RegistroBitacora2("ManUserLog", "Update", ErrLinea, ErrNumber, ErrDescr);
                    goto case 0;
            }
        }
    }
}
