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
    public partial class ActualizaDom : Form
    {
        public ActualizaDom()
        {
            InitializeComponent();
        }

        private void ActualizaDom_Load(object sender, EventArgs e)
        {
            string cmd = string.Format("SELECT * FROM Domicilios WHERE idDir='{0}' ",InfoClientes.idDir);

            try
            {
                DataSet ds = Herramientas.SetConsultas(cmd) ;

                tbCalle.Text = ds.Tables[0].Rows[0]["Calle"].ToString();

                tbSector.Text = ds.Tables[0].Rows[0]["Sector"].ToString();

                tbCiudad.Text = ds.Tables[0].Rows[0]["Ciudad"].ToString();
               
            }
            catch(Exception ex)
            {
               MessageBox.Show("Error cargando los datos "+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string cmd = string.Format("EXEC ActualizaDomicilios '{0}','{1}','{2}','{3}' ", InfoClientes.idDir, tbCalle.Text, tbSector.Text, tbCiudad.Text);
            try
            {
                Herramientas.SetConsultas(cmd) ;

                MessageBox.Show("Se actualizo la direccion", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();   

            }
            catch(Exception ex)
            {
                MessageBox.Show("No se pudo actualizar la direccion " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
