using MonitoringWorks.DataAccess;
using System.Collections.Generic;

namespace ManUserLog
{
  public class _ManUserLogLogica
  {
        public _ManUserLogAccesoADatos cadProject = new _ManUserLogAccesoADatos();
        public bool Main()
        {
            General.strLocalAssemblyName = "MANUSERLOG";
            ModGeneral.ExternalInit();
            ModGeneral.GetParameters();
            return true;
        }

        public List<UsuariosSys_UsuariosWin> ConsultaUsuarios() => this.cadProject.ConsultaUsuarios();

        public string DesbloqueaUsuario(string strLlave) => this.cadProject.DesbloqueaUsuario(strLlave);
    }
}
