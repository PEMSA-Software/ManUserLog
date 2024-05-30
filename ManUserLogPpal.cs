using Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ManUserLog
{
  public class ManUserLogPpal : Form
  {
    public _ManUserLogLogica cloProject = new _ManUserLogLogica();
    public Logica.Logica cloLogica = new Logica.Logica();
    private IContainer components = (IContainer) null;
    private DataGridView dgvUsuarios;
    private Button btnDesbloquear;
    private Button btnSalir;

    public ManUserLogPpal() => this.InitializeComponent();

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

    private void ManUserLogPpal_Load(object sender, EventArgs e)
    {
      this.SetDGProperties();
      List<UsuariosSys> usuariosSysList = this.cloProject.ConsultaUsuarios();
      if (usuariosSysList.Count == 0)
        return;
      foreach (UsuariosSys usuariosSys in usuariosSysList)
        this.dgvUsuarios.Rows.Add((object[]) new string[4]
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
      this.cloProject.DesbloqueaUsuario(this.dgvUsuarios.CurrentRow.Cells["Usuario"].Value.ToString());
      this.dgvUsuarios.Rows.RemoveAt(this.dgvUsuarios.CurrentRow.Index);
    }

    private void btnSalir_Click(object sender, EventArgs e) => this.Close();

    private void ManUserLogPpal_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.cloLogica.EliminateFunctionInUse((int) Sistema.sysCodigoFuncion);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.dgvUsuarios = new DataGridView();
      this.btnDesbloquear = new Button();
      this.btnSalir = new Button();
      ((ISupportInitialize) this.dgvUsuarios).BeginInit();
      this.SuspendLayout();
      this.dgvUsuarios.AllowUserToAddRows = false;
      this.dgvUsuarios.AllowUserToDeleteRows = false;
      this.dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvUsuarios.Location = new Point(12, 12);
      this.dgvUsuarios.Name = "dgvUsuarios";
      this.dgvUsuarios.RowHeadersWidth = 30;
      this.dgvUsuarios.Size = new Size(476, 199);
      this.dgvUsuarios.TabIndex = 0;
      this.btnDesbloquear.BackColor = SystemColors.Control;
      this.btnDesbloquear.FlatAppearance.BorderColor = Color.DarkGray;
      this.btnDesbloquear.FlatStyle = FlatStyle.Flat;
      this.btnDesbloquear.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.btnDesbloquear.Location = new Point(12, 218);
      this.btnDesbloquear.Name = "btnDesbloquear";
      this.btnDesbloquear.Size = new Size(112, 27);
      this.btnDesbloquear.TabIndex = 1;
      this.btnDesbloquear.Text = "&Desbloquear";
      this.btnDesbloquear.UseVisualStyleBackColor = false;
      this.btnDesbloquear.Click += new EventHandler(this.btnDesbloquear_Click);
      this.btnSalir.BackColor = SystemColors.Control;
      this.btnSalir.FlatAppearance.BorderColor = Color.DarkGray;
      this.btnSalir.FlatStyle = FlatStyle.Flat;
      this.btnSalir.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.btnSalir.Location = new Point(403, 218);
      this.btnSalir.Name = "btnSalir";
      this.btnSalir.Size = new Size(85, 27);
      this.btnSalir.TabIndex = 2;
      this.btnSalir.Text = "&Salir";
      this.btnSalir.UseVisualStyleBackColor = false;
      this.btnSalir.Click += new EventHandler(this.btnSalir_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = Color.White;
      this.ClientSize = new Size(500, 254);
      this.Controls.Add((Control) this.btnSalir);
      this.Controls.Add((Control) this.btnDesbloquear);
      this.Controls.Add((Control) this.dgvUsuarios);
      this.MaximizeBox = false;
      this.Name = "ManUserLogPpal";
      this.Text = "Usuarios Conectados";
      this.FormClosing += new FormClosingEventHandler(this.ManUserLogPpal_FormClosing);
      this.Load += new EventHandler(this.ManUserLogPpal_Load);
      ((ISupportInitialize) this.dgvUsuarios).EndInit();
      this.ResumeLayout(false);
    }
  }
}
