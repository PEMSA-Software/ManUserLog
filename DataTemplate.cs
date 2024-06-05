using MonitoringWorks.DataAccess;

namespace ManUserLog
{
    interface IUsuariosSys_UsuariosWin : IUsuariosSys, IUsuariosWin  { }

    public class UsuariosSys_UsuariosWin : IUsuariosSys_UsuariosWin
    {
        public string CodigoUsuario { get; set; }
        public string Contraseña { get; set; }
        public string CodigoPersona { get; set; }
        public string Nombre { get; set; }
        public string GruposAut { get; set; }
        public string QuienRegistro { get; set; }
        public char Estado { get; set; }
        public string IP { get; set; }
        public int Puerto { get; set; }
        public string CodigoUsuarioWin { get; set; }
        public int NoPuerto { get; set; }

        public UsuariosSys_UsuariosWin() { }
    }
}
