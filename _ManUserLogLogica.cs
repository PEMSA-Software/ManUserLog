using Comun;
using System;
using System.Collections.Generic;

namespace ManUserLog
{
  public class _ManUserLogLogica
  {
    public _ManUserLogAccesoADatos cadProject = new _ManUserLogAccesoADatos();
    public Logica.Logica cloLogica = new Logica.Logica();

    public bool Main()
    {
      Sistema.strLocalAssemblyName = "MANUSERLOG";
      if (!this.cloLogica.GetParameters())
      {
        if (!this.ExisteError())
          ;
        return false;
      }
      Sistema.bytCodigoEmpresaReal = Convert.ToByte(Sistema.sysCodigoEmpresa);
      Sistema.strPathSistema = "C:\\Siatec\\SiatecInfo\\";
      Sistema.strPathEmpresa = "C:\\Siatec\\SiatecInfo\\G" + Sistema.sysCodigoGrupo + "\\E" + Sistema.sysCodigoEmpresa + "\\";
      Sistema.strPathGrupo = "C:\\Siatec\\SiatecInfo\\G" + Sistema.sysCodigoGrupo + "\\E0";
      return true;
    }

    public bool ExisteError()
    {
      bool flag = false;
      if (Sistema.sysError != 0L || Sistema.sysMensaje != "")
        flag = true;
      return flag;
    }

    public List<UsuariosSys> ConsultaUsuarios() => this.cadProject.ConsultaUsuarios();

    public string DesbloqueaUsuario(string strLlave) => this.cadProject.DesbloqueaUsuario(strLlave);
  }
}
