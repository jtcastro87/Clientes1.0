using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriasP;

namespace PracticaDesarrollo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
         
            //Evalua si los campos de usuarios y contraseña no estan vacios
           if (tbUsuario.Text.Trim() == String.Empty || tbContraseña.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe de ingresar un usuario y contraseña","Campos Vacios",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    //String de consulta a la base de datos
                    string cmd = string.Format("SELECT * FROM Usuarios WHERE Login='{0}' AND Contraseña='{1}'", tbUsuario.Text.Trim(), tbContraseña.Text.Trim());
                    DataSet DS = Herramientas.SetConsultas(cmd);

                    string usuario = DS.Tables[0].Rows[0]["Login"].ToString().Trim();
                    string pass = DS.Tables[0].Rows[0]["Contraseña"].ToString().Trim();

                    Principal p = new Principal();
                    this.Hide();
                    p.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Usuario o contraseña invalidos: " + ex.Message,"Error de Credenciales ",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }              
            }
           
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
