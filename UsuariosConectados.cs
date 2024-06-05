using MonitoringWorks.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManUserLog
{
    public partial class UsuariosConectados : Form
    {
        public _ManUserLogLogica cloProject = new _ManUserLogLogica();
        public UsuariosConectados()
        {
            this.InitializeComponent();
        }
        private void SetDGProperties()
        {
            this.dgvUsuarios.ColumnCount = 4;
            this.dgvUsuarios.ColumnHeadersVisible = true;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.Black,
                Font = new Font("Verdana", 9f, FontStyle.Bold)
            };
            this.dgvUsuarios.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular);
            this.dgvUsuarios.Columns[0].Name = "Usuario";
            this.dgvUsuarios.Columns[1].Name = "IP";
            this.dgvUsuarios.Columns[2].Name = "Puerto";
            this.dgvUsuarios.Columns[3].Name = "Equipo";
            this.dgvUsuarios.Columns[0].Width = 120;
            this.dgvUsuarios.Columns[1].Width = 110;
            this.dgvUsuarios.Columns[2].Width = 65;
            this.dgvUsuarios.Columns[3].Width = 130;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 232, 234);
            this.dgvUsuarios.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 232, 234);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetDGProperties();
            List<Tables.UsuariosSys> usuariosSysList = this.cloProject.ConsultaUsuarios();
            if (usuariosSysList.Count == 0)
                return;
            foreach (Tables.UsuariosSys usuariosSys in usuariosSysList)
                this.dgvUsuarios.Rows.Add((object[])new string[4]
                {
          usuariosSys.CodigoUsuario,
          usuariosSys.IP,
          usuariosSys.Puerto.ToString(),
          usuariosSys.CodigoUsuarioWin.Substring(3)
                });
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Si desbloquea a este usuario podria interrumpir su operación, ¿Desea continuar?", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (this.dgvUsuarios.Rows.Count == 0) //No hay filas
                return;
            this.cloProject.DesbloqueaUsuario(this.dgvUsuarios.CurrentRow.Cells["Usuario"].Value.ToString());
            this.dgvUsuarios.Rows.RemoveAt(this.dgvUsuarios.CurrentRow.Index);
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void ManUserLogPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            ModGeneral.EliminateFunctionInUse((int)General.sysCodigoFuncion);
        }

    }
}
