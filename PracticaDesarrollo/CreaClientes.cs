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

//Esta es la ventana en la cual se crean los nuevos clientes

namespace PracticaDesarrollo
{
    public partial class CreaClientes : Form
    {
        public CreaClientes()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void btnCrear_Click(object sender, EventArgs e)
        {
            //Aqui obtengo los utimos ID de cada tabla
            string cmddir = string.Format("SELECT  TOP 1 idDir FROM Domicilios ORDER BY idDir DESC");

            string cmdcliente = string.Format("SELECT  TOP 1 idCliente FROM Clientes ORDER BY idCliente DESC");

            int indiceDir;

            int indiceCliente;

            try
            {
                //En esta parte evaluos que los textbox esten llenos para luegos tomar la informacion y insertarla en cada una de las tablas (Clientes y Domicilio)
                DataSet DSD = Herramientas.SetConsultas(cmddir);

                indiceDir = Convert.ToInt32(DSD.Tables[0].Rows[0]["idDir"].ToString()) + 1;

                DataSet DSC = Herramientas.SetConsultas(cmdcliente);

                indiceCliente = Convert.ToInt32(DSC.Tables[0].Rows[0]["idCliente"].ToString()) + 1;

                if (string.IsNullOrEmpty(tbCedula.Text.Trim()) == false && string.IsNullOrEmpty(tbNombre.Text.Trim()) == false && string.IsNullOrEmpty(tbApellido.Text.Trim()) == false && string.IsNullOrEmpty(tbCedula.Text.Trim()) == false && string.IsNullOrEmpty(tbTelefono.Text.Trim()) == false && string.IsNullOrEmpty(tbCalle.Text.Trim()) == false && string.IsNullOrEmpty(tbSector.Text.Trim()) == false && string.IsNullOrEmpty(tbCiudad.Text.Trim()) == false)
                {
                    string cmdc = string.Format("EXEC CreaClientes '{0}','{1}','{2}','{3}','{4}'", indiceCliente, tbNombre.Text.Trim(), tbApellido.Text.Trim(), tbCedula.Text.Trim(), tbTelefono.Text.Trim());

                    Herramientas.SetConsultas(cmdc);

                    string cmdd = string.Format("EXEC CreaDomicilios '{0}','{1}','{2}','{3}','{4}'", indiceDir, tbCalle.Text.Trim(), tbSector.Text.Trim(), tbCiudad.Text.Trim(), indiceCliente);

                    Herramientas.SetConsultas(cmdd);
                } 
                else
                {
                    MessageBox.Show("Todos los campos deben ser llenados", "Campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuarios "+ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tbNombre.Text = "";
            tbApellido.Text = "";
            tbCedula.Text = "";
            tbTelefono.Text = "";
            tbCalle.Text = "";
            tbSector.Text = "";
            tbCiudad.Text = "";
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbNombre.Text = "";
            tbApellido.Text = "";
            tbCedula.Text = "";
            tbTelefono.Text = "";
            tbCalle.Text = "";
            tbSector.Text = "";
            tbCiudad.Text = "";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
